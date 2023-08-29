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

        HashSet<(string cp, string ville)> lesCommunes = new HashSet<(string cp, string ville)>(); 

        CarburantContext _context = new CarburantContext();

        public Results()
        {

        }

        public void addValues(string nom, double valeur, string codePostal, string ville)
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

            lesCommunes.Add((codePostal, ville));

        }

        public void GetAverage(DateTime date)
        {
            List<PrixCarburantFrance> lesPrix = new List<PrixCarburantFrance>();

            //Insertion des valeurs
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

            //Insertion des communes
            foreach(var uneCommune in lesCommunes)
            {
                Commune existingCommune = _context.Communes.Where(t => t.CodePostal == uneCommune.cp).FirstOrDefault();

                if(existingCommune == null)
                {
                    _context.Communes.Add(new Commune { CodePostal = uneCommune.cp, Ville = uneCommune.ville });
                    _context.SaveChanges();
                }
            }

            _context.PrixCarburantFrance.AddRange(lesPrix);
            _context.SaveChanges();
            

        }
    }

    
}
