using System.Collections.Generic;
using _GAME_.Scripts.Models;
using OrangeBear.EventSystem;

namespace OrangeBear.Bears
{
    public class PipeBear : Bear
    {
        #region Public Variables

        public List<ColorData> colorData;

        public TileBear linkedTile;

        #endregion

        #region Public Methods

        public void AddColor(ColorData color)
        {
            colorData.Add(color);
        }

        #endregion
    }
}