using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestWordAI
{
    public class Soubory
    {
        public static string[] SouboryVeSlozce(string rootPath)
        {
            //string rootPath = @"C:\Cesta\K\Složce";

            // Najde všechny .doc a .docx soubory včetně podadresářů
            var wordFiles = Directory.EnumerateFiles(rootPath, "*.*", SearchOption.AllDirectories)
                                     .Where(f => f.EndsWith(".dotm", StringComparison.OrdinalIgnoreCase) ||
                                                 f.EndsWith(".docx", StringComparison.OrdinalIgnoreCase));
            foreach (var file in wordFiles)
                Console.WriteLine(file);

            return [.. wordFiles];
        }
    }
}
