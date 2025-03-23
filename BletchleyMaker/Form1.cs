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
        internal Grid grid;
        public List<Label> componentArray;

        public Form1()
        {
            InitializeComponent();

            componentArray = new List<Label> { col1row1, col2row1, col3row1, col4row1, col5row1, col6row1, col1row2, col2row2, col3row2, col4row2, col5row2, col6row2, col1row3, col2row3, col3row3, col4row3, col5row3, col6row3, col1row4, col2row4, col3row4, col4row4, col5row4, col6row4, col1row5, col2row5, col3row5, col4row5, col5row5, col6row5, col1row6, col2row6, col3row6, col4row6, col5row6, col6row6 };
            grid = new Grid(componentArray);

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
            grid.Generate();
            execute.PerformClick();
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
                ThrowError(1);
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

            outputBox.Text = cipher.GetText();
            CleanUpText();
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
            DialogResult result = MessageBox.Show("This will take you to the Github page for this project.", "Support", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

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

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                execute.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void CleanUpText()
        {
            string process = outputBox.Text;
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
    }
}
