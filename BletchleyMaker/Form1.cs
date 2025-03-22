using System.Diagnostics;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32.SafeHandles;

namespace BletchleyMaker
{
    public partial class Form1 : Form
    {
        public Grid grid;
        public List<Label> componentArray;

        public Form1()
        {
            InitializeComponent();

            componentArray = new List<Label> { col1row1, col2row1, col3row1, col4row1, col5row1, col6row1, col1row2, col2row2, col3row2, col4row2, col5row2, col6row2, col1row3, col2row3, col3row3, col4row3, col5row3, col6row3, col1row4, col2row4, col3row4, col4row4, col5row4, col6row4, col1row5, col2row5, col3row5, col4row5, col5row5, col6row5, col1row6, col2row6, col3row6, col4row6, col5row6, col6row6 };
            grid = new Grid(componentArray);

            grid.Generate(false);
        }

        private void makeGrid_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You cannot reverse this, do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }

            if (useSymbols.Checked)
                grid.Generate(true);
            else
                grid.Generate(false);

            Console.WriteLine(grid.GetGrid());
        }

        private void execute_Click(object sender, EventArgs e)
        {
            string input = inputBox.Text.ToUpper();
            string rule = ruleBox.Text.ToUpper();

            if (!ValidateRule(rule))
            { return; }

            if (!(grid.ValidateCharacterSet(input)))
            {
                ThrowError(1);
                return;
            }

            Cipher cipher = new Cipher(input, rule);

            if (decodeCheck.Checked)
            {
                cipher.Decode(grid.GetGrid());
            }
            else
            {
                cipher.Encode(grid.GetGrid());
            }
            outputBox.Text = cipher.GetText();
        }

        private bool ValidateRule(string rule)
        {
            string[] availableRules = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };

            bool valid = false;

            foreach (string availableRule in availableRules)
            {
                if (availableRule == rule)
                { valid = true; break; }
            }
            if (!valid)
            {
                ThrowError(0);
            }
            return valid;

        }



        public void ThrowError(int errorType)
        {
            string[] errors = { "INCORRECT RULE", "UNKNOWN CHARACTER" };

            outputBox.Text = "Error occured: " + errors[errorType];
        }

        private void supportButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will take you to the Github page for this project.", "Support", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/shr0m/BletchleyMaker",
                    UseShellExecute = true
                });
            }
            else
            {
                return;
            }
        }
    }

    public class Grid
    {
        private char[] gridArr = new char[36];
        private List<Label> components;

        public Grid(List<Label> compo)
        {
            for (int i = 0; i < compo.Count; i++)
            {
                gridArr[i] = Convert.ToChar(compo[i].Text);
            }
            components = compo;
        }

        public void Generate(bool symbols)
        {

            List<char> normChars = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            List<char> symbolChars = new List<char> { '@', 'B', 'C', 'D', 'E', 'F', 'G', 'H', ';', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '$', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '£', '4', '5', '6', '7', '8', '9' };
            Random rnd = new Random();

            List<char> chosenChars = symbols ? new List<char>(symbolChars) : new List<char>(normChars);
            for (int i = 0; i < gridArr.Length; i++)
            {
                int chosenIndex = rnd.Next(0, chosenChars.Count);
                gridArr[i] = chosenChars[chosenIndex];
                chosenChars.RemoveAt(chosenIndex);
            }
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Text = gridArr[i].ToString();
            }
        }

        public char[,] GetGrid()
        {
            char[,] final = new char[6, 6];
            int row = 0;
            int column = 0;

            for (int i = 0; i < gridArr.Length; i++)
            {
                final[row, column] = gridArr[i];
                column++;

                if (column == 6)
                {
                    column = 0;
                    row++;
                }
            }
            return final;
        }
        public bool ValidateCharacterSet(string input)
        {
            foreach (char c in input)
            {
                foreach (char c2 in gridArr)
                {
                    if (c == c2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class Cipher
    {

        private string Text;
        private string EncodeRule;
        private string DecodeRule;
        
        public Cipher(string plain, string r)
        {
            Text = plain;
            DecodeRule = r;
            string[] decodeRules = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };
            string[] encodeRules = { "X", "D1", "U1", "R1", "L1", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5" };

            for (int i = 0; i < decodeRules.Length; i++)
            {
                if (decodeRules[i] == r)
                {
                    EncodeRule = encodeRules[i];
                }
            }

        }

        public void Encode(char[,] gridArray)
        {
            RemoveWhitespace();
            char ruleChoice = EncodeRule[0];

            switch (ruleChoice)
            {
                case 'R':
                    RightEncode(gridArray); break;
                case 'L':
                    LeftEncode(gridArray); break;
                case 'U':
                    UpEncode(gridArray); break;
                case 'D':
                    DownEncode(gridArray); break;
            }
        }

        private void UpEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 0)
                    {
                        row = 5; 
                    }
                    else
                    {
                        row--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final; 
        }

        private void DownEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 5)
                    {
                        row = 0;
                    }
                    else
                    {
                        row++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void LeftEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 0)
                    {
                        column = 5;
                    }
                    else
                    {
                        column--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void RightEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 5)
                    {
                        column = 0;
                    }
                    else
                    {
                        column++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void RemoveWhitespace()
        {
            string final = "";
            foreach (char c in Text)
            {
                if (c != ' ')
                {
                    final += c;
                }
            }
            Text = final;
        }

        public string GetText()
        {
            return Text;
        }

        public void Decode(char[,] gridArray)
        {
            RemoveWhitespace();
            char ruleChoice = EncodeRule[0];

            switch (ruleChoice)
            {
                case 'R':
                    RightDecode(gridArray); break;
                case 'L':
                    LeftDecode(gridArray); break;
                case 'U':
                    UpDecode(gridArray); break;
                case 'D':
                    DownDecode(gridArray); break;
            }
        }

        private void RightDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 0)
                    {
                        column = 5;
                    }
                    else
                    {
                        column--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }
        private void LeftDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 5)
                    {
                        column = 0;
                    }
                    else
                    {
                        column++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }
        private void UpDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 5)
                    {
                        row = 0;
                    }
                    else
                    {
                        row++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }
        private void DownDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 0)
                    {
                        row = 5;
                    }
                    else
                    {
                        row--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

    }
}
