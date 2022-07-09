using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;
using ChessGame;


namespace ChessConsole
{
    internal class Screen
    {
        //showing board concerns to screen, therefore it wil be setted in a screen Class
        public static void ShowMatch(ChessMatch match)
        {
            ShowBoardBegin(match.board);
            Console.WriteLine();
            ShowCapturedPieces(match);
            Console.WriteLine($"Shift: {match.Shift}");
            if (!match.Fininshed)
            {
                Console.WriteLine($"Awayting for move : {match.PresentPlayer}");
                Console.WriteLine();

                if (match.Check)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine($"Aware now! You, {match.PresentPlayer}, are in check!");
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("End of the Match,");
                Console.WriteLine($"{match.PresentPlayer} CheckMate!");
                Console.WriteLine("-----------------");
            }

            
        }

        public static void ShowCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.WriteLine("White: ");
            ShowSet(match.capturedPieces(PieceColor.White));
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Black: ");
            ShowSet(match.capturedPieces(PieceColor.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ShowSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write($"{piece} ");
            }
            Console.WriteLine("]");
            Console.WriteLine();
        }
        public static void ShowBoardBegin(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    ShowPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void ShowBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor originalBg = ConsoleColor.Black;
            ConsoleColor modifiedBg = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMovements[i, j])
                    {
                        Console.BackgroundColor = modifiedBg;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBg;
                    }
                    ShowPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBg;

                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse($"{s[1]}");
            return new ChessPosition(column, row);

        }
        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == PieceColor.White)
                {
                    Console.Write($"{piece} ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{piece} ");
                    Console.ForegroundColor = aux;
                }
            }

        }
    }
}
