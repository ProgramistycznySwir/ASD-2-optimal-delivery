using System;
using System.Collections.Generic;
using System.Text;

namespace ASD___2
{
    public static class Testing
    {
        public static void WriteDistance(List<Station> stations)
        {
            foreach (Station station in stations)
                Console.WriteLine($"{station} -> {station.DistanceTo(stations)[1]}");
        }
    }
}
