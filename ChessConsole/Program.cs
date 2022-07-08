using System;
using BoardG;
using BoardG.Enums;
using ChessGame;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                ChessMatch match = new ChessMatch();
                
                
                Screen.ShowBoard(match.board);

            }
            catch (ExceptionBoard e)
            {
                Console.WriteLine(e.Message);
            }
            /*
            ChessPosition position = new ChessPosition('a', 9);

            Console.WriteLine(position);
            Console.WriteLine(position.ChesstoMatrix());
            */
        }
    }
}