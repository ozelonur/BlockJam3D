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
        [SerializeField] private Transform pipeParent;
        
        [Header("Prefab")] [SerializeField] private BoardBear boardPrefab;
        [SerializeField] private CubeBear cubePrefab;
        [SerializeField] private PipeBear pipePrefab;

        #endregion

        #region Public Variables

        public CustomObjectPool<BoardBear> boardPool;
        public CustomObjectPool<CubeBear> cubePool;
        public CustomObjectPool<PipeBear> pipePool;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            boardPool = new CustomObjectPool<BoardBear>(boardPrefab, 10, boardsParent);
            cubePool = new CustomObjectPool<CubeBear>(cubePrefab, 10, cubeParent);
            pipePool = new CustomObjectPool<PipeBear>(pipePrefab, 10, pipeParent);
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
        
        public PipeBear GetPipe()
        {
            return pipePool.Get();
        }

        public void ReturnBoard(BoardBear board)
        {
            boardPool.Release(board);
        }
        
        public void ReturnCube(CubeBear cube)
        {
            cubePool.Release(cube);
        }
        
        public void ReturnPipe(PipeBear pipe)
        {
            pipePool.Release(pipe);
        }

        #endregion
    }
}