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
            Dictionary dictionary = new Dictionary();

            //dictionary.CreateNewDictionary("English_Russian");
            //dictionary.AddWord("English_Russian", "Hello", "Привет");
            //dictionary.AddWord("English_Russian", "Word", "Слово");

            //dictionary.ReadDictionary("English_Russian");

            dictionary.SearchTranslationWord("English_Russian", "Word");
        }
    }

    
}
