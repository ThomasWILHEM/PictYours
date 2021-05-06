using BiblioClasse;
using System;
using Xunit;

namespace TestUnitaire
{
    /// <summary>
    /// Les tests pour la classe Utilisateur se font par le biais de la Classe Amateur car elle hérite de Utilisateur
    /// </summary>
    public class Utilisateur_Test
    {
        [Fact]
        public void Test_Nom()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.Equal("John", a.Nom);
        }

        [Fact]
        public void Test_Pseudo()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.Equal("@johndoe", a.Pseudo);
        }

        [Fact]
        public void Test_MDP()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.Equal("mdp", a.MotDePasse);
        }

        [Fact]
        public void Test_AjoutePhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, 0, "p1", ECategorie.Automobile);
            a.AjouterPhoto(p);

            Assert.Contains(p, a.MesPhotos);
        }

        [Fact]
        public void Test_SupprimerPhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, 0, "p1", ECategorie.Automobile);
            a.AjouterPhoto(p);
            a.SupprimerPhoto("p1");

            Assert.DoesNotContain(p, a.MesPhotos);
        }

        [Fact]
        public void Test_Description()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            a.Description = "Voici ma description";
            Assert.Equal("Voici ma description", a.Description);
        }

        //==================================================

        [Fact]
        public void Test_Null_Nom()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur(null, "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_Null_Prenom()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", null, "johndoe", "mdp", "img/amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_Null_MDP()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", "Doe", "johndoe", null, "img/amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_Null_PhotoDeProfil()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", "Doe", "johndoe", "mdp", null, DateTime.Now));
        }
    }
}
