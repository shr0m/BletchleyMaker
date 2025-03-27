using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker
{
    internal class Print
    {
        private char[,] grid;
        private PrintDialog printDialog;
        private PrintDocument printDoc;
        public Print(char[,] gridSu)
        {
            grid = gridSu;

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
            DrawGrid(e.Graphics!, 20, 20);
        }
        private void DrawGrid(Graphics g, int xOffset, int yOffset)
        {
            int cellWidth = 50;
            int cellHeight = 50;
            int rows = 6;
            int columns = 6;

            Pen pen = new Pen(Color.Black, 2);

            for (int col = 0; col <= columns; col++)
            {
                int x = xOffset + col * cellWidth;
                g.DrawLine(pen, x, yOffset, x, yOffset + rows * cellHeight);
            }

            for (int row = 0; row <= rows; row++)
            {
                int y = yOffset + row * cellHeight;
                g.DrawLine(pen, xOffset, y, xOffset + columns * cellWidth, y);
            }

            Font font = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    string cellContent = Convert.ToString(grid[row, col]);
                    SizeF textSize = g.MeasureString(cellContent, font);
                    float x = xOffset + col * cellWidth + (cellWidth - textSize.Width) / 2;
                    float y = yOffset + row * cellHeight + (cellHeight - textSize.Height) / 2;
                    g.DrawString(cellContent, font, brush, x, y);
                }
            }
        }
    }
}
