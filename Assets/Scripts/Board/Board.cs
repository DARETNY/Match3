namespace Game
{
    public class Board
    {
        public readonly Cell[,] Grid;


        public Board(int width, int height, CellType[,] gridTypes, float cellsize)
        {
            Grid = new Cell[width,height];

        
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x,y] = new Cell(x * cellsize, -y * cellsize, gridTypes[x, y]);
               
                }
            }
        
        }
    
    
    
    }   
}
