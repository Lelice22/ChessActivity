using System;
using System.Collections.Generic;
using BoardG;
using BoardG.Enums;

namespace ChessGame
{
    internal class Queen : Piece
    {
        public Queen(PieceColor color, Board board) : base(color, board)
        {
        }
        public override string ToString()
        {
            return "Q";
        }
        public override bool[,] PossibleMovements(Position position)
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            int inicialrow = position.Row;
            int inicialcolumn = position.Column;
            //Upprightway
            if (position.Row > 0 && position.Column < 7)
            {
                position.SetPosition(position.Row - 1, position.Column + 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Row--;
                    position.Column++;

                }
            }
            //Downrigthway
            position.Row = inicialrow;
            position.Column = inicialcolumn;
            if (position.Row < 7 && position.Column < 7)
            {
                position.SetPosition(position.Row + 1, position.Column + 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Row++;
                    position.Column++;
                }

            }
            //Uppleftway
            position.Row = inicialrow;
            position.Column = inicialcolumn;
            if (position.Row > 0 && position.Column > 0)
            {
                position.SetPosition(position.Row - 1, position.Column - 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Row--;
                    position.Column--;
                }
            }
            //Downleftway
            position.Row = inicialrow;
            position.Column = inicialcolumn;
            if (position.Row < 7 && position.Column > 0)
            {
                position.SetPosition(position.Row + 1, position.Column - 1);
                while (board.ValidPosition(position) && AllowedMove(position))
                {
                    mat[position.Row, position.Column] = true;
                    if (board.piece(position) != null && board.piece(position).Color != Color)
                    {
                        position.Row = inicialrow;
                        position.Column = inicialcolumn;
                        break;
                    }
                    position.Row++;
                    position.Column--;
                }
            }
            position.Row = inicialrow;
            position.Column = inicialcolumn;
            //Uppway
            if (position.Row > 0)
            {
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
            //Downway
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
            //Downway
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
