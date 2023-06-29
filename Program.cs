using System.Data;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using Fuel.Models;

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

            ZipFetcher zipFetcher = new ZipFetcher();
            Results resultat = new Results();

            IEnumerable<Datas> grandChildData =
            from el in StreamRootChildDoc(zipFetcher.xmlPath + zipFetcher.xmlFileName)
            where (int)el.Attribute("cp") == 26000
            let a = new XmlSerializer(typeof(Datas)).Deserialize(el.CreateReader())
            select a as Datas;


            foreach (Datas data in grandChildData)
            {

                foreach (Prix p in data.lesPrix)
                {
                    resultat.addValues(p.Nom, Convert.ToDouble(p.Valeur, CultureInfo.InvariantCulture));
                }
            }

            resultat.GetAverage();
        }
    }
}
