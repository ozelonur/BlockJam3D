using OrangeBear.Entity;

namespace _GAME_.Scripts.Entities
{
    public class BoardData : Entity<BoardData>
    {
        #region Public Variables

        public int BoardCount;

        #endregion

        #region Inherit Methods

        protected override bool Init() => true;

        #endregion

        #region Public Methods

        public void IncreaseBoardCount(int count)
        {
            BoardCount += count;
            Save();
        }

        #endregion
    }
}