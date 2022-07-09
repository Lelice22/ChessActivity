using System;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int Shift { get; private set; }
        public PieceColor PresentPlayer { get; private set; }
        public bool Fininshed { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            Shift = 1;
            PresentPlayer = PieceColor.White;
            Fininshed = false;
            SetPiece();
        }

        public void MakeMove(Position origin, Position final)
        {
            Piece piece = board.RemovePiece(origin);
            piece.AddNumberOfMovements();
            Piece capturedPiece = board.RemovePiece(final);
            board.SetPiece(piece, final);
        }
        
        public void MakePlay(Position origin, Position final)
        {
            MakeMove(origin, final);
            Shift++;
            changePlayer();
        }
        public void ValidateOrigin(Position position)
        {
            if (board.piece(position) == null)
            {
                throw new ExceptionBoard("There is no piece in this chosen position.");
            }
            if (PresentPlayer != board.piece(position).Color)
            {
                throw new ExceptionBoard($"This piece does not concer to you. You are playing {PresentPlayer}s");
            }
            if (!board.piece(position).AnyPossibleMovements())
            {
                throw new ExceptionBoard("There are no possible movements for this chosen piece");
            }
        }
        public void ValidateFinalPosition(Position origin, Position final)
        {
            if (!board.piece(origin).PossibleMove(final))
            {
                throw new ExceptionBoard("This move is ilegal.");
            }
        }
        private void changePlayer()
        {
            if (PresentPlayer == PieceColor.Black)
            {
                PresentPlayer = PieceColor.White;
            }
            else
            {
                PresentPlayer = PieceColor.Black;
            }
        }
        private void SetPiece()
        {
            board.SetPiece(new Rook(PieceColor.Black, board), new ChessPosition('c', 1).ChesstoMatrix());
            board.SetPiece(new King(PieceColor.White, board), new ChessPosition('h', 8).ChesstoMatrix());
            board.SetPiece(new King(PieceColor.Black, board), new ChessPosition('a', 4).ChesstoMatrix());
            board.SetPiece(new Rook(PieceColor.White, board), new ChessPosition('h', 7).ChesstoMatrix());
            board.SetPiece(new Rook(PieceColor.White, board), new ChessPosition('g', 8).ChesstoMatrix());
            board.SetPiece(new Rook(PieceColor.White, board), new ChessPosition('g', 7).ChesstoMatrix());


        }
    }
}
