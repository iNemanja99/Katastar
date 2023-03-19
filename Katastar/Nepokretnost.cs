using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katastar
{
    public class Nepokretnost
    {
        public int Id { get; set; }
        public string Vlasnik { get; set; }
        public double Povrsina { get; set; }
        public string BrojKatastarskeParcele { get; set; }
        public string Ulica { get; set; }
        public DateTime DatumPoslednjeIzmene { get; set; } 

        public Nepokretnost() 
        {
            Vlasnik = "";
            BrojKatastarskeParcele = "";
            Ulica = "";
            DatumPoslednjeIzmene = DateTime.Now;
        }

        public Nepokretnost(int id, string vlasnik, double povrsina, string brojKatastarskeParcele, string ulica, DateTime datum)
        {
            Id= id;
            Vlasnik = vlasnik;
            Povrsina = povrsina;
            BrojKatastarskeParcele = brojKatastarskeParcele;
            Ulica = ulica;
            DatumPoslednjeIzmene = datum;
        }

        public string FormirajDatum()
        {
            return DatumPoslednjeIzmene.ToString("dd.MM.yyyy.");
        }


        public override string ToString()
        {
            return string.Format("{0,15:d} {1,15} {2,10:0.00} {3,15} {4,15} {5,15}", Id, Vlasnik, Povrsina, BrojKatastarskeParcele, Ulica, FormirajDatum());
        }


    }
}
