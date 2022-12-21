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
            int tie = 0;
            int res = 4;
            Console.WriteLine("\nWELCOME TO THE BLACKJACK TABLE\n");
            while (res == 4)
            {
                res = Sel(res, won, lost, tie);
                if (res == 1) { lost++; }
                if (res == 2) { won++; }
                if (res == 3) { tie++; }
                res = 4;
            }
        }
        // La funzione "Sel" gestisce la selezione tra: "Play" , "Punteggio" , "Fine".
        static int Sel(int sres, int swon, int slost, int stie)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nPLAY / SCORE / EXIT\n");
            Console.ForegroundColor = ConsoleColor.Red;
            var input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        // Il comando "Play" fa iterare la funzione "Gioco" e resituisce il risultato.
            while (input == "PLAY" || input == "play")
            {
                sres = Game(sres);
                input = "";
            }
        // Il comando "Score" stampa il una stringa contenete il punteggio.
            while (input == "SCORE" || input == "score")
            {
                Console.WriteLine($"\nWON:   {swon}\nLOST:  {slost}\nTIED:  {stie}");
                input = "";
            }
        // Il comando "Exit" stampa: "THANK YOU FOR THE GAME!" e termina il programma.
            while (input == "EXIT" || input == "exit")
            {
                Console.WriteLine("\nTHANK YOU FOR THE GAME!");
                Environment.Exit(0);
            }
            return sres;
        }
        // La funzione "Game" gestisce la singola partita.
        static int Game(int args, int result = 0)
        {
            var rd = new Random();
            List<int> c = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                c.Add(rd.Next(1, 14));
            }
            Console.WriteLine("\nYOUR CARDS:\n");
            Console.WriteLine($"{FormatHand(c)}    TOT VALUE: {HandScore(c)}");
            List<int> m = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                m.Add(rd.Next(1, 14));
            }
            Console.WriteLine("\nDEALER CARDS:\n");
            Console.WriteLine($"{FormatHand(m)}    TOT VALUE: {HandScore(m)}");
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
                    Console.WriteLine("\nYOUR CARDS:\n");
                    Console.WriteLine($"{FormatHand(c)}   TOT VALUE: {HandScore(c)}");
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
                Console.WriteLine("\nDEALER CARDS:\n");
                Console.WriteLine($"{FormatHand(m)}   TOT VALUE: {HandScore(m)}");
                if (HandScore(m) < 22)
                {
                    if (HandScore(c) > HandScore(m) || HandScore(c) == HandScore(m))
                        if (HandScore(c) > HandScore(m))
                        {
                            result = 2;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nYOU WON");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            result = 3;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nTIE");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    else
                    {
                        result = 1;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nYOU LOST");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    result = 2;
                    Console.WriteLine("\nDEALER HAND HAS BUSTED");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYOU WON");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                result = 1;
                Console.WriteLine("\nYOUR HAND HAS BUSTED");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nYOU LOST");
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