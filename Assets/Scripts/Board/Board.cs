using Game.Cells;

namespace Game.Board
{
    public class Board
    {
        public readonly Cell[,] Grid;
        public readonly int Width;
        public readonly int Height;


        public Board(int width, int height, CellType[,] gridTypes, float cellSize)
        {
            Width = width;
            Height = height;
            Grid = new Cell[width,height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x,y] = new Cell(x * cellSize, -y * cellSize, gridTypes[x, y]);
               
                }
            }
        
        }

        public override string ToString()
        {
            string msg = string.Empty;
            
            msg += "***************\n";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    msg += $"({x}, {y} {Grid[x, y].CellType})";
                }
                msg += "\n---";

                msg += "\n";
            }

            return msg;
        }
    }   
}
