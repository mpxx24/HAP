using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace htmlAP
{
    class Program
    {
        public static IEnumerable<Rekord> ZnajdzTopSto(char znak)
        {
            var licznik = 1;
            var strona = new HtmlWeb { AutoDetectEncoding = false, OverrideEncoding = Encoding.GetEncoding("iso-8859-2") };
            HtmlDocument dokument;

            switch (znak)
            {
                case '1':
                    dokument = strona.Load("http://thepiratebay.se/top/all");
                    break;
                case '2':
                    dokument = strona.Load("http://thepiratebay.se/top/100");
                    break;
                case '3':
                    dokument = strona.Load("http://thepiratebay.se/top/400");
                    break;
                case '4':
                    dokument = strona.Load("http://thepiratebay.se/top/200");
                    break;
                default:
                    dokument = strona.Load("http://thepiratebay.se/top/all");
                    break;
            }

            var buforNazwa = new string[3];
            var listaRekordow = new List<Rekord>();

            Console.WriteLine("\n\nThePirateBay top 100:\n");

            //wyciagnij nazwe torrenta
            var linki =
                dokument.DocumentNode.Descendants("a").Where(x => x.Attributes.Contains("href"));
            foreach (var link in linki)
            {
                var pozycja = new Rekord();
                if (link.Attributes["href"].Value.Contains("torrent") &&
                    !link.Attributes["href"].Value.Contains("magnet"))
                {
                    buforNazwa = link.Attributes["href"].Value.Split('/');
                    pozycja.nazwa = licznik + ") " + buforNazwa[3].Replace("_", " ");
                    listaRekordow.Add(pozycja);
                    licznik++;
                }
            }

            //wyciagnij seedy i peery
            var seedy = dokument.DocumentNode.Descendants("td").Where(x => x.Attributes.Contains("align")).ToList();
            for (int i = 0; i < seedy.Count; i++)
            {
                for (int j = 0; j < listaRekordow.Count; j++)
                {
                    listaRekordow[j].seedy = seedy[0].InnerText;
                    listaRekordow[j].peery = seedy[1].InnerText;
                    seedy.RemoveAt(0);
                    seedy.RemoveAt(0);
                }
            }

            //wyciagnij kategorię torrenta
            var kategorie = linki.Where(x => x.Attributes["href"].Value.Contains("/browse/")).ToList();
            for (int i = 0; i < kategorie.Count; i++)
            {
                for (int j = 0; j < listaRekordow.Count; j++)
                {
                    listaRekordow[j].kategoria = kategorie[0].InnerText;
                    kategorie.RemoveAt(0);
                    kategorie.RemoveAt(0);
                }
            }
            
            return listaRekordow;
        }

        static void Main()
        {
            Console.WriteLine("Ktore top100 chcesz wyswietlic/przeszukac ?");
            Console.WriteLine("1) top100 All");
            Console.WriteLine("2) top100 Muzyka");
            Console.WriteLine("3) top100 Gry");
            Console.WriteLine("4) top100 Wideo");
            ConsoleKeyInfo znak = Console.ReadKey();
            var listaRekordow = ZnajdzTopSto(znak.KeyChar);
            
            Console.WriteLine("1) wyswietl");
            Console.WriteLine("2) przeszukaj");
            ConsoleKeyInfo znak2 = Console.ReadKey();

            if (znak2.KeyChar == '1')
            {
                foreach (var rekord in listaRekordow)
                {
                    Console.Write("\n" + rekord.kategoria + " -> " + rekord.nazwa);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" " + rekord.seedy);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" " + rekord.peery);
                    Console.ResetColor();
                    //Console.Write(rekord);
                    Console.WriteLine();
                }
            }
            else if(znak2.KeyChar == '2')
            {
                Console.WriteLine("\nszukaj\n");
                var wprowadzonyTekst = Console.ReadLine();
                listaRekordow.CzyZawiera(wprowadzonyTekst);
            }
            
        }
    }
}
