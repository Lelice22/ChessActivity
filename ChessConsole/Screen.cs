using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;


namespace ChessConsole
{
    internal class Screen
    {
        //showing board concerns to screen, therefore it wil be setted in a screen Class
        public static void ShowBoard(Board board)
        {
            for (int i = 0; i<board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ShowPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void ShowPiece(Piece piece)
        {
            if (piece.Color == PieceColor.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }

    }
}
