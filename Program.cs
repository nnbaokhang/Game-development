using System;
/*Credit to:
 * Guild 7
 * Khang Nguyen
 * 
 * Khuong Nguyen 
 * Serena Perez 
 * Dylan Pearson
 */


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
