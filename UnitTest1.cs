using System;
using ProjetInfo_Crochet_Watrigant;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjetInfo_Crochet_Watrigant
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            int[] tab = new int[] { 1, 0, 1, 0, 0, 0, 0, 1 };
            int[] tab1 = new int[] { 0, 0, 3, 4 };
            int[] octet_res = MyImage.Convertir_Int_To_Binaire(161, 8);
            int[] inverse = MyImage.Inversion_Octet(tab);
            int[] byte_res = MyImage.Convertir_Int_To_Byte(772, 4);
            int[] couleur = MyImage.Convertir_Couleur_To_Binaire(245);
            int[] couleur_res = new int[] { 1, 1, 1, 1, 0, 1, 0, 1 };

            int attendu = 161;
            Assert.AreEqual(attendu, MyImage.Convertir_Binaire_to_int(tab));

            attendu = 772;
            Assert.AreEqual(attendu, MyImage.Big_to_int(tab1));
            Assert.AreEqual(MyImage.Little_to_int(MyImage.Inversion_Octet(tab1)), attendu);

            for (int i = 0; i < tab.Length; i++)
            {
                Assert.AreEqual(inverse[i], tab[tab.Length - i - 1]);
                Assert.AreEqual(couleur[i], couleur_res[i]);
                Assert.AreEqual(tab[i], octet_res[i]);
            }

            for (int i = 0; i < tab1.Length; i++)
            {
                Assert.AreEqual(byte_res[i], tab1[tab1.Length - i - 1]);
            }

        }
    }
}