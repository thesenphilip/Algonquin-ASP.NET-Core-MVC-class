/*
Name: Philip Thesen
Student ID: 040797646
Class:  CST8359 lab# 304
Due Date: May 29, 2021
 */



using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
namespace Lab1

{
    class Program
    {
      
        static void Main(string[] args)
        {
            IList<string> words = new List<string>();
            string menuChoice;
          
            do
            {
                Menu();
                menuChoice = Console.ReadLine();
                Console.Clear();
                switch (menuChoice)
                {
                    case "1":
                        ImportWords(words);
                        break;
                    case "2":
                        BubbleSort(words);
                        break;
                    case "3":
                        LamdaSort(words);
                        break;
                    case "4":
                        CountDistinctWords(words);
                        break;
                    case "5":
                        TakeFirstTenWords(words);
                        break;
                    case "6":
                        NumWordsStartsWithJ(words);
                        break;
                    case "7":
                        WordsEndingWithD(words);
                        break;
                    case "8":
                        WordsGreaterThanFour(words);
                        break;
                    case "9":
                        WordsLessThanThree(words);
                        break;
                    case "x":
                        
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Input\n\n\n");
                        Console.ResetColor();
                        break;
                }

            } while (!menuChoice.Equals("x"));
         
        }

        public static void Menu()
        {
            Console.WriteLine("Hello World!! My First C# App \n");
            Console.WriteLine("Options ");
            Console.WriteLine("------------------------------\n");
            Console.WriteLine("1 - Import Words from File ");
            Console.WriteLine("2 - Bubble Sort words ");
            Console.WriteLine("3 - LINQ / Lambda sort words ");
            Console.WriteLine("4 - Count the Distinct Words ");
            Console.WriteLine("5 - Take the first 10 words ");
            Console.WriteLine("6 - Get the number of words that start with 'j' and display the count ");
            Console.WriteLine("7 - Get and display of words that end with 'd' and display the count ");
            Console.WriteLine("8 - Get and display of words that are greater than 4 characters long, and display the count ");
            Console.WriteLine("9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count ");
            Console.WriteLine("x – Exit\n");
            Console.Write("Make your selection: ");
        }

        public static void ImportWords(IList<string> words)
        {

            if (words.Count() > 0)
            {
                Console.WriteLine("File has already been imported");
                Console.WriteLine("Current word count is: " + words.Count() + "\n");
            }
            else
            {
                Console.WriteLine("Reading Words");
                try
                {
                    String line;
                    using (StreamReader stream = new StreamReader("Words.txt"))
                    {
                        while ((line = stream.ReadLine()) != null)
                        {
                            words.Add(line);
                        }
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not open file");
                    Console.WriteLine(e.Message);

                }
                Console.WriteLine("Reading Words complete");
                Console.WriteLine("Number of words found: " + words.Count() + "\n");
            }

        }

        public static IList<string> BubbleSort(IList<string> words)
        {

            IList<string> tempList = words.ToList();

            String tempString;
            int count = tempList.Count();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < count-1; i++)
             {
                 for (int j = i+ 1; j < count; j++)
                 {
                     if (String.Compare(tempList.ElementAt(i), tempList.ElementAt(j)) >0)
                     {
                         tempString = tempList.ElementAt(i);
                         tempList[i] = tempList[j];
                         tempList[j] = tempString;
                     }
                 }
             }

            timer.Stop();
            Console.WriteLine("Time elapsed " + timer.Elapsed.TotalMilliseconds + " ms");

            return tempList;
        }
        public static void LamdaSort(IList<string> words)
        {

            Stopwatch timer = new Stopwatch();
            timer.Start();

            var sort = from word in words
                       orderby word
                       select word;

            timer.Stop();

            Console.WriteLine("Time elapsed " + timer.Elapsed.TotalMilliseconds + " ms");
           
        }
        public static void CountDistinctWords(IList<string> words)
        {
            var distinct = (from word in words
                       select word).Distinct().Count();

            Console.WriteLine("The number of distinct word is:  " + distinct);
        }
        public static void TakeFirstTenWords(IList<string> words)
        {
            var firstTen = (from word in words
                            select word).Take(10).ToList();

            for (int i = 0; i < firstTen.Count(); i++)
            {
                Console.WriteLine(firstTen.ElementAt(i));
            }

          Console.WriteLine("\n");

        }
        public static void NumWordsStartsWithJ(IList<string> words)
        {
            var jWords = (from word in words
                            where word.StartsWith("j")
                            select word).ToList();

            for (int i = 0; i < jWords.Count(); i++)
            {
                Console.WriteLine(jWords.ElementAt(i));
            }

            Console.WriteLine("The number that start with J is:  " + jWords.Count()+ "\n");
        }
        public static void WordsEndingWithD(IList<string> words)
        {
            var dWords = (from word in words
                          where word.EndsWith("d")
                          select word).ToList();

            for (int i = 0; i < dWords.Count(); i++)
            {
                Console.WriteLine(dWords.ElementAt(i));
            }

            Console.WriteLine("The number that end with D is:  " + dWords.Count() + "\n");
        }


        public static void WordsGreaterThanFour(IList<string> words)
        {
            var greaterThanFour = (from word in words
                          where word.Length > 4
                          select word).ToList();

            for (int i = 0; i < greaterThanFour.Count(); i++)
            {
                Console.WriteLine(greaterThanFour.ElementAt(i));
            }

            Console.WriteLine("Number of words longer than 4 characters :  " + greaterThanFour.Count() + "\n");
        }

        public static void WordsLessThanThree(IList<string> words)
        {
            var lessThanThree = (from word in words
                          where word.StartsWith("a") && word.Length <3
                          select word).ToList();

            for (int i = 0; i < lessThanThree.Count(); i++)
            {
                Console.WriteLine(lessThanThree.ElementAt(i));
            }

            Console.WriteLine("Number of words longer less than 3 characters and start with 'a':  " + lessThanThree.Count() + "\n");
        }

    }
}
