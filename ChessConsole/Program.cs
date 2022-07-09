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
                        Console.Clear();
                        Screen.ShowBoardBegin(match.board);
                        Console.WriteLine();
                        Console.WriteLine($"Shift: {match.Shift}");
                        Console.WriteLine($"Awayting for move : {match.PresentPlayer}");

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ChesstoMatrix();
                        match.ValidateOrigin(origin);

                        bool[,] possiblemovements = match.board.piece(origin).PossibleMovements(origin);

                        Console.Clear();
                        Screen.ShowBoard(match.board, possiblemovements);

                        Console.Write("Final position: ");
                        Position final = Screen.ReadChessPosition().ChesstoMatrix();
                        match.ValidateFinalPosition(origin, final);

                        match.MakePlay(origin, final);
                    }
                    catch (ExceptionBoard e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

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