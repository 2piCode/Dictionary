using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DictionaryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary dictonary = new Dictionary();

            dictonary.CreateNewDictionary("English_Russian");
            dictonary.AddWord("English_Russian", "Hello", "Привет");
            dictonary.AddWord("English_Russian", "Word", "Слово");
        }
    }

    
}
