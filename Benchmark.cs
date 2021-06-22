using System;
using System.Collections.Generic;


// Ten algorytm ma pesymistyczną złożoność czasową O(n)=n^2
//  bo musi przeiterować całą tablicę n razy.
namespace ASD___2
{
	// Powinno się to prawidłowo zrobić za pomocą BenchmarkDotNet, ale Diagnostics.Stopwatch też się zda.
	/// <summary>
	/// Klasa do mierzenia sprawności wszystkich rozwiązań.
	/// </summary>
	public static class Benchmark
    {
		public static void Perform(List<Station> listOfStations)
        {
			Console.WriteLine("\n");

			Tuple<string, Tuple<int, long>> solution;

			var stopwatch = new System.Diagnostics.Stopwatch();
			if(listOfStations.Count < 50_000)
			{
				stopwatch.Restart();
				stopwatch.Start();
				solution = new Tuple<string, Tuple<int, long>>("Bubble", Bubble.Solution(listOfStations));
				stopwatch.Stop();
				Console.WriteLine($" > > >Bubble time: {stopwatch.ElapsedMilliseconds}ms");
				Console.WriteLine($">>>Solution:\n {solution.Item2.Item1}\n {solution.Item2.Item2}");
				Console.WriteLine($">>>Real distance of solution: {listOfStations[solution.Item2.Item1]}");
			}
			else
            {
				Console.WriteLine($" > > >Bubble time: at least one universe lifetime...");
				Console.WriteLine($">>>Solution: who cares?!");
				Console.WriteLine($">>>Real distance of solution: ???");
			}

            var sortedList = new List<Station>(listOfStations);
			stopwatch.Restart();
			stopwatch.Start();
			solution = new Tuple<string, Tuple<int, long>>("Iteration", Iteration.Solution(sortedList));
			stopwatch.Stop();
			Console.WriteLine($"\n > > >Iteration time: {stopwatch.ElapsedMilliseconds}ms");
			Console.WriteLine($">>>Solution:\n {solution.Item2.Item1}\n {solution.Item2.Item2}");
			Console.WriteLine($">>>Real distance of solution: {listOfStations[solution.Item2.Item1]}");

			sortedList = new List<Station>(listOfStations);
			stopwatch.Restart();
			stopwatch.Start();
			solution = new Tuple<string, Tuple<int, long>>("Recursion", Recursion.Solution(sortedList));
			stopwatch.Stop();
			Console.WriteLine($"\n > > >Recursion time: {stopwatch.ElapsedMilliseconds}ms");
			Console.WriteLine($">>>Solution:\n {solution.Item2.Item1}\n {solution.Item2.Item2}");
			Console.WriteLine($">>>Real distance of solution: {listOfStations[solution.Item2.Item1]}");
		}

		public static List<Station> CreateTestList(int lenght, int maxDistance = 10000, bool readToConsole = false)
		{
			Console.WriteLine($">>>Creating random list with\n  -lenght:{lenght}\n  -maxDistance:{maxDistance}\n");
			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Restart();
			stopwatch.Start();


			var result = new List<Station>(lenght);

			Random rng = new Random();

			for (int i = 0; i < lenght; i++)
				result.Add(new Station(i, rng.Next(maxDistance)));

			if (readToConsole)
			{
				Console.WriteLine("Benchmark list:");
				foreach(Station station in result)
					Console.WriteLine(station);
			}

			stopwatch.Stop();
			Console.WriteLine($">>>List has been created in {stopwatch.ElapsedMilliseconds}ms");
			return result;
        }
    }
}
