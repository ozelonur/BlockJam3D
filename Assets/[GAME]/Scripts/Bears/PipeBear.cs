using System.Collections.Generic;
using _GAME_.Scripts.Models;
using OrangeBear.EventSystem;

namespace OrangeBear.Bears
{
    public class PipeBear : Bear
    {
        #region Public Variables

        public List<ColorData> colorData;

        #endregion

        #region Private Variables

        private TileBear _linkedTile;

        #endregion
    }
}