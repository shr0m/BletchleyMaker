using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker.Functions
{
    internal class Open
    {
        private List<string> loadedCodes = new List<string>();
        public List<char> GetList() => decodedList;
        public List<string> GetSavedCodes() => loadedCodes;
        private List<char> decodedList;
        public bool WasSuccessful { get; private set; } = false;

        public Open()
        {
            decodedList = new List<char>();

            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BletchleyMaker Grid Files (*.bmc)|*.bmc|All Files (*.*)|*.*",
                DefaultExt = "bmc",  // Set default file extension
                AddExtension = true   // Ensure extension is added automatically
            };

            // Show the OpenFileDialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    ReadFromFile(filePath);
                    WasSuccessful = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading or decoding the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WasSuccessful = false;
                }
            }
        }


        private void ReadFromFile(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            string[] sections = fileContent.Split(new[] { "---" }, StringSplitOptions.None);

            if (sections.Length >= 1)
            {
                // Decode grid
                byte[] gridBytes = Convert.FromBase64String(sections[0]);
                ConvertByteArrayToList(gridBytes);
            }

            if (sections.Length > 1)
            {
                // Decode saved codes
                loadedCodes.Clear();
                string[] encodedCodes = sections[1].Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string encoded in encodedCodes)
                {
                    loadedCodes.Add(Encoding.UTF8.GetString(Convert.FromBase64String(encoded)));
                }
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

        public List<char> MyGetList()
        {
            return decodedList;
        }
    }
}
