using System;
using System.Collections.Concurrent;
using Analyzator;
using DataConsumer;

namespace Analyzator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberFrequency = new ConcurrentDictionary<int, int>();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "exchangeData");
            var average = 0.0;
            var averageSquare = 0.0;
            var standardDeviation = 0.0;
            var mode = 0.0;
            var median = 0.0;
            var count = 0;
            while (true)
            {
                count++;
                var statsCalculator = new StatsCalculator();
                using (var mutex = new Mutex(false, "ExchangeFileAcces"))
                {
                    mutex.WaitOne();
                    try
                    {
                        using (StreamReader file = new StreamReader(path))
                        {
                            var number = int.Parse(File.ReadAllText(path));
                            standardDeviation = statsCalculator.CalculateStandardDeviation(average, averageSquare, count, number);//nan problem
                            average = statsCalculator.CalculateAverage(average, count - 1, number);
                            averageSquare = statsCalculator.CalculateAverage(averageSquare, count - 1, number * number);
                            mode = statsCalculator.CalculateMode(numberFrequency, number);
                            median = statsCalculator.CalculateMedian(numberFrequency);
                            //file.Close();
                            Console.WriteLine(number);
                            Console.WriteLine($"Der:{standardDeviation}|Av:{average}|AvS:{averageSquare}|mod:{mode}|med:{median}");
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
