using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _2021_emelt_kalapacsvetes
{
    class versenyzo
    {
        public string Nev { get; private set; }
        public string Csoport { get; private set; }
        public string Nemzet { get; private set; }//8.Feldathoz kell
        public string Kod { get; private set; }
        public string Sorozat { get; private set; }//9.feladat kiírásához
        public double [] Dobasok { get; private set; }
        public double Eredmeny { get; private set; }//7.feladat
        public versenyzo(string s)
        {
            string[] tmp = s.Split(';');
            Nev = tmp[0];
            Csoport = tmp[1];
            string[] temp2 = tmp[2].Split('(');
            Nemzet = temp2[0].Trim();
            Kod = temp2[1].Trim(')');

            Dobasok = new double[3];
            for(int i=3;i<tmp.Length;i++)
            {

                string d = tmp[i];//csak az egszerűsítés végettt lett egyenlő
                Sorozat += d;
                if (i < tmp.Length - 1) Sorozat += ";";//sorozat 9.feladathoz
                try
                {

                    Dobasok[i - 3] = double.Parse(tmp[i]); //dobások nulladik értéke
                }
                catch(Exception ex)     
                {
                    if (d == "x") Dobasok[i - 3] = -1.0;//dobások ha x akkor -1
                    if (d == "-") Dobasok[i - 3] = -2.0;//dobások ha - akkor -2

                }
            }
            Eredmeny = -1.0;//7. Feladat megoldása
            foreach (var d in Dobasok) if (d > Eredmeny) Eredmeny = d;

        }

      
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = File.OpenText("Selejtezo2012.txt");
            List<versenyzo> versenyzok = new List<versenyzo>();
            string fejlec = sr.ReadLine();
            while (!sr.EndOfStream) versenyzok.Add(new versenyzo(sr.ReadLine()));
            sr.Close();
            int n = versenyzok.Count;
            //5.feladat
            Console.WriteLine($"5.feladat:Versenyzők száma {n} fő");
            //6.feladat
            int bejutok = 0;
            foreach (var v in versenyzok)
                if (v.Sorozat.Contains('-')) bejutok++;
            Console.WriteLine($"tovább jutok :{bejutok} fő");
            //6. feladat  ugyan az bonyolultan
            bejutok = 0;
            double szint = 78;
            foreach(var v in versenyzok)
                if(v.Dobasok[0]>szint|| v.Dobasok[1]>szint)bejutok++;
            Console.WriteLine($"6.Feladat : {szint:f2} m -től dobással tovább jutók: {bejutok}");
            //7.Fealadat megoldva konstruktorban
            //8.Fealdat konstruktorban kész
            //9.Feladat:

            int max = 0;
            for (int i = 0; i < n; i++)
                if (versenyzok[max].Eredmeny < versenyzok[i].Eredmeny) max = i;
            versenyzo w = versenyzok[max];//hogy ne kelljn mindig leírni
            Console.WriteLine("9.feladat: A selejtező győztese: ");
            Console.WriteLine($"\tNév:{w.Nev}");
            Console.WriteLine($"\tCsoport:{w.Csoport}");
            Console.WriteLine($"\tNemzet:{w.Nemzet}");
            Console.WriteLine($"\tNemzet Kód:{w.Kod}");
            Console.WriteLine($"\tSorozat:{w.Sorozat}");
            Console.WriteLine($"\tEredmeny:{w.Eredmeny}");







        }
    }
}
