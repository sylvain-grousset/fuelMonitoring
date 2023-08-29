using System.Data;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using Fuel.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel
{
    public class Program
    {

        static IEnumerable<XElement> StreamRootChildDoc(string xmlPath)
        {
            using (XmlReader reader = XmlReader.Create(xmlPath))
            {
                reader.MoveToContent();
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "pdv")
                            {
                                XElement el = XElement.ReadFrom(reader) as XElement;
                                if (el != null)
                                    yield return el;
                            }
                            break;
                    }
                }
            }
        }


        static void Main()
        {
            DateTime dateToGet = DateTime.Now.AddDays(-1);
            //dateToGet = new DateTime(2023, 07, 02);
            //while (true)
            //{
                ZipFetcher zipFetcher = new ZipFetcher(dateToGet);
                Results resultat = new Results();

                if (zipFetcher.IsOK)
                {
                    IEnumerable<Datas> grandChildData =
                        from el in StreamRootChildDoc(zipFetcher.xmlPath + zipFetcher.xmlFileName)
                        let a = new XmlSerializer(typeof(Datas)).Deserialize(el.CreateReader())
                        select a as Datas;

                    //where(int)el.Attribute("cp") == 26000


                    foreach (Datas data in grandChildData)
                    {
                        foreach (Prix p in data.lesPrix)
                        {
                            if(dateToGet.Date == p.MiseAJour.Date)
                            {
                                resultat.addValues(p.Nom, Convert.ToDouble(p.Valeur, CultureInfo.InvariantCulture), data.cp, data.Ville);
                            }
                            
                        }
                    }

                    resultat.GetAverage(dateToGet);
                    //Console.WriteLine(dateToGet.ToString() + " : OK.");
                }
               
                //dateToGet = dateToGet.AddDays(-1);
            //}
        }
    }
}
