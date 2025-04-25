using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker.Forms
{
    public partial class AutomationPrompt : Form
    {
        private Main form = null!;
        private string contents = string.Empty;
        public AutomationPrompt(Main bletchleyform)
        {
            InitializeComponent();
            form = bletchleyform;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearAuto_Click(object sender, EventArgs e)
        {
            automationBox.Text = "";
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            contents = automationBox.Text;
            if (!Validation())
            {
                return;
            }

            
        }

        private bool Validation()
        {
            string[] acceptableParts = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };

            if (contents.Length == 0)
            {
                MessageBox.Show("Please enter a rule", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            string[] parts = contents.Split(',');

            foreach (string part in parts)
            {
                foreach (string accept in acceptableParts)
                {
                    if (part.Trim().ToUpper() == accept)
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Invalid rule. Please follow this format: X,U2,D3,D5...", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
