using System.Collections.Generic;
using BoardG;
using BoardG.Enums;
using ChessConsole;

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
        public Piece EnPassantSusceptible { get; private set; }
        public Screen screen;


        public ChessMatch()
        {
            board = new Board(8, 8);
            Shift = 1;
            PresentPlayer = PieceColor.White;
            Fininshed = false;
            pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            Check = false;
            EnPassantSusceptible = null;
            screen = new Screen();
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

            //Special move: Castle
            //Kingside Castle
            if (piece is King && final.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position finalR = new Position(origin.Row, origin.Column + 1);
                MakeMove(originR, finalR);
            }
            //Queenside Castle
            if (piece is King && final.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position finalR = new Position(origin.Row, origin.Column - 1);
                MakeMove(originR, finalR);
            }

            //Special move : En Passant
            if (piece is Pawn && origin.Column != final.Column && capturedPiece == null)
            {
                Position poscapturedP = new Position(origin.Row, final.Column);
                capturedPiece = board.RemovePiece(poscapturedP);
                Captured.Add(capturedPiece);
            }


            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position final, Piece capturePiece)
        {
            
            Piece piece = board.RemovePiece(final);
            piece.SubtractNumberOfMovements();
            if (capturePiece != null && capturePiece == EnPassantSusceptible)
            {
                board.SetPiece(capturePiece, final);
                Captured.Remove(capturePiece);
            }
            board.RemovePiece(origin);
            board.SetPiece(piece, origin);
            //Special move: Castle
            //Kingside Castle
            if (piece is King && final.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position finalR = new Position(origin.Row, origin.Column + 1);
                UndoMovement(originR, finalR, null);

            }
            //Queenside Castle
            if (piece is King && final.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position finalR = new Position(origin.Row, origin.Column - 1);
                UndoMovement(originR, finalR, null);
            }
            //Special move : En Passant
            if (piece is Pawn && origin.Column != final.Column && capturePiece == EnPassantSusceptible)
            {

                Piece capPawn = board.RemovePiece(final);
                Position capPawninicial = new Position(origin.Row, final.Column);
                board.SetPiece(capPawn, capPawninicial);
                piece.SubtractNumberOfMovements();

            }
        }
        public void MakePlay(Position origin, Position final)
        {
            Piece capturedPiece = MakeMove(origin, final);
            Piece p = board.piece(final);
            //Special move: Promotion
            if(p is Pawn && (p.Color == PieceColor.White && final.Row == 0) || (p.Color == PieceColor.Black && final.Row == 7))
            {
                p = board.RemovePiece(final);
                pieces.Remove(p);
                Piece newpiece = ChooseNewPiece(p.Color);
                board.SetPiece(newpiece, final);
                pieces.Add(newpiece);

            }

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

            //Special move : En Passant
            if (p is Pawn && (final.Row == origin.Row - 2 || final.Row == origin.Row + 2))
            {
                EnPassantSusceptible = p;
            }
            else
            {
                EnPassantSusceptible = null;
            }
        }

        public Piece ChooseNewPiece(PieceColor color)
        {
            Piece newpiece = null;

            char c = screen.ShowChooseNewPiece();
            if (c == 'Q')
            {
                newpiece = new Queen(color, board);
            }
            if (c == 'R')
            {
                newpiece = new Rook(color, board);
            }
            if (c == 'B')
            {
                newpiece = new Bishop(color, board);
            }
            if (c == 'N')
            {
                newpiece = new Knight(color, board);
            }
            return newpiece;
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
            SetNewPiece('a', 8, new Rook(PieceColor.Black, board));
            SetNewPiece('b', 8, new Knight(PieceColor.Black, board));
            SetNewPiece('c', 8, new Bishop(PieceColor.Black, board));
            SetNewPiece('d', 8, new Queen(PieceColor.Black, board));
            SetNewPiece('e', 8, new King(PieceColor.Black, board, this));
            SetNewPiece('f', 8, new Bishop(PieceColor.Black, board));
            SetNewPiece('g', 8, new Knight(PieceColor.Black, board));
            SetNewPiece('h', 8, new Rook(PieceColor.Black, board));
            SetNewPiece('a', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('b', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('c', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('d', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('e', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('f', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('g', 7, new Pawn(PieceColor.Black, board, this));
            SetNewPiece('h', 7, new Pawn(PieceColor.Black, board, this));

            SetNewPiece('a', 1, new Rook(PieceColor.White, board));
            SetNewPiece('b', 1, new Knight(PieceColor.White, board));
            SetNewPiece('c', 1, new Bishop(PieceColor.White, board));
            SetNewPiece('d', 1, new Queen(PieceColor.White, board));
            SetNewPiece('e', 1, new King(PieceColor.White, board, this));
            SetNewPiece('f', 1, new Bishop(PieceColor.White, board));
            SetNewPiece('g', 1, new Knight(PieceColor.White, board));
            SetNewPiece('h', 1, new Rook(PieceColor.White, board));
            SetNewPiece('a', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('b', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('c', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('d', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('e', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('f', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('g', 2, new Pawn(PieceColor.White, board, this));
            SetNewPiece('h', 2, new Pawn(PieceColor.White, board, this));
        }
    }
}
