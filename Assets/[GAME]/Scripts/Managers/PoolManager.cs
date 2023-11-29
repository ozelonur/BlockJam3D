using OrangeBear.Bears;
using OrangeBear.Core;
using OrangeBear.Utilities;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class PoolManager : Manager<PoolManager>
    {
        #region Serialized Fields

        [Header("Parents")] [SerializeField] private Transform boardsParent;
        [SerializeField] private Transform cubeParent;
        
        [Header("Prefab")] [SerializeField] private BoardBear boardPrefab;
        [SerializeField] private CubeBear cubePrefab;

        #endregion

        #region Public Variables

        public CustomObjectPool<BoardBear> boardPool;
        public CustomObjectPool<CubeBear> cubePool;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            boardPool = new CustomObjectPool<BoardBear>(boardPrefab, 10, boardsParent);
            cubePool = new CustomObjectPool<CubeBear>(cubePrefab, 10, cubeParent);
        }

        #endregion

        #region Public Methods

        public BoardBear GetBoard()
        {
            return boardPool.Get();
        }
        
        public CubeBear GetCube()
        {
            return cubePool.Get();
        }

        public void ReturnBoard(BoardBear board)
        {
            boardPool.Release(board);
        }
        
        public void ReturnCube(CubeBear cube)
        {
            cubePool.Release(cube);
        }

        #endregion
    }
}