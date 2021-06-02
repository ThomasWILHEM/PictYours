using BiblioClasse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonPersistance
{
    public class JsonPers : IPersistanceManager
    {
        public static string filePath = "../../../JSON";
        public static string fileName = "persistance.json";
        public static string PersFile { get; private set; } = Path.Combine(filePath, fileName);

        public JsonPers()
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        }

        public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            return (null, null, null, 0);
        }

        public void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            DataToPersist data = new();
            data.ListeUtilisateurs = listeUtilisateur;
            data.PhotosParUtilisateurs = photosParUtilisateurs;
            data.ListeUtilisateursParPhotosAimees = listeUtilisateursParPhotosAimees;
            data.ProchainIdentifiant = prochainIdentifiant;

            JsonSerializer serializer = new();
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.All;
            serializer.Formatting = Formatting.Indented;
            using(StreamWriter s = new(PersFile))
                using(JsonWriter writer = new JsonTextWriter(s))
            {
                serializer.Serialize(writer, data);
            }
            //var json = JsonConvert.SerializeObject(data);
            //File.WriteAllText(PersFile, json);
        }
    }
}
