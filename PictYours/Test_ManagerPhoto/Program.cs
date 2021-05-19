using System;
using BiblioClasse;

namespace Test_ManagerPhoto
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_ManagerPhoto();
        }

        static ManagerPhoto managerPhoto = new ManagerPhoto();

        static Utilisateur utilisateur = new Amateur(
            "MARQUES","Florent","florent.marques"
            ,"mdp","/img/photo.png",DateTime.Now);

        static Photo photo1 = new Photo(
            "/img/maPhoto.png","La photo","Clermont-Ferrand",
            utilisateur,DateTime.Today,ECategorie.Nature);

        static Photo photo2 = new Photo(
            "/img/voiture.png","Ma voiture","Cournon",
            utilisateur,DateTime.Today.AddDays(1),ECategorie.Automobile);

        static void AffichePhotos()
        {
            Console.WriteLine("\n------Photos par utilisateurs-------");
            foreach (var p in managerPhoto.PhotosParUtilisateurs)
            {
                Console.WriteLine(p.Key.ToString() + " | " + p.Value.Count);
            }
            Console.WriteLine("------Mes photos-------");
            foreach (var p in utilisateur.MesPhotos)
            {
                Console.WriteLine(p);
            }
        }

        static void AffichePhotosAimees()
        {
            Console.WriteLine("\n------Utilisateurs par photos aimées-------");
            foreach (var p in managerPhoto.ListeUtilisateursParPhotosAimees)
            {
                Console.WriteLine(p.Key.ToString() + " | " + p.Value.Count);
            }
            Console.WriteLine("------Photos aimées-------");
            foreach (var p in (utilisateur as Amateur).PhotosAimees)
            {
                Console.WriteLine(p);
            }
        }

        static void Test_ManagerPhoto()
        {
            
            utilisateur.EstConnecte=true;
            try
            {

                managerPhoto.PosterUnePhoto(utilisateur, photo1);

                managerPhoto.AimerUnePhoto(utilisateur, photo1);
                try
                {
                    managerPhoto.AimerUnePhoto(utilisateur, photo2); //Ne s'ajoute pas car pas encore postée
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }

                managerPhoto.PosterUnePhoto(utilisateur, photo2);

                try
                {
                    managerPhoto.AimerUnePhoto(utilisateur, photo1); //Ne s'ajoute pas car déjà présente
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                managerPhoto.AimerUnePhoto(utilisateur, photo2);

                AffichePhotos();
                AffichePhotosAimees();

                managerPhoto.NePlusAimerUnePhoto(utilisateur, photo2);

                AffichePhotos();
                AffichePhotosAimees();

                managerPhoto.SupprimerUnePhoto(utilisateur, photo1);
                managerPhoto.SupprimerUnePhoto(utilisateur, photo2);


                AffichePhotos();
                AffichePhotosAimees();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
