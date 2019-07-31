using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Liker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Liker liker = new Liker(getConfig());
                int result = liker.Like().Result;
                Log($"Лайков поставлено: {result}");
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
        }

        static Config getConfig()
        {
            string path = "config.json";
            if (!File.Exists(path))
            {
                Console.WriteLine("Config file doesn't exist");
                Log("Config file doesn't exist");
                Environment.Exit(0);
            }

            string configString;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    configString = reader.ReadToEnd();
                }
            }
        
            return JsonConvert.DeserializeObject<Config>(configString);
        }

        static void Log(string text)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            string record = $"[{dateTime}]  {text}\n";

            using (FileStream fileStream = File.Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(record);
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}