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
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Assert.Equal("John", a.Nom);
        }
        
        [Fact]
        public void Test_Avec_Nom_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur(null, "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_Pseudo()
        {
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Assert.Equal("johndoe", a.Pseudo);
        }

        [Fact]
        public void Test_Avec_Pseudo_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", "Doe", null, "mdp", "amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_PhotoDeProfil()
        {
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Assert.Equal("amateur.png", a.PhotoDeProfil);
        }

        [Fact]
        public void Test_Avec_PhotoDeProfil_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", "Doe", "johndoe", "mdp", null, DateTime.Now));
        }

        [Fact]
        public void Test_Description()
        {
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", "Voici ma description", DateTime.Now);
            Assert.Equal("Voici ma description", a.Description);
        }

        //==================================================

        [Fact]
        public void Test_AjoutePhoto()
        {
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Photo p = new Photo("photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, ECategorie.Automobile);
            a.AjouterPhoto(p);

            Assert.Contains(p, a.MesPhotos);
        }

        [Fact]
        public void Test_SupprimerPhoto()
        {
            Utilisateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Photo p = new Photo("photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, ECategorie.Automobile);
            a.AjouterPhoto(p);
            a.SupprimerPhoto(p.Identifiant);

            Assert.DoesNotContain(p, a.MesPhotos);
        }

        


        
    }
}
