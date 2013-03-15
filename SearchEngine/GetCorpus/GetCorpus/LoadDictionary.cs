using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCorpus
{
    class LoadDictionary
    {
        //TODO : penser a oter les accents et les stop_words
        public static Dictionary<String, String> getDictionary(String folder)
        {
           Dictionary<String, String> dico = new Dictionary<String, String>();
            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                String line = "";
                StreamReader sr = new StreamReader(file);               
                while((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new Char[] { ' ', '\t'});
                    if (!dico.ContainsKey(words[0]))
                        dico.Add(words[0], words[1]);
                }
            }
            return dico;
        }
    }
}
