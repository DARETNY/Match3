using System;
using System.Collections.Generic;

namespace Game.Board
{
    public static class MatchUtils
    {
        public static List<Tuple<int, int>> FindMatches(this Board board)
        {
            List<Tuple<int, int>> matches = new();

            // Check for horizontal matches
            board.FindHorizontalMatches(matches);
            
            // Check for vertical matches
            board.FindVerticalMatches(matches);
            
            // Check for quad matches
            board.FindQuadMatches(matches);
            
            return matches;
        }

        private static void FindHorizontalMatches(this Board board, List<Tuple<int, int>> matches)
        {
            for (int y = 0; y < board.Height; y++)
            {
                var sequentialMatches = new List<Tuple<int, int>> { new(0, y) };
                for (int x = 1; x < board.Width; x++)
                {
                    if (board.Grid[x, y].CellType == 0) continue;
                    
                    if (board.Grid[x, y].CellType == board.Grid[x - 1, y].CellType)
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
        }

        private static void FindVerticalMatches(this Board board, List<Tuple<int, int>> matches)
        {
            for (int x = 0; x < board.Width; x++)
            {
                var sequentialMatches = new List<Tuple<int, int>> { new(x, 0) };
                for (int y = 1; y < board.Height; y++)
                {
                    if (board.Grid[x, y].CellType == 0) continue;
                    
                    if (board.Grid[x, y].CellType == board.Grid[x, y - 1].CellType)
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
        }
        
        public static void FindQuadMatches(this Board board, List<Tuple<int, int>> matches)
        {
            for (int x = 0; x < board.Width - 1; x++)
            {
                //var sequentialMatches = new List<Tuple<int, int>> { new(x, 0) };
                for (int y = 0; y < board.Height - 1; y++)
                {
                    var cellType = board.Grid[x, y].CellType;
                    if (cellType == 0) continue;

                    if (cellType == board.Grid[x + 1, y].CellType &&
                        cellType == board.Grid[x, y + 1].CellType &&
                        cellType == board.Grid[x + 1, y + 1].CellType)
                    {
                        var sequentialMatches = new List<Tuple<int, int>>
                        {
                            new( x, y),
                            new( x + 1, y),
                            new( x, y + 1),
                            new( x + 1, y + 1),
                        };
                        
                        matches.AddRange(sequentialMatches);
                    }
                }
            }
        }
    }
}