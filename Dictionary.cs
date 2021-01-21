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
            bool wordIsInDictionary = false;

            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            XmlNode wordPosition = null;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == word.ToLower())
                {
                    wordPosition = node;
                    wordIsInDictionary = true;
                    break;
                }
            }

            if (wordIsInDictionary && CheckTranslation(wordPosition, translation))
            {
                Console.WriteLine("This translation is already in the dictionary");
                return;
            }

            if (wordPosition == null)
                wordPosition = doc.CreateElement(word.ToLower());

            XmlNode translate = doc.CreateElement($"A{wordPosition.ChildNodes.Count + 1}");
            XmlNode wordTranslation = doc.CreateTextNode(translation.ToLower());

            translate.AppendChild(wordTranslation);

            wordPosition.AppendChild(translate);
           
            if(!wordIsInDictionary)
                root.AppendChild(wordPosition);

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

        public void DeleteWord(string DictionaryName, string word)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if(node.Name == word.ToLower())
                {
                    root.RemoveChild(node);
                    Console.WriteLine("Removal done");
                    break;
                }
            }

            doc.Save($"{DictionaryName}.xml");
        }

        public void DeleteTranslate(string DictionaryName, string word, string translate)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load($"{DictionaryName}.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == word.ToLower() && node.ChildNodes.Count > 1)
                {
                    foreach (XmlNode childNode in node.ChildNodes) 
                        {
                        if (childNode.FirstChild.Value.ToLower() == translate.ToLower())
                        {
                            node.RemoveChild(childNode);
                            Console.WriteLine("Removal done");
                            break;
                        }
                    }
                }
            }

            doc.Save($"{DictionaryName}.xml");
        }
        
        private bool CheckTranslation( XmlNode wordPosition, string translation)
        { 
            foreach (XmlNode node in wordPosition.ChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Value == translation.ToLower())
                        return true;
                }
            }
            return false;
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
