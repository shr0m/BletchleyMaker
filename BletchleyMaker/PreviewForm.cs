using System;
using System.Drawing;
using System.Windows.Forms;

namespace BletchleyMaker
{
    public partial class PreviewForm : Form
    {
        private string[,] GridArray;

        // Constructor to receive grid data from the main form
        public PreviewForm(char[,] gridArray)
        {
            InitializeComponent();

            // Convert char[,] to string[,] for display purposes
            string[,] final = new string[6, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    final[i, j] = Convert.ToString(gridArray[i, j]);
                }
            }

            GridArray = final;
        }

        // Override OnPaint to handle custom drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Call method to draw the grid
            DrawGrid(e.Graphics, 20, 20);
        }

        // Method to draw the grid on the form
        private void DrawGrid(Graphics g, int xOffset, int yOffset)
        {
            int cellWidth = 50;
            int cellHeight = 50;
            int rows = 6;
            int columns = 6;

            Pen pen;

            // Draw columns
            for (int col = 0; col <= columns; col++)
            {
                int x = xOffset + col * cellWidth;
                if (col == 0 || col == 6 || col == 3)
                {
                    pen = new Pen(Color.Black, 4);
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                }
                g.DrawLine(pen, x, yOffset, x, yOffset + rows * cellHeight);
            }

            // Draw rows
            for (int row = 0; row <= rows; row++)
            {
                int y = yOffset + row * cellHeight;
                if (row == 0 || row == 6 || row == 3)
                {
                    pen = new Pen(Color.Black, 4);
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                }
                g.DrawLine(pen, xOffset, y, xOffset + columns * cellWidth, y);
            }

            // Font and brush for the text
            Font font = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Draw the content in the grid cells
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    string cellContent = GridArray[row, col];
                    SizeF textSize = g.MeasureString(cellContent, font);
                    float x = xOffset + col * cellWidth + (cellWidth - textSize.Width) / 2;
                    float y = yOffset + row * cellHeight + (cellHeight - textSize.Height) / 2;
                    g.DrawString(cellContent, font, brush, x, y);
                }
            }
        }
    }
}
