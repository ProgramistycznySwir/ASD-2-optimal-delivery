using System;
using System.Collections.Generic;


// Ten algorytm ma pesymistyczną złożoność czasową O(n)=n*lg(n)
//  bo musi przeiterować całą tablicę lg(n) razy (tak samo jak iteracyjny).
namespace ASD___2
{
    public static class Recursion
    {
        public static Tuple<int, long> Solution(List<Station> stations)
        {
            //stations.Sort((a, b) => a.dist.CompareTo(b.dist));
            stations = Sorting.CountSort(stations, 10_000);

            int half = (int)Math.Floor(stations.Count / 2d);
            return Step(stations, 0, half, half, Int64.MaxValue);
        }

        /// <summary>
        /// Recursion step.
        /// </summary>
        static Tuple<int, long> Step(List<Station> stations, int lastIndex, int indexToCheck, int half, long best)
        {
            long[] data = stations[indexToCheck].DistanceTo(stations);

            if (best < data[1])
                return new Tuple<int, long>(stations[lastIndex].num, best);

            half = (int)Math.Floor(half / 2d);
            if (data[0] > 0)
                return Step(stations, indexToCheck, indexToCheck + half, half, data[1]);
            return Step(stations, indexToCheck, indexToCheck - half, half, data[1]);
        }

    }
}
