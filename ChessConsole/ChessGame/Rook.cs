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
            int inicialrow = position.Row;
            int inicialcolumn = position.Column;
            
            //Uppway
            if (position.Row > 0) {  
            position.SetPosition(position.Row - 1, position.Column);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        break;
                    }
                    position.Row -= 1;
                }
            }
            //Downwayway
            position.Row = inicialrow;
            if (position.Row < 8)
            {
                position.SetPosition(position.Row + 1, position.Column);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        break;
                    }
                    position.Row += 1;
                }

            }
            //Leftway
            position.Row = inicialrow;
            if (position.Column > 0)
            {
                position.SetPosition(position.Row, position.Column - 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Column -= 1;
                }
            }
            //Downwayway
            position.Column = inicialcolumn;
            if (position.Column < 8)
            {
                position.SetPosition(position.Row, position.Column + 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Column += 1;
                }
            }
            position.Column = inicialcolumn;
            return mat;
        }
    }
}
