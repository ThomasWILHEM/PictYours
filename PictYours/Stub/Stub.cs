using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        Amateur pierre = new Amateur("Pierre", "Jean", "pierre.jean", "mdp", "user.png", "Description de Jean Pierre", DateTime.Now.AddYears(-20));
        Amateur tulipe = new Amateur("Tulipe", "Estelle", "estelletulipe", "mdp", "estelle_rond.png", "Description d'Estelle Tulipe", DateTime.Now.AddYears(-15));
        Amateur thomas = new Amateur("Wilhem", "Thomas", "Atrium", "mdp", "pp.jpg", "Description de Thomas Wilhem", DateTime.Now.AddYears(-18));
        Commercial mozilla = new Commercial("Mozilla", "mozilla", "mdp", "mozilla.png", "mozilla.fr", "Firefox - le navigateur indépendant soutenu par une organisation à but non lucratif.");

        private string imagesStub = "images_stub";
        private string imagesPictYours = "../images";

        public Stub()
        {
            if (!Directory.Exists(imagesPictYours)) Directory.CreateDirectory(imagesPictYours);
            if (!Directory.Exists($"{imagesPictYours}/profils")) Directory.CreateDirectory($"{imagesPictYours}/profils");
            if (!Directory.Exists($"{imagesPictYours}/photos")) Directory.CreateDirectory($"{imagesPictYours}/photos");

            foreach (var sourceFile in Directory.GetFiles($"{imagesStub}/profils"))
            {
                FileInfo fi = new(sourceFile);
                string destFile = $"{imagesPictYours}/profils/{fi.Name}";
                if (!File.Exists(destFile))
                {
                    File.Copy(sourceFile, destFile, true);
                }
            }

            foreach (var sourceFile in Directory.GetFiles($"{imagesStub}/photos"))
            {
                FileInfo fi = new(sourceFile);
                string destFile = $"{imagesPictYours}/photos/{fi.Name}";
                if (!File.Exists(destFile))
                {
                    File.Copy(sourceFile, destFile, true);
                }
            }
        }

        public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees()
        {
            List<Utilisateur> listeUtilisateurs = ChargeUtilisateurs();
            var dicoPhotos = ChargePhotos();
            foreach (var entry in dicoPhotos)
            {
                var utilisateur = listeUtilisateurs.Find(u => entry.Key.Pseudo == u.Pseudo);
                utilisateur.EstConnecte = true;

                //Reverse une copie de la liste de photo de l'utilisateur car lors de la méthode AjouterPhoto(),
                //la photo est inséré en première position.
                var listePhotosInversee = new List<Photo>(entry.Value);
                listePhotosInversee.Reverse();
                foreach (var photo in listePhotosInversee)
                {
                    utilisateur.AjouterPhoto(photo);
                }
                utilisateur.EstConnecte = false;
            }
            //Recherche le prochain identifiant
            int nombrePhotos = 0;
            foreach (var values in dicoPhotos.Values)
            {
                nombrePhotos += values.Count;
            }

            return (listeUtilisateurs, dicoPhotos, new Dictionary<Photo, List<Amateur>>(), nombrePhotos);
        }

        public void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant)
        {
            Debug.WriteLine("Sauvegarde demandée");
        }

        private List<Utilisateur> ChargeUtilisateurs()
        {
            return new List<Utilisateur> { pierre, tulipe, thomas, mozilla };
        }

        private Dictionary<Utilisateur, List<Photo>> ChargePhotos()
        {
            Dictionary<Utilisateur, List<Photo>> Dico = new Dictionary<Utilisateur, List<Photo>>();
            Dico.Add(pierre, new List<Photo> {
                new Photo("pp.jpg", "test", "test", pierre, DateTime.Today.AddMonths(-6), ECategorie.Automobile),
                new Photo("vacances6.jpg", "test2", "test1532", pierre, DateTime.Today.AddDays(-5), ECategorie.Animal)
            });

            Dico.Add(tulipe, new List<Photo> {
                new Photo("vacances1.jpg", "test2", "test1532", tulipe, DateTime.Today.AddYears(-1), ECategorie.Sport),
                new Photo("vacances2.jpg", "test2", "test1532", tulipe, DateTime.Today, ECategorie.Cuisine),
                new Photo("vacances3.jpg", "test2", "test1532", tulipe, DateTime.Today.AddMonths(-3), ECategorie.Mode)
            });

            Dico.Add(thomas, new List<Photo> { new Photo("montagne.jpg", "test", "test", thomas, DateTime.Today, ECategorie.Animal) });

            Dico.Add(mozilla, new List<Photo> {
                new Photo("vacances4.jpg", "test2", "test1532", mozilla, DateTime.Today.AddMonths(-1), ECategorie.Animal),
                new Photo("vacances5.jpg", "test2", "test1532", mozilla, DateTime.Today.AddDays(-14), ECategorie.Automobile),
                new Photo("user.png", "test156", "test123", mozilla, DateTime.Today, ECategorie.Sport),
                new Photo("estelle_rond.png", "test2", "test1532", mozilla, DateTime.Today, ECategorie.Nature)
            });
            return Dico;
        }
    }
}

