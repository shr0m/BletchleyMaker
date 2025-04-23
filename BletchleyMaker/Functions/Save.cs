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

        public Save(char[,] saveGrid)
        {
            grid = saveGrid;
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = "txt",  // Set default file extension
                AddExtension = true
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = saveFile.FileName;

                // Write the Base64 encoded grid to the file
                WriteToFile(filePath);

                MessageBox.Show($"File saved to: {filePath}", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void WriteToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Convert the grid to a byte array
                byte[] byteArray = GridToByteArray();

                // Base64 encode the byte array
                string base64Encoded = Convert.ToBase64String(byteArray);

                // Write the Base64 string to the file
                writer.Write(base64Encoded);
            }
        }

        private byte[] GridToByteArray()
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Create a byte array large enough to hold all the characters from the grid
            byte[] byteArray = new byte[rows * cols];

            int index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    byteArray[index++] = (byte)grid[row, col];
                }
            }

            return byteArray;
        }
    }
}
