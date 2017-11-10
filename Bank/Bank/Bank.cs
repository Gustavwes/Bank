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
    class Bank
    {
        public List<Kund> kundLista = KundRegister.HämtaKunder();
       public List<Konto> kontoLista = KontoRegister.HämtaKonto();
        public List<Transaktion> transaktionsLista = new List<Transaktion>();


        public Bank()
        {
            bool live = true;

            LäggTillBefintligaKonton(kundLista, kontoLista);


            while (live)
            {

                BankMeny();
                RäknaKundKonto();
                Console.WriteLine("Mata in Val: ");
                string input = Console.ReadLine();
                try
                {

                    switch (input)
                    {
                        case "0":
                            AvslutaOchSpara();
                            live = false;
                            break;
                        case "1":
                            SökKund();
                            break;
                        case "2":
                            VisaKund();
                            break;
                        case "3":
                            LäggTillKund();
                            break;
                        case "4":
                            TaBortKund();
                            break;
                        case "5":
                            SkapaKonto();
                            break;
                        case "6":
                            TaBortKonto();
                            break;
                        case "7":
                            Insättning();
                            break;
                        case "8":
                            Uttag();
                            break;
                        case "9":
                            Överföring();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Felaktig inmatning. Återgår till menyn.");

                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Det finns ingen kund eller konto med detta nummer. Återgår till menyn.");
                }
                catch (InvalidOperationException)
                {
                    var kontoListan = kundLista.SelectMany(x => x.konton);
                    Console.WriteLine("Det finns ingen kund eller konto med det numret. Alla kunder finns mellan {0} - {1}. Alla konton finns mellan {2} - {3}.",
                        kundLista.Min(x => x.KundNummer), kundLista.Max(x => x.KundNummer), kontoListan.Min(x => x.KontoNummer), kontoListan.Max(x => x.KontoNummer));

                }

            }
        }

        public static void LäggTillBefintligaKonton(List<Kund> kundLista, List<Konto> kontoLista)
        {

            foreach (var kund in kundLista)
            {
                var kundKonton = kontoLista.Where(x => x.Ägare == kund.KundNummer);
                foreach (var konto in kundKonton)
                {
                    kund.konton.Add(konto);
                }
            }
        }
        public static void BankMeny()
        {
            Console.WriteLine("\nHUVUDMENY\n" +
                              "0) Avsluta och spara\n" +
                              "1) Sök kund\n" +
                              "2) Visa kundbild\n" +
                              "3) Skapa Kund\n" +
                              "4) Ta bort kund\n" +
                              "5) Skapa konto\n" +
                              "6) Ta bort konto\n" +
                              "7) Insättning\n" +
                              "8) Uttag\n" +
                              "9) Överföring");

        }

        public void AvslutaOchSpara()
        {
            string filename = DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            Console.WriteLine($"\nSparar till {filename}...");
            using (StreamWriter writer = new StreamWriter(filename))
            {
                int kontoRäknare = 0;
                writer.WriteLine(kundLista.Count);
                foreach (var kund in kundLista)
                {
                    writer.WriteLine($"{kund.KundNummer};{kund.OrganisationsNummer};{kund.Namn};{kund.Adress};{kund.Stad};{kund.PostOrt};{kund.PostNummer};{kund.Land};{kund.TelefonNummer}");
                }
               

                var kontoListan = kundLista.SelectMany(x => x.konton);
                writer.WriteLine(kontoListan.Count());

                foreach (var konto in kontoListan)
                {

                    writer.WriteLine($"{konto.KontoNummer};{konto.Ägare};{konto.Saldo}");
                }
                RäknaKundKonto();
                Console.ReadLine();
            }
        }
        public void SökKund()
        {
            Console.WriteLine("Skriv in namn eller postort: ");
            string sökOrd = Console.ReadLine().TrimEnd(' ');
            var sökLista = kundLista.Where(x => (x.Namn.ToLower().Contains(sökOrd) || x.PostOrt.ToLower().Contains(sökOrd)));
            foreach (var namn in sökLista)
            {
                Console.WriteLine(namn.KundNummer + " " + namn.Namn);
            }
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void VisaKund()
        {
            try
            {
                int kundNummer = 0;
                Console.WriteLine("Vill du söka på kund eller konto nummer?\n1) Kundnummer\n2) Kontonummer");
                string svar = Console.ReadLine().ToLower();
                if (svar == "1")
                {
                    Console.WriteLine("Skriv in kundnummer: ");
                    kundNummer = int.Parse(Console.ReadLine());
                }
                if (svar == "2")
                {
                    Console.WriteLine("Skriv in kontonummer: ");
                    int kontoNummer = int.Parse(Console.ReadLine());
                    var kundKonton = kundLista.SelectMany(x => x.konton);
                    kundNummer = kundKonton.Single(x => x.KontoNummer == kontoNummer).Ägare;
                }


                decimal kundSaldo = 0;
                Console.WriteLine(kundLista.Single(x => x.KundNummer == kundNummer).ToString() + "\n");
                var söktKund = kundLista.Single(x => x.KundNummer == kundNummer);
                Console.WriteLine("Konton: ");
                foreach (var konto in söktKund.konton)
                {
                    Console.WriteLine(konto.ToString());
                    kundSaldo += konto.Saldo;
                }
                Console.WriteLine("\nTotalsaldo: " + kundSaldo + " kr");

                Console.WriteLine("\nTransaktionshistorik: ");
                
                //var transaktionsHistorik = transaktionsLista.Where(x => x.KontoNummer == söktKund.konton.Where(y=>y.KontoNummer == ));
                //foreach (var transaktion in transaktionsHistorik)
                //{
                //    Console.WriteLine(transaktion.ToString());
                //}
                Console.WriteLine("Tryck Enter för att fortsätta");
                Console.ReadLine();

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Fel inmatning. Återgår till menyn.");
            }



        }

        public void LäggTillKund()
        {
            string[] kundInfo = new string[8];
            int kundNummer = kundLista.Max(x => x.KundNummer) + 1;
            Console.WriteLine("Skriv in Organisation Nummer för nya kunden");
            kundInfo[0] = Console.ReadLine();
            Console.WriteLine("Skriv in Namn på nya kunden");
            kundInfo[1] = Console.ReadLine();
            Console.WriteLine("Skriv in en gatuadress för nya kunden: ");
            kundInfo[2] = Console.ReadLine();
            Console.WriteLine("Skriv in kundens stad:");
            kundInfo[3] = Console.ReadLine();
            Console.WriteLine("Skriv in kundens post-ort");
            kundInfo[4] = Console.ReadLine();
            Console.WriteLine("Skriv in ett post-nummer för kunden");
            kundInfo[5] = Console.ReadLine();
            Console.WriteLine("Skriv in kundens Land: ");
            kundInfo[6] = Console.ReadLine();
            Console.WriteLine("Skriv in kundens telefonnummer");
            kundInfo[7] = Console.ReadLine();

            Kund newkund = new Kund(kundNummer, kundInfo[0], kundInfo[1], kundInfo[2], kundInfo[3], kundInfo[4], kundInfo[5], kundInfo[6], kundInfo[7]);
            kundLista.Add(newkund);
            var allaKonton = kundLista.SelectMany(x => x.konton);
            var newKontoNummer = allaKonton.Max(x => x.KontoNummer) + 1;
            Konto newKonto = new Konto(newKontoNummer, newkund.KundNummer, 0);
            newkund.konton.Add(newKonto);

            Console.WriteLine("Har nu lagt till kund <" + newkund.KundNummer + "> " + newkund.Namn);
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();


        }

        public void TaBortKund()
        {
            Console.WriteLine("Skriv in Kundnummer på den kund du vill ta bort: ");
            try
            {
                bool ejtomtKonto = false;
                int value = int.Parse(Console.ReadLine());
                var taBortKund = kundLista.SingleOrDefault(kund => kund.KundNummer == value);

                Console.WriteLine(taBortKund.ToString());

                Console.WriteLine("Vill du verkligen ta bort den här kunden?");
                string jaNej = Console.ReadLine();
                if (jaNej.ToLower() == "ja")
                {
                    foreach (var konto in taBortKund.konton)
                    {
                        if (konto.Saldo > 0)
                        {
                            Console.WriteLine(
                             "Kan inte ta bort kund eftersom kunden har pengar kvar på detta konto: ");
                            Console.WriteLine("<" + konto.KontoNummer + ">" + " " + konto.Saldo);
                            ejtomtKonto = true;
                        }

                    }
                    if (!ejtomtKonto)
                    {
                        Console.WriteLine("Har nu tagit bort kund " + taBortKund.Namn);

                        for (int i = 0; i < taBortKund.konton.Count; i++)
                        {
                            taBortKund.konton.Remove(taBortKund.konton[0]);
                        }

                        kundLista.Remove(taBortKund);
                    }


                }
                Console.WriteLine("Tryck Enter för att fortsätta");
                Console.ReadLine();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Det finns ingen kund med det kundnumret. Alla kunder finns mellan {0} - {1}.",
                    kundLista.Min(x => x.KundNummer), kundLista.Max(x => x.KundNummer));
            }
            
        }

        public void SkapaKonto()
        {

            Console.WriteLine("Skriv in Kundnummer på ägaren till kontot:");
            int kundnummer = int.Parse(Console.ReadLine());
            int ägare = kundnummer;
            var kontoListan = kundLista.SelectMany(x => x.konton);

            int kontoNummer = kontoListan.Max(x => x.KontoNummer) + 1;
            var kund = kundLista.Single(x => x.KundNummer == kundnummer);

            Konto newKonto = new Konto(kontoNummer, ägare, 0);
            kund.konton.Add(newKonto);
            Console.WriteLine("Har nu lagt till konto " + newKonto.KontoNummer + " till kund " + kund.Namn);
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void TaBortKonto()
        {
            Console.WriteLine("Skriv kundnumret på en kund vars konto du vill ta bort");
            int valKundNummer = int.Parse(Console.ReadLine());

            var valdKund = kundLista.Single(x => x.KundNummer == valKundNummer);
            Console.WriteLine("Lista av " + valdKund.Namn + " konton:");
            foreach (var konto in valdKund.konton)
            {
                Console.WriteLine(konto.ToString());
            }
            Console.WriteLine("Skriv in vilket konto du vill ta bort:");
            int valKontoNummer = int.Parse(Console.ReadLine());

            var valtKonto = valdKund.konton.Single(x => x.KontoNummer == valKontoNummer);

            if (valtKonto.Saldo > 0)
                Console.WriteLine("Du kan inte ta bort ett konto som har pengar kvar. Återgår till Menyn.");
            else
            {
                Console.WriteLine("Har nu tagit bort " + valdKund.Namn + " konto: " + valtKonto.KontoNummer);

                valdKund.konton.Remove(valtKonto);
            }
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void Insättning()
        {
            Console.WriteLine("Skriv in vilket konto du vill sätta in pengar på:");
            int inputKonto = int.Parse(Console.ReadLine());
            var allaKonton = kundLista.SelectMany(x => x.konton);
            var valtKonto = allaKonton.Single(x => x.KontoNummer == inputKonto);

            Console.WriteLine("Valda Kontot: \n" + valtKonto.ToString());

            Console.WriteLine("Hur mycket vill du sätta in på kontot?");
            decimal summa = decimal.Parse(Console.ReadLine().Replace('.', ','));
            if (summa < 0)
                Console.WriteLine("Kan inte sätta in negativ summa. Återgår till menyn.");
            else
            {
                //valtKonto.Saldo += summa;
                valtKonto.SättSaldo(summa);
                Console.WriteLine("Nya Saldot: " + valtKonto.Saldo);
            }
            var överföring = new Transaktion(summa, valtKonto.KontoNummer, "Insättning");
            TransaktionsSparare.SparaTransaktion(överföring);
            transaktionsLista.Add(överföring);
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void Uttag()
        {
            Console.WriteLine("Vilket konto vill du ta ut pengar från?");
            int kontoNummer = int.Parse(Console.ReadLine());

            var allaKonton = kundLista.SelectMany(x => x.konton);
            var kontoVal = allaKonton.Single(x => x.KontoNummer == kontoNummer);
            Console.WriteLine("Det här kontot har: " + kontoVal.Saldo + " kr");
            Console.WriteLine("Hur mycket vill du ta ut?");
            decimal summa = decimal.Parse(Console.ReadLine().Replace('.', ','));
            if (summa > kontoVal.Saldo)
                Console.WriteLine("Du kan inte ta ut mer pengar än vad som finns på kontot. Återgår till menyn.");
            else
            {
                //kontoVal.Saldo -= summa;
                Console.WriteLine("Sätta Saldotest: Skriv in Saldo");
                kontoVal.SättSaldo(-summa);
                Console.WriteLine("Nya saldot för konto: " + kontoVal.ToString());
            }
            var överföring = new Transaktion(summa, kontoVal.KontoNummer, "Uttag");
            transaktionsLista.Add(överföring);
            TransaktionsSparare.SparaTransaktion(överföring);
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void Överföring()
        {

            Console.WriteLine("Skriv in vilket kontonummer du vill överföra från:");
            int sändare = int.Parse(Console.ReadLine());
            var allaKonton = kundLista.SelectMany(x => x.konton);
            var sändareInfo = allaKonton.Single(x => x.KontoNummer == sändare);

            Console.WriteLine("Skriv in vilket kontonummer du vill överföra till");
            int mottagare = int.Parse(Console.ReadLine());

            var mottagarInfo = allaKonton.Single(x => x.KontoNummer == mottagare);

            Console.WriteLine("Kontona i fråga:\nSändare Saldo: " + sändareInfo.Saldo + "\nMottagar Saldo: " + mottagarInfo.Saldo);
            Console.WriteLine("Hur mycket vill du överföra?");
            decimal summa = decimal.Parse(Console.ReadLine().Replace('.', ','));
            if (sändareInfo.Saldo < summa)
                Console.WriteLine("Det finns inte nog med pengar på konto: " + sändareInfo.KontoNummer);
            else
            {
                //sändareInfo.Saldo -= summa;
                sändareInfo.SättSaldo(-summa);
                //mottagarInfo.Saldo += summa;
                mottagarInfo.SättSaldo(summa);
                Console.WriteLine("Ny balans på kontona i fråga:\nSändare Saldo: " + sändareInfo.Saldo + "\nMottagar Saldo: " + mottagarInfo.Saldo);

            }
            var överföring = new Transaktion(summa, sändareInfo.KontoNummer, mottagarInfo.KontoNummer, "Överföring");
            TransaktionsSparare.SparaTransaktion(överföring);
            transaktionsLista.Add(överföring);
            Console.WriteLine("Tryck Enter för att fortsätta");
            Console.ReadLine();
        }

        public void RäknaKundKonto()
        {

            int kundRäknare = kundLista.Count;
            Console.WriteLine("\nAntal kunder: " + kundRäknare);

            int kontoRäknare = 0;
            var kontoListan = kundLista.SelectMany(x => x.konton);
            foreach (var konto in kontoListan)
            {
                kontoRäknare++;
            }
            Console.WriteLine("Antal Konton: " + kontoRäknare);

            decimal totalSaldo = 0;
            foreach (var konto in kontoListan)
            {
                totalSaldo += konto.Saldo;
            }

            Console.WriteLine("Totalsaldo för alla kunder: " + totalSaldo + " kr");
        }
    }
}
