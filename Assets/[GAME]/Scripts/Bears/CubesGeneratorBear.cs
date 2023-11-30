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
            int count = _tiles.Sum(tile => tile.containsPipe ? tile.cubeCountInPipe : 1);
            OBDebug.Log("Count: " + count);

            int totalSetsOfThree = count / 3;

            int setsPerColor = totalSetsOfThree / cubeColors.Count;

            int additionalSets = totalSetsOfThree % cubeColors.Count;

            _generatedColors = new List<ColorData>();
            cubeColorCounts.Clear();

            foreach (ColorData color in cubeColors)
            {
                for (int i = 0; i < setsPerColor; i++)
                {
                    AddSetOfThreeCubes(color);
                }
            }

            for (int i = 0; i < additionalSets; i++)
            {
                ColorData additionalColor = cubeColors[Random.Range(0, cubeColors.Count)];
                AddSetOfThreeCubes(additionalColor);
            }

            int remainingCubes = count % 3;
            while (remainingCubes > 0)
            {
                ColorData randomColor = cubeColors[Random.Range(0, cubeColors.Count)];
                _generatedColors.Add(randomColor);
                if (cubeColorCounts.ContainsKey(randomColor.color))
                {
                    cubeColorCounts[randomColor.color]++;
                }
                else
                {
                    cubeColorCounts[randomColor.color] = 1;
                }

                remainingCubes--;
            }

            GenerateCubesOnTiles();
        }

        private void AddSetOfThreeCubes(ColorData color)
        {
            for (int i = 0; i < 3; i++)
            {
                _generatedColors.Add(color);
                if (cubeColorCounts.ContainsKey(color.color))
                {
                    cubeColorCounts[color.color]++;
                }
                else
                {
                    cubeColorCounts[color.color] = 1;
                }
            }
        }

        private void GenerateCubesOnTiles()
        {
            foreach (TileBear tile in _tiles)
            {
                switch (tile.containsPipe)
                {
                    case false when _generatedColors.Count > 0:
                    {
                        int colorIndex = Random.Range(0, _generatedColors.Count);
                        tile.GenerateCubeOnTile(_generatedColors[colorIndex]);
                        _generatedColors.RemoveAt(colorIndex);
                        break;
                    }
                    case true:
                    {
                        for (int i = 0; i < tile.cubeCountInPipe && _generatedColors.Count > 0; i++)
                        {
                            int colorIndex = Random.Range(0, _generatedColors.Count);
                            tile.pipeBear.AddColor(_generatedColors[colorIndex]);
                            _generatedColors.RemoveAt(colorIndex);
                        }

                        break;
                    }
                }
            }

            Roar(CustomEvents.CheckSides);
        }

        #endregion

        #region Public Methods

        public bool CheckAreTilesEmpty()
        {
            foreach (TileBear tile in _tiles)
            {
                if (tile.containsPipe)
                {
                    if (tile.pipeBear.colorData.Count > 0)
                    {
                        return false;
                    }
                }
                
                else
                {
                    if (tile.currentCube != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}