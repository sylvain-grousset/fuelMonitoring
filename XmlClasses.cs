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
        //Attributs de pdv
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("latitude")]
        public string Latitude { get; set; }

        [XmlAttribute("longitude")]
        public string Longitude { get; set; }

        [XmlAttribute("cp")]
        public string cp { get; set; }

        //Elements fils de pdv
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
