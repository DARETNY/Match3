using System;
using UnityEngine;

namespace Game
{
    public static class BoardUtils
    {
        public static bool HasCell(this Tuple<int, int> index, Board board)
        {
            if (board.Width <= index.Item1 || board.Height <= index.Item2) return false;
            return true;
        }

        public static void Swap(this Board board, 
            Tuple<int, int> startIndex, 
            Tuple<int, int> endIndex)
        {
            Debug.Log(board);
            
            // Swap cell
            (board.Grid[startIndex.Item1, startIndex.Item2], board.Grid[endIndex.Item1, endIndex.Item2]) = 
                (board.Grid[endIndex.Item1, endIndex.Item2], board.Grid[startIndex.Item1, startIndex.Item2]);

            // Swap X and Y values
            var item1 = board.Grid[startIndex.Item1, startIndex.Item2];
            var item2 = board.Grid[endIndex.Item1, endIndex.Item2];

            ((item1.X, item2.X), (item1.Y, item2.Y)) = 
                ((item2.X, item1.X), (item2.Y, item1.Y));
        }
        
        public static void Swap(this SpriteRenderer[,] views, 
            Tuple<int, int> startIndex, 
            Tuple<int, int> endIndex)
        {
            // Swap cell
            (views[startIndex.Item1, startIndex.Item2], views[endIndex.Item1, endIndex.Item2]) = 
                (views[endIndex.Item1, endIndex.Item2], views[startIndex.Item1, startIndex.Item2]);
        }
    }
}
