﻿using Fuel.Models;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace Fuel
{
    internal class ZipFetcher
    {
        private string url = "https://donnees.roulez-eco.fr/opendata/jour/";

        private string zipPath = @"C:\Users\sgrousset\Downloads\";
        public string xmlPath = @"C:\Users\sgrousset\Downloads\";

        private string zipFileName = "";
        public string xmlFileName = "";

        private CarburantContext _context = new CarburantContext();

        /// <summary>
        ///     Constructeur de base pour récupérer un zip pour J-1.
        /// </summary>
        public ZipFetcher()
        {
            checkExisting(DateTime.Now);
            FetchZip();
            Unzip();
        }

        /// <summary>
        ///     Constructeur utilisé pour récupérer un zip pour une date donnée.
        /// </summary>
        /// <param name="zipDate"></param>
        public ZipFetcher(DateTime zipDate)
        {
            this.url += zipDate.ToString("yyyyMMdd");

            checkExisting(zipDate);
            FetchZip();
            Unzip();
        }

        /// <summary>
        ///     Vérifie si il existe déjà une entrée dans la BDD pour la date donnée.
        /// </summary>
        /// <param name="zipDate"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void checkExisting(DateTime zipDate)
        {
            if (this._context.Histos.Any(t => t.Date == DateOnly.FromDateTime(zipDate)))
            {
                throw new InvalidOperationException("Une valeur est déjà présente à la date du "+ zipDate.ToString());
            }
        }

        /// <summary>
        ///     Télécharge & sauvegarde le fichier zip.
        /// </summary>
        private void FetchZip()
        {
            using (HttpClient client = new HttpClient())
            {
                //Requête http
                HttpResponseMessage response = client.GetAsync(this.url).Result;

                //Récupération du nom du fichier zip téléchargé.
                this.zipFileName = response.Content.Headers.ContentDisposition.FileName.Trim('\"');

                //Récupération des données du zip.
                byte[] data = response.Content.ReadAsByteArrayAsync().Result;

                //Écriture du zip sur le disque dur.
                File.WriteAllBytes(this.zipPath + this.zipFileName, data);
            }
        }

        /// <summary>
        ///     Dézip un zip.
        /// </summary>
        private void Unzip()
        {
            using (ZipArchive archive = ZipFile.OpenRead(this.zipPath + this.zipFileName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //Je récupère le nom du fichier à l'intérieur du zip.
                    this.xmlFileName = entry.FullName;

                    //J'extrais le zip et overwrite le fichier si il existe.
                    entry.ExtractToFile(Path.Combine(this.xmlPath, entry.FullName), true);
                }
            }
        }


    }
}