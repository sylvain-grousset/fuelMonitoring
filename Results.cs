using Fuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel
{

    public class Results
    {

        private class t
        {
            public double valeur;
            public int count;

            public t(double valeur, int count)
            {
                this.valeur = valeur;
                this.count = count;
            }
            public double Average => valeur / count;

        }

        Dictionary<string, t> mesValeurs = new Dictionary<string, t>();

        CarburantContext _context = new CarburantContext();

        public Results()
        {

        }

        public void addValues(string nom, double valeur)
        {
            if (this.mesValeurs.TryGetValue(nom, out t a))
            {
                a.count++;
                a.valeur += valeur;
            }
            else
            {
                this.mesValeurs.Add(nom, new t(valeur, 1));
            }
        }

        public void GetAverage()
        {
            List<Histo> lesNouveauxHisto = new List<Histo>();
            foreach(var nom in this.mesValeurs.Keys)
            {
                Console.WriteLine(nom +" : "+this.mesValeurs[nom].Average);

            }
        }
    }

    
}
