using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class BoardControllerBear : Bear
    {
        #region Private Variables

        private List<BoardBear> _boards;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.AddBoardToBoardController, AddBoard);
                Register(CustomEvents.CheckBoard, CheckBoard);
                Register(CustomEvents.MoveCubeToTheBoard, MoveCubeToTheBoard);
            }

            else
            {
                Unregister(CustomEvents.AddBoardToBoardController, AddBoard);
                Unregister(CustomEvents.CheckBoard, CheckBoard);
                Unregister(CustomEvents.MoveCubeToTheBoard, MoveCubeToTheBoard);
            }
        }

        private void MoveCubeToTheBoard(object[] arguments)
        {
            CubeBear cube = (CubeBear)arguments[0];

            PlaceItemOnBoard(cube);
        }

        private void AddBoard(object[] arguments)
        {
            _boards ??= new List<BoardBear>();

            BoardBear board = (BoardBear)arguments[0];
            _boards.Add(board);
        }

        #endregion

        #region Private Methods

        private void PlaceItemOnBoard(CubeBear newItem)
        {
            int placementIndex = GetPlacementIndex(newItem);

            if (placementIndex != -1)
            {
                if (_boards[placementIndex].currentCube != null)
                {
                    // Shift the cubes starting from the index right after the matching cube
                    ShiftCubesRightFromIndex(placementIndex);
                    placementIndex++; // Update the placement index to the next slot
                }

                _boards[placementIndex].currentCube = newItem;
                newItem.Move(_boards[placementIndex].transform.position);
            }
        }

        private void ShiftCubesRightFromIndex(int startIndex)
        {
            if (startIndex < 0 || startIndex >= _boards.Count - 1)
            {
                return;
            }

            // Shift the cubes to the right starting from the index after the matching cube
            for (int i = _boards.Count - 1; i > startIndex + 1; i--)
            {
                _boards[i].currentCube = _boards[i - 1].currentCube;

                if (_boards[i].currentCube != null)
                {
                    _boards[i].currentCube.Shift(_boards[i].transform.position);
                }
            }
        }


        private int GetPlacementIndex(CubeBear newItem)
        {
            int foundIndex = -1;

            for (int i = 0; i < _boards.Count; i++)
            {
                if (_boards[i].currentCube != null &&
                    _boards[i].currentCube.currentColor == newItem.currentColor)
                {
                    return i;
                }
                else if (foundIndex == -1 && _boards[i].currentCube == null)
                {
                    foundIndex = i;
                }
            }

            return foundIndex;
        }

        private void CheckBoard(object[] arguments)
        {
            bool shouldReorder = false;
            int firstIndex = -1;

            for (int i = 0; i < _boards.Count - 2; i++)
            {
                if (_boards[i].currentCube == null || _boards[i + 1].currentCube == null ||
                    _boards[i + 2].currentCube == null)
                    continue;

                if (_boards[i].currentCube.currentColor == _boards[i + 1].currentCube.currentColor &&
                    _boards[i + 1].currentCube.currentColor == _boards[i + 2].currentCube.currentColor)
                {
                    _boards[i].currentCube.Explode();
                    _boards[i + 1].currentCube.Explode();
                    _boards[i + 2].currentCube.Explode();
                    
                    _boards[i].currentCube = null;
                    _boards[i + 1].currentCube = null;
                    _boards[i + 2].currentCube = null;

                    shouldReorder = true;
                    firstIndex = i;
                    break; // Remove this if you want to check for multiple sets of matching cubes
                }
            }

            if (shouldReorder)
            {
                StartCoroutine(ReorderCubes(firstIndex));
            }
        }

        private IEnumerator ReorderCubes(int firstIndex)
        {
            yield return new WaitForSeconds(.3f);
            
            for (int i = 0; i < _boards.Count; i++)
            {
                if (_boards[i].currentCube == null)
                {
                    continue;
                }
                
                int emptyBoardIndex = GetEmptyBoardIndex();
                
                if (emptyBoardIndex == -1)
                {
                    yield break;
                }

                if (emptyBoardIndex < firstIndex)
                {
                    continue;
                }

                yield return new WaitForSeconds(.05f);
                _boards[emptyBoardIndex].currentCube = _boards[i].currentCube;
                _boards[i].currentCube = null;
                _boards[emptyBoardIndex].currentCube.Reorder(_boards[emptyBoardIndex].transform.position);
            }
        }
        
        private int GetEmptyBoardIndex()
        {
            for (int i = 0; i < _boards.Count; i++)
            {
                if (_boards[i].currentCube == null)
                {
                    return i;
                }
            }

            return -1;
        }


        #endregion

        #region Public Methods

        public BoardBear GetEmptyBoard()
        {
            foreach (BoardBear board in _boards)
            {
                if (board.currentCube == null)
                {
                    return board;
                }
            }

            return null;
        }

        #endregion
    }
}