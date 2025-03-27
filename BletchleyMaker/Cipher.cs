using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BletchleyMaker
{
    internal class Cipher
    {

        private string Text;
        private string EncodeRule;
        private string DecodeRule;

        public Cipher(string plain, string r)
        {
            Text = plain.Replace(" ", "");
            DecodeRule = r;
            string[] decodeRules = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };
            string[] encodeRules = { "X", "D1", "U1", "R1", "L1", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5" };

            int final = 0;
            for (int i = 0; i < decodeRules.Length; i++)
            {
                if (decodeRules[i] == r)
                {
                    final = i;
                    break;
                }
            }

            EncodeRule = encodeRules[final];
        }

        public void Encode(char[,] gridArray)
        {
            char ruleChoice = EncodeRule[0];

            switch (ruleChoice)
            {
                case 'R':
                    RightEncode(gridArray); break;
                case 'L':
                    LeftEncode(gridArray); break;
                case 'U':
                    UpEncode(gridArray); break;
                case 'D':
                    DownEncode(gridArray); break;
                case 'X':
                    XConvert(gridArray); break;
            }
        }

        private void UpEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 0)
                    {
                        row = 5;
                    }
                    else
                    {
                        row--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void DownEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 5)
                    {
                        row = 0;
                    }
                    else
                    {
                        row++;
                    }
                }

                final += gridArray[row, column];
            }

            Text = final;
        }

        private void LeftEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 0)
                    {
                        column = 5;
                    }
                    else
                    {
                        column--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void RightEncode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 5)
                    {
                        column = 0;
                    }
                    else
                    {
                        column++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        public string GetText()
        {
            return Text;
        }

        public void Decode(char[,] gridArray)
        {
            char ruleChoice = EncodeRule[0];

            switch (ruleChoice)
            {
                case 'R':
                    RightDecode(gridArray); break;
                case 'L':
                    LeftDecode(gridArray); break;
                case 'U':
                    UpDecode(gridArray); break;
                case 'D':
                    DownDecode(gridArray); break;
                case 'X':
                    XConvert(gridArray); break;
            }
        }

        private void RightDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 0)
                    {
                        column = 5;
                    }
                    else
                    {
                        column--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void LeftDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (column == 5)
                    {
                        column = 0;
                    }
                    else
                    {
                        column++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void UpDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 5)
                    {
                        row = 0;
                    }
                    else
                    {
                        row++;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void DownDecode(char[,] gridArray)
        {
            int index = Convert.ToInt32(EncodeRule[1]);
            string final = "";

            for (int i = 0; i < Text.Length; i++)
            {
                int row = -1, column = -1;

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (Text[i] == gridArray[r, c])
                        {
                            row = r;
                            column = c;
                            break;
                        }
                    }
                    if (row != -1) break;
                }

                if (row == -1 || column == -1)
                {
                    final += Text[i];
                    continue;
                }

                for (int j = 0; j < index; j++)
                {
                    if (row == 0)
                    {
                        row = 5;
                    }
                    else
                    {
                        row--;
                    }
                }


                final += gridArray[row, column];
            }

            Text = final;
        }

        private void XConvert(char[,] gridArray)
        {
            string final = "";
            for (int loop = 0; loop < Text.Length; loop++)
            {
                int row = -1;
                int column = -1;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (gridArray[i, j] == Text[loop])
                        {
                            row = i;
                            column = j;
                        }
                    }
                }
                int finalRow = HorizX(row);
                int finalColumn = VertX(column);

                final += gridArray[finalRow, finalColumn];
            }
            Text = final;
        }

        private int VertX(int column)
        {
            int move = -100;

            switch (column)
            {
                case 0:
                    move = 5; break;
                case 1:
                    move = 3; break;
                case 2:
                    move = 1; break;
                case 3:
                    move = -1; break;
                case 4:
                    move = -3; break;
                case 5:
                    move = -5; break;
            }

            if (move == -100)
            {
                return 0;
            }
            else
            {
                return column + move;
            }

        }

        private int HorizX(int row)
        {
            int move = -100;

            switch (row)
            {
                case 0:
                    move = 5; break;
                case 1:
                    move = 3; break;
                case 2:
                    move = 1; break;
                case 3:
                    move = -1; break;
                case 4:
                    move = -3; break;
                case 5:
                    move = -5; break;
            }

            if (move == -100)
            {
                return 0;
            }
            else
            {
                return row + move;
            }

        }
    }
}
