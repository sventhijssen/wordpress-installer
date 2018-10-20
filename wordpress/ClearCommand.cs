using System;

namespace wordpress
{
    class ClearCommand : ICommand
    {
        public bool Execute()
        {
            Console.Clear();
            return false;
        }
    }
}