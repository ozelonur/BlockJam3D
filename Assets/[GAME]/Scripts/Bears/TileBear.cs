using _GAME_.Scripts.Managers;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class TileBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Transform cubeParent;

        #endregion

        #region Public Variables

        public bool isFilled;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Unregister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] arguments)
        {
            if (!isFilled)
            {
                return;
            }

            CubeBear cube = PoolManager.Instance.GetCube();
            Transform cubeTransform = cube.transform;
            cubeTransform.SetParent(cubeParent);
            cubeTransform.localPosition = Vector3.zero;

            cube.InitCube();
        }

        #endregion
    }
}