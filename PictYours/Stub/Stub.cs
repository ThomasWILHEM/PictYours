using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        Amateur florent = new Amateur("Marques", "Florent", "flomSStaar", "IUT63", "florent.JPG", "Je suis Florent Marques et je suis actuellement à l'IUT informatique de Clermont Ferrand", DateTime.Now.AddYears(-18));
        Amateur tulipe = new Amateur("Tulipe", "Estelle", "estelletulipe", "mdp", "estelle_rond.png", "Description d'Estelle Tulipe", DateTime.Now.AddYears(-15));
        Amateur thomas = new Amateur("Wilhem", "Thomas", "Atrium", "IUT63", "thomas.jpg", "Je m'appelle Thomas, j'ai 18 ans et je suis en étide d'Informatique à Clermont Ferrand", new DateTime(2003,02,26));
        Amateur daniel = new Amateur("Craig", "Daniel", "JamesBond", "007", "danielpp.jpg", "\"Mr Bond, vous avez la fâcheuse habitude de survivre\"", new DateTime(1968, 03, 02));
        Commercial mozilla = new Commercial("Mozilla", "mozilla", "mieuxquechrome", "mozilla.png", "mozilla.fr", "Firefox - le navigateur indépendant soutenu par une organisation à but non lucratif.");

        private string imagesStub = "images_stub";
        private string destImages = "images";

        public Stub()
        {
            if(Directory.Exists(destImages)) Directory.Delete(destImages,true);
            Directory.Move(imagesStub, destImages);
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
            return new List<Utilisateur> { florent, tulipe, thomas, daniel,mozilla };
        }

        private Dictionary<Utilisateur, List<Photo>> ChargePhotos()
        {
            Dictionary<Utilisateur, List<Photo>> Dico = new Dictionary<Utilisateur, List<Photo>>();
            Dico.Add(florent, new List<Photo> {
                new Photo("iut.jpg", "IUT de Clermont Ferrand", "Cezeaux", florent, new DateTime(2020,09,03), ECategorie.Autre),
                new Photo("uca.png", "Le début d'une aventure", "IUT Informatique Clermont Ferrand", florent, new DateTime(2020,09,03), ECategorie.Autre),
               
            });

            Dico.Add(tulipe, new List<Photo> {
                new Photo("maison.jpg", "Ma maison de vacance", "Dans le sud", tulipe, DateTime.Today.AddYears(-1), ECategorie.Autre)               
            });

            Dico.Add(thomas, new List<Photo> { 
                new Photo("interstellar.jpg", "L'un des meilleurs films de cette dernière décénie", "Au cinéma", thomas, new DateTime(2016,05,15), ECategorie.Autre),
                new Photo("astro4.jpg", "Même avec une polution lumineuse, on arrive à de bons résultats", "France", thomas, new DateTime(2017,6,23), ECategorie.Nature),
                new Photo("astro1.jpg", "Un ciel avec le soleil couchant", "France", thomas, new DateTime(2019,02,13), ECategorie.Nature), 
                new Photo("astro3.jpg", "Le ciel accompagné par la Voie Lactée", "France", thomas, new DateTime(2020,12,02), ECategorie.Nature),
                new Photo("astro2.jpg", "Une nuit étoilée", "Du côté de Bordeaux", thomas, new DateTime(2021,05,02), ECategorie.Nature)
            });

            Dico.Add(daniel, new List<Photo> { 
                new Photo("voiture.jpg", "Ma voiture d'espion", "Angleterre", daniel, DateTime.Today.AddDays(-560), ECategorie.Automobile),
                new Photo("bond.jpg", "La fin pour Daniel Craig ?", "Angleterre", daniel, DateTime.Today.AddDays(-25), ECategorie.Autre)
            });

            Dico.Add(mozilla, new List<Photo> {
                new Photo("Mozilla_logo.svg.png", "Notre nouveau logo", "Internet", mozilla, DateTime.Today.AddMonths(-1), ECategorie.Autre),
                new Photo("mozilla-firefox.jpg", "Notre nouvelle charte graphique", "Internet", mozilla, DateTime.Today.AddDays(-15), ECategorie.Autre)
            });
            return Dico;
        }
    }
}

