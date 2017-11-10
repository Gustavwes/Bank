using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Transaktion
    {
        public decimal Summa { get; set; }
        public int KontoNummer { get; set; }
        public int KontoNummer2 { get; set; }
        public string TypAvTransaktion { get; set; }

        public Transaktion(decimal summa, int kontoNummer, string typAvTransaktion) //uttag/insättning
        {
            Summa = summa;
            KontoNummer = kontoNummer;
            TypAvTransaktion = typAvTransaktion;
        }
        public Transaktion(decimal summa, int sändare, int mottagare, string typAvTransaktion) //Överföring
        {
            Summa = summa;
            KontoNummer = sändare;
            KontoNummer2 = mottagare;
            TypAvTransaktion = typAvTransaktion;
        }

        public override string ToString()
        {
            return TypAvTransaktion + " " + Summa + " till: " + KontoNummer2;
        }
    }
}
