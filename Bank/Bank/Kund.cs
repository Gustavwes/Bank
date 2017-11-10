using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Kund
    {
        public int KundNummer { get; private set; }
        public string OrganisationsNummer { get; private set; }
        public string Namn { get; private set; }
        public string Adress { get; private set; }
        public string Stad { get; private set; }
        public string PostOrt { get; private set; }
        public string PostNummer { get; private set; }
        public string Land { get; private set; }
        public string TelefonNummer { get; private set; }
        public List<Konto> konton = new List<Konto>();


        
        public Kund(int kundNummer, string organisationsNummer, string namn, string adress, string stad, string postOrt, string postNummer, string land, string telefonNummer)
        {
            KundNummer = kundNummer;
            OrganisationsNummer = organisationsNummer;
            Namn = namn;
            Adress = adress;
            Stad = stad;
            PostOrt = postOrt;
            PostNummer = postNummer;
            Land = land;
            TelefonNummer = telefonNummer;
        }

        public override string ToString()
        {
            return "\nKundnummer: " + KundNummer + "\nOrganisationsnummer: " + OrganisationsNummer + "\nNamn: " + Namn + "\nAdress: " + Adress + ", " + 
                PostNummer + ", " + PostOrt + ", " + Stad +   ", " + Land +
                   "\nTelefonnummer: " + TelefonNummer;
        }

       
    }
}
