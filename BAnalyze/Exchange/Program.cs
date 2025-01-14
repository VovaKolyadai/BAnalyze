using System.Xml.Linq;

namespace Exchange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "exchangeData");
            int value = 0;
            while (true)
            {
                value = random.Next(-100, 100);
                using(Mutex mutex = new Mutex(false, "ExchangeFileAcces"))
                {
                    mutex.WaitOne();
                    try
                    {
                        using (StreamWriter file = new StreamWriter(path))
                        {
                            file.WriteLine(value);
                            file.Close();
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
                Console.WriteLine(value);
            }
        }
    }
}
