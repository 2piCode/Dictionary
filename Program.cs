using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DictionaryApp
{
    class Program
    {
        static void CreateNewDictionary(string DictionaryName)
        {
            XmlTextWriter writer = new XmlTextWriter($"{DictionaryName}.xml", Encoding.Unicode);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument();

            writer.WriteStartElement($"{DictionaryName}");

            writer.WriteEndElement();

            writer.Close();

            WriteNameDictionary(DictionaryName);
        }

        static void WriteNameDictionary(string DictionaryName)
        {
            using (StreamReader sr = new StreamReader("Dictionary.txt"))
            {
                while (!sr.EndOfStream)
                {
                    if (DictionaryName == sr.ReadLine())
                    {
                        Console.WriteLine("Such a dictionary already exists");
                        return;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter("Dictionary.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DictionaryName);
            }
        }

        static void Main(string[] args)
        {
            CreateNewDictionary("English_Russian");

            Dictionary dictionary = new Dictionary("English_Russian");

            //dictionary.CreateNewDictionary("English_Russian");
            //dictionary.AddWord("Hello", "Привет");
            dictionary.AddWord( "Word", "Программа");
            dictionary.AddWord("Word", "Слово");

            //dictionary.ReadDictionary();

            //dictionary.DeleteWord("Hello");

            dictionary.ReadDictionary();

            dictionary.ChangeWord("Word", "Power");

            //dictionary.ChangeTranslate("Hello", "Привет", "Пока");

            dictionary.ReadDictionary();
            //dictionary.DeleteWord("English_Russian", "Word");

            //dictionary.SearchTranslationWord("English_Russian", "Word");
        }
    }

    
}
