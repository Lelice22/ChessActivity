using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class King : Piece
    {
        private ChessMatch match;
        public King(PieceColor color, Board board, ChessMatch match) : base(color, board)
        {
            this.match = match;
        }
        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Position pos = new Position(position.Row + i, position.Column + j);
                    if (board.ValidPosition(pos) && AllowedMove(pos) && !(i == 0 && j == 0))
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                }
            }

            //SpecialMove : Castle
            if (NumberofMovements == 0 && !match.Check)
            {

                //Kingside Castle
                Position posR = new Position(position.Row, position.Column + 3);
                if (TryRookforCastle(posR))
                {
                    Position p1 = new Position(position.Row, position.Column + 1);
                    Position p2 = new Position(position.Row, position.Column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.Row, position.Column + 2] = true;
                    }
                }
                //Queenside Castle
                Position posR2 = new Position(position.Row, position.Column - 4);
                if (TryRookforCastle(posR2))
                {
                    Position p1 = new Position(position.Row, position.Column - 1);
                    Position p2 = new Position(position.Row, position.Column - 2);
                    Position p3 = new Position(position.Row, position.Column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.Row, position.Column - 2] = true;
                    }
                }

            }
            return mat;
        }
        private bool TryRookforCastle(Position pos)

        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.Color == Color && p.NumberofMovements == 0;
        }
    }
}
