using System.Globalization;

namespace Katastar
{
    public class Program
    {
        public static bool proveraId(string id) 
        {
            int unos = Int32.Parse(id);
            try 
            {
                
                if(unos < 1) 
                {
                    return false;
                }
                return true;
            }catch(Exception e) 
            {
                return false;
            }
        }

        public static bool proveraPovrsine(string povrsina) 
        {
            try 
            {
                double unos = Int32.Parse(povrsina);
                if (unos > 0)
                {
                    return true;
                }
                return false;
            }catch(Exception e) 
            {
                return false;
            }
            
        }

        public static bool proveraParcele(string parcela) 
        {
            try 
            {
                if(parcela.Length == 4) 
                {
                    return true;
                }
                return false;
            } catch(Exception e) 
            {
                return false;
            }
        }

        public static bool proveraDatuma(string datum) 
        {
            try 
            {
                DateTime datum1;
                if(!DateTime.TryParseExact(datum, "dd.MM.yyyy.", null, DateTimeStyles.None, out datum1)) 
                {
                    return false;
                }
                return true;
            }catch(Exception e)
            {
                return true;
            }
        }


        public static  void unosKatastra(Katastar katastar) 
        {
            Console.WriteLine("Unesi ime katastra");
            string imeKatastra = Console.ReadLine();
            Console.WriteLine("Unesi adresu katastra");
            string adresa = Console.ReadLine();
            katastar.NazivKatastra = imeKatastra;
            katastar.AdresaKatastra = adresa;
        }

        public static void unesiNepokretnost(Katastar katastar) 
        {
            int id;
            string ids;
            string vlasnik;
            double povrsina;
            string povrsinas;
            string brParcele;
            string ulica;
            DateTime datumIzmene;
            string datumIzmenes;

            do
            {
                Console.WriteLine("Unesite id nepokretnosti:");
                ids = Console.ReadLine();
            } while (!proveraId(ids));
            id = Int32.Parse(ids);

            Console.WriteLine("Unesite naziv vlasnika: ");
            vlasnik = Console.ReadLine();

            do
            {
                Console.WriteLine("Unesite povrsinu parcele: ");
                povrsinas = Console.ReadLine();
            } while (!proveraPovrsine(povrsinas));
            povrsina = Double.Parse(povrsinas);

            do
            {
                Console.WriteLine("Unesite broj parcele (4 karaktera): ");
                brParcele = Console.ReadLine();
            } while (!proveraParcele(brParcele));

            Console.WriteLine("Unesite naziv ulice:  ");
            ulica = Console.ReadLine();

            do
            {
                Console.WriteLine("Unesite datum poslednje izmene (ocekivani format dd.MM.yyyy.): ");
                datumIzmenes = Console.ReadLine();
            } while (!proveraDatuma(datumIzmenes));

            //datumIzmene = DateTime.Parse(datumIzmenes);
            datumIzmene = DateTime.ParseExact(datumIzmenes, "dd.MM.yyyy.", CultureInfo.InvariantCulture);

            Nepokretnost nepokretnost = new Nepokretnost(id, vlasnik, povrsina, brParcele, ulica, datumIzmene);
            bool provera = katastar.dodavanjeNepokretnosti(nepokretnost);

            if (provera)
            {
                Console.WriteLine("Nepokretnost je uspesno dodata.");
            }
            else
            {
                Console.WriteLine("Nepokretnost nije uspesno dodata.");
            }

        }


        public static void izmeniNepokretnost(Katastar katastar) 
        {
            int id;
            string ids;
            string vlasnik;
            double povrsina;
            string povrsinas;
            string brParcele;
            string ulica;
            DateTime datumIzmene;
            string datumIzmenes;

            do
            {
                Console.WriteLine("Unesite id nepokretnosti:");
                ids = Console.ReadLine();
            } while (!proveraId(ids));
            id = Int32.Parse(ids);

            Console.WriteLine("Unesite naziv vlasnika: ");
            vlasnik = Console.ReadLine();

            do
            {
                Console.WriteLine("Unesite povrsinu parcele: ");
                povrsinas = Console.ReadLine();
            } while (!proveraPovrsine(povrsinas));
            povrsina = Double.Parse(povrsinas);

            do
            {
                Console.WriteLine("Unesite broj parcele (4 karaktera): ");
                brParcele = Console.ReadLine();
            } while (!proveraParcele(brParcele));

            Console.WriteLine("Unesite naziv ulice:  ");
            ulica = Console.ReadLine();

            do
            {
                Console.WriteLine("Unesite datum poslednje izmene (ocekivani format dd.MM.yyyy.): ");
                datumIzmenes = Console.ReadLine();
            } while (!proveraDatuma(datumIzmenes));

            datumIzmene = DateTime.ParseExact(datumIzmenes, "dd.MM.yyyy.", CultureInfo.InvariantCulture);

            Nepokretnost nepokretnost = new Nepokretnost(id, vlasnik, povrsina, brParcele, ulica, datumIzmene);
            Nepokretnost provera = katastar.izmenaNepokretnosti(nepokretnost);

            if (provera != null)
            {
                Console.WriteLine("Nepokretnost je uspesno dodata.");
            }
            else
            {
                Console.WriteLine("Nepokretnost nije uspesno dodata.");
            }
        }

        public static void izbrisiNepokretnost(Katastar katastar) 
        {
            int id;
            string ids;
            do
            {
                Console.WriteLine("Unesite id broj nepokretnosti za brisanje:");
                ids = Console.ReadLine();
            } while (!proveraId(ids));
            id = Int32.Parse(ids);
            katastar.brisanjeNepokretnosti(id);
        }


        public static void pretragaPoVlasniku(Katastar katastar) 
        {
            Console.WriteLine("Unesite ime vlasnika");
            string vlasnik = Console.ReadLine();
            katastar.ispisNepokretnostiVlasnika(vlasnik);
        }

        public static void pretragaPoParceli(Katastar katastar) 
        {
            string brParcele;
            double minPov;
            double maxPov;
            string minPovs;
            string maxPovs;

            do
            {
                Console.WriteLine("Unesite minimalnu povrsinu parcele: ");
                minPovs = Console.ReadLine();
            } while (!proveraPovrsine(minPovs));
            minPov = Double.Parse(minPovs);

            do
            {
                Console.WriteLine("Unesite minimalnu povrsinu parcele: ");
                maxPovs = Console.ReadLine();
            } while (!proveraPovrsine(maxPovs));
            maxPov = Double.Parse(maxPovs);

            do
            {
                Console.WriteLine("Unesite broj parcele (4 karaktera): ");
                brParcele = Console.ReadLine();
            } while (!proveraParcele(brParcele));
            katastar.ispisNepokretnostiNaParceli(brParcele,minPov,maxPov);

        }

        public static void prikazProsecnePovrsine(Katastar katastar) 
        {
            Console.WriteLine("Unesite ulicu: ");
            string ulica = Console.ReadLine();
            katastar.izracunajProsecnuPovrsinuUUlici(ulica);
        }


        static void Main(string[] args)
        {
            Katastar katastar = new Katastar();
            katastar.ucitaj("text.txt");

            string input;
            do 
            {
                Console.WriteLine("Meni:");
                Console.WriteLine("1. Unos katastra");
                Console.WriteLine("2. Unos nove nepokretnosti");
                Console.WriteLine("3. Ispis svih nepokretnosti");
                Console.WriteLine("4. Izmena nepokretnosti");
                Console.WriteLine("5. Brisanje nepokretnosti");
                Console.WriteLine("6. Pretraga nepokretnosti po vlasniku");
                Console.WriteLine("7. Pretraga nepokretnosti po parceli");
                Console.WriteLine("8. Prikaz prosecne povrsine nepokretnosti za zadatu ulicu");
                Console.WriteLine("9. Prikaz izmenjenih nepokretnosti u zadatom periodu");
                Console.WriteLine("10. Ispis podataka o katastru");
                Console.WriteLine("x. Izlaz");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        unosKatastra(katastar);
                        katastar.sacuvaj("text.txt");
                        break;
                    case "2":
                        unesiNepokretnost(katastar);
                        katastar.sacuvaj("text.txt");
                        break;
                    case "3":
                        katastar.ispisNepokretnosti();
                        break;
                    case "4":
                        izmeniNepokretnost(katastar);
                        katastar.sacuvaj("text.txt");
                        break;
                    case "5":
                        izbrisiNepokretnost(katastar);
                        katastar.sacuvaj("text.txt");
                        break;
                    case "6":
                        pretragaPoVlasniku(katastar);
                        break;
                    case "7":
                        pretragaPoParceli(katastar);
                        break;
                    case "8":
                        prikazProsecnePovrsine(katastar);
                        break;
                    case "9":
                        //prikazIzmenjenihNepokretnosti(katastar);
                        break;
                    case "10":
                        Console.WriteLine(katastar);
                        break;
                    default:
                        Console.WriteLine("Pogresan izbor opcije. Pokusajte ponovo.");
                        break;
                }

            } while (input != "x");

        }
    }
}