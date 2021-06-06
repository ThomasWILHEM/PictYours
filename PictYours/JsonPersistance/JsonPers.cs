using BiblioClasse;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace JsonPersistance
{
    public class JsonPers : IPersistanceManager
    {
        public string FilePath { get; private set; } = "JSON";
        public string FileName { get; private set; } = "persistance.json";
        public string PersFile => Path.Combine(FilePath, FileName);

        public JsonPers()
        {
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
        }

        public JsonPers(string filePath)
        {
            FilePath = filePath;
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);
        }

        public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            if (!File.Exists(PersFile))
            {
                Debug.WriteLine($"Le fichier de persistance {PersFile} n'existe pas");
                return (new List<Utilisateur>(), new Dictionary<Utilisateur, List<Photo>>(), new Dictionary<Photo, List<Amateur>>(), 0);
            }
            var json = File.ReadAllText(PersFile);

            var data = JsonConvert.DeserializeObject<DataToPersist>(json, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = new DictionaryAsArrayResolver()
            });
            return (data.ListeUtilisateurs, data.PhotosParUtilisateurs, data.ListeUtilisateursParPhotosAimees, data.ProchainIdentifiant);
        }

        public void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            DataToPersist data = new();
            data.ListeUtilisateurs = listeUtilisateur;
            data.PhotosParUtilisateurs = photosParUtilisateurs;
            data.ListeUtilisateursParPhotosAimees = listeUtilisateursParPhotosAimees;
            data.ProchainIdentifiant = prochainIdentifiant;

            DefaultContractResolver contractResolver = new DictionaryAsArrayResolver();
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver
            });
            File.WriteAllText(PersFile, json);
        }
    }
}
