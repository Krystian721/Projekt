using System;

namespace Kaczki
{
#if WINDOWS
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DuckHunt game = new DuckHunt())
            {
                game.Run();
            }
        }
    }
#endif
}

