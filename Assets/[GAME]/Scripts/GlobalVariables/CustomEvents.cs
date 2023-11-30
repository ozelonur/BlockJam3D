namespace _GAME_.Scripts.GlobalVariables
{
    /// <summary>
    /// This class is used to store custom events.
    /// </summary>
    public static class CustomEvents
    {
        public const string SetBoardCount = nameof(SetBoardCount);
        
        public const string AddBoardToBoardController = nameof(AddBoardToBoardController);
        public const string AddTileToCubeGenerator = nameof(AddTileToCubeGenerator);
        
        public const string CheckBoard = nameof(CheckBoard);
        public const string CheckSides = nameof(CheckSides);
        
        public const string MoveCubeToTheBoard = nameof(MoveCubeToTheBoard);
        
        public const string TileIsEmptyNowAlert = nameof(TileIsEmptyNowAlert);
    }
}