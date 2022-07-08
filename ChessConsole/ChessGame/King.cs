using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class King : Piece
    {
        public King(PieceColor color, Board board): base(color, board)
        {
        }
        public override string ToString()
        {
            return "K";
        }
    } 
}
