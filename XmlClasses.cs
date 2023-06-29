using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fuel
{
    [XmlRoot("pdv")]
    public class Datas
    {
        [XmlElement("ville")]
        public string Ville { get; set; }

        [XmlElement("adresse")]
        public string Adresse { get; set; }

        [XmlElement("prix")]
        public List<Prix> lesPrix { get; set; }

    }

    public class Prix
    {
        [XmlAttribute("nom")]
        public string Nom { get; set; }

        [XmlAttribute("valeur")]
        public string Valeur { get; set; }

        [XmlAttribute("maj")]
        public DateTime MiseAJour { get; set; }

    }



}
