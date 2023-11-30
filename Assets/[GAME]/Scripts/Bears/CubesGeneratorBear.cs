using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class CubesGeneratorBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private List<ColorData> cubeColors;

        #endregion

        #region Private Variables

        private Dictionary<CubeColor, int> cubeColorCounts = new();
        private List<TileBear> _tiles;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.AddTileToCubeGenerator, AddTile);
            }

            else
            {
                Unregister(GameEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.AddTileToCubeGenerator, AddTile);
            }
        }

        private void AddTile(object[] arguments)
        {
            _tiles ??= new List<TileBear>();

            TileBear tile = (TileBear)arguments[0];

            _tiles.Add(tile);
        }

        private void OnGameStart(object[] arguments)
        {
            foreach (ColorData color in cubeColors)
            {
                cubeColorCounts[color.color] = 0;
            }
            
            GenerateCubes();
        }

        #endregion

        #region Private Methods

        private void GenerateCubes()
        {
            foreach (ColorData color in cubeColors)
            {
                while (cubeColorCounts[color.color] < 3)
                {
                    TileBear tile = GetRandomTile();

                    if (tile != null)
                    {
                        tile.GenerateCubeOnTile(color);
                        cubeColorCounts[color.color]++;
                    }
                    
                    else
                    {
                        break;
                    }
                }
            }
        }

        private TileBear GetRandomTile()
        {
            List<TileBear> emptyTiles = _tiles.Where(tile => tile.currentCube == null).ToList();
            
            return emptyTiles.Count == 0 ? null : emptyTiles[Random.Range(0, emptyTiles.Count)];
        }

        #endregion
    }
}