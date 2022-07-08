using System;
using BoardG;

namespace ChessGame
{
    internal class ChessPosition
    {
        public int Row { get; set; }
        public char Column { get; set; }

        public ChessPosition(char column, int row)
        {
            Row = row;
            Column = column;
        }

        public Position ChesstoMatrix()
        {
            return new Position(8 - Row, Column - 'a');
        }
        public override string ToString()
        {
            return $"{Column}, {Row}";
        }
    }
}
