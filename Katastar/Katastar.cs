using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katastar
{
    public class Katastar
    {
        public string NazivKatastra { get; set; }
        public string AdresaKatastra {get; set; }
        public List<Nepokretnost> NepokretnostiLista { get; set; }

        

        public Katastar() 
        {
            NazivKatastra = "";
            AdresaKatastra = "";
            NepokretnostiLista = new List<Nepokretnost>();
        }

        public Katastar(string nazivKatastra, string adresaKatastra, List<Nepokretnost> nepokretnosti) 
        {
            NazivKatastra = nazivKatastra;
            AdresaKatastra = adresaKatastra;
            NepokretnostiLista = nepokretnosti;
        }

        public bool dodavanjeNepokretnosti(Nepokretnost nepokretnost) 
        {
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                if (NepokretnostiLista[i].Id == nepokretnost.Id)
                {
                    return false;
                }
            }
            NepokretnostiLista.Add(nepokretnost);
            return true;
                
        }


        public void sacuvaj(string putanja) 
        {
            List<string> linije = new List<string>();
            linije.Add(NazivKatastra + ";" + AdresaKatastra);
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                Nepokretnost nepokretnost = NepokretnostiLista[i];
                int id = nepokretnost.Id;
                string vlasnik = nepokretnost.Vlasnik;
                double povrsina = nepokretnost.Povrsina;
                string brParcele = nepokretnost.BrojKatastarskeParcele;
                string ulica = nepokretnost.Ulica;
                string datum = nepokretnost.FormirajDatum();

                string linija = id + ";" + vlasnik + ";" + povrsina + ";" + brParcele + ";" + ulica + ";" + datum;
                //string linija = $"Id nekretnine: {id}, vlasnik: {vlasnik}, povrsina: {povrsina}, " +
                   // $"brParcele: {brParcele}, ulica: {ulica}, datum: {datum}\n";
                linije.Add(linija);
            }

            try 
            {
                File.WriteAllText(putanja, string.Join("\n", linije));
            }
            catch 
            {
                Console.WriteLine("doslo je do gereske");
            }

        }


        public void ucitaj(string putanja) 
        {
            string[]? linije = null;
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("dd.MM.yyyy.");
            try
            {
                if (!File.Exists(putanja)) return;
                linije = File.ReadAllLines(putanja);
                if (linije.Count() == 0) return;
            }
            catch
            {
                Console.WriteLine("doslo je do gereske");
            }

            string[] linijaKatastar = linije[0].Split(";");

            Katastar katastar = new Katastar();
            katastar.NazivKatastra = linijaKatastar[0];
            katastar.AdresaKatastra = linijaKatastar[1];

            for (int i = 1; i < linije.Length; i++)
            {
                Nepokretnost nepokretnost = new Nepokretnost();
                string[] nepokretnostLinija = linije[i].Split(";");

                nepokretnost.Id = Int32.Parse(nepokretnostLinija[0]);
                nepokretnost.Vlasnik = nepokretnostLinija[1];
                nepokretnost.Povrsina = Double.Parse(nepokretnostLinija[2]);
                nepokretnost.BrojKatastarskeParcele = nepokretnostLinija[3];
                nepokretnost.Ulica = nepokretnostLinija[4];
                nepokretnost.DatumPoslednjeIzmene = DateTime.ParseExact(nepokretnostLinija[5], "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                NepokretnostiLista.Add(nepokretnost);
            }
        }

        public void ispisNepokretnosti() 
        {
            Console.WriteLine("{0,15} {1,15} {2,15} {3,15} {4,15} {5,15}", "Id", "Vlasnik", "Povrsina", "Broj parcele", "Ulica", "Datum izmene");
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                Console.WriteLine(NepokretnostiLista[i]);
            }
        }

        public Nepokretnost izmenaNepokretnosti(Nepokretnost izmenjenaNepokretnost) 
        {
            for (int i = 0; i < NepokretnostiLista.Count ; i++)
            {
                if (NepokretnostiLista[i].Id == izmenjenaNepokretnost.Id) 
                {
                    return NepokretnostiLista[i];
                }
            }
            return null;
        }

        public void brisanjeNepokretnosti(int id) 
        {
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                if (NepokretnostiLista[i].Id == id) 
                {
                    NepokretnostiLista.RemoveAt(i);
                    Console.WriteLine("Nepokretnost je uspesno izbrisana.");
                    return;
                }
            }
            Console.WriteLine("Nepokretnost nije uspesno izbrisana.");
        }



        public List<Nepokretnost> ispisNepokretnostiVlasnika(string vlasnik)
        {
            List<Nepokretnost> lista = new List<Nepokretnost>();
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                if (NepokretnostiLista[i].Vlasnik == vlasnik) 
                {
                    lista.Add(NepokretnostiLista[i]);
                    Console.WriteLine(NepokretnostiLista[i]);
                }
            }
            return lista;
        }


        public List<Nepokretnost> ispisNepokretnostiNaParceli(string brParcele, double minPovrsina, double maxPovrsina) 
        {
            List<Nepokretnost> lista = new List<Nepokretnost>();
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                if (NepokretnostiLista[i].BrojKatastarskeParcele == brParcele 
                    && NepokretnostiLista[i].Povrsina >= minPovrsina
                    && NepokretnostiLista[i].Povrsina <= maxPovrsina) 
                {
                    lista.Add(NepokretnostiLista[i]);
                    Console.WriteLine(NepokretnostiLista[i]);
                }
            }
            return lista;
        }


        public double izracunajProsecnuPovrsinuUUlici(string ulica) 
        {
            int brojac = 0;
            double suma = 0.0;
            double prosek = 0.0;
            for (int i = 0; i < NepokretnostiLista.Count; i++)
            {
                if (NepokretnostiLista[i].Ulica == ulica) 
                {
                    suma += NepokretnostiLista[i].Povrsina;
                    brojac++;
                }
            }
            if(brojac > 0) 
            {
                prosek = suma / brojac;
                Console.WriteLine("Prosecna povrsina parcele u ulici " + ulica + "je " + prosek);
                return prosek;
            }
            Console.WriteLine("Zadati kriterijum nije dao rezultat.");
            return prosek;
        }





        public override string ToString()
        {
            return NazivKatastra + AdresaKatastra;
        }



    }
}
