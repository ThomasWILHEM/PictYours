using System;
using BiblioClasse;
using Xunit;

namespace TestUnitaire
{
    public class Commercial_Test
    {
        [Fact]
        public void Test_SiteWeb()
        {
            Commercial commercial = new Commercial("Mozilla", "mozilla", "mdp",
                "mozilla.png", "mozilla.com", "description mozilla");

            Assert.Equal("mozilla.com", commercial.SiteWeb);
        }

        [Theory]
        [InlineData(-3, 0)]
        [InlineData(3, 3)]
        public void Test_NombreDeVisites(int input, int expected)
        {
            Commercial commercial = new Commercial("Mozilla", "mozilla", "mdp",
                "mozilla.png", "mozilla.com", "description mozilla");

            if (input < 0)
            {
                for (int i = input; i < 0; i++)
                {
                    commercial.NombreDeVisites--;
                }
            }
            else
            {
                for (int i = 0; i < input; i++)
                {
                    commercial.NombreDeVisites++;
                }
            }

            Assert.Equal(expected, commercial.NombreDeVisites);
        }

        [Fact]
        public void Test_MettreEnAvantUnePhoto()
        {
            Commercial commercial = new Commercial("Mozilla", "mozilla", "mdp",
                "mozilla.png", "mozilla.com", "description mozilla");
            Photo p1 = new Photo("pates.png", "Description de la photo", "Clermont-Ferrand", commercial, DateTime.Now, ECategorie.Cuisine);
            Photo p2 = new Photo("photo", "Ceci est une photo", "Clermont-Ferrand", commercial, DateTime.Now, ECategorie.Automobile);
            commercial.AjouterPhoto(p1);
            commercial.AjouterPhoto(p2);

            commercial.MettreEnAvantUnePhoto(p2);

            Assert.Equal(p2, commercial.MesPhotos[0]);

        }
    }
}
