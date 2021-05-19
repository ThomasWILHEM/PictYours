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
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.Equal("Doe", a.Prenom);
        }

        [Fact]
        public void Test_Avec_Prenom_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", null, "johndoe", "mdp", "img/amateur.png", DateTime.Now));
        }

        [Fact]
        public void Test_PhotosAimees()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.NotNull(a.PhotosAimees);
        }

        //==================================================

        [Fact]
        public void Test_AimerPhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, 0, "p1", ECategorie.Automobile);
            a.AimerPhoto(p);

            Assert.Contains(p, a.PhotosAimees);
        }

        [Fact]
        public void Test_NePlusAimerPhoto()
        {
            Amateur a = new Amateur("John", "Doe", "johndoe", "img/amateur.png", "mdp", DateTime.Now);
            Photo p = new Photo("img/photo", "Ceci est une photo", "Clermont-Ferrand", a, DateTime.Now, 0, "p1", ECategorie.Automobile);
            a.AimerPhoto(p);
            a.NePlusAimerPhoto("p1");
            Assert.DoesNotContain(p, a.PhotosAimees);
        }
    }
}
