using System;
using System.Collections.Generic;
using UnityEngine;

namespace Board
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

        public static List<Tuple<int, int>> FindMatches(this Board board)
        {
            List<Tuple<int, int>> matches = new();

            // Check for horizontal matches
            for (int y = 0; y < board.Height; y++)
            {
                var sequentialMatches = new List<Tuple<int, int>> { new(0, y) };
                for (int x = 1; x < board.Width; x++)
                {
                    if (board.Grid[x, y].CellType != 0 && board.Grid[x, y].CellType == board.Grid[x - 1, y].CellType)
                    {
                        sequentialMatches.Add(new(x, y));
                    }
                    else
                    {
                        if (sequentialMatches.Count >= 3)
                        {
                            foreach (var match in sequentialMatches)
                            {
                                matches.Add(match);
                            }
                        }

                        // Clear list
                        sequentialMatches = new List<Tuple<int, int>> { new(x, y) };
                    }
                }

                if (sequentialMatches.Count >= 3)
                {
                    foreach (var match in sequentialMatches)
                    {
                        matches.Add(match);
                    }
                }
            }

            // Check for vertical matches
            for (int x = 0; x < board.Width; x++)
            {
                var sequentialMatches = new List<Tuple<int, int>> { new(x, 0) };
                for (int y = 1; y < board.Height; y++)
                {
                    if (board.Grid[x, y].CellType != 0 && board.Grid[x, y].CellType == board.Grid[x, y - 1].CellType)
                    {
                        sequentialMatches.Add(new(x, y));
                    }
                    else
                    {
                        if (sequentialMatches.Count >= 3)
                        {
                            matches.AddRange(sequentialMatches);
                        }

                        // Clear list
                        sequentialMatches = new List<Tuple<int, int>> { new(x, y) };
                    }
                }

                if (sequentialMatches.Count >= 3)
                {
                    matches.AddRange(sequentialMatches);
                }
            }

            return matches;
        }


        
    }
}