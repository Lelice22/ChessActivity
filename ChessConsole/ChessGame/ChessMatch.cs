using System.Collections.Generic;
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
        private HashSet<Piece> pieces;
        private HashSet<Piece> Captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            Shift = 1;
            PresentPlayer = PieceColor.White;
            Fininshed = false;
            pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            SetPiece();
        }

        public void MakeMove(Position origin, Position final)
        {
            Piece piece = board.RemovePiece(origin);
            piece.AddNumberOfMovements();
            Piece capturedPiece = board.RemovePiece(final);
            board.SetPiece(piece, final);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
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
            if (!board.piece(origin).PossibleMove(origin, final))
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

        public HashSet<Piece> capturedPieces(PieceColor color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> AvailablePieces(PieceColor color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }
        public void SetNewPiece(char column, int row, Piece piece)
        {
            board.SetPiece(piece, new ChessPosition(column, row).ChesstoMatrix());
            pieces.Add(piece);
        }
        private void SetPiece()
        {
            SetNewPiece('c', 1, new Rook(PieceColor.Black, board));
            SetNewPiece('c', 2, new Rook(PieceColor.White, board));
            SetNewPiece('h', 4, new King(PieceColor.Black, board));
            SetNewPiece('g', 8, new King(PieceColor.White, board));

        }
    }
}
