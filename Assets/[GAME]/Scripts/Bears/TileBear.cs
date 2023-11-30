using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Models;
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
        public CubeBear currentCube;

        #endregion


        #region MonoBehaviour Methods

        private void Start()
        {
            if (!isFilled)
            {
                return;
            }

            Roar(CustomEvents.AddTileToCubeGenerator, this);
        }

        #endregion

        #region Public Methods

        public void GenerateCubeOnTile(ColorData color)
        {
            CubeBear cube = PoolManager.Instance.GetCube();
            Transform cubeTransform = cube.transform;
            cubeTransform.SetParent(cubeParent);
            cubeTransform.localPosition = Vector3.zero;

            cube.InitCube(color);
            
            currentCube = cube;
        }

        #endregion
    }
}