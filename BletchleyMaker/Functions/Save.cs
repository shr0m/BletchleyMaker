using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker.Functions
{
    internal class Save
    {
        private char[,] grid;
        private List<string> savedCodes;

        public Save(char[,] saveGrid, List<string> savedCodes)
        {
            grid = saveGrid;
            this.savedCodes = savedCodes;

            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "BletchleyMaker Grid Files (*.bmc)|*.bmc|All Files (*.*)|*.*",
                DefaultExt = "bmc",
                AddExtension = true
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFile.FileName;
                WriteToFile(filePath);
                MessageBox.Show($"File saved to: {filePath}", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void WriteToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Create a string list with serialized data
                List<string> lines = new List<string>();

                // Flatten the grid into a string
                int rows = grid.GetLength(0);
                int cols = grid.GetLength(1);
                StringBuilder gridData = new StringBuilder();
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        gridData.Append(grid[row, col]);
                    }
                }

                lines.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes(gridData.ToString())));
                lines.Add("---"); // delimiter between grid and saved codes

                foreach (string code in savedCodes)
                {
                    lines.Add(Convert.ToBase64String(Encoding.UTF8.GetBytes(code)));
                }

                writer.Write(string.Join("\n", lines));
            }
        }
    }
}
