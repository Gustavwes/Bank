using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class KundRegister
    {
        public static List<Kund> HämtaKunder()
        {
            List<Kund> inskrivnaKunder = new List<Kund>();


            var kundRegister = "bankdata.txt";
            using (StreamReader reader = new StreamReader(kundRegister))
            {

                int antalKunder = int.Parse(reader.ReadLine());

                for (int i = 0; i < antalKunder; i++)
                {
                    string kundRad = reader.ReadLine();
                    string[] kundInfo = kundRad.Split(';');
                    Kund kund = new Kund(int.Parse(kundInfo[0]), kundInfo[1], kundInfo[2], kundInfo[3], kundInfo[4],
                        kundInfo[5], kundInfo[6], kundInfo[7], kundInfo[8]);
                    inskrivnaKunder.Add(kund);

                }
            }
            return inskrivnaKunder;
        }
    }
}
