using BiblioClasse;
using JsonPersistance;
using Stub;
using System;
using System.Collections.Generic;

namespace JsonPersTest
{
    class JsonPersTest
    {
        public IPersistanceManager Persistance { get; private set; }

        public JsonPersTest(IPersistanceManager persistance)
        {
            Persistance = persistance;
        }

        (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            return ((List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant))(Persistance?.ChargeDonnees());
        }

        void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            Persistance.SauvegardeDonnees(listeUtilisateur, photosParUtilisateurs, listeUtilisateursParPhotosAimees, prochainIdentifiant);
        }

        static void Main(string[] args)
        {
            List<Utilisateur> listeUtilisateurs;
            Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs;
            Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees;
            int prochainIdentifiant;

            var Manager = new JsonPersTest(new Stub.Stub());
            var data = Manager.ChargeDonnees();
            listeUtilisateurs = data.listeUtilisateurs;
            photosParUtilisateurs = data.photosParUtilisateurs;
            listeUtilisateursParPhotosAimees = data.listeUtilisateursParPhotosAimees;
            prochainIdentifiant = data.prochainIdentifiant;

            Manager.Persistance = new JsonPers("../../../JSON");
            Manager.SauvegardeDonnees(listeUtilisateurs, photosParUtilisateurs, listeUtilisateursParPhotosAimees, prochainIdentifiant);

            data = Manager.ChargeDonnees();
            listeUtilisateurs = data.listeUtilisateurs;
            photosParUtilisateurs = data.photosParUtilisateurs;
            listeUtilisateursParPhotosAimees = data.listeUtilisateursParPhotosAimees;
            prochainIdentifiant = data.prochainIdentifiant;

            foreach (var entry in photosParUtilisateurs)
            {
                Console.WriteLine(entry.Key);
                foreach (var p in entry.Value)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
