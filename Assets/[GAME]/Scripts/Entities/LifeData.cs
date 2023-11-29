using OrangeBear.Entity;

namespace _GAME_.Scripts.Entities
{
    public class LifeData : Entity<LifeData>
    {
        #region Public Variables

        public int Life;

        #endregion

        #region Inherit Methods

        protected override bool Init() => true;

        #endregion

        #region Public Methods

        public void AddLife(int amount)
        {
            Life += amount;
            Save();
        }
        
        public void RemoveLife(int amount)
        {
            Life -= amount;
            Save();
        }

        #endregion
    }
}