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

                while (!match.Fininshed)
                {
                    try
                    {
                        //Console.Clear();
                        Screen.ShowMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ChesstoMatrix();
                        match.ValidateOrigin(origin);

                        bool[,] possiblemovements = match.board.piece(origin).PossibleMovements(origin);

                        //Console.Clear();
                        Screen.ShowBoard(match.board, possiblemovements);

                        Console.Write("Final position: ");
                        Position final = Screen.ReadChessPosition().ChesstoMatrix();
                        match.ValidateFinalPosition(origin, final);
                        Console.WriteLine();
                        match.MakePlay(origin, final);

                    }
                    catch (ExceptionBoard e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.ShowMatch(match);



            }
            catch (ExceptionBoard e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}