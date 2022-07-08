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
        public Piece piece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        public void SetPiece(Piece piece, Position position)
        {
            if (PieceinPosition(position))
            {
                throw new ExceptionBoard("There is already a piece in this position.");
            }
            Pieces[position.Row, position.Column] = piece;
            piece.position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (piece(position) == null)
            {
                return null;
            }
            Piece aux = piece(position);
            aux.position = null;
            Pieces[position.Row, position.Column] = null;
            return aux;
        }
        public bool PieceinPosition(Position position)
        {
            ValidatePosition(position);
            return piece(position) != null; //revisar Piece e piece
        }
        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Column < 0 || position.Row >= Rows || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }
        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {

                throw new ExceptionBoard("Invalid placement of this piece");
            }
        }

    }
}
