using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker.Functions
{
    internal class Print
    {
        private char[,] grid;
        private string[] codes;
        private PrintDialog printDialog;
        private PrintDocument printDoc;
        public Print(char[,] gridSu, List<string> codesSu)
        {
            grid = gridSu;
            codes = codesSu.ToArray();

            // Create a PrintDialog instance
            printDialog = new PrintDialog();

            // Create a PrintDocument instance
            printDoc = new PrintDocument();

            // Assign the PrintDocument to the PrintDialog
            printDialog.Document = printDoc;

            // Hook up the PrintPage event to define what gets printed
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            // Show the PrintDialog to let the user choose the printer and settings
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Start the print process if the user clicks "OK" in the print dialog
                try
                {
                    printDoc.Print();
                }
                catch (Win32Exception)
                {
                    return;
                }
            }
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawGrid(e.Graphics!, 20);
            DrawCodes(e.Graphics!, 20);
        }

        private void DrawGrid(Graphics g, int yOffset)
        {
            int cellWidth = 100;
            int cellHeight = 100;
            int rows = 6;
            int columns = 6;

            // Calculate total grid width and height
            int totalGridWidth = columns * cellWidth;
            int totalGridHeight = rows * cellHeight;

            // Get the width of the drawing surface
            float surfaceWidth = g.VisibleClipBounds.Width;

            // Calculate xOffset to center the grid horizontally on the drawing surface
            int xOffset = (int)((surfaceWidth - totalGridWidth) / 2);  // Center horizontally

            // Keep a small offset for the top (adjust as necessary)
            yOffset = 10;  // Can be modified for more or less top padding

            // Draw vertical lines (columns)
            for (int col = 0; col <= columns; col++)
            {
                Pen pen = new Pen(Color.Black, 2);
                if (col == 0 || col == 3 || col == 6)
                {
                    pen = new Pen(Color.Black, 4);  // Thicker lines for specific columns
                }

                int x = xOffset + col * cellWidth;
                g.DrawLine(pen, x, yOffset, x, yOffset + totalGridHeight);
            }

            // Draw horizontal lines (rows)
            for (int row = 0; row <= rows; row++)
            {
                Pen pen = new Pen(Color.Black, 2);
                if (row == 0 || row == 3 || row == 6)
                {
                    pen = new Pen(Color.Black, 4);  // Thicker lines for specific rows
                }
                int y = yOffset + row * cellHeight;
                g.DrawLine(pen, xOffset, y, xOffset + totalGridWidth, y);
            }

            // Draw text in the grid cells
            Font font = new Font("Arial", 20);
            SolidBrush brush = new SolidBrush(Color.Black);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    string cellContent = Convert.ToString(grid[row, col]);  // Assuming grid[row, col] holds the content
                    SizeF textSize = g.MeasureString(cellContent, font);
                    float x = xOffset + col * cellWidth + (cellWidth - textSize.Width) / 2;
                    float y = yOffset + row * cellHeight + (cellHeight - textSize.Height) / 2;
                    g.DrawString(cellContent, font, brush, x, y);
                }
            }
        }

        private void DrawCodes(Graphics g, int yOffset)
        {
            // Define the font for the codes
            Font codeFont = new Font("Arial", 16);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Set a vertical offset to place the codes below the grid
            int verticalOffset = yOffset + 6 * 100 + 20;  // Add 20 for spacing after the grid

            // Loop through the codes array and draw each string below the grid
            for (int i = 0; i < codes.Length; i++)
            {
                string code = codes[i];

                // Measure the text width to center it horizontally
                SizeF textSize = g.MeasureString(code, codeFont);
                float x = (g.VisibleClipBounds.Width - textSize.Width) / 2;  // Center horizontally

                // Calculate the vertical position for each code, with added spacing (e.g., 25px between each code)
                float y = verticalOffset + i * 30;  // 30px space between codes

                // Draw the code string
                g.DrawString(code, codeFont, brush, x, y);
            }
        }
    }
}
