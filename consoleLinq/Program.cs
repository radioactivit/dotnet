using System;
using System.Collections.Generic;
using System.Linq;
using LinqFaroShuffle;

namespace consoleLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            /*var firstTenIntegers = GetFirstTenIntegers();
            Console.WriteLine("Enumerable d'entier");
            foreach (var integer in firstTenIntegers)
            {
                Console.WriteLine(integer);
            }*/


            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                                from r in Ranks().LogQuery("Value Generation")
                                select new { Suit = s, Rank = r })
                                    .LogQuery("Starting Deck")
                                    .ToArray(); ;

            // var reverseStartingDeck = Ranks().SelectMany(s => Suits().Select(r => new { Rank = r, Suit = s }));

            // Display each card that we've generated and placed in startingDeck in the console

            /*Console.WriteLine("startingDeck");
            foreach (var card in startingDeck)
            {
                Console.WriteLine(card);
            }*/

            /*Console.WriteLine("reverseStartingDeck");
            foreach (var card in reverseStartingDeck)
            {
                Console.WriteLine(card);
            }*/



            // 52 cards in a deck, so 52 / 2 = 26    
            var halfNumber = (int)Math.Round( new decimal(startingDeck.Count() / 2));
            var top = startingDeck.Take(halfNumber);
            var bottom = startingDeck.Skip(halfNumber);

            var firstShuffle = top.InterleaveSequenceWith(bottom);

            Console.WriteLine("postShuffle");
            foreach (var c in firstShuffle)
            {
                Console.WriteLine(c);
            }

            var times = 0;
            // We can re-use the shuffle variable from earlier, or you can make a new one
            var shuffle = startingDeck;

            /*Console.WriteLine("How many shuffles to match starting order");
            times = 0;
            // We can re-use the shuffle variable from earlier, or you can make a new one
            shuffle = startingDeck;
            do
            {
                shuffle = shuffle.Take(halfNumber).InterleaveSequenceWith(shuffle.Skip(halfNumber));

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
                times++;

            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);*/


            Console.WriteLine("How many reverse shuffles to match starting order");
            times = 0;
            // We can re-use the shuffle variable from earlier, or you can make a new one
            shuffle = startingDeck;
            do
            {
                shuffle = shuffle.Skip(halfNumber).LogQuery("Bottom Half")
                    .InterleaveSequenceWith(shuffle.Take(halfNumber).LogQuery("Top Half"))
                    .LogQuery("Shuffle")
                    .ToArray();

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
                times++;

            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);


        }

        static IEnumerable<int> GetFirstTenIntegers()
        {
            for (int i = 1; i <= 10; i++)
            {
                yield return i;
            }
        }

        static IEnumerable<string> Suits()
        {
            Console.WriteLine("Suits");
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<string> Ranks()
        {
            Console.WriteLine("Ranks");
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
