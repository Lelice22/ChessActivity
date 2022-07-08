using System;
using System.Collections.Generic;
using BoardG.Enums;

namespace BoardG
{
    internal class Piece
    {
        public Position position { get; set; }
        public PieceColor Color { get; protected set; }
        public int NumberofMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Position position, PieceColor color, Board board)
        {
            this.position = position;
            Color = color;
            this.board = board;
            NumberofMovements = 0;

        }
   
    }
}
