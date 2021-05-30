using BiblioClasse;
using Xunit;
using System;

namespace TestUnitaire
{
    public class Photo_Test
    {
        Utilisateur utilisateur = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);

        [Fact]
        public void Test_CheminPhoto()
        {
            Photo photo = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal("pates.png", photo.CheminPhoto);
        }

        [Fact]
        public void Test_Avec_CheminPhoto_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Photo(null, "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine));
        }

        [Fact]
        public void Test_Description()
        {
            Photo photo = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal("Description de la photo", photo.Description);
        }

        [Fact]
        public void Test_Lieu()
        {
            Photo photo = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal("Clermont-Ferrand", photo.Lieu);
        }

        [Fact]
        public void Test_Avec_Lieu_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Photo("pates.png", "Description de la photo", null, utilisateur, DateTime.Now, ECategorie.Cuisine));
        }

        [Fact]
        public void Test_Proprietaire()
        {
            Photo photo = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal(utilisateur, photo.Proprietaire);
        }

        [Fact]
        public void Test_Avec_Proprietaire_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", null, DateTime.Now, ECategorie.Cuisine));
        }

        [Fact]
        public void Test_Categorie()
        {
            Photo photo = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal(ECategorie.Cuisine, photo.Categorie);
        }

        //==================================================

        [Fact]
        public void Test_Augmenter_Jaimes()
        {
            Photo photo = new Photo("d","d","d", utilisateur, DateTime.Now,ECategorie.Autre);
            Assert.Equal(0, photo.NbJaimes);
            photo.AugmenterJaimes();
            Assert.Equal(1, photo.NbJaimes);
        }

        [Fact]
        public void Test_Diminuer_Jaimes()
        {
            Photo photo = new Photo("d", "d", "d", utilisateur, DateTime.Now, ECategorie.Autre);
            Assert.Equal(0, photo.NbJaimes);
            photo.DiminuerJaimes();
            Assert.Equal(0, photo.NbJaimes);
            photo.AugmenterJaimes();
            photo.AugmenterJaimes();
            photo.AugmenterJaimes();
            Assert.Equal(3, photo.NbJaimes);
            photo.DiminuerJaimes();
            Assert.Equal(2, photo.NbJaimes);
        }
    }
}
