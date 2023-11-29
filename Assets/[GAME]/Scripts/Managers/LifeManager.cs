using _GAME_.Scripts.Entities;
using _GAME_.Scripts.Extensions;
using OrangeBear.Core;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class LifeManager : Manager<LifeManager>
    {
        #region Serialized Fields

        [Header("Config")] [SerializeField] private int initialLifeCount;

        #endregion

        #region Properties

        public LifeData lifeData { get; private set; }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            lifeData = LifeData.Get();

            if (lifeData == null)
            {
                lifeData = new();

                bool isSuccessful = lifeData.Register();

                if (!isSuccessful)
                {
                    OBDebug.LogError("LifeData could not be registered!");
                }
            }

            lifeData.Load();
            
            if (lifeData.Life == 0)
            {
                AddLife(initialLifeCount);
            }
        }

        #endregion

        #region Public Methods

        public void AddLife(int amount)
        {
            lifeData.AddLife(amount);
        }
        
        public void RemoveLife(int amount)
        {
            lifeData.RemoveLife(amount);
        }

        #endregion
    }
}