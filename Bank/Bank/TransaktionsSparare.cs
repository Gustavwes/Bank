using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class TransaktionsSparare
    {
        static string filename = ("transaktionslogg") + ".txt";

        public static void SparaTransaktion(Transaktion transaktion)
        {
            Console.WriteLine($"\nSparar till {filename}...");

           
                using (StreamWriter writer = new StreamWriter(filename, append:true))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd-HHmmss ") + transaktion.TypAvTransaktion + " Kontonummer: " + transaktion.KontoNummer + " till " + transaktion.KontoNummer2 + " Summa: " + transaktion.Summa + " kr");
                    
                }
            
        }

    }
}
