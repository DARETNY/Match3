using System;

namespace Combos
{
    public static class ComboUtils
    {
        public static void QuadComboCheck(this Board.Board board, Tuple<int, int> firstİndex,Tuple<int,int> lastİndex)
        {
            
            // logic like that: (x_0,y_0)...(x_1,y_1)
            
            var x_0 = firstİndex.Item1;
            var x_1 = firstİndex.Item2;

            var y_0 = lastİndex.Item1;
            var y_1 = lastİndex.Item2;
            
            
            
        }
    }
}