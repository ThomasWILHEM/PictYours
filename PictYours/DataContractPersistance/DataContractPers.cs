using BiblioClasse;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DataContractPersistance
{
    public class DataContractPers : IPersistanceManager
    {
        /// <summary>
        /// Nom du fichier ou sont stockés les informations
        /// </summary>
        public string PersName { get; set; } = "persistance.xml";

        /// <summary>
        /// Chemin pour accéder au fichier
        /// </summary>
        public string PersPath => Path.Combine(Directory.GetCurrentDirectory(), RelativePath);

        /// <summary>
        /// Fichier contenant les données de l'application
        /// </summary>
        public string PersFile => Path.Combine(PersPath, PersName);

        /// <summary>
        /// Chemin relatif du fichier de persistance (Endroit où il est stocké)
        /// </summary>
        public string RelativePath { get; set; } = "XML";

        protected XmlObjectSerializer Serializer { get; set; } = new DataContractSerializer(typeof(DataToPersist), new DataContractSerializerSettings() { PreserveObjectReferences = true });

        /// <summary>
        /// Constructeur par défaut de DataContractPers
        /// </summary>
        public DataContractPers() { }

        /// <summary>
        /// Constructeur de DataContractPers en choissisant le chemin du fichier de persistance
        /// </summary>
        /// <param name="persPath">Chemin du fichier de persistance</param>
        public DataContractPers(string persPath) => RelativePath = persPath;


        /// <summary>
        /// Méthode permettant de charger les données depuis un fichier
        /// </summary>
        /// <returns>Cette méthode retourne la liste des utilisateurs, les deux dictionnaires (photosParUtilisateurs et listeUtilisateursParPhotosAimees) et le prochain identifiant de photo</returns>
        public virtual (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            if (!File.Exists(PersFile))
            {
                Debug.WriteLine($"Le fichier de chargement des données: {PersFile} n'existe pas");
                return (new List<Utilisateur>(), new Dictionary<Utilisateur, List<Photo>>(), new Dictionary<Photo, List<Amateur>>(), 0);
            }

            DataToPersist data;

            using (Stream s = File.OpenRead(PersFile))
            {
                data = Serializer.ReadObject(s) as DataToPersist;
            }

            return (data.ListeUtilisateurs, data.PhotosParUtilisateurs, data.ListeUtilisateursParPhotosAimees, data.ProchainIdentifiant);
        }

        /// <summary>
        /// Méthode permettant de sauvegarder les données dans un fichier XML
        /// </summary>
        /// <param name="listeUtilisateur">Liste de tous les utilisateurs de l'application</param>
        /// <param name="photosParUtilisateurs">Dictonnaire qui possède en clé un Utilisateur et en valeur la liste de ses photos</param>
        /// <param name="listeUtilisateursParPhotosAimees">Dictionnaire qui possède en clé une photo et en valeur la liste des personnes qui ont aimées cette photo</param>
        /// <param name="prochainIdentifiant">Prochain identifiant de photo</param>
        public virtual void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
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
