using System;
using BoardG;
using BoardG.Enums;
using ChessGame;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            try
            {
                Board board = new Board(8, 8);
                //Console.WriteLine("Generated");
                board.SetPiece(new Rook(PieceColor.Black, board), new Position(0, 0));
                board.SetPiece(new Rook(PieceColor.Black, board), new Position(1, 3));
                board.SetPiece(new King(PieceColor.Black, board), new Position(2, 4));
                board.SetPiece(new King(PieceColor.Black, board), new Position(0, 9));

                Screen.ShowBoard(board);
            }
            catch (ExceptionBoard e)
            {
                Console.WriteLine(e.Message);
            }
            */
            ChessPosition position = new ChessPosition('a', 9);

            Console.WriteLine(position);
            Console.WriteLine(position.ChesstoMatrix());
            
        }
    }
}