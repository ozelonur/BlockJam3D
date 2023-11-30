using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Extensions;
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

        private List<ColorData> _generatedColors;

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
            int maxSetsForAnyColor = _tiles.Count / cubeColors.Count;
            int maxTotalSets = maxSetsForAnyColor * cubeColors.Count;

            foreach (ColorData colorData in cubeColors)
            {
                cubeColorCounts[colorData.color] = 0;
            }

            for (int i = 0; i < maxTotalSets; i++)
            {
                foreach (ColorData color in cubeColors.Where(color =>
                             cubeColorCounts[color.color] < maxSetsForAnyColor * 3))
                {
                    CreateSetOfThreeCubes(color);
                }
            }

            int remainingTiles = _tiles.Count - (maxTotalSets * 3);

            while (remainingTiles > 0)
            {
                ColorData additionalColor = cubeColors[Random.Range(0, cubeColors.Count)];

                if (cubeColorCounts[additionalColor.color] >= 3) continue;

                CreateSetOfThreeCubes(additionalColor);
                remainingTiles -= 3;
            }
            
            GenerateCubesFromList();
        }

        private void CreateSetOfThreeCubes(ColorData color)
        {
            _generatedColors ??= new List<ColorData>();

            int createdCubes = 0;
            while (createdCubes < 3)
            {
                TileBear tile = GetRandomTile();
                if (tile != null && cubeColorCounts[color.color] < (_tiles.Count / cubeColors.Count) * 3)
                {
                    // tile.GenerateCubeOnTile(color);
                    _generatedColors.Add(color);
                    cubeColorCounts[color.color]++;
                    createdCubes++;
                }
                else
                {
                    break;
                }
            }
        }

        private void GenerateCubesFromList()
        {
            foreach (ColorData color in _generatedColors)
            {
                TileBear tile = GetRandomTile();
                if (tile != null)
                {
                    tile.GenerateCubeOnTile(color);
                }
            }
        }

        private TileBear GetRandomTile()
        {
            List<TileBear> emptyTiles = _tiles.Where(tile => tile.currentCube == null).ToList();

            OBDebug.Log("Empty tiles count: " + emptyTiles.Count);

            return emptyTiles.Count == 0 ? null : emptyTiles[Random.Range(0, emptyTiles.Count)];
        }

        #endregion
    }
}