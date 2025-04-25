using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BletchleyMaker.Functions
{
    internal class Open
    {
        private List<string> loadedCodes = new List<string>();
        private List<char> decodedList = new List<char>();

        public List<char> GetList() => decodedList;
        public List<string> GetSavedCodes() => loadedCodes;
        public bool WasSuccessful { get; private set; } = false;

        public Open(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    ReadFromFile(filePath);
                    WasSuccessful = true;
                }
                else
                {
                    MessageBox.Show($"File does not exist: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading or decoding the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            decodedList.Clear();
            foreach (byte b in byteArray)
            {
                decodedList.Add((char)b);
            }
        }
    }
}