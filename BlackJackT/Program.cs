using System;

namespace BlackJackT
{
    internal class Program
    {   
        // La funzione "Main" gestisce il punteggio e la reiterazione della funzione "Sel".
        static void Main(string[] args)
        {
            int won = 0;
            int lost = 0;
            int res = 3;
            Console.WriteLine("\nBENVENUTO AL TAVOLO DI BLACKJACK\n");
            while (res == 3)
            {
                res = Sel(res, won, lost);
                if (res == 1) { lost++; }
                if (res == 2) { won++; }
                res = 3;
            }
        }
        // La funzione "Sel" gestisce la selezione tra: "Play" , "Punteggio" , "Fine".
        static int Sel(int rres, int wwon, int llost)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nPLAY / PUNTEGGIO / FINE\n");
            Console.ForegroundColor = ConsoleColor.Red;
            var input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        // Il comando "Play" fa iterare la funzione "Gioco" e resituisce il risultato.
            while (input == "PLAY" || input == "play")
            {
                rres = Gioco(rres);
                input = "";
            }
        // Il comando "Punteggio" stampa il una stringa contenete il punteggio.
            while (input == "PUNTEGGIO" || input == "punteggio")
            {
                Console.WriteLine($"\nMANI VINTE: {wwon}\nMANI PERSE: {llost}");
                input = "";
            }
        // Il comando "Fine" stampa il punteggio e termina il programma.
            while (input == "FINE" || input == "fine")
            {
                Console.WriteLine($"\nIL PUNTEGGIO FINALE E':\n\nMANI VINTE: {wwon}\nMANI PERSE: {llost}");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nGRAZIE PER AVER GIOCATO!");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
            }
            return rres;
        }
        // La funzione "Gioco" gestisce la singola partita.
        static int Gioco(int args, int result = 0)
        {
            var rd = new Random();
            List<int> c = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                c.Add(rd.Next(1, 14));
            }
            Console.WriteLine("\nLE TUE CARTE SONO:\n");
            Console.WriteLine($"{FormatHand(c)}    VALORE TOT: {HandScore(c)}");
            List<int> m = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                m.Add(rd.Next(1, 14));
            }
            Console.WriteLine("\nLE CARTE DEL TAVOLO SONO:\n");
            Console.WriteLine($"{FormatHand(m)}    VALORE TOT: {HandScore(m)}");
            while (HandScore(c) < 22)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nHIT / STAND:\n");
                Console.ForegroundColor = ConsoleColor.Red;
                var command = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (command == "HIT" || command == "hit")
                {
                    c.Add(rd.Next(1, 14));
                    Console.WriteLine("\nLE TUE CARTE SONO:\n");
                    Console.WriteLine($"{FormatHand(c)}   VALORE TOT: {HandScore(c)}");
                }
                else
                {
                    break;
                }
            }
            if (HandScore(c) < 22)
            {
                while (HandScore(m) < 17)
                {
                    m.Add(rd.Next(1, 14));
                }
                Console.WriteLine("\nLE CARTE DEL TAVOLO SONO:\n");
                Console.WriteLine($"{FormatHand(m)}   VALORE TOT: {HandScore(m)}");
                if (HandScore(m) < 22)
                {
                    if (HandScore(c) > HandScore(m) || HandScore(c) == HandScore(m))
                        if (HandScore(c) > HandScore(m))
                        {
                            result = 2;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nHAI VINTO");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nPAREGGIO");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    else
                    {
                        result = 1;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nHAI PERSO");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    result = 2;
                    Console.WriteLine("\nLA MANO DEL TAVOLO E' SCOPPIATA");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHAI VINTO");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                result = 1;
                Console.WriteLine("\nLA MANO E' SCOPPIATA");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nHAI PERSO");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return result;
        }
        // La funzione "FormatHand" restituisce i simboli corrispondenti al valore della Lista.
        static string FormatHand(List<int> l)
        {
            const string symbols = "A23456789XJQK";
            return string.Join(" | ", l.Select(x => symbols[x - 1]));
        }
        // La funzione "HandScore" restituisce la somma dei valori della Lista.
        static int HandScore(List<int> l)
        {
            int score = l.Sum(x => Math.Min(x, 10));
            bool ace = l.Any(x => x == 1);
            if (ace && score <= 10)
            {
                score += 10;
            }
            return score;
        }
    }
}