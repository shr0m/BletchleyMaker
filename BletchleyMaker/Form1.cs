using System.CodeDom.Compiler;
using Microsoft.Win32.SafeHandles;

namespace BletchleyMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void makeGrid_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You cannot reverse this, do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }

            Label[] components = { col1row1, col2row1, col3row1, col4row1, col5row1, col6row1, col1row2, col2row2, col3row2, col4row2, col5row2, col6row2, col1row3, col2row3, col3row3, col4row3, col5row3, col6row3, col1row4, col2row4, col3row4, col4row4, col5row4, col6row4, col1row5, col2row5, col3row5, col4row5, col5row5, col6row5, col1row6, col2row6, col3row6, col4row6, col5row6, col6row6 };

            Grid grid = new Grid(components);
            if (useSymbols.Checked)
                grid.Generate(true);
            else
                grid.Generate(false);

            Console.WriteLine(grid.GetGrid());
        }

        private void execute_Click(object sender, EventArgs e)
        {
            string input = inputBox.Text;
            string rule = ruleBox.Text;

            outputBox.Text = input;
        }
    }

    public class Grid
    {
        private char[] gridArr = new char[36];
        private Label[] components;

        public Grid(Label[] compo)
        {
            for (int i = 0; i < compo.Length; i++)
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
            for (int i = 0; i < components.Length; i++)
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
    }
}
