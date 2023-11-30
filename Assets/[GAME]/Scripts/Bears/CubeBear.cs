using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Models;
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

        #endregion

        #region Private Variables

        private Transform _exitWay;
        private Transform _cubeTransform;

        private bool _onTheWay;

        #endregion

        #region Public Variables

        public CubeColor currentColor;
        public TileBear currentTile;

        #endregion

        #region Public Methods

        public void InitCube(ColorData color, bool isFromPipe = false)
        {
            _onTheWay = false;
            currentColor = color.color;

            Material material = meshRenderer.material;
            
            material.color = color.colorValue;

            if (!isFromPipe)
            {
                _cubeTransform.localScale = Vector3.one;
            }

            Transform root = transform.root;
            _exitWay = root.GetComponent<GameLevelBear>().exitWay;
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _cubeTransform = transform;
        }

        private void OnMouseDown()
        {
            if (_onTheWay)
            {
                return;
            }

            _onTheWay = true;
            
            Roar(CustomEvents.MoveCubeToTheBoard, this);
        }

        #endregion

        #region Public Methods

        public void Explode()
        {
            _cubeTransform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack).SetLink(gameObject).OnComplete(() =>
            {
                _cubeTransform.DOKill();
                PoolManager.Instance.ReturnCube(this);
            });
        }

        public void Move(Vector3 target)
        {
            Vector3 position = _cubeTransform.position;
            Vector3 targetPosition = new Vector3(position.x, position.y, _exitWay.position.z);
            
            currentTile.currentCube = null;
            
            Roar(CustomEvents.TileIsEmptyNowAlert, currentTile);
            
            currentTile = null;
            
            transform.DOMove(targetPosition, 0.5f).OnComplete(() =>
            {
                transform.DOMove(target, 0.5f).OnComplete(() =>
                {
                    Roar(CustomEvents.CheckBoard);
                });
            });
        }
        
        public void Shift(Vector3 target)
        {
            transform.DOMove(target, 0.5f);
        }
        
        public void Reorder(Vector3 target)
        {
            transform.DOMove(target, 0.5f);
        }

        #endregion
    }
}