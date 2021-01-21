using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DictionaryApp
{
    public class Dictionary
    {
        private string fileName { get; }

        public Dictionary()
        {
            fileName = "Dictionary.xml";
        }

        public void CreateNewDictionary(string DictionaryName)
        {
            XmlTextWriter writer = new XmlTextWriter($"{DictionaryName}.xml", Encoding.Unicode);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument();

            writer.WriteStartElement($"{DictionaryName}");

            writer.WriteEndElement();

            writer.Close();
        }

        public void AddWord(string DictionaryName, string word, string translation)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            XmlNode newWord = doc.CreateElement(word.ToLower());
            XmlNode translate = doc.CreateElement("Translate");
            XmlNode wordTranslation = doc.CreateTextNode(translation.ToLower());

            translate.AppendChild(wordTranslation);
            newWord.AppendChild(translate);

            root.AppendChild(newWord);

            doc.Save($"{DictionaryName}.xml");
        }

        public void ReadDictionary(string DictionaryName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            OutputNode(root);
            Console.WriteLine();
        }

        public void SearchTranslationWord(string DictionaryName, string word)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if(node.Name == word.ToLower())
                {
                    OutputNode(node);
                    break;
                }
            }
            Console.WriteLine();
        }

        private void OutputNode(XmlNode root, int indent = 0)
        {
            Console.Write($"{new string('\t', indent)}{root.LocalName} ");

            foreach (var child in root.ChildNodes)
            {
                if (child is XmlElement node)
                {
                    Console.WriteLine();
                    OutputNode(node, indent + 1);
                }

                if (child is XmlText text)
                {
                    Console.Write($"- {text.Value}");
                }
            }
        }

    }
}
