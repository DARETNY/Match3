namespace Game.Cells
{
    public static class CellSettings
    {
        public const int MIN_BASIC_CELL_ID = 1;
        public const int MAX_BASIC_CELL_ID = 5;
        
        public const int MIN_SPECIAL_CELL_ID = 100;
        public const int MAX_SPECIAL_CELL_ID = 100;
    }
    
    public enum CellType
    {
        None = 0,
        Yellow = CellSettings.MIN_BASIC_CELL_ID,
        Red = CellSettings.MIN_BASIC_CELL_ID + 1,
        Blue = CellSettings.MIN_BASIC_CELL_ID + 2,
        White = CellSettings.MIN_BASIC_CELL_ID + 3,
        Green = CellSettings.MAX_BASIC_CELL_ID,
        
        // Special
        SquareMatchBomb = CellSettings.MIN_SPECIAL_CELL_ID,
        FiveMatchBomb = CellSettings.MIN_SPECIAL_CELL_ID + 1,
        PowerXMatchBomb = CellSettings.MAX_SPECIAL_CELL_ID,
    }
}
