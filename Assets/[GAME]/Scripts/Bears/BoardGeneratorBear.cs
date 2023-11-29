using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class BoardGeneratorBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform boardsParent;

        #endregion

        #region Private Variables

        private PoolManager _poolManager;

        private int _boardCount;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            _poolManager = PoolManager.Instance;
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.SetBoardCount, GetBoardCount);
            }

            else
            {
                Unregister(CustomEvents.SetBoardCount, GetBoardCount);
            }
        }

        private void GetBoardCount(object[] arguments)
        {
            _boardCount = (int)arguments[0];
            GenerateBoards();
        }

        #endregion

        #region Private Methods

        private void GenerateBoards()
        {
            const float spacing = 2.25f;
            float startX = -(_boardCount - 1) * spacing / 2;

            for (int i = 0; i < _boardCount; i++)
            {
                BoardBear board = _poolManager.GetBoard();
                Transform boardTransform;
                (boardTransform = board.transform).SetParent(boardsParent);
                
                float xPos = startX + i * spacing;
                boardTransform.localPosition = new Vector3(xPos, 0, 0);

                board.InitBoard();
            }
        }


        #endregion
    }
}