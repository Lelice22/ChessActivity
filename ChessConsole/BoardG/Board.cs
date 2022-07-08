using System;
using System.Collections.Generic;

namespace BoardG
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Piece;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Piece = new Piece[rows, columns];
        }
    }
}
