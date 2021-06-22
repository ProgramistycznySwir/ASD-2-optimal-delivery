using System;
using System.Collections.Generic;

// [IMPORTANT]: Bubble sort może czasami wyrzucać inną stację niż inne metody, ale obie są poprawne

// Metoda bąbelkowa ma złożoność czasową O(n) = n^2

namespace ASD___2
{
    class Bubble
    {
        // Wykonuje się ok. 450ms dla 5000 obiektów,
        //  co oznacza, że dla 2*10^7 obiektów wykonywałby się w 83 dni,
        //  nie jest do końca satysfakcjonujący wynik.
        // Ta metoda jest jedynie szybsza do ok. 400 elementów ponieważ nie wymaga sortowania.
        public static Tuple<int, long> Solution(List<Station> stations)
        {
            long distSum = Int64.MaxValue;
            int optimalIndex = 0;
            for(int i = 0; i < stations.Count; i++)
            {
                long distance = stations[i].DistanceTo(stations)[1];
                if (distance < distSum)
                {
                    optimalIndex = i;
                    distSum = distance;
                }
            }

            return new Tuple<int, long>(optimalIndex, distSum);
        }
    }
}
