using BiblioClasse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DataContractPersistance
{
    public class DataContractPers : IPersistanceManager
    {
        public string PersName = "persistance.xml";
        public string PersPath = Path.Combine(Directory.GetCurrentDirectory(), "..//XML");
        public string PersFile => Path.Combine(PersPath, PersName);

        private DataContractSerializer Serializer { get; set; } = new DataContractSerializer(typeof(DataToPersist), new DataContractSerializerSettings() { PreserveObjectReferences = true });

        public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            if (!File.Exists(PersFile)) throw new FileNotFoundException($"Le fichier de chargement des données: {PersFile} n'existe pas");

            DataToPersist data;

            using (Stream s = File.OpenRead(PersFile))
            {
                data = Serializer.ReadObject(s) as DataToPersist;
            }

            return (data.ListeUtilisateurs, data.PhotosParUtilisateurs, data.ListeUtilisateursParPhotosAimees, data.ProchainIdentifiant);
        }

        public void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            if (!Directory.Exists(PersPath)) Directory.CreateDirectory(PersPath);

            DataToPersist data = new DataToPersist();
            data.ListeUtilisateurs.AddRange(listeUtilisateur);
            data.PhotosParUtilisateurs = photosParUtilisateurs;
            data.ListeUtilisateursParPhotosAimees = listeUtilisateursParPhotosAimees;
            data.ProchainIdentifiant = Photo.prochainIdentifiant;

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (TextWriter tw = File.CreateText(PersFile))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, settings))
                {
                    Serializer.WriteObject(writer, data);
                }
            }
        }
    }
}
