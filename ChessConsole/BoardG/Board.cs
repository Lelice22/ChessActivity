using System;
using System.Collections.Generic;

namespace BoardG
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public void SetPiece(Piece piece, Position position)
        {
            Pieces[position.Row, position.Column] = piece;
            piece.position = position;
        }
    }
}
