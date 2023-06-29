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

        public void GetAverage(DateTime date)
        {

            Histo unHisto = new Histo();

            foreach(string nom in this.mesValeurs.Keys)
            {
                switch (nom)
                {
                    case "Gazole":
                        unHisto.Gazole = Convert.ToDouble(this.mesValeurs[nom].Average.ToString("0.000"));
                        break;

                    case "SP95":
                        unHisto.Sp95 = Convert.ToDouble(this.mesValeurs[nom].Average.ToString("0.000"));
                        break;

                    case "SP98":
                        unHisto.Sp98 = Convert.ToDouble(this.mesValeurs[nom].Average.ToString("0.000"));
                        break;

                    case "E85":
                        unHisto.E85 = Convert.ToDouble(this.mesValeurs[nom].Average.ToString("0.000"));
                        break;
                }
            }

            unHisto.Date = DateOnly.FromDateTime(date);

            _context.Histos.Add(unHisto);
            _context.SaveChanges();
        }
    }

    
}
