using OrangeBear.Core;
using TMPro;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private TMP_Text levelText;

        #endregion

        #region Inherit Methods

        protected override void GetLevelNumber(object[] arguments)
        {
            int levelIndex = (int) arguments[0];
            levelText.text = $"Level {levelIndex}";
        }

        #endregion
    }
}