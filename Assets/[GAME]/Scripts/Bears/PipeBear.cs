using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
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

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.TileIsEmptyNowAlert, TileIsEmptyNowAlert);
            }

            else
            {
                Unregister(CustomEvents.TileIsEmptyNowAlert, TileIsEmptyNowAlert);
            }
        }

        private void TileIsEmptyNowAlert(object[] arguments)
        {
            TileBear tile = (TileBear)arguments[0];

            if (tile != linkedTile)
            {
                return;
            }

            if (colorData.Count == 0)
            {
                return;
            }
            
            ColorData color = colorData[0];
            linkedTile.GenerateCubeOnTileFromPipe(color);

            colorData.RemoveAt(0);
        }

        #endregion

        #region Public Methods

        public void AddColor(ColorData color)
        {
            colorData.Add(color);
        }

        #endregion
    }
}