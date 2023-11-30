using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class CubeCollisionBear : Bear
    {
        #region Serialized Fields

        [Header("Configurations")]
        [SerializeField] private float rayLength = 1f;

        [SerializeField] private LayerMask layerMask;

        #endregion

        #region Private Variables

        private Transform _cubeTransform;
        private CubeBear _cubeBear;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _cubeTransform = transform;
            _cubeBear = GetComponent<CubeBear>();
        }

        private void Update()
        {
            Vector3 forward = _cubeTransform.forward;
            Vector3 right = _cubeTransform.right;
            Vector3[] directions =
            {
                forward,
                -forward,
                right,
                -right
            };
            
            foreach (Vector3 direction in directions)
            {
                Debug.DrawRay(transform.position + Vector3.up * .5f, direction * rayLength, Color.red);
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.CheckSides, CheckSides);
            }

            else
            {
                Unregister(CustomEvents.CheckSides, CheckSides);   
            }
        }

        private void CheckSides(object[] arguments)
        {
            // if (!AllSideClosed())
            // {
            //     _cubeBear.Up();
            //     return;
            // }
            //
            // _cubeBear.Down();
        }

        #endregion

        #region Private Methods

        private bool AllSideClosed()
        {
            Vector3 forward = _cubeTransform.forward;
            Vector3 right = _cubeTransform.right;
            Vector3[] directions =
            {
                forward,
                -forward,
                right,
                -right
            };

            foreach (Vector3 direction in directions)
            {
                Debug.DrawRay(transform.position + Vector3.up * .5f, direction * rayLength, Color.red);
                if (!Physics.Raycast(_cubeTransform.position + Vector3.up, direction, out RaycastHit hit, rayLength, layerMask))
                {
                    return false;
                }
                OBDebug.Log(hit.collider.name + " " + _cubeTransform.localPosition);
            }
            return true;
        }

        #endregion
    }
}