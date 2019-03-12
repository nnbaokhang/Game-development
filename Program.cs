using System;
<<<<<<< HEAD:Lab4_pingpong/Program.cs
/*Credit to:
 * Guild 7
 * Khang Nguyen
 * Serena Perez 
 * Khuong Nguyen 
 * Dylan Pearson
 */
=======

>>>>>>> f89c1e157912c5cfe7b8def10e4ffa82ed2a65c0:Program.cs
namespace PingPong
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Put menu here and go to different state of the game
            using (var game = new Game1())
               game.Run();
        }
    }
#endif
}
