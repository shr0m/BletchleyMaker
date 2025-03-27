using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker
{
    internal class Save
    {
        private char[,] grid;
        public Save(char[,] saveGrid)
        {
            grid = saveGrid;
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFile.DefaultExt = "txt";  // Set default file extension
            saveFile.AddExtension = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = saveFile.FileName;

                // Write some content to the selected file path
                WriteToFile(filePath);

                MessageBox.Show($"File saved to: {filePath}", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void WriteToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (char c in grid)
                {
                    writer.Write(c);
                }
            }
        }
    }
}
