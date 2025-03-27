using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker
{
    internal class Open
    {
        private List<char> list = new List<char>();
        public Open()
        {
            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Optional: Set file filter (e.g., text files, all files, etc.)
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.DefaultExt = "txt";  // Set default file extension
            openFileDialog.AddExtension = true;  // Ensure extension is added automatically

            // Show the OpenFileDialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                // Read and display the content of the file
                ReadFromFile(filePath);
            }

        }

        private void ReadFromFile(string filePath)
        {
            try
            {
                // Read all text from the file using StreamReader or File.ReadAllText
                string fileContent = File.ReadAllText(filePath);
                foreach (char c in fileContent)
                {
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., file not found, access denied, etc.)
                MessageBox.Show($"Error reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<char> GetList()
        {
            return list;
        }
    }
}
