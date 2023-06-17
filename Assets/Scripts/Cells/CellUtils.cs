using System;
using UnityEngine;

namespace Game.Cells
{
    public static class CellUtils
    {
        public static Color GetColor(this CellType cellType)
        {
            var color = Color.black;
            switch (cellType)
            {
                case CellType.None:
                    color = Color.black;
                    break;
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
                case CellType.SquareMatchBomb:
                    color = Color.cyan;
                    break;
                case CellType.FiveMatchBomb:
                    color = Color.magenta;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null);
            }

            return color;
        }
    }
}