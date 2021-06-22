using System;
using System.Collections.Generic;
using System.Text;

namespace ASD___2
{
    public static class Sorting
    {
        public static List<Station> CountSort(List<Station> list, int k)
        {
            int[] count = new int[k+1];

            for (int i = 0; i < list.Count; i++)
                count[list[i].dist]++;

            for (int i = 1; i < count.Length; i++)
                count[i] = count[i] + count[i - 1];

            var sorted = new List<Station>(list.Count);
            for (int i = 0; i < list.Count; i++)
                sorted.Add(new Station());

            for (int i = list.Count - 1; i >= 0; i--)
                sorted[--count[list[i].dist]] = list[i];

            return sorted;
        }

        public static void SortBenchmark()
        {
            var list = Benchmark.CreateTestList(20_000_000);

            var solution = new List<Station>(list);
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Restart();
            stopwatch.Start();
            solution = CountSort(list, 20_000);
            stopwatch.Stop();
            Console.WriteLine($" > > >CountSort time: {stopwatch.ElapsedMilliseconds}ms");

            solution = new List<Station>(list);
            stopwatch.Restart();
            stopwatch.Start();
            solution.Sort((a, b) => a.dist.CompareTo(b.dist));
            stopwatch.Stop();
            Console.WriteLine($" > > >QuickSort time: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
