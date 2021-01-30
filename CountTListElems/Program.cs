using System;
using System.Collections.Generic;
using System.Linq;

namespace CountTListElems
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<int> collection = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                collection.Add(rnd.Next(1, 11));
            }
            //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
            //а) для целых чисел:
            //б) *для обобщенной коллекции;
            //в) *используя Linq.
            CountDistinctElems(collection);
            Console.ReadLine();
        }

        static void CountDistinctElems<T>(List<T> collection)
        {
            Console.WriteLine("Коллекция целых чисел от 1 до 10:");

            var sortedElems = from el in collection
                              orderby el ascending
                              select el;

            var distinctSortedElems = from el in sortedElems
                                      group el by el;

            foreach(IGrouping<T, T> elemGroup in distinctSortedElems)
            {
                Console.WriteLine("Число: {0} встречается: {1}", elemGroup.Key, collection.Count(el => el.Equals(elemGroup.Key)));
            }

            //collection.Sort();
            //foreach (T elem in collection.Distinct())
            //{
            //    Console.WriteLine("Число: {0} встречается: {1}", elem, collection.Count(el => el.Equals(elem)));
            //}

        }

    }
}
