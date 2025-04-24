using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BletchleyMaker.Functions
{
    internal class Grid
    {
        // Represents the 6x6 grid as a flat array of 36 characters
        private char[] gridArr = new char[36];

        // List of label components used for displaying the grid
        private List<Label> components;

        // Reference to the Main form, typed correctly for access to custom methods
        private BletchleyMaker.Main Main = null!;

        // The full list of characters available for use in the grid
        private List<char> Chars;

        // Constructor initializes the grid with label text and stores references
        public Grid(List<Label> compo, List<char> charinp, Main main)
        {
            for (int i = 0; i < compo.Count; i++)
            {
                gridArr[i] = Convert.ToChar(compo[i].Text);
            }
            components = compo;
            Main = main;
            Chars = charinp;
        }

        // Randomly fills the grid with characters from the original list
        public void Generate()
        {
            List<char> tempChar = new List<char>(Chars); // make a copy to work with
            Random rnd = new Random();
            for (int i = 0; i < gridArr.Length; i++)
            {
                int chosenIndex = rnd.Next(0, tempChar.Count); // use the copy
                gridArr[i] = tempChar[chosenIndex];
                tempChar.RemoveAt(chosenIndex);
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (gridArr[i] != '&')
                { components[i].Text = gridArr[i].ToString(); }
                else
                { components[i].Text = "&&"; }
                
            }
            Main.SetChars(new List<char>(Chars)); // optional: pass original list back to Main
        }

        // Converts the flat grid array into a 2D 6x6 array
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

        // Checks if all characters in the input string exist in the current grid
        public bool ValidateCharacterSet(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < gridArr.Length; j++)
                {
                    if (input[i] == gridArr[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Sets the grid explicitly using a given array of characters
        public void SetGrid(char[] chars)
        {
            List<char> newChars = CheckChars(chars);

            for (int i = 0; i < 36; i++)
            {
                try
                {
                    components[i].Text = newChars[i].ToString();
                    gridArr[i] = newChars[i];
                }
                catch (Exception)
                {
                    // If characters are missing or invalid, reset to a generated grid
                    MessageBox.Show("Grid does not fit format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Generate();
                    return;
                }
            }

            MessageBox.Show("Grid set successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        // Removes spaces from a character array before it's used to populate the grid
        private List<char> CheckChars(char[] chars)
        {
            List<char> final = chars.ToList();

            for (int i = 0; i < final.Count; i++)
            {
                if (final[i] == ' ')
                {
                    final.RemoveAt(i);
                    i--;
                }
            }

            return final;
        }

        public void SetChars(List<char> newChars)
        {
            Chars = newChars;
        }
    }
}