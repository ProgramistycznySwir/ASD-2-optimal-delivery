

// Author: FreeDOOM#4231 on Discord


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ASD___2
{
    class Program
    {
		readonly static string[] _fileName_ = new string[] {
			"_test.txt",			 // 0
			"5.txt",				// 1
			"_A_2_Pietrzeniuk.txt" // 2
			};

		static void Main(string[] args)
		{
			// Do łatwego ustawiania z którego pliku chcemy skorzystać.
			string fileName = _fileName_[0];

            //var benchmarkList = Benchmark.CreateTestList(20_000_000);
            ////var benchmarkList = LoadStations($"OD_in{fileName}", true);
            //Benchmark.Perform(benchmarkList);
            //return;

            //CreateTestFile($"OD_in{fileName}", 100);
            List<Station> list;
			try { list = LoadStations($"OD_in{fileName}", true); }
			catch (Exception e) { Console.WriteLine(e);  return; }
			Testing.WriteDistance(list);
			// Lista przed podaniem do algorytmu iteracyjnego jest kopiowana, by algorytm rekurencyjny nie otrzymywał
			//  z góry posortowanej listy.
            var toDisplay = new Tuple<string, Tuple<int, long>>[] {
                new Tuple<string, Tuple<int, long>>("Bubble", Bubble.Solution(list)),
                new Tuple<string, Tuple<int, long>>("Iteration", Iteration.Solution(new List<Station>(list))),
				new Tuple<string, Tuple<int, long>>("Recursion", Recursion.Solution(list))
			};

            //SaveResults($"OD_out{fileName}", toDisplay[1].Item2, true);
            SaveResults($"OD_out{fileName}", toDisplay, true);
        }


		#region >>> Obsługa plików <<<

		static List<Station> LoadStations(string fileName__, bool readToConsole = false)
		{
			if (!File.Exists(fileName__))
				throw new FileNotFoundException($"There is no file {fileName__} in program directory.");

			string[] lines = File.ReadAllLines(fileName__);

			int count = Convert.ToInt32(lines[0]);
			var resultList = new List<Station>(count);
			for (int i = 0; i < count;)
				resultList.Add(new Station(i, Convert.ToInt32(lines[++i])));

			if(readToConsole)
			{
				Console.WriteLine($"{fileName__} stations:");
				foreach (Station station in resultList)
					Console.WriteLine(station);
			}

			return resultList;
		}

		static void SaveResults(string fileName__, Tuple<string, Tuple<int, long>>[] toDisplay, bool readToConsole = false)
		{
			if (File.Exists(fileName__))
				File.Delete(fileName__);

			foreach (Tuple<string, Tuple<int, long>> item in toDisplay)
				File.AppendAllText(fileName__, $"\n{item.Item1} Method: \n{item.Item2.Item1}\n{item.Item2.Item2}\n");

			if(readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		/// <summary>
		/// Saves only data specified in exercise instruction.
		/// </summary>
		/// <param name="fileName__"></param>
		/// <param name="toDisplay"></param>
		static void SaveResults(string fileName__, Tuple<int, long> toDisplay, bool readToConsole = false)
		{
			if (File.Exists(fileName__))
				File.Delete(fileName__);

			File.WriteAllText(fileName__, $"{toDisplay.Item1}\n{toDisplay.Item2}\n");

			if (readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		static void CreateTestFile(string fileName__, int sampleSize = 0, bool readToConsole = false)
		{
			if (File.Exists(fileName__))
				File.Delete(fileName__);

			Random rng = new Random();

			if (sampleSize == 0)
				sampleSize = rng.Next(30, 50);

			File.AppendAllText(fileName__, $"{sampleSize}");

			for (int i = 1; i <= sampleSize; i++)
				File.AppendAllText(fileName__, $"\n{rng.Next(0, 10000)}");

			if(readToConsole)
			{
				Console.WriteLine($"\n{fileName__} contents:\n");
				Console.WriteLine(File.ReadAllText(fileName__));
			}
		}

		#endregion

		public static float AvarageDistance(List<Station> stations)
		{
			int sum = 0;

			foreach (Station station in stations)
				sum += station.dist;

			return sum / stations.Count;
		}

	}

	/// <summary>
	/// Stores station number and distance from road-start.
	/// </summary>
	public struct Station
    {
		public int num;
		public int dist;

		public Station(int num, int dist)
			=> (this.num, this.dist) = (num, dist);

		/// <returns> Difference to station</returns>
		public int DifferenceTo(Station station)
			=> station.dist - dist;

		/// <summary>
		/// Calculates summary distance to every object in input list.
		/// </summary>
		/// <returns>0 - difference, 1 - distance</returns>
		public long[] DistanceTo(List<Station> stations)
		{
			long sumDifference = 0;
			long sumDistance = 0;
			int difference_;
			foreach (Station station in stations)
            {
				sumDifference += difference_ = DifferenceTo(station);
				sumDistance += Math.Abs(difference_);
            }
			return new long[] { sumDifference, sumDistance };
		}

		public override string ToString()
			=> $"{num}. {dist}";
	}
}
