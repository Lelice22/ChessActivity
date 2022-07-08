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
            Board board = new Board(8, 8);
            Console.WriteLine( "Generated");
            board.SetPiece(new Rook(PieceColor.Black, board), new Position(0, 0));
            board.SetPiece(new Rook(PieceColor.Black, board), new Position(1, 3));
            board.SetPiece(new King(PieceColor.Black, board), new Position(2, 4));

            Screen.ShowBoard(board);
        }
    }
}