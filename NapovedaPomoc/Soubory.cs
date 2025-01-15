using System.Collections.Generic;
using System.Xml.Linq;

namespace NapovedaPomoc
{
    public static class Soubory
    {
        /// <summary>
        ///  Uložení třídy
        /// </summary>
        public static void SaveJson<T>(this IEnumerable<T> moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            File.WriteAllText(cesta, jsonString);
        }

        /// <summary>
        ///  Uložení třídy
        /// </summary>
        public static void SaveJson<T>(this List<T> moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            File.WriteAllText(cesta, jsonString);
        }

        /// <summary>
        /// Uložení třídy
        /// </summary>
        public static IEnumerable<T> LoadJson<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                IEnumerable<T> moje = System.Text.Json.JsonSerializer.Deserialize<List<T>>(jsonString)!;
                return moje;
            }
            return new List<T>();
        }

        /// <summary>
        /// Uložení třídy
        /// </summary>
        public static List<T> LoadJsonList<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                List<T> moje = System.Text.Json.JsonSerializer.Deserialize<List<T>>(jsonString)!;
                return moje;
            }
            return new List<T>();
        }

        /// <summary>        
        /// Uloži string[] do txt        
        /// </summary>
        public static void SaveTXT(this string[] data, string Cesta)
        {
            using var zak = new System.IO.StreamWriter(Cesta);
            foreach (string item in data)
                zak.WriteLine(item.ToString());
            zak.Close();
        }

        /// <summary>        
        /// Nacti string[] z txt podle radku        
        /// </summary>
        public static string[] LoadTXT(string Cesta)
        {
            string Pole = "";
            using FileStream fs = new FileStream(Cesta, FileMode.Open, FileAccess.Read);
            using StreamReader sw = new(fs);
            while (!sw.EndOfStream)
            {
                Pole += sw.ReadLine().ToString() + ";";
            }
            sw.Close();
            //return Pole.Split('\u002C');
            return Pole.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>        
        /// Nacti XmlDocument ze souboru  
        /// </summary>
        public static XDocument LoadXML(string Cesta)
        {
            if (!File.Exists(Cesta)) return null;
            XDocument Pole = XDocument.Load(Cesta);
            //string xmlString = File.ReadAllText(Cesty.PodporaDataXml);
            //Pole.LoadXml(xmlString);
            return Pole;
        }

        /// <summary>
        /// Uložení xml dokumentu do souboru Cesta
        /// </summary>       
        public static void SaveXML(this XDocument doc, string Cesta)
        {
            if (File.Exists(Cesta)) return;
            doc.Save(Cesta);
        }
    }
}
