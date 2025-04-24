using System.Diagnostics;
using BletchleyMaker.Forms;
using BletchleyMaker.Functions;

namespace BletchleyMaker
{
    public partial class Main : Form
    {
        internal Grid grid;
        private List<Label> componentArray;
        private List<string> savedCodes;
        private ViewCodes view = null!;
        private List<char> Chars = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        public Main()
        {
            InitializeComponent();
            this.AcceptButton = execute;
            savedCodes = new List<string>();
            componentArray = new List<Label> { col1row1, col2row1, col3row1, col4row1, col5row1, col6row1, col1row2, col2row2, col3row2, col4row2, col5row2, col6row2, col1row3, col2row3, col3row3, col4row3, col5row3, col6row3, col1row4, col2row4, col3row4, col4row4, col5row4, col6row4, col1row5, col2row5, col3row5, col4row5, col5row5, col6row5, col1row6, col2row6, col3row6, col4row6, col5row6, col6row6 };
            grid = new Grid(componentArray, Chars, this);

            grid.Generate();
        }

        private void makeGrid_Click(object sender, EventArgs e)
        {

            if (decodeCheck.Checked && inputBox.Text.Trim() != "")
            {
                DialogResult result = MessageBox.Show("Generating while decoding can mess up your cipher. Continue?", "You are decoding", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else if (savedCodes.Count > 0)
            {
                DialogResult result = MessageBox.Show("Generating a new grid will clear all saved codes. Continue?", "You have saved codes", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if (result == DialogResult.No)
                {
                    return;
                }
                savedCodes.Clear();
            }
            grid.Generate();
            execute.PerformClick();
            savedCodes.Clear();
        }

        private void execute_Click(object sender, EventArgs e)
        {
            string input = inputBox.Text.ToUpper().Trim();
            string rule = ruleBox.Text.ToUpper().Trim();

            if (!ValidateRule(rule))
            { return; }

            Cipher cipher = new Cipher(input, rule);

            if (!(grid.ValidateCharacterSet(cipher.GetText())))
            {
                MessageBox.Show("An unknown character is present, please only use A-Z and 0-9", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (decodeCheck.Checked)
            {
                cipher.Decode(grid.GetGrid());
            }
            else
            {
                cipher.Encode(grid.GetGrid());
            }

            if (splitBox.Checked)
            {
                CleanUpText(cipher.GetText());
            }
            else
            {
                outputBox.Text = cipher.GetText();
            }
        }

        private bool ValidateRule(string rule)
        {
            string[] availableRules = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };

            if (rule == "")
            {
                return false;
            }

            bool valid = false;

            foreach (string availableRule in availableRules)
            {
                if (availableRule == rule)
                { valid = true; break; }
            }
            if (!valid)
            {
                MessageBox.Show("Invalid rule", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            return valid;

        }

        private void ruleBox_TextChanged(object sender, EventArgs e)
        {
            if (ruleBox.Text.ToUpper().Trim() == "X")
            {
                decodeCheck.Visible = false;
                decodeCheck.Checked = false;
            }
            else
            {
                decodeCheck.Visible = true;
            }
        }

        private void CleanUpText(string text)
        {
            string process = text.Replace(" ", "");
            for (int i = 5; i < process.Length; i += 6)
            {
                process = process.Insert(i, " ");
            }

            outputBox.Text = process;
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            inputBox.Text = outputBox.Text;
        }

        private void guideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://raf.mod.uk/aircadets/") { UseShellExecute = true });
        }

        private void errorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/shr0m/BletchleyMaker/issues") { UseShellExecute = true });
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/shr0m/BletchleyMaker?tab=readme-ov-file#support") { UseShellExecute = true });
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Print print = new Print(grid.GetGrid(), savedCodes);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save save = new Save(grid.GetGrid());
        }
        private void supportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Open open = new Open();
            List<char> list = open.GetList();

            grid.SetGrid(list.ToArray());
        }

        private void splitBox_CheckedChanged(object sender, EventArgs e)
        {
            if (splitBox.Checked)
            {
                CleanUpText(outputBox.Text);
            }
            else
            {
                outputBox.Text = outputBox.Text.Replace(" ", "");
            }
        }

        private void addCode_Click(object sender, EventArgs e)
        {
            if (savedCodes.Count != 10)
            {
                decodeCheck.Checked = false;
                execute.PerformClick();
                if (outputBox.Text.Replace(" ", "") != "")
                {
                    if (!splitBox.Checked)
                    {
                        CleanUpText(outputBox.Text);
                    }
                    savedCodes.Add(ruleBox.Text.ToUpper() + "   " + outputBox.Text);
                    MessageBox.Show("Code added", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else
                {
                    MessageBox.Show("You can't add an empty code", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            else
            {
                MessageBox.Show("You can't add more than 10 codes", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

        }

        private void viewCodes_Click(object sender, EventArgs e)
        {
            if (view == null || view.IsDisposed)
            {
                view = new ViewCodes(savedCodes, this);
                view.Show();
            }
            else
            {
                view.BringToFront();
            }
        }

        public void RemoveCode(int code)
        {
            savedCodes.RemoveAt(code);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await UpdateChecker.CheckForUpdate();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeGrid.PerformClick();
        }

        private void manualAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGrid newGrid = new NewGrid();

            if (newGrid.ShowDialog() == DialogResult.OK)
            {
                List<char> list = newGrid.GetList();
                grid.SetGrid(list.ToArray());
            }

            newGrid.Dispose();
        }

        public void SetChars(List<char> tempChar)
        {
            Chars = tempChar;
        }

        private void characterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterSet charSet = new CharacterSet(Chars, this);
            charSet.Show();
            charSet.BringToFront();
        }
    }
}
