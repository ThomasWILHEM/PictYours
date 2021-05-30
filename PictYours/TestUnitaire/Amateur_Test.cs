using BiblioClasse;
using System;
using Xunit;

namespace TestUnitaire
{
    public class Amateur_Test
    {
        [Fact]
        public void Test_Prenom()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Assert.Equal("Doe", a.Prenom);
        }

        [Fact]
        public void Test_Avec_Prenom_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", null, "johndoe", "mdp", "amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_PhotosAimees()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Assert.NotNull(a.PhotosAimees);
        }

        //==================================================

        [Fact]
        public void Test_AimerPhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "amateur.png", DateTime.Now);
            Photo p = new Photo("photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, ECategorie.Automobile);
            a.AjouterPhoto(p); // Etape essentielle pour pouvoir aimer une photo
            a.AimerPhoto(p);
            Assert.Contains(p, a.PhotosAimees);
        }

        [Fact]
        public void Test_NePlusAimerPhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "amateur.png", "mdp", DateTime.Now);
            Photo p = new Photo("photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, ECategorie.Automobile);
            a.AjouterPhoto(p); // Etape essentielle pour pouvoir aimer une photo
            a.AimerPhoto(p);
            a.NePlusAimerPhoto(p.Identifiant);
            Assert.DoesNotContain(p, a.PhotosAimees);
        }
    }
}
