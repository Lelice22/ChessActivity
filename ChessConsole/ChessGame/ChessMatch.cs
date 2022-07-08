using System;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        private int Shift;
        private PieceColor PresentPlayer; 

        public ChessMatch()
        {
            board = new Board(8, 8);
            Shift = 1;
            PresentPlayer = PieceColor.White;
            SetPiece();
        }

        public void MakeMove(Position origin, Position final)
        {
            Piece piece = board.RemovePiece(origin);
            piece.AddNumberOfMovements();
            Piece capturedPiece = board.RemovePiece(final);
            board.SetPiece(piece, final);

        }
        private void SetPiece()
        {
            board.SetPiece(new Rook(PieceColor.Black, board), new ChessPosition('c', 1).ChesstoMatrix());
            board.SetPiece(new King(PieceColor.White, board), new ChessPosition('h', 8).ChesstoMatrix());
            board.SetPiece(new King(PieceColor.Black, board), new ChessPosition('a', 4).ChesstoMatrix());
            board.SetPiece(new Rook(PieceColor.White, board), new ChessPosition('b', 2).ChesstoMatrix());
        }
    }
}
