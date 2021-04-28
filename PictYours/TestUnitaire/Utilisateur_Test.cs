using BiblioClasse;
using System;
using Xunit;

namespace TestUnitaire
{
    public class Utilisateur_Test
    {
        [Fact]
        public void Test_Nom()
        {
            Utilisateur u = new Utilisateur("John","Doe","johndoe",DateTime.Now,"mdp");
            Assert.Equal("John", u.Nom);
        }

        [Fact]
        public void Test_Prenom()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Assert.Equal("Doe", u.Prenom);
        }

        [Fact]
        public void Test_Pseudo()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Assert.Equal("@johndoe", u.Pseudo);
        }

        [Fact]
        public void Test_MDP()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Assert.Equal("mdp", u.MotDePasse);
        }

        [Fact]
        public void Test_AjoutePhoto()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe",DateTime.Now, "mdp");
            Photo p = new Photo("img/photo","Ceci est une photo","Clermont-Ferrand",u, DateTime.Now,0,"p1");
            u.AjouterPhoto(p);

            Assert.Contains(p, u.MesPhotos);
        }

        [Fact]
        public void Test_SupprimerPhoto()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", u, DateTime.Now, 0, "p1");
            u.AjouterPhoto(p);
            u.SupprimerPhoto(p);

            Assert.DoesNotContain(p, u.MesPhotos);
        }

        [Fact]
        public void Test_AimerPhoto()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", u, DateTime.Now, 0, "p1");
            u.AimerPhoto(p);

            Assert.Contains(p, u.PhotosAimees);
        }

        [Fact]
        public void Test_NePlusAimerPhoto()
        {
            Utilisateur u = new Utilisateur("John", "Doe", "johndoe", DateTime.Now, "mdp");
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", u, DateTime.Now, 0, "p1");
            u.AimerPhoto(p);
            u.NePlusAimerPhoto(p);
            Assert.DoesNotContain(p, u.PhotosAimees);
        }
    }
}
