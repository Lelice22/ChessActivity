using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Pawn : Piece
    {
        public Pawn(PieceColor color, Board board) : base(color, board)
        {
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
                int row = position.Row - 1;
                Position pos = new Position(row, position.Column);
                if (board.piece(pos) == null && board.ValidPosition(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Position pos2 = new Position(row, position.Column + i);
                    if (board.ValidPosition(pos2) && (board.piece(pos2) != null && board.piece(pos2).Color != Color))
                    {
                        mat[pos2.Row, pos2.Column] = true;

                    }
                }

            }
            else
            {
                int row = position.Row + 1;
                Position pos = new Position(row, position.Column);
                if (board.piece(pos) == null && board.ValidPosition(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Position pos2 = new Position(row, position.Column + i);
                    if (board.ValidPosition(pos2) && (board.piece(pos2) != null && board.piece(pos2).Color != Color))
                    {
                        mat[pos2.Row, pos2.Column] = true;

                    }
                }
            }
            return mat;
        }
    }
}
