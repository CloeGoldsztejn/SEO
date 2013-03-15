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
    class TreatCorpus
    {
        List<String> content;
        public void GetCorpus()
        {
            string sURL = "http://fr.wikipedia.org/wiki/Francolin_somali";
            List<String> nexturls = new List<String>();

            for (int i = 0; i < 1000; i++)
            {
                /*
                 * Create the request
                 */
                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(sURL);

                WebProxy myProxy = new WebProxy("myproxy", 80);
                myProxy.BypassProxyOnLocal = true;

                wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                Stream objStream;
                try
                {
                    objStream = wrGETURL.GetResponse().GetResponseStream();
                }
                catch (Exception)
                {
                    sURL = "http://fr.wikipedia.org" + nexturls[i];
                    Console.WriteLine("next url is : (exception) " + sURL);
                    continue;
                }

                StreamReader objReader = new StreamReader(objStream);

                /*
                 * Treat line read
                 */
                String path = @"C:\\Users\\Public\\Documents\\corpus\file" + i.ToString() + ".txt";
                FileStream fs = System.IO.File.OpenWrite(path);
                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                    {
                        //var regex = new Regex("/wiki/[^\\s<>\":]*"); //Old Regex
                        var regex = new Regex("/wiki/[a-zA-Z0-9/_():.%]*");
                        if (regex.IsMatch(sLine))
                        {
                            var myCapturedText = regex.Match(sLine).Groups[0].Value;
                            if (!nexturls.Contains(myCapturedText))
                            {
                                //Console.WriteLine("This is my captured text: {0}", myCapturedText);
                                nexturls.Add(myCapturedText);
                            }
                        }
                        fs.Write(System.Text.Encoding.UTF8.GetBytes(sLine), 0, sLine.Count());
                        //Console.WriteLine(sLine);
                    }
                }
                fs.Close();
                //Console.WriteLine("Next address is" + NextAddress);
                objReader.Close();
                sURL = "http://fr.wikipedia.org" + nexturls[i];
                Console.WriteLine("next url is : " + sURL);
                //Console.ReadLine();
            }
            //Console.ReadLine();
        }
        public void RemoveTags()
        {
            //Open a file
            string[] filePaths = Directory.GetFiles(@"C:\\Users\\Public\\Documents\\corpus\\");
            content = new List<string>();
            foreach (String elt in filePaths)
            {
                StreamReader fs = new StreamReader(elt);
                String fileContent = "";
                String line = "";
                while ((line = fs.ReadLine()) != null)
                {
                    line = Regex.Replace(line, "<[^>]*>", "");
                    fileContent += line;
                }
                content.Add(fileContent);
                fs.Close();
            }
        }


    }
}
