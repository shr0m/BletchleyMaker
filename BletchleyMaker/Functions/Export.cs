using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

namespace BletchleyMaker.Functions
{
    internal class Export
    {
        private char[,] grid;
        private string[] codes;

        public Export(char[,] gridSu, List<string> codesSu)
        {
            grid = gridSu;
            codes = codesSu.ToArray();
        }

        public void ExportToPdf(string filePath)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            PdfContentByte g = writer.DirectContent;
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            int cellSize = 60;
            int rows = 6;
            int columns = 6;

            // Starting from the top down, so max height minus top margin
            float startY = doc.PageSize.Height - 100;
            float startX = (doc.PageSize.Width - (columns * cellSize)) / 2;

            // Draw grid + text
            DrawGrid(g, baseFont, startX, startY, cellSize);

            // Draw codes beneath
            DrawCodes(g, baseFont, startX, startY - (rows * cellSize) - 40);
            doc.Close();

            MessageBox.Show("PDF exported successfully.");
        }

        private void DrawGrid(PdfContentByte g, BaseFont font, float startX, float startY, int cellSize)
        {
            int rows = 6;
            int columns = 6;

            // First draw all lines individually to apply different widths
            for (int row = 0; row <= rows; row++)
            {
                if (row == 3)
                    g.SetLineWidth(3f); // Middle horizontal line
                else
                    g.SetLineWidth(1.5f);

                g.MoveTo(startX, startY - row * cellSize);
                g.LineTo(startX + columns * cellSize, startY - row * cellSize);
                g.Stroke();
            }

            for (int col = 0; col <= columns; col++)
            {
                if (col == 3)
                    g.SetLineWidth(3f); // Middle vertical line
                else
                    g.SetLineWidth(1.5f);

                g.MoveTo(startX + col * cellSize, startY);
                g.LineTo(startX + col * cellSize, startY - rows * cellSize);
                g.Stroke();
            }

            // Draw cell content
            g.BeginText();
            g.SetFontAndSize(font, 18);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    string text = grid[row, col].ToString();
                    float textWidth = font.GetWidthPoint(text, 18);
                    float x = startX + col * cellSize + (cellSize - textWidth) / 2;
                    float y = startY - (row + 1) * cellSize + (cellSize - 18) / 2;

                    g.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
                }
            }

            g.EndText();
        }

        private void DrawCodes(PdfContentByte g, BaseFont font, float xStart, float yStart)
        {
            g.BeginText();
            g.SetFontAndSize(font, 12);

            float lineHeight = 26;
            float maxWidth = PageSize.A4.Width - 100;
            int maxLines = 10;
            int totalLinesDrawn = 0;

            for (int i = 0; i < codes.Length && totalLinesDrawn < maxLines; i++)
            {
                string code = codes[i];
                List<string> wrappedLines = WrapText(code, font, 14, maxWidth);

                foreach (var line in wrappedLines)
                {
                    if (totalLinesDrawn >= maxLines)
                        break;

                    float textWidth = font.GetWidthPoint(line, 14);
                    float x = (PageSize.A4.Width - textWidth) / 2;
                    float y = yStart - totalLinesDrawn * lineHeight;

                    g.ShowTextAligned(PdfContentByte.ALIGN_LEFT, line, x, y, 0);
                    totalLinesDrawn++;
                }
            }

            g.EndText();
        }

        private List<string> WrapText(string text, BaseFont font, float fontSize, float maxWidth)
        {
            List<string> lines = new List<string>();
            string currentLine = "";
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                string testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                float testWidth = font.GetWidthPoint(testLine, fontSize);

                if (testWidth <= maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentLine))
                        lines.Add(currentLine);

                    currentLine = word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
                lines.Add(currentLine);

            return lines;
        }

    }
}