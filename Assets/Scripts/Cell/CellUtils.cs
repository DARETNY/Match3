using UnityEngine;

namespace Game
{
    public static class CellUtils
    {
        public static Color GetColor(this CellType cellType)
        {
            var color = Color.black;
            switch (cellType)
            {
                case CellType.Yellow:
                    color = Color.yellow;
                    break;
                case CellType.Red:
                    color = Color.red;
                    break;
                case CellType.Blue:
                    color = Color.blue;
                    break;
                case CellType.White:
                    color = Color.white;
                    break;
                case CellType.Green:
                    color = Color.green;
                    break;
            }

            return color;
        }
    }
}