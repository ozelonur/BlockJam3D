using _GAME_.Scripts.Entities;
using _GAME_.Scripts.Extensions;
using OrangeBear.Core;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class MoneyManager : Manager<MoneyManager>
    {
        #region Serialized Fields

        [Header("Config")] [SerializeField] private int initialMoneyCount;

        #endregion
        #region Properties

        public MoneyData moneyData { get; private set; }

        #endregion

        #region MonoBehaivour Methods

        private void Start()
        {
            moneyData = MoneyData.Get();

            if (moneyData == null)
            {
                moneyData = new();

                bool isSuccessful = moneyData.Register();

                if (!isSuccessful)
                {
                    OBDebug.LogError("MoneyData could not be registered!");
                }
            }

            moneyData.Load();
            
            if (moneyData.Money == 0)
            {
                AddMoney(initialMoneyCount);
            }
        }

        #endregion

        #region Public Methods

        public void AddMoney(int amount)
        {
            moneyData.AddMoney(amount);
        }
        
        public void SubtractMoney(int amount)
        {
            moneyData.SubtractMoney(amount);
        }

        #endregion
    }
}