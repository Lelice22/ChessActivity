using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Rook : Piece
    {
        public Rook(PieceColor color, Board board) : base(color, board)
        {
        }
        public override string ToString()
        {
            return "R";
        }
        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            //Uppway
            //Uppway
            Position pos = new Position(position.Row - 1, position.Column);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row--;
            }

            //Downway
            pos = new Position(position.Row + 1, position.Column);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row++;
            }

            //Leftway
            pos = new Position(position.Row, position.Column - 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column--;
            }


            //Rightway
            pos = new Position(position.Row, position.Column + 1);
            while (board.ValidPosition(pos) && AllowedMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column++;
            }
            return mat;
        }
    }
}