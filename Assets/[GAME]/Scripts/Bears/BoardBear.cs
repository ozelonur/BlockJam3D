using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;

namespace OrangeBear.Bears
{
    public class BoardBear : Bear
    {
        #region Public Variables

        public CubeBear currentCube;

        #endregion

        #region Public Variables

        public void InitBoard()
        {
            Roar(CustomEvents.AddBoardToBoardController, this);
        }

        #endregion
    }
}