using BiblioClasse;
using System;
using Xunit;

namespace TestUnitaire
{
    public class UtilisateurPrive_Test
    {
        [Fact]
        public void Test_MDP()
        {
            UtilisateurPrive a = new Amateur("John", "Doe", "johndoe", "mdp", "img/amateur.png", DateTime.Now);
            Assert.Equal("mdp", a.MotDePasse);
        }

        [Fact]
        public void Test_Null_MDP()
        {
            Assert.Throws<ArgumentNullException>(() => new Amateur("John", "Doe", "johndoe", null, "img/amateur.png", DateTime.Now));
        }
    }
}
