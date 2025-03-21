using System.CodeDom.Compiler;

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
            Label[] components = { col1row1, col2row1, col3row1, col4row1, col5row1, col6row1, col1row2, col2row2, col3row2, col4row2, col5row2, col6row2, col1row3, col2row3, col3row3, col4row3, col5row3, col6row3, col1row4, col2row4, col3row4, col4row4, col5row4, col6row4, col1row5, col2row5, col3row5, col4row5, col5row5, col6row5, col1row6, col2row6, col3row6, col4row6, col5row6, col6row6 };

            Grid grid = new Grid(components);
            if (useSymbols.Checked)
                grid.Generate(true);
            else
                grid.Generate(false);
        }

    }

    public class Grid
    {
        char[] gridArr = new char[36];
        Label[] components;
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
            
            Random rnd = new Random();
            for (int i = 0; i < gridArr.Length; i++)
            {
                List<char> normChars = new List<char>{ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                List<char> symbolChars = new List<char>{ '@', 'B', 'C', 'D', 'E', 'F', 'G', 'H', ';', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '$', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '£', '4', '5', '6', '7', '8', '9' };

                if (!symbols)
                {
                    int chosenIndex = rnd.Next(0, normChars.Count);
                    gridArr[i] = normChars[chosenIndex];
                    normChars.RemoveAt(chosenIndex);
                }
                else
                {
                    int chosenIndex = rnd.Next(0, symbolChars.Count);
                    gridArr[i] = symbolChars[chosenIndex];
                    symbolChars.RemoveAt(chosenIndex);
                }
            }

            for (int i = 0; i < components.Length; i++)
            {
                components[i].Text = gridArr[i].ToString();
            }
        }
    }
}
