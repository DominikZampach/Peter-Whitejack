using System;

namespace Peter_Whitejack
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int kredit = 1000;
            int[] hodnotyKaret = {11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10};   
            string[] nazvyKaret = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
            string[] kartyHrace = new string[15];
            string[] barvyKaretHrace = new string[15];
            string[] barva = {"♠", "♣", "♥", "♦"};
            int randomPocitadlo = r.Next(0, 13);
            int randomBarva = r.Next(0, 4);
            int vklad;
            string hraniZnovu = null;
            string odpoved = null;
            int body = 0;
            int x = 1;
            int i;
            int bodyRobota = 0;
            string[] kartyRobota = new string[15];
            string[] barvyKaretRobota = new string[15];

            Console.WriteLine("Vítej v mojí hře Blackjacku proti botovi. Tvůj základní kredit je {0} $.", kredit);
            Console.WriteLine("Pro začátek hry stiskni Enter");
            Console.ReadLine();
            Console.Clear();
            bool opakovani = true;
            while (opakovani == true)
            {
                x = 1;  //kdyby chtel hrac hrat znovu, aby mel vyresetovane veci
                body = 0;
                bodyRobota = 0;
                Array.Clear(kartyHrace, 0, kartyHrace.Length);
                Array.Clear(barvyKaretHrace, 0, barvyKaretHrace.Length);
                Array.Clear(kartyRobota, 0, kartyRobota.Length);
                Array.Clear(barvyKaretRobota, 0, kartyRobota.Length);

                Console.Clear();
                Console.Write("Vítej ve hře, zadej, kolik chceš vsadit (Tvůj aktuální kredit je {0} $) : ", kredit);
                while ((!int.TryParse(Console.ReadLine(), out vklad) || (vklad > kredit) || (vklad <= 0)))     //1. pokud vklad není int 2. pokud je vklad vetsi nez kredit 3. pokud je vklad mensi nez 1
                    Console.Write("Neplatný vklad, prosím zadejte znovu: ");
                Console.ReadLine();
                while (x <= 2)      //prvni faze, brani 2 karet
                {
                    randomPocitadlo = r.Next(0, 13);
                    randomBarva = r.Next(0, 4);
                    kartyHrace[x] = nazvyKaret[randomPocitadlo];
                    barvyKaretHrace[x] = barva[randomBarva];
                    while ((x != 1) && (barvyKaretHrace[x] == barvyKaretHrace[x - 1]) && (kartyHrace[x] == kartyHrace[x - 1]))  //aby nebyly 2 stejne karty
                    {
                        randomPocitadlo = r.Next(0, 13);
                        randomBarva = r.Next(0, 4);
                        kartyHrace[x] = nazvyKaret[randomPocitadlo];
                        barvyKaretHrace[x] = barva[randomBarva];
                    }
                    body = body + hodnotyKaret[randomPocitadlo];
                    Console.WriteLine("Tvoje {0}. karta je {1}{2}.", x, barva[randomBarva], nazvyKaret[randomPocitadlo]);
                    Console.WriteLine("Tvůj počet bodů je {0}", body);
                    Console.ReadLine();
                    x++;
                }
                if (body < 21)  //zbytecne ukazovat cloveku kterej ma 21 bodu ze zakladu (eso + 10price karta)
                {
                    //druha faze, brani vice karet
                    Console.Write("Chceš si vzít další kartu?: ");
                    odpoved = Console.ReadLine();
                    odpoved = odpoved.ToLower();
                    while ((odpoved != "ano") && (odpoved != "ne"))
                    {
                        Console.Write("Neplatný vstup, zadejte prosím znovu: ");
                        odpoved = Console.ReadLine();
                        odpoved = odpoved.ToLower();
                    }
                }

                while (odpoved == "ano" && body < 21)
                {
                    randomPocitadlo = r.Next(0, 13);
                    randomBarva = r.Next(0, 4);
                    kartyHrace[x] = nazvyKaret[randomPocitadlo];
                    barvyKaretHrace[x] = barva[randomBarva];
                    for (i = 1; i <= 14; i++)
                    {
                        while ((barvyKaretHrace[x] == barvyKaretHrace[i]) && (kartyHrace[x] == kartyHrace[i]) && (x != i))
                        {
                            randomPocitadlo = r.Next(0, 13);
                            randomBarva = r.Next(0, 4);
                            kartyHrace[x] = nazvyKaret[randomPocitadlo];
                            barvyKaretHrace[x] = barva[randomBarva];
                        }
                    }
                    body = body + hodnotyKaret[randomPocitadlo];
                    Console.WriteLine("Tvoje {0}. karta je {1}{2}.", x, barva[randomBarva], nazvyKaret[randomPocitadlo]);
                    Console.WriteLine("Tvůj počet bodů je {0}", body);
                    Console.WriteLine();
                    if (body < 21)
                    {
                        Console.Write("Chceš si vzít další kartu?: ");
                        odpoved = Console.ReadLine();
                        odpoved = odpoved.ToLower();
                        while ((odpoved != "ano") && (odpoved != "ne"))
                        {
                            Console.Write("Neplatný vstup, zadejte prosím znovu: ");
                            odpoved = Console.ReadLine();
                            odpoved = odpoved.ToLower();
                        }
                    }
                    else if (body == 21)
                    {
                        Console.WriteLine("Dosáhl jsi nejlepšího výsledku, takže si už nemůžeš vzít další karty.");
                        Console.ReadLine();
                    }
                    else if (body > 21)   //Když přesáhne, nemá cenu ani vytahovat karty za bota nebo ma jeste uno reverse card a muze to otocit s jeho esem
                    {
                        for (i = 1; i <= 14; i++)
                        {
                            if (kartyHrace[i] == nazvyKaret[0])
                            {
                                Console.WriteLine("Máš štěstí, máš eso, takže tvůj počet bodů co za něj získáš se snižuje na 1.");
                                body = body - 11 + 1;
                                kartyHrace[i] = null;   //musim zamezit aby se to rozbilo (jako se ted stalo :D)
                            }
                        }
                        if (body <= 21)
                        {
                            Console.WriteLine("Jsi znovu ve hře s {0} body!", body);
                            Console.ReadLine();
                            Console.Write("Chceš si vzít další kartu?: ");
                            odpoved = Console.ReadLine();
                            odpoved = odpoved.ToLower();
                            while ((odpoved != "ano") && (odpoved != "ne"))
                            {
                                Console.Write("Neplatný vstup, zadejte prosím znovu: ");
                                odpoved = Console.ReadLine();
                                odpoved = odpoved.ToLower();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Přesáhl jsi limit 21 bodů, takže jsi prohrál :(");
                            kredit = kredit - vklad;
                            Console.WriteLine("Tvůj počet bodů po stržení tvé sázky je: {0}", kredit);
                            Console.ReadLine();
                        }
                    }
                    x++;
                }
                if (odpoved == "ne")
                {
                    Console.WriteLine("Tvůj konečný počet nahraných bodů je {0}.", body);
                    Console.ReadLine();
                }
                x = 1;  //vynulovani x pro nasledne znovu uziti
                if (body <= 21)     //3. fáze, výklad karet robota
                {
                    Console.WriteLine("Blahopřeji, nepřestřelil jsi. Teď bude vykládat karty tvůj oponent, robot.");
                    Console.ReadLine();
                    while (bodyRobota <= body && bodyRobota != 21)
                    {
                        randomPocitadlo = r.Next(0, 13);
                        randomBarva = r.Next(0, 4);
                        kartyRobota[x] = nazvyKaret[randomPocitadlo];
                        barvyKaretRobota[x] = barva[randomBarva];
                        for (i = 1; i <= 14; i++)
                        {
                            while ((barvyKaretRobota[x] == barvyKaretRobota[i]) && (kartyRobota[x] == kartyRobota[i]) && (x != i))   //neopakovani karet robota
                            {
                                randomPocitadlo = r.Next(0, 13);
                                randomBarva = r.Next(0, 4);
                                kartyRobota[x] = nazvyKaret[randomPocitadlo];
                                barvyKaretRobota[x] = barva[randomBarva];
                            }
                            while ((barvyKaretRobota[x] == barvyKaretHrace[i]) && (kartyRobota[x] == kartyHrace[i]) && (x != i))
                            {
                                randomPocitadlo = r.Next(0, 13);
                                randomBarva = r.Next(0, 4);
                                kartyRobota[x] = nazvyKaret[randomPocitadlo];
                                barvyKaretRobota[x] = barva[randomBarva];
                            }
                        }
                        bodyRobota = bodyRobota + hodnotyKaret[randomPocitadlo];
                        Console.WriteLine("Robotova {0}. karta je {1}{2}.", x, barvyKaretRobota[x], kartyRobota[x]);
                        Console.WriteLine("Celkový počet bota je {0}", bodyRobota);
                        Console.ReadLine();
                        x++;
                    }
                    if (bodyRobota > 21)    //Robot přesáhl limit
                    {
                        Console.WriteLine("Gratuluji, vyhrál jsi");
                        kredit = kredit + vklad;
                        Console.ReadLine();
                    }else if (bodyRobota > body && bodyRobota <= 21)     //Robot má pod 21 bodů a stále má více než hráč
                    {
                        Console.WriteLine("Bohužel, prohrál jsi, snad budeš mít více štěstí další hru.");
                        kredit = kredit - vklad;
                        Console.ReadLine();
                    }else if (bodyRobota == 21 && body == 21)
                    {
                        Console.WriteLine("Partie skončila nerozhodně, takže jsi na nule, nic neztrácíš ani nezískáváš.");
                        kredit = kredit + 0;
                        Console.ReadLine();
                    }
                    }
                Console.Write("Chcete hrát znovu?: ");      //hraní znovu
                hraniZnovu = Console.ReadLine();
                hraniZnovu = hraniZnovu.ToLower();
                while ((hraniZnovu != "ano") && (hraniZnovu != "ne"))
                {
                    Console.Write("Neplatný vstup, zadejte prosím znovu: ");
                    hraniZnovu = Console.ReadLine();
                    hraniZnovu = hraniZnovu.ToLower();
                }
                if (hraniZnovu == "ano")
                {
                    Console.WriteLine("Děkuji za vaši odpověď, přeji více štěstí v další hře.");
                    Console.ReadLine();
                    opakovani = true;
                }
                else if (hraniZnovu == "ne")
                {
                    Console.WriteLine("Děkuji za zahrátí mé hry");
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }
                }
            }    
        }
    }

