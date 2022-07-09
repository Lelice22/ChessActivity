using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class King : Piece
    {
        public King(PieceColor color, Board board) : base(color, board)
        {
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
            return mat;
        }
    }
}
