using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Bishop : Piece
    {
        public Bishop(PieceColor color, Board board) : base(color, board)
        {
        }
        public override string ToString()
        {
            return "B";
        }
        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];

            //Upprightway
            Position pos = new Position(position.Row - 1, position.Column + 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row--;
                pos.Column++;
            }

            //Downrigthway
            pos = new Position(position.Row + 1, position.Column + 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row++;
                pos.Column++;
            }




            //Uppleftway
            pos = new Position(position.Row - 1, position.Column - 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row--;
                pos.Column--;
            }

            //Downleftway
            pos = new Position(position.Row + 1, position.Column - 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row++;
                pos.Column--;
            }
            return mat;
        }
    }
}
