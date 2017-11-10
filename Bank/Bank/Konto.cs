using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Bank
{
   public class Konto
    {
        public int KontoNummer { get; private set; }
        public decimal Saldo { get; private set; }
        public int Ägare { get; private set; }


        public Konto(int kontoNummer, int ägare, decimal saldo)
        {
            KontoNummer = kontoNummer;
            Ägare = ägare;
            Saldo = saldo;
        }

        public override string ToString()
        {
            return KontoNummer + ": " + Saldo + " kr";
        }

        public void SättSaldo(decimal saldoÄndring)
        {
            Saldo += saldoÄndring;
            //return Saldo;

        }
    }
}
