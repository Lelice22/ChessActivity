using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Knight : Piece
    {
        public Knight(PieceColor color, Board board) : base(color, board)
        {
        }
        public override string ToString()
        {
            return "N";
        }

        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            int inicialrow = position.Row;
            int inicialcolumn = position.Column;

            for (int i = -1; i < 2; i += 2)
            {
                int row = inicialrow + i;
                int column2 = inicialcolumn + i;
                for (int j = -2; j < 3; j = j + 4)
                {
                    int column = inicialcolumn + j;
                    int row2 = inicialrow + j;
                    Position pos = new Position(row, column);
                    if (board.ValidPosition(pos) && AllowedMove(pos))
                    {
                        mat[pos.Row, pos.Column] = true;
                    }
                    Position pos2 = new Position(row2, column2);
                    if (board.ValidPosition(pos2) && AllowedMove(pos2))
                    {
                        mat[pos2.Row, pos2.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}