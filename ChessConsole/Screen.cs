using System;
using System.Collections.Generic;
using BoardG;


namespace ChessConsole
{
    internal class Screen
    {
        //showing board concerns to screen, therefore it wil be setted in a screen Class
        public static void ShowBoard(Board board)
        {
            for (int i = 0; i<board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{board.piece(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
