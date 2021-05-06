using BiblioClasse;
using Xunit;
using System;

namespace TestUnitaire
{
    public class Photo_Test
    {
        Utilisateur utilisateur = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
        //Test du constructeur

        [Fact]
        public void Test_CheminPhoto()
        {
            Photo photo = new Photo("/img/pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal("/img/pates.png", photo.CheminPhoto);
        }

        [Fact]
        public void Test_Null_CheminPhoto()
        {
            Assert.Throws<ArgumentNullException>(() => new Photo(null, "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine));
        }

        [Fact]
        public void Test_Description()
        {
            Photo photo = new Photo("/img/pates.png", "Description de la photo", "Clermont-Ferrand", utilisateur, DateTime.Now, ECategorie.Cuisine);
            Assert.Equal("Description de la photo", photo.Description);
        }

        [Fact]
        public void Test_Augmenter_Jaimes()
        {
            Photo photo = new Photo("","","",new Commercial("Commercial","commercial","mdp","img/photo.png","google.fr"),DateTime.Now,ECategorie.Autre);
            Assert.Equal(0, photo.NbJaimes);
            photo.AugmenterJaimes();
            Assert.Equal(1, photo.NbJaimes);
        }

        [Fact]
        public void Test_Diminuer_Jaimes()
        {
            Photo photo = new Photo("", "", "", new Commercial("Commercial", "commercial", "mdp", "img/photo.png", "google.fr"), DateTime.Now, ECategorie.Autre);
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
