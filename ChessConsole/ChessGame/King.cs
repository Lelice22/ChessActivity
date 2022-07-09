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
            int inicialrow = position.Row;
            int inicialcolumn = position.Column;
            for (int i = -1; i < 2; i++)
            {
                position.Row = inicialrow + i;
                for (int j = -1; j < 2; j++)
                {
                    position.Column = inicialcolumn + j;
                    if (board.ValidPosition(position) && AllowedMove(position) && !(i == 0 && j == 0))
                    {
                        mat[position.Row, position.Column] = true;
                    }
                }
            }
            position.Row = inicialrow;
            position.Column = inicialcolumn;
            return mat;
        }
    }
}
