using System;
using System.Collections.Generic;
using BoardG.Enums;

namespace BoardG
{
    abstract class Piece
    {
        public Position position { get; set; }
        public PieceColor Color { get; protected set; }
        public int NumberofMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(PieceColor color, Board board)
        {
            this.position = null;
            Color = color;
            this.board = board;
            NumberofMovements = 0;

        }
        public void AddNumberOfMovements()
        {
            NumberofMovements++;
        }
        public bool AnyPossibleMovements()
        {
            bool[,] mat = PossibleMovements(position);
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool AllowedMove(Position position)
        {
            Piece piece = board.piece(position);
            return piece == null || piece.Color != Color;
        }
        public bool PossibleMove(Position origin, Position final)
        {
            //Oddly redundant
            return PossibleMovements(origin)[final.Row, final.Column]; //check if final is into checked feasibilities
        }
        public abstract bool[,] PossibleMovements(Position position);
   
    }
}
