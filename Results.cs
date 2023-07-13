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
            List<PrixCarburantFrance> lesPrix = new List<PrixCarburantFrance>();

            foreach(string nom in this.mesValeurs.Keys)
            {
                PrixCarburantFrance unPrix = new PrixCarburantFrance();
                TypesCarburant monCarburant = _context.TypesCarburants.Where(t => t.Type == nom).FirstOrDefault();
                unPrix.PrixMoyen = Convert.ToDouble(this.mesValeurs[nom].Average.ToString("0.000"));
                unPrix.Date = DateOnly.FromDateTime(date);
                unPrix.IdCarburantNavigation = monCarburant;
                unPrix.IdCarburant = monCarburant.Id;
                lesPrix.Add(unPrix);
            }

            _context.PrixCarburantFrance.AddRange(lesPrix);
            _context.SaveChanges();
        }
    }

    
}
