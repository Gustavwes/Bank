using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class KontoRegister
    {
        public static List<Konto> HämtaKonto()
        {
            List<Konto> inskrivnaKonton = new List<Konto>();


            var kundRegister = "bankdata.txt";
            using (StreamReader reader = new StreamReader(kundRegister))
            {

                int antalKunder = int.Parse(reader.ReadLine());

                int antalKonton=0;
                for (int i = 0; i <= (antalKunder+antalKonton); i++)
                {
                    string kundRad = reader.ReadLine();
                    if (i == antalKunder)
                    {
                        antalKonton = int.Parse(kundRad);
                       
                    }
                    if (i > antalKunder)
                    {
                    string[] kontoInfo = kundRad.Split(';');
                    Konto konto = new Konto(int.Parse(kontoInfo[0]), int.Parse(kontoInfo[1]), decimal.Parse(kontoInfo[2], CultureInfo.InvariantCulture));
                    inskrivnaKonton.Add(konto);
                   
                        
                    }
                }
            }
            return inskrivnaKonton;
        }
    }
}
