using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetCorpus
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<String, String> dico = LoadDictionary.getDictionary(@"C:\\Users\\Cloe\\Downloads\\dico\\dico\\");
            foreach (var item in dico)
            {
                Console.WriteLine(item.Key + " : " + item.Value);
            }
            Console.ReadLine();
            TreatCorpus tc = new TreatCorpus();
            tc.RemoveTags();
        }
    }
}
