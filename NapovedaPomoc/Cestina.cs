using System.Data;

namespace NapovedaPomoc
{
    /// <summary>
    /// Čeština
    /// </summary>
    public class Cestina
    {
        /// <summary> 
        /// Převod znakové sady 852- do 1250
        /// </summary>
        /// <param name="text">původní string</param>
        /// <returns>string nová znaková sada</returns>	
        string Prevod(string text)
        {
            byte[] bajty = System.Text.Encoding.GetEncoding(852).GetBytes(text);   //převést text na bajty
            return System.Text.Encoding.GetEncoding(1250).GetString(bajty);
        }
        /// <summary>
        /// Převod Dos (sada 852) do (sady 1250) Tabulky DataTable tedy převod do češtiny
        /// </summary>
        public DataTable Tabulka(DataTable Dos)
        {
            Console.WriteLine("Převod znakové sady do češtiny");
            DataTable cestina = new DataTable { TableName = "cestina" };
            DataRow slo;
            foreach (DataColumn i in Dos.Columns) //Sloupce 
            {
                cestina.Columns.Add(i.ToString(), typeof(string)); //Názvy sloupců do navratové tabulky
            }

            foreach (DataRow i in Dos.Rows)
            {
                slo = cestina.NewRow();  //vždy nový řádek
                int pi = 0;
                for (int j = 0; j < Dos.Columns.Count; j++)
                {
                    slo[pi] = Prevod(i[j].ToString());
                    pi++;
                }
                cestina.Rows.Add(slo); //řádky do návratové tabulky
            }
            return cestina;
        }
    }
}
