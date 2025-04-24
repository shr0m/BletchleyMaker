using System;
using System.CodeDom;
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
    public partial class CharacterSet : Form
    {
        private List<char> Chars;
        private BletchleyMaker.Main form = null!;
        public CharacterSet(List<char> chars, Main inpForm)
        {
            InitializeComponent();
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
            this.StartPosition = FormStartPosition.CenterParent;

            Chars = chars;
            form = inpForm;

            characterBox.Text = string.Join("", Chars);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string text = characterBox.Text.ToUpper().Trim();
            if (Validation(text))
            {
                form.SetChars(text.ToList<char>()); // Update Main's list
                form.grid.SetChars(text.ToList<char>()); // Update Grid's list    
                form.grid.Generate();

                this.Close();
            }
        }

        private bool Validation(string input)
        {
            if (input.Length != 36)
            {
                MessageBox.Show("Character set must be 36 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }

            HashSet<char> seen = new HashSet<char>();
            foreach (char c in input)
            {
                if (!seen.Add(c))
                {
                    MessageBox.Show("Duplicate characters exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return false;
                }
            }

            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetChars_Click(object sender, EventArgs e)
        {
            characterBox.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }
    }
}
