using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(PieceColor color, Board board, ChessMatch match) : base(color, board)
        {
            this.match = match; 
        }
        public override string ToString()
        {
            return "p";
        }
        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            //Upprightway
            if (Color == PieceColor.White)
            {
                int treshold = -2;
                if (NumberofMovements == 0 && board.piece(new Position(position.Row - 1, position.Column)) == null)
                {
                    treshold = -3;
                }
                for (int j = -1; j > treshold; j--)
                {
                    Position pos = new Position(position.Row + j, position.Column);
                    if (board.piece(pos) == null && board.ValidPosition(pos))
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Position pos2 = new Position(position.Row - 1, position.Column + i);
                        if (board.ValidPosition(pos2) && (board.piece(pos2) != null && board.piece(pos2).Color != Color))
                        {
                            mat[pos2.Row, pos2.Column] = true;

                        }
                    }
                    //Special move : En Passant
                    //White
                    if (position.Row == 3)
                    {
                        Position left = new Position(position.Row, position.Column - 1);
                        if (board.ValidPosition(left) && board.piece(left) is Pawn && board.piece(left).Color != Color && board.piece(left) == match.EnPassantSusceptible)
                        {
                            mat[2, left.Column] = true;
                        }

                        Position right = new Position(position.Row, position.Column + 1);
                        if (board.ValidPosition(right) && board.piece(right) is Pawn && board.piece(right).Color != Color && board.piece(right) == match.EnPassantSusceptible)
                        {
                            mat[2, right.Column] = true;
                        }
                    }
                }

            }
            else
            {
                int treshold = + 2;
                if (NumberofMovements == 0 && board.piece(new Position(position.Row + 1, position.Column)) == null)
                {
                    treshold = + 3;
                }
                for (int j = 1; j < treshold; j++)
                {
                    Position pos = new Position(position.Row + j, position.Column);
                    if (board.piece(pos) == null && board.ValidPosition(pos))
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Position pos2 = new Position(position.Row + 1, position.Column + i);
                        if (board.ValidPosition(pos2) && (board.piece(pos2) != null && board.piece(pos2).Color != Color))
                        {
                            mat[pos2.Row, pos2.Column] = true;

                        }
                    }
                    //Special move : En Passant
                    //Black
                    if (position.Row == 4)
                    {
                        Position left = new Position(position.Row, position.Column - 1);
                        if (board.ValidPosition(left) && board.piece(left) is Pawn && board.piece(left).Color != Color && board.piece(left) == match.EnPassantSusceptible)
                        {
                            mat[5, left.Column] = true;
                        }

                        Position right = new Position(position.Row + 1, position.Column + 1);
                        if (board.ValidPosition(right) && board.piece(right) is Pawn && board.piece(right).Color != Color && board.piece(right) == match.EnPassantSusceptible)
                        {
                            mat[5, right.Column] = true;
                        }
                    }
                }
            }

            return mat;
        }
    }
}
