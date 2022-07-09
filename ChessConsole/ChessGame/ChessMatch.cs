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
        public bool Check { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            Shift = 1;
            PresentPlayer = PieceColor.White;
            Fininshed = false;
            pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            Check = false;
            SetPiece();
        }

        public Piece MakeMove(Position origin, Position final)
        {
            Piece piece = board.RemovePiece(origin);
            piece.AddNumberOfMovements();
            Piece capturedPiece = board.RemovePiece(final);
            board.SetPiece(piece, final);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position final, Piece capturePiece)
        {
            
            Piece piece = board.RemovePiece(final);
            piece.SubtractNumberOfMovements();
            if (capturePiece != null)
            {
                board.SetPiece(capturePiece, final);
                Captured.Remove(capturePiece);
            }

            Piece capturedPiece = board.RemovePiece(origin);
            board.SetPiece(piece, origin);
        }
        public void MakePlay(Position origin, Position final)
        {
            Piece capturedPiece = MakeMove(origin, final);

            if (InCheck(PresentPlayer))
            {
                UndoMovement(origin, final, capturedPiece);
                throw new ExceptionBoard("You shall not set your king in such danger.");
            }
            if (InCheck(Opponent(PresentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (TryCheckMate(Opponent(PresentPlayer)))
            {
                Fininshed = true;
            }
            else
            {
                Shift++;
                changePlayer();
            }
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
        public PieceColor Opponent(PieceColor color)
        {
            if (color == PieceColor.Black)
            {
                return PieceColor.White;
            }
            else
            {
                return PieceColor.Black;
            }
        }
        private Piece king(PieceColor color)
        {
            foreach (Piece x in AvailablePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool InCheck(PieceColor color)
        {
            Piece k = king(color);
            if (k == null)
            {
                throw new ExceptionBoard($"There is no {color} king in this match.");

            }
            foreach (Piece x in AvailablePieces(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements(x.position);
                if (mat[k.position.Row, k.position.Column])
                {
                    return true;
                }

            }
            return false;
        }

        public bool TryCheckMate(PieceColor color)
        {
            if (!InCheck(color))
            {
                return false;
            }
            foreach (Piece x in AvailablePieces(color))
            {
                bool[,] mat = x.PossibleMovements(x.position);
                for (int i = 0; i < board.Rows; i++)
                {
                    for (int j= 0; j < board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position final = new Position(i, j);
                            Piece p = MakeMove(origin, final);
                            bool test = InCheck(color);
                            UndoMovement(origin, final, p);
                            if (!test)
                            {
                                return false;
                            }

                        }

                    }
                } 
            }
            return true;
        }

        public void SetNewPiece(char column, int row, Piece piece)
        {
            board.SetPiece(piece, new ChessPosition(column, row).ChesstoMatrix());
            pieces.Add(piece);
        }
        private void SetPiece()
        {
            SetNewPiece('d', 1, new Rook(PieceColor.Black, board));
            SetNewPiece('c', 2, new Rook(PieceColor.White, board));
            SetNewPiece('h', 4, new King(PieceColor.Black, board));
            SetNewPiece('g', 8, new King(PieceColor.White, board));
            SetNewPiece('b', 7, new Rook(PieceColor.Black, board));
            SetNewPiece('c', 3, new Rook(PieceColor.Black, board));


        }
    }
}
