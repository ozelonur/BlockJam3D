using _GAME_.Scripts.Enums;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class CubeBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Renderer meshRenderer;

        [SerializeField] private Color redColor;
        [SerializeField] private Color greenColor;
        [SerializeField] private Color blueColor;
        [SerializeField] private Color yellowColor;

        #endregion

        #region Private Variables

        private Transform _exitWay;

        #endregion

        #region Public Methods

        public void InitCube()
        {
            CubeColor color = (CubeColor)Random.Range(0, 4);

            Material material = meshRenderer.material;
            material.color = color switch
            {
                CubeColor.Red => redColor,
                CubeColor.Green => greenColor,
                CubeColor.Blue => blueColor,
                CubeColor.Yellow => yellowColor,
                _ => material.color
            };

            _exitWay = transform.root.GetComponent<GameLevelBear>().exitWay;
        }

        #endregion

        #region MonoBehaviour Methods

        private void OnMouseDown()
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y, _exitWay.position.z);
            transform.DOMove(target, 0.5f);
        }

        #endregion
    }
}