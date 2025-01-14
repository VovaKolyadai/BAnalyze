using System.Xml.Linq;

namespace Exchange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "exchangeData");
            while (true)
            {
                using (StreamWriter file = new StreamWriter(path))
                {
                    file.WriteLine(random.Next(0,int.MaxValue));
                    file.Close();
                }   
            }
        }
    }
}
