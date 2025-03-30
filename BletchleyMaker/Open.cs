using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker
{
    internal class Open
    {
        private List<char> decodedList;

        public Open()
        {
            decodedList = new List<char>();

            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = "txt",  // Set default file extension
                AddExtension = true   // Ensure extension is added automatically
            };

            // Show the OpenFileDialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                // Read and decode the content from the file
                ReadFromFile(filePath);
            }
        }

        private void ReadFromFile(string filePath)
        {
            try
            {
                // Read all text from the file (Base64 encoded string)
                string fileContent = File.ReadAllText(filePath);

                // Decode the Base64 string into a byte array
                byte[] decodedBytes = Convert.FromBase64String(fileContent);

                // Convert the byte array back into a List<char>
                ConvertByteArrayToList(decodedBytes);
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., file not found, Base64 decode error, etc.)
                MessageBox.Show($"Error reading or decoding the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertByteArrayToList(byte[] byteArray)
        {
            // Convert the byte array to a List<char>
            decodedList.Clear();  // Clear the list before adding new data

            foreach (byte b in byteArray)
            {
                decodedList.Add((char)b);  // Cast each byte back to a char
            }
        }

        public List<char> GetList()
        {
            return decodedList;
        }
    }
}
