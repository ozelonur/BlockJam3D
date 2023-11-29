using _GAME_.Scripts.Entities;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using OrangeBear.Core;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class BoardManager : Manager<BoardManager>
    {
        #region Serialized Fields

        [Header("Config")] [SerializeField] private int initialBoardCount;

        #endregion

        #region Properties

        public BoardData boardData { get; private set; }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            boardData = BoardData.Get();

            if (boardData == null)
            {
                boardData = new();

                bool isSuccessful = boardData.Register();

                if (!isSuccessful)
                {
                    OBDebug.LogError("BoardData could not be registered!");
                }
            }

            boardData.Load();

            if (boardData.BoardCount == 0)
            {
                IncreaseBoardCount(initialBoardCount);
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
            }

            else
            {
                Unregister(GameEvents.InitLevel, InitLevel);
            }
        }

        private void InitLevel(object[] arguments)
        {
            Roar(CustomEvents.SetBoardCount, GetBoardCount());
        }

        #endregion

        #region Private Methods

        private int GetBoardCount()
        {
            return boardData.BoardCount;
        }

        #endregion

        #region Public Methods

        public void IncreaseBoardCount(int count)
        {
            boardData.IncreaseBoardCount(count);
        }

        #endregion
    }
}