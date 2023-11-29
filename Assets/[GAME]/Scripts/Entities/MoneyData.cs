using OrangeBear.Entity;

namespace _GAME_.Scripts.Entities
{
    public class MoneyData : Entity<MoneyData>
    {
        #region Public Variables

        public int Money;

        #endregion

        #region Inherit Methods

        protected override bool Init() => true;

        #endregion

        #region Public Methods

        public void AddMoney(int amount)
        {
            Money += amount;
            Save();
        }
        
        public void SubtractMoney(int amount)
        {
            Money -= amount;
            Save();
        }

        #endregion
    }
}