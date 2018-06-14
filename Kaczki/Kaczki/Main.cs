namespace Kaczki
{
    #region Usings 
    using System.ComponentModel;
    #endregion
    public class Main
    {
        #region Fields
        private DuckHunt game;
        #endregion

        #region Public Properties
        public DuckHunt Game { get => game; set => game = value; }
        #endregion

        #region Constructors and Deconstructors
        public Main()
        {
            Game = new DuckHunt();
        }
        #endregion

        #region Public Methods and Operators
        public void StartApplication()
        {
            Game.Run();
        }
        #endregion
    }
}
