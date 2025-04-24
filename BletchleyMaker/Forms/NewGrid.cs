using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker
{
    public partial class NewGrid : Form
    {
        private TextBox[] compArray;
        private List<char> list = new List<char>();
        private char[] Chars;
        public NewGrid(List<char> chars)
        {
            InitializeComponent();
            this.AcceptButton = submit;

            TextBox[] comp =
            {
                input1, input2, input3, input4, input5, input6,
                input7, input8, input9, input10, input11, input12,
                input13, input14, input15, input16, input17, input18,
                input19, input20, input21, input22, input23, input24,
                input25, input26, input27, input28, input29, input30,
                input31, input32, input33, input34, input35, input36
            };
            compArray = comp;

            for (int i = 0; i < compArray.Length; i++)
            {
                compArray[i].Tag = i;
                compArray[i].MaxLength = 1;

                compArray[i].TextChanged += AutoAdvance!;
                compArray[i].KeyDown += MoveWithArrows!;
                compArray[i].KeyDown += HandleBackspace!;
            }

            Chars = new char[36];
            for (int i = 0; i < Chars.Length; i++)
            {
                Chars[i] = chars[i];
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            foreach (TextBox box in compArray)
            {
                list.Add(Convert.ToChar(box.Text.ToUpper()));
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInputs()
        {
            HashSet<char> seenChars = new HashSet<char>();

            foreach (TextBox textBox in compArray)
            {
                string input = textBox.Text.Trim().ToUpper();

                if (input.Length != 1)
                {
                    MessageBox.Show("Each field must contain exactly one character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return false;
                }

                char character = input[0];

                if (!Chars.Contains(character))
                {
                    MessageBox.Show("Invalid character detected. Please use characters in the character set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return false;
                }

                if (seenChars.Contains(character))
                {
                    MessageBox.Show($"Duplicate character detected: '{character}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return false;
                }

                seenChars.Add(character);
            }

            return true;
        }
        public List<char> GetList()
        {
            return list;
        }
        private void MoveWithArrows(object sender, KeyEventArgs e)
        {
            TextBox? current = sender as TextBox;
            int index = (int)current!.Tag!;

            int row = index / 6;
            int col = index % 6;

            if (e.KeyCode == Keys.Left && col > 0)
                compArray[index - 1].Focus();

            else if (e.KeyCode == Keys.Right && col < 5)
                compArray[index + 1].Focus();

            else if (e.KeyCode == Keys.Up && row > 0)
                compArray[index - 6].Focus();

            else if (e.KeyCode == Keys.Down && row < 5)
                compArray[index + 6].Focus();
        }
        private void AutoAdvance(object sender, EventArgs e)
        {
            TextBox? current = sender as TextBox;
            int index = (int)current!.Tag!;

            string input = current.Text.Trim().ToUpper();

            if (input.Length == 1)
            {
                char c = input[0];

                // Check if the character is in the allowed character set
                if (!Chars.Contains(c))
                {
                    current.Clear();
                    return;
                }

                // Check for duplicate entries
                foreach (TextBox tb in compArray)
                {
                    if (tb != current && tb.Text.ToUpper() == c.ToString())
                    {
                        current.Clear();
                        return;
                    }
                }

                // Set properly formatted character and move focus
                current.Text = c.ToString();
                if (index < compArray.Length - 1)
                {
                    compArray[index + 1].Focus();
                }
            }
        }
        private void HandleBackspace(object sender, KeyEventArgs e)
        {
            TextBox? current = sender as TextBox;
            int index = (int)current!.Tag!;

            if (e.KeyCode == Keys.Back)
            {
                if (!string.IsNullOrEmpty(current.Text))
                {
                    current.Clear();
                }
                else if (index > 0)
                {
                    compArray[index - 1].Focus();
                    compArray[index - 1].Clear();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
