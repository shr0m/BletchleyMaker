using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker
{
    internal class Grid
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

        public void Generate()
        {

            List<char> chars = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random rnd = new Random();
            for (int i = 0; i < gridArr.Length; i++)
            {
                int chosenIndex = rnd.Next(0, chars.Count);
                gridArr[i] = chars[chosenIndex];
                chars.RemoveAt(chosenIndex);
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
            for (int i = 0; i < input.Length; i++)
            {
                bool found = false;

                for (int j = 0; j < gridArr.Length; j++)
                {
                    if (input[i] == gridArr[j])
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
