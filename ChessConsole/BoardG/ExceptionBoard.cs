using System;


namespace BoardG
{
    class ExceptionBoard : Exception
    {
        public ExceptionBoard(string msg) : base(msg)
        {
        }
    }
}
