using System;
using System.Collections.Generic;

// Nie wiem, czy czegoś nie rozumiem czy co, ale wszystko wskazuje na to,
//  że pętla w tej funkcji wykonuje się tylko 2 razy.
// Tak samo jest w implementacji rekurencyjnej.
// W pętli tej funkcji nawet upewniłem się by wykonała się ona tylko 2 razy i to robi,
//  w czasie 2 iteracji wytwarza ten sam wynik co metoda bąbelkowa (raczej prawidłowy).
// OK. Winą tego stanu rzeczy była losowość danych, z racji że ich rozkład był liniowy wynik zawsze był
//  po środku, gdy zastosowałem dane pseudolosowe (wpisane ręcznie) algorytm musiał się wykazać.


// Ten algorytm ma pesymistyczną złożoność czasową O(n)=n*lg(n)
//  bo musi przeiterować całą tablicę lg(n) razy.
namespace ASD___2
{
    public static class Iteration
    {
        public static Tuple<int, long> Solution(List<Station> stations)
        {
            //stations.Sort((a, b) => a.dist.CompareTo(b.dist));
            stations = Sorting.CountSort(stations, 10_000);

            // Definiowane przed pętlą by program nie tracił na to czasu później.
            int half = (int)Math.Floor(stations.Count / 2d);
            int lastIndexToCheck = 0;
            int indexToCheck = half;
            long lastOptimalDistanceSum = Int64.MaxValue;
            long optimalDistanceSum;
            long[] data;

            while(true)
            {
                data = stations[indexToCheck].DistanceTo(stations);
                optimalDistanceSum = data[1];

                if (lastOptimalDistanceSum < optimalDistanceSum)
                    return new Tuple<int, long>(stations[lastIndexToCheck].num, lastOptimalDistanceSum);

                lastIndexToCheck = indexToCheck;
                lastOptimalDistanceSum = optimalDistanceSum;

                half = (int)Math.Floor(half / 2d);
                if (data[0] > 0)
                    indexToCheck += half;
                else
                    indexToCheck -= half;
            }
            throw new NotImplementedException("Code shouldn't reach this part :<");
        }
    }
}
