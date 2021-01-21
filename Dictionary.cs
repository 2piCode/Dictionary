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
        private string DictionaryName { get; }

        private XmlDocument doc;

        public Dictionary(string DictionaryName)
        {
            this.DictionaryName = DictionaryName;

            try
            {
                doc = new XmlDocument();
                doc.Load($"{DictionaryName}.xml");
            }
            catch (Exception)
            {

                Console.WriteLine("First, create a dictionary using the function: CreateNewDictionary");
                throw;
            }
        }
        
        public void AddWord(string word, string translation)
        {
            bool wordIsInDictionary = false;

            XmlNode root = doc.DocumentElement;

            XmlNode wordPosition = null;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name.ToLower() == word.ToLower())
                {
                    wordPosition = node;
                    wordIsInDictionary = true;
                    break;
                }
            }

            if (wordIsInDictionary && CheckTranslationInDictinoary(wordPosition, translation))
            {
                Console.WriteLine("This translation is already in the dictionary");
                return;
            }

            if (wordPosition == null)
                wordPosition = doc.CreateElement(word.ToLower());

            XmlNode translate = doc.CreateElement($"A{wordPosition.ChildNodes.Count + 1}");
            XmlNode wordTranslation = doc.CreateTextNode(translation);

            translate.AppendChild(wordTranslation);

            wordPosition.AppendChild(translate);
           
            if(!wordIsInDictionary)
                root.AppendChild(wordPosition);

            doc.Save($"{DictionaryName}.xml");
        }

        public void ReadDictionary() 
        { 

            XmlNode root = doc.DocumentElement;

            OutputNode(root);
            Console.WriteLine();
        }

        public void SearchTranslationWord(string word)
        {
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if(node.Name.ToLower() == word.ToLower())
                {
                    OutputNode(node);
                    break;
                }
            }
            Console.WriteLine();
        }

        public void ChangeWord(string word, string replacment)
        {
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name.ToLower() == word.ToLower())
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AddWord(replacment, childNode.FirstChild.Value);
                    }
                    DeleteWord(word);
                }
            }
        }

        public void ChangeTranslate(string word, string translate,string replacment)
        {
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name.ToLower() == word.ToLower())
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.FirstChild.Value.ToLower() == translate.ToLower())
                        {
                            childNode.FirstChild.Value = replacment;
                            doc.Save($"{DictionaryName}.xml");
                            return;
                        }
                    }
                }
            }
        }

        public void DeleteWord(string word)
        {
            XmlNode root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if(node.Name.ToLower() == word.ToLower())
                {
                    root.RemoveChild(node);
                    Console.WriteLine("Removal done");
                    break;
                }
            }

            doc.Save($"{DictionaryName}.xml");
        }

        public void DeleteTranslate(string word, string translate)
        {
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
        
        private bool CheckTranslationInDictinoary(XmlNode wordPosition, string translation)
        { 
            foreach (XmlNode node in wordPosition.ChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Value.ToLower() == translation.ToLower())
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
