using UnityEngine;

namespace Game.Cells
{
    public class Cell
    {
        public CellType CellType;
        public float X, Y;

        public Cell(float x, float y, CellType cellType)
        {
            this.X = x;
            this.Y = y;
            this.CellType = cellType;
        }

        public Vector3 Pos()
        {
            return new Vector3(X, Y);
        }

        public override string ToString()
        {
            return Pos().ToString();
        }
    }
}

