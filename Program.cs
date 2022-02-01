using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjetInfo_Crochet_Watrigant
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int choix = 0;
            Console.Write("\n"
                            + "                       - Rendu final de Problème Scientifique et Informatique - " + "\n"
                            + "                                - Par Albert De Watrigant et Tom Crochet -          " + "\n"
                            + "                                                                                    " + "\n"
                            + "                                                BIENVENUE                                                         " + "\n");
            while (choix < 30)
            {

                //On créer un affichage soigné
                Console.Write("\n" + "\n" + "                                          Que voulez-vous faire ?                                                  " + "\n"
                             + "                                                                                    " + "\n"
                             + "               (1)                               (2)                              (3)                      " + "\n"
                             + "    * * * * * * * * * * * * * * *     * * * * * * * * * * * * *     * * * * * * * * * * * * * * * *   " + "\n"
                             + "    *                           *     *                       *     *                             *" + "\n"
                             + "    *    MODIFIER DES IMAGES    *     *    CRÉER UNE IMAGE    *     *    MANIPULER DES QR-CODE    *" + "\n"
                             + "    *                           *     *                       *     *                             *" + "\n"
                             + "    * * * * * * * * * * * * * * *     * * * * * * * * * * * * *     * * * * * * * * * * * * * * * *   " + "\n"
                             + "                                                                                    " + "\n"
                             + "                       Vous pouvez rentrer 0 à tout moment pour revenir au début.                                                                            " + "\n"
                                 + "" +
                             "\n \n");
                Console.WriteLine("Entrez le numéro associé à votre choix.");
                choix = Convert.ToInt32(Console.ReadLine());
                if (choix == 0)
                {
                    continue;
                }
                Console.WriteLine("\n" + "Vous avez choisis :");
                if (choix == 1)
                {
                    Console.WriteLine("\n"
                                + "    * * * * * * * * * * * * * * *     " + "\n"
                                + "    *                           *" + "\n"
                                + "    *    MODIFIER DES IMAGES    *" + "\n"
                                + "    *                           *" + "\n"
                                + "    * * * * * * * * * * * * * * *" + "\n");
                    Console.WriteLine("\n" + "Dans cette catégorie vous pouvez :" + "\n"
                                           + " • Traiter une image : " + "\n"
                                           + "   - Passer d'une photo en couleur à une photo en noir et blanc [1]" + "\n"
                                           + "   - Effectuer un effet miroir vertical [2]" + "\n"
                                           + "   - Effectuer un effet miroir horizontal [3]" + "\n"
                                           + "   - Effectuer une rotation de l'image [4]" + "\n"
                                           + "   - Agrandir l'image [5]" + "\n"
                                           + "   - Rétrecir l'image [6]" + "\n"
                                           + " \n"
                                           + " • Appliquer un filtre (matrice de convolution :" + "\n"
                                           + "   - Effectuer une détection des contours [7]" + "\n"
                                           + "   - Effectuer un renforcement des bords [8]" + "\n"
                                           + "   - Appliquer un flou puissant [9]" + "\n"
                                           + "   - Appliquer un flou Gaussien [10]" + "\n"
                                           + "   - Effectuer un repoussage [11]" + "\n"
                                           + " \n"
                                           + " • Utiliser nos innovations : " + "\n"
                                           + "   - Inverser les couleurs d'une image [12]" + "\n"
                                           +"    - Laisser la couleur dominante de chaque pixel [13]"+"\n"
                                           +"    - Laisser qu'une couleur sur l'image [14]"+"\n"
                                           +"    - Appliquer un filtre passe-bas [15]"+"\n"
                                           +"    - Appliquer un filtre passe-haut [16]"+"\n"
                                           +"    - Aiguiser l'image [17]"+"\n"
                                           +"    - Aiguiser l'image d'une autre façon [18]"+"\n"
                                           +"    - Assombrir ou éclaircir une image [19]"+"\n"
                                           +"    - Laisser la couleur faible de chaque pixel [20]"+"\n"
                                           
                                           );
                    choix = Convert.ToInt32(Console.ReadLine());
                    if (choix == 0)
                    {
                        continue;
                    }
                    string nomImage = ChoisirImage();
                    if (nomImage == "0")
                    {
                        continue;
                    }
                    if (choix == 1) { Noir_et_Blanc(nomImage); }
                    if (choix == 2) { MiroirV(nomImage); }
                    if (choix == 3) { MiroirH(nomImage); }
                    if (choix == 4)
                    {
                        Console.WriteLine("\n" + "De quel angle (en °) voulez-vous tourner l'image ?");
                        choix = Convert.ToInt32(Console.ReadLine());
                        if (choix == 0) { continue; }
                        Rotation_Test(choix, nomImage);
                    }
                    else
                    {
                        if (choix == 5)
                        {
                            Console.WriteLine("\n" + "De quel coefficient voulez-vous agrandir l'image ?");
                            choix = Convert.ToInt32(Console.ReadLine());
                            if (choix == 0) { continue; }
                            Agrandir_test(choix, nomImage);
                        }
                        else
                        {
                            if (choix == 6)
                            {
                                Console.WriteLine("\n" + "De quel coefficient voulez-vous rétrecir l'image ?");
                                choix = Convert.ToInt32(Console.ReadLine());
                                if (choix == 0) { continue; }
                                Retrecir_test(choix, nomImage);
                            }
                            else
                            {
                                if (choix == 7) { DetectionContours(nomImage); }
                                if (choix == 8) { RenforcementBords(nomImage); }
                                if (choix == 9) { Test_flouPuissant(nomImage); }
                                if (choix == 10) { Flou_Gaussien(nomImage); }
                                if (choix == 11) { Repoussage(nomImage); }
                                if (choix == 12) { Inversion(nomImage); }
                                if (choix == 13) { Test_dominant(nomImage); }
                                if (choix == 15) { FiltrePasseBas(nomImage); }
                                if (choix == 16) { FiltrePasseHaut(nomImage); }
                                if (choix == 17) { Test_sharpen(nomImage); }
                                if(choix == 18) { Test_sharpen1(nomImage); }
                                if (choix == 20) { Test_Faible(nomImage); }
                                else if(choix==14)
                                {
                                    Console.WriteLine("Quelle couleur souhaitez-vous laisser ? Entrez R, V ou B");
                                    string res = Console.ReadLine();
                                    Test_Mono(nomImage, res);
                                }
                                else if(choix==19)
                                {
                                    Console.WriteLine("Entrez un coefficient (négatif si vous voulez assombrir, positif si voulez éclaircir)");
                                    int val = Convert.ToInt32(Console.ReadLine());
                                    Test_Brillance(nomImage, val);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (choix == 2)
                    {
                        Console.WriteLine("\n"
                                    + "    * * * * * * * * * * * * *     " + "\n"
                                    + "    *                       *" + "\n"
                                    + "    *    CRÉER UNE IMAGE    *" + "\n"
                                    + "    *                       *" + "\n"
                                    + "    * * * * * * * * * * * * *" + "\n");
                        Console.WriteLine("\n" + "Dans cette catégorie vous pouvez :" + "\n"
                                               + " - Créer une image décrivant une fractale [1]" + "\n"
                                               + " - Créer un histogramme se rapportant à une image [2]" + "\n"
                                               + " - Coder une image dans une image [3]" + "\n"
                                               + " - Décoder une image dans une image [4] (l'image décodée est l'image que vous avez codé juste avant)" + "\n"
                                               + " - Nouvelle image créée à partir de l'addition de deux autres [5] (fonctionne très bien en additionnant coco (ou tigre) et une des images noires)"+"\n");
                        choix = Convert.ToInt32(Console.ReadLine());
                        if (choix == 0)
                        {
                            continue;
                        }
                        if (choix == 1)
                        {
                            Console.WriteLine("\n" + "Quel type de fractale voulez-vous créer ?" + "\n"
                                              + " - une fractale quelconque [1]" + "\n"
                                              + " - la fractale de Mandelbrot [2]" + "\n"
                                              + " - un ensemble de Julia [3]" + "\n");
                            choix = Convert.ToInt32(Console.ReadLine());
                            if (choix == 0)
                            {
                                continue;
                            }
                            fractale("coco.bmp", choix);
                        }
                        else
                        {
                            if (choix == 2)
                            {
                                string nomImage = ChoisirImage();
                                if (nomImage == "0")
                                {
                                    continue;
                                }
                                Console.WriteLine("Quelle couleur souhaitez-vous que l'histogramme traite ? (entrez R, V ou B)");
                                string lettre = Console.ReadLine();
                                Histo(nomImage, lettre);
                            }
                            else
                            {
                                if (choix == 3)
                                {
                                    Console.WriteLine("il faut choisir deux images de la même taille ! (tigre avec coco ou LenaRouge avec lena)");
                                    Console.WriteLine("Pour l'image d'origine :");
                                    string nomImageOri = ChoisirImage();
                                    if (nomImageOri == "0")
                                    {
                                        continue;
                                    }
                                    Console.WriteLine("Pour l'image à cacher :");
                                    string nomImageCach = ChoisirImage();
                                    if (nomImageCach == "0")
                                    {
                                        continue;
                                    }
                                    CoderImageDansUneImage(nomImageOri, nomImageCach);
                                }
                                else
                                {
                                    if (choix == 4)
                                    {
                                        Decodage("codage.bmp");
                                    }
                                    else
                                    {
                                        if (choix == 5)

                                        {
                                            Console.WriteLine("Choisissez la première image à additionner :");
                                            string nomImage1 = ChoisirImage();
                                            if (nomImage1 == "0")
                                            {
                                                continue;
                                            }
                                            Console.WriteLine("Et la deuxième image à additionner :");
                                            string nomImage2 = ChoisirImage();
                                            if (nomImage2 == "0")
                                            {
                                                continue;
                                            }
                                            AdditionImages(nomImage1, nomImage2);
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (choix == 3)
                        {
                            Console.WriteLine("\n"
                                        + "    * * * * * * * * * * * * * * * *     " + "\n"
                                        + "    *                             *" + "\n"
                                        + "    *    MANIPULER DES QR-CODE    *" + "\n"
                                        + "    *                             *" + "\n"
                                        + "    * * * * * * * * * * * * * * * *" + "\n");
                            Console.WriteLine("\n" + "Dans cette catégorie vous pouvez :" + "\n"
                                               + " - Générer un QR-Code [1]" + "\n"
                                               + " - Lire un QR-Code [2]" + "\n");
                            choix = Convert.ToInt32(Console.ReadLine());
                            if (choix == 0) { continue; }
                            if (choix == 1)
                            {
                                TestQR("qr");
                            }
                            if (choix == 2)
                            {
                                Console.WriteLine("Entrez le nom de l'image. \nEcrivez qr.bmp pour décoder le code créer précedemment ou qrV2.bmp pour une QR-Code de version 2 test");
                                string nomQR = Console.ReadLine();
                                LecteurQR(nomQR);
                            }
                        }
                    }
                }
                choix = Convert.ToInt32(Console.ReadLine());
                if (choix == 0)
                {
                    continue;
                }
            }

            Console.ReadLine();
        }

        #region FONCTIONS DEMANDEES

        static void fractale(string nom, int type)
        {
            NewImage origine = new NewImage(nom, 800, 800);
            int choix = 0;
            if (type == 1)
            {
                origine.fractaleQuelconque();
            }
            if (type == 2)
            {
                origine.Mandelbrot();
            }
            if (type == 3)
            {
                Console.WriteLine("Quel ensemble de Julia souhaitez-vous ? Entrez un nombre entre 1 et 6. ");
                choix = Convert.ToInt32(Console.ReadLine());
                if (choix == 1) { origine.EnsembleDeJulia1(); }
                if (choix == 2) { origine.EnsembleDeJulia2(); }
                if (choix == 3) { origine.EnsembleDeJulia3(); }
                if (choix == 4) { origine.EnsembleDeJulia4(); }
                if (choix == 5) { origine.EnsembleDeJulia5(); }
                if (choix == 6) { origine.EnsembleDeJulia6(); }
            }
            origine.From_Image_To_File("fractale.bmp");
            Process.Start("fractale.bmp");
        }
        static void Noir_et_Blanc(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.Filtre_NB();
            origine.From_Image_To_File("Filtre_NB.bmp");
            Process.Start("Filtre_NB.bmp");
        }
        static void DetectionContours(string nom)
        {

            int[,] noyau = { { 0, 0, 1, 1, 1, 0, 0 }, { 0, 1, 1, 1, 1, 1, 0 }, { 1, 1, -1, -4, -1, 1, 1 }, { 1, 1, -4, -8, -4, 1, 1 }, { 1, 1, -1, -4, -1, 1, 1 }, { 0, 1, 1, 1, 1, 1, 0 }, { 0, 0, 1, 1, 1, 0, 0 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 7);
            origine.From_Image_To_File("contours.bmp");
            Process.Start("contours.bmp");
        }
        static void Decodage(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.DecoderImage();
            origine.From_Image_To_File("decodage.bmp");
            Process.Start("decodage.bmp");
        }
        static void Repoussage(string nom)
        {
            int[,] noyau = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("Aiguisée.bmp");
            Process.Start("Aiguisée.bmp");
        }
        static void RenforcementBords(string nom)
        {
            int[,] noyau = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 2, 0, 0 }, { 0, 0, -1, 0, 0 }, { 0, 0, -1, 0, 0 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 5);
            origine.From_Image_To_File("Bords.bmp");
            Process.Start("Bords.bmp");
        }
        static void Agrandir_test(int coeff, string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.AgrandirImage(coeff);
            origine.From_Image_To_File("Agrandir.bmp");
            Process.Start("Agrandir.bmp");
        }
        static void Retrecir_test(int coeff, string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.RetrecirImage(coeff);
            origine.From_Image_To_File("retrecir.bmp");
            Process.Start("retrecir.bmp");
        }
        static void Rotation_Test(int angle, string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.Rotation(angle);
            origine.From_Image_To_File("Rotation.bmp");
            Process.Start("Rotation.bmp");
        }

        static void MiroirV(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.Miroir_Vertical();
            origine.From_Image_To_File("Miroir.bmp");
            Process.Start("Miroir.bmp");
        }
        static void MiroirH(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.Miroir_horizontale();
            origine.From_Image_To_File("Miroir.bmp");
            Process.Start("Miroir.bmp");
        }
        static void Affichage_fichierBMP(string nom)
        {
            byte[] fichier = File.ReadAllBytes(nom);
            Console.Write("\n Header \n");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(fichier[i] + " ");
            }
            Console.Write("\n header info \n");
            for (int i = 14; i < 54; i++)
            {
                Console.Write(fichier[i] + " ");
            }
            Console.Write("\n IMAGE \n");
            for (int i = 54; i < fichier.Length; i = i + 60)
            {
                for (int j = i; j < i + 60; j++)
                {
                    Console.Write(fichier[j] + " ");
                }
                Console.WriteLine(" ");

            }
        }
        static void Image_to_file(string nom)
        {
            MyImage test = new MyImage(nom);
            test.From_Image_To_File("TestSortie.bmp");
            Process.Start("TestSortie.bmp");
        }
        static void Test_flouPuissant(string nom)
        {
            int[,] noyau = { { 0, 1, 2, 1, 0 }, { 1, 4, 8, 4, 1 }, { 2, 8, 16, 8, 2 }, { 1, 4, 8, 4, 1 }, { 0, 1, 2, 1, 0 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("FlouP.bmp");
            Process.Start("FlouP.bmp");
        }
        static void Flou_Gaussien(string nom)
        {
            int[,] noyau = { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("Flou.bmp");
            Process.Start("Flou.bmp");
        }
        static void CoderImageDansUneImage(string ImageOrigine, string ImageACacher)
        {
            MyImage origine = new MyImage(ImageOrigine);
            MyImage cachée = new MyImage(ImageACacher);
            origine.CoderUneImageDansUneImage(cachée);
            origine.From_Image_To_File("codage.bmp");
            Process.Start("codage.bmp");
        }
        static void Histo(string nom, string lettre)
        {
            MyImage origine = new MyImage(nom);
            NewImage res = new NewImage("histo", 256, 300);
            //256 pour les abscisses car nos pixels peuvent aller de 0 à 255
            //300 pou les ordonnées, trouvé de façon expérimentale
            res.AffichageHisto(origine.Histogramme(), lettre); //on crée notre histogramme dans une nouvelle image 
            res.From_Image_To_File("AffichageHisto.bmp");
            Process.Start("AffichageHisto.bmp");
        }

        static void LecteurQR(string nom)
        {
            MyImage origine = new MyImage(nom);
            int version = origine.Version();
            int taillecase = origine.TailleCase(version);
            int[,] mat = origine.FromQrToMat(version, taillecase);
            bool recherche = origine.MotifsRechercheBienPlacés(version, mat);
            List<int[]> zone = origine.ZoneNonDécodable(version);
            List<int> chaine = origine.FromMatToChaîne(version, mat, zone);
            string message = origine.DécodageChaîne(version, chaine);
            Console.WriteLine(message);
            Process.Start(nom);
        }
        #endregion

        #region INNOVATION
        static void Inversion(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.Inversion();
            origine.From_Image_To_File("inversion.bmp");
            Process.Start("inversion.bmp");
        }
        static void AdditionImages(string nom1, string nom2)
        {
            MyImage image1 = new MyImage(nom1);
            MyImage image2 = new MyImage(nom2);
            NewImage nouvelle = new NewImage("addition", Convert.ToInt32(image1.Largeur), Convert.ToInt32(image1.Hauteur));
            nouvelle.Addition(image1, image2);
            nouvelle.From_Image_To_File("addition.bmp");
            Process.Start("addition.bmp");
        }
        static void Test_Faible(string nom)
        {
            MyImage origine = new MyImage(nom);
            origine.CouleurFaible();
            origine.From_Image_To_File("faible.bmp");
            Process.Start("faible.bmp");
        }
        static void Test_Brillance(string nom, int coeff)
        {
            MyImage origine = new MyImage(nom);
            origine.Brillance(coeff);
            origine.From_Image_To_File("brillance.bmp");
            Process.Start("brillance.bmp");
        }
        static void Test_dominant(string nom)
        {
            
            MyImage origine = new MyImage(nom);
            origine.CouleurDominante();
            origine.From_Image_To_File("dominant.bmp");
            Process.Start("dominant.bmp");
        }
        static void Test_sharpen(string nom)
        {
            int[,] noyau = { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("Sharpen.bmp");
            Process.Start("Sharpen.bmp");
        }
        static void Test_sharpen1(string nom)
        {
            int[,] noyau = { { 1, 1, 1 }, { 1, -7, 1 }, { 1, 1, 1 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("Sharpen1.bmp");
            Process.Start("Sharpen1.bmp");
        }
        static void FiltrePasseBas(string nom)
        {
            int[,] noyau = { { 1, 1, 1 }, { 1, 4, 1 }, { 1, 1, 1 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("PasseBas.bmp");
            Process.Start("PasseBas.bmp");
        }
        static void FiltrePasseHaut(string nom)
        {
            int[,] noyau = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            MyImage origine = new MyImage(nom);
            origine.Convolution(noyau, 0);
            origine.From_Image_To_File("PasseHaut.bmp");
            Process.Start("PasseHaut.bmp");
        }
        static void Test_Mono(string nom, string lettre)
        {
            MyImage origine = new MyImage(nom);
            origine.MonoCouleur(lettre);
            origine.From_Image_To_File("Mono.bmp");
            Process.Start("Mono.bmp");
        }

        

        static string ChoisirImage() //Méthode qui permet à l'utilisateur de choisir quelle image il veut traiter parmis celles disponibles dans son fichier debug
        {
            string[] fichiers = Directory.GetFiles("./images");
            List<string> listeImages = new List<string>();
            int choix = 0;
            for (int i = 0; i < fichiers.Length; i++)
            {
                if (fichiers[i].Contains(".bmp") == true)
                {
                    char[] caractèresImage = fichiers[i].ToCharArray();
                    string nomImage = "";
                    for (int j = 9; j < caractèresImage.Length - 4; j++) //On ne sélectionne que les caractères du nom de l'image, en enlevant le chemin et le .bmp
                    {
                        nomImage += caractèresImage[j];
                    }
                    listeImages.Add(nomImage);
                }
            }
            string message = "Quelle image voulez-vous modifier ? \n";
            for (int i = 0; i < listeImages.Count; i++)
            {
                message += "\n" + " - " + listeImages[i] + " [" + (i + 1) + "]";
            }
            Console.WriteLine(message);
            choix = Convert.ToInt32(Console.ReadLine());
            if (choix == 0)
            {
                return "0";
            }
            string nomComplet = "./images./" + listeImages[choix - 1] + ".bmp";
            return nomComplet;
        }
        #endregion



        static void TestQR(string nom)
        {
            NewImage origine = new NewImage(nom, 200, 600); //peu importe la taille, la fonction la changera pour l'adapter à la version
            Console.WriteLine("Quel message voulez-vous coder dans ce QR-Code ?");
            string message = Console.ReadLine();
            origine.AffichageQR(message);
            origine.From_Image_To_File("qr.bmp");
            Process.Start("qr.bmp");
        }
    }
}
