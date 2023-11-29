using OrangeBear.Bears;
using OrangeBear.Core;
using OrangeBear.Utilities;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class PoolManager : Manager<PoolManager>
    {
        #region Serialized Fields

        [Header("Prefabs")] [SerializeField] private Transform boardsParent;
        [Header("Parents")] [SerializeField] private BoardBear boardPrefab;

        #endregion

        #region Public Variables

        public CustomObjectPool<BoardBear> boardPool;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            boardPool = new CustomObjectPool<BoardBear>(boardPrefab, 10, boardsParent);
        }

        #endregion

        #region Public Methods

        public BoardBear GetBoard()
        {
            return boardPool.Get();
        }

        public void ReturnBoard(BoardBear board)
        {
            boardPool.Release(board);
        }

        #endregion
    }
}