using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjetInfo_Crochet_Watrigant
{
    /// <summary>
    /// classe permettant la manipulation de fichiers bitmap
    /// </summary>
    public class MyImage
    {
        private double tailleFichier;
        private double tailleOffset;
        private double largeur;
        private double hauteur;
        private double nbrBits;
        private Pixel[] tabImage;
        private string typeFichier;
        private string nomFichier;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="myfile"></param> nom du fichier à traiter
        public MyImage(string myfile)
        {
            this.nomFichier = myfile;
            double compteur = 0;
            int increment = 0;
            byte[] fichier = File.ReadAllBytes(myfile);

            compteur = 0;
            if (fichier[0] == 66 && fichier[1] == 77)
            {
                this.typeFichier = "BM";
            }
            this.tailleOffset = 0;
            for (int i = 14; i < 18; i++)
            {
                this.tailleOffset += fichier[i] * Math.Pow(256, compteur);
                compteur++;
            }
            compteur = 0;
            this.hauteur = 0;
            for (int a = 22; a < 26; a++)
            {
                this.hauteur += fichier[a] * Math.Pow(256, compteur);
                compteur++;
            }
            compteur = 0;
            this.tailleFichier = 0;
            for (int b = 2; b < 6; b++)
            {
                this.tailleFichier += fichier[b] * Math.Pow(256, compteur);
                compteur++;
            }
            this.largeur = 0;
            compteur = 0;
            for (int c = 18; c < 22; c++)
            {
                this.largeur += fichier[c] * Math.Pow(256, compteur);
                compteur++;
            }
            this.nbrBits = 0;
            compteur = 0;
            for (int d = 28; d < 30; d++)
            {
                this.nbrBits += fichier[d] * Math.Pow(256, compteur);
                compteur++;
            }
            Pixel[] stock = new Pixel[(Convert.ToInt32(this.largeur * this.hauteur))];
            //Pixel[] stock = new Pixel[1000*1000];
            for (int i = 54; i < fichier.Length; i = i + 3)
            {
                Pixel lot = new Pixel(5, 5, 5);
                //obligé d'intervertir le bleu et le rouge pour avoir RVB dans l'ordre
                lot.B = fichier[i];
                lot.V = fichier[i + 1];
                lot.R = fichier[i + 2];
                stock[increment] = lot;
                increment++;
            }
            this.tabImage = stock;



        }
        #region PROPRIETES
        /// <summary>
        /// propriété tailleFichier
        /// </summary>
        public double TailleFichier
        {
            get => tailleFichier;
            set => tailleFichier = value;
        }
        /// <summary>
        /// propriété tailleOffset
        /// </summary>
        public double TailleOffset
        {
            get => tailleOffset;
            set => tailleOffset = value;
        }
        /// <summary>
        /// propriété largeur
        /// </summary>
        public double Largeur
        {
            get => largeur;
            set => largeur = value;
        }
        /// <summary>
        /// propriété hauteur
        /// </summary>
        public double Hauteur
        {
            get => hauteur;
            set => hauteur = value;
        }
        /// <summary>
        /// propriété nbrBits
        /// </summary>
        public double NbrBits
        {
            get => nbrBits;
            set => nbrBits = value;
        }
        /// <summary>
        /// propriété tailleFichier
        /// </summary>
        public string TypeFichier
        {
            get => typeFichier;
            set => typeFichier = value;
        }
        /// <summary>
        /// propriété typeFichier
        /// </summary>
        public string NomFichier
        {
            get => nomFichier;
            set => nomFichier = value;
        }
        /// <summary>
        /// propriété tabImage
        /// </summary>
        public Pixel[] TabImage
        {
            get => tabImage;
            set => tabImage = value;
        }

        #endregion


        #region FONCTIONS DEMANDEES

        /// <summary>
        /// modifie les pixels de la matrice en faisant la moyenne des trois bytes ce qui fait une filtre noir et blanc
        /// </summary>
        public void Filtre_NB()
        {
            for (int i = 0; i < this.tabImage.Length; i++)
            {
                byte moyenne = Convert.ToByte((this.tabImage[i].R + this.tabImage[i].V + this.tabImage[i].B) / 3);
                this.tabImage[i].R = moyenne;
                this.tabImage[i].V = moyenne;
                this.tabImage[i].B = moyenne;
            }
        }
        /// <summary>
        /// retourne l'image de la gauche vers la droite
        /// </summary>
        public void Miroir_horizontale()
        {
            int compteur = 0;
            int largeur = Convert.ToInt32(this.largeur);
            int hauteur = Convert.ToInt32(this.hauteur);
            Pixel[] copie = new Pixel[this.tabImage.Length];
            for (int i = 0; i < this.tabImage.Length; i++)
            {
                copie[i] = this.tabImage[i];
            }
            for (int a = 0; a < hauteur; a++)
            {
                for (int b = 0; b < largeur; b++)
                {
                    this.tabImage[b + compteur * largeur] = copie[largeur + compteur * largeur - b - 1];
                }
                compteur++;
            }
        }
        /// <summary>
        /// retourne l'image du haut vers le bas
        /// </summary>
        public void Miroir_Vertical()
        {

            int largeur = Convert.ToInt32(this.largeur);
            int hauteur = Convert.ToInt32(this.hauteur);
            Pixel[,] copie = new Pixel[hauteur, largeur];
            Pixel[,] copie1 = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++) // je crée une matrice de pixel qui sera plus facile à manipuler
            {
                for (int j = 0; j < largeur; j++)
                {
                    copie[i, j] = this.tabImage[j + i * largeur];
                }
            }
            for (int a = 0; a < largeur; a++) // je retourne de bas en haut les valeurs de la matrice 
            {
                for (int b = 0; b < hauteur; b++)
                {
                    copie1[b, a] = copie[hauteur - b - 1, a];
                }
            }
            for (int i = 0; i < hauteur; i++) //je copie la matrice retournée dans le tableau de Pixels de notre image
            {
                for (int j = 0; j < largeur; j++)
                {
                    this.tabImage[j + i * largeur] = copie1[i, j];
                }
            }


        }
        /// <summary>
        /// affiche les caractéristiques d'un fichier bitmap
        /// </summary>
        /// <returns></returns> chaine d'informations
        public string Tostring()
        {
            string réponse = " Largeur : " + this.largeur + "\n Hauteur : " + this.hauteur + "\n Taille fichier : " + this.tailleFichier + "\n Taille Offset : " + this.tailleOffset + "\n Nombre de bits : " + this.nbrBits + "\n Type Fichier : " + this.typeFichier;
            return réponse;
        }
        /// <summary>
        /// sauvegarde un fichier de byte au format bitmap
        /// </summary>
        /// <param name="file"></param> nom du fichier de sauvegarde
        public void From_Image_To_File(string file)
        {
            int nombreZéros = ((Convert.ToInt32(this.hauteur) * 3) % 4);
            byte[] fichier = new byte[Convert.ToInt32(this.tailleFichier) + nombreZéros * Convert.ToInt32(this.hauteur)];
            for (int k = 0; k < fichier.Length; k++) //on remplit le fichier de 0
            {
                fichier[k] = 0;
            }
            fichier[0] = 66;
            fichier[1] = 77;


            int[] taille_byte = Convertir_Int_To_Byte(this.tailleFichier, 4);
            for (int b = 2; b < 6; b++)
            {
                fichier[b] = Convert.ToByte(taille_byte[b - 2]);
            }

            int taille_générale = 54;
            int[] general_byte = Convertir_Int_To_Byte(taille_générale, 4);
            for (int k = 10; k < 14; k++)
            {
                fichier[k] = Convert.ToByte(general_byte[k - 10]);
            }

            int[] offset_byte = Convertir_Int_To_Byte(this.tailleOffset, 4);
            for (int i = 14; i < 18; i++)
            {
                fichier[i] = Convert.ToByte(offset_byte[i - 14]);
            }

            int[] largeur_byte = Convertir_Int_To_Byte(this.largeur, 4);
            for (int c = 18; c < 22; c++)
            {
                fichier[c] = Convert.ToByte(largeur_byte[c - 18]);
            }

            int[] hauteur_byte = Convertir_Int_To_Byte(this.hauteur, 4);
            for (int a = 22; a < 26; a++)
            {
                fichier[a] = Convert.ToByte(hauteur_byte[a - 22]);
            }


            int quantite_plan = 1;
            int[] plan_byte = Convertir_Int_To_Byte(quantite_plan, 2);
            for (int j = 26; j < 28; j++)
            {
                fichier[j] = Convert.ToByte(plan_byte[j - 26]);
            }

            int[] nbrBits_byte = Convertir_Int_To_Byte(this.nbrBits, 2);
            for (int d = 28; d < 30; d++)
            {
                fichier[d] = Convert.ToByte(nbrBits_byte[d - 28]);
            }

            int compression = 0; //on part du principe que notre image n'a pas de compression
            int[] compression_byte = Convertir_Int_To_Byte(compression, 4);
            for (int k = 30; k < 34; k++)
            {
                fichier[k] = Convert.ToByte(compression_byte[k - 30]);
            }

            int tailleImage = Convert.ToInt32(this.tailleFichier - taille_générale);
            int[] tailleImage_Byte = Convertir_Int_To_Byte(tailleImage, 4);
            for (int k = 34; k < 38; k++)
            {
                fichier[k] = Convert.ToByte(tailleImage_Byte[k - 34]);
            }

            int resolution_horizontale = 0;
            int[] resolH_byte = Convertir_Int_To_Byte(resolution_horizontale, 4);
            for (int k = 38; k < 42; k++)
            {
                fichier[k] = Convert.ToByte(resolH_byte[k - 38]);
            }

            int resolution_verticale = 0;
            int[] resolV_byte = Convertir_Int_To_Byte(resolution_verticale, 4);
            for (int k = 42; k < 46; k++)
            {
                fichier[k] = Convert.ToByte(resolV_byte[k - 42]);
            }

            int quantite_couleurs = 0; //0 correspond au maximum possible
            int[] quantite_couleurs_byte = Convertir_Int_To_Byte(quantite_couleurs, 4);
            for (int k = 46; k < 50; k++)
            {
                fichier[k] = Convert.ToByte(quantite_couleurs_byte[k - 46]);
            }

            int couleurs_importantes = 0; // 0 signifie que toutes les couleurs sont importantes
            int[] importance_byte = Convertir_Int_To_Byte(couleurs_importantes, 4);
            for (int k = 50; k < 54; k++)
            {
                fichier[k] = Convert.ToByte(importance_byte[k - 50]);
            }

            int compteur = 0;
            for (int x = 54; x < fichier.Length; x = x + 3)
            {
                //j'ai été obligé d'intervertir le bleu et le Rouge afin qu'on ait RVB dans l'ordre
                fichier[x] = this.tabImage[compteur].B;
                fichier[x + 1] = this.tabImage[compteur].V;
                fichier[x + 2] = this.tabImage[compteur].R;
                compteur++;
            }

            File.WriteAllBytes(file, fichier);
        }
        /// <summary>
        /// applique une rotation d'un angle quelconque sur une image
        /// </summary>
        /// <param name="angle"></param> valeur de l'angle souhaité
        public void Rotation(double angle)
        {
            angle = (angle * Math.PI) / 180; //on passe l'angle en radian
            Pixel[,] origine = CréationMatricePixel();  //on transforme le tabImage en matrice
            int max = Math.Max(origine.GetLength(0) * 2, origine.GetLength(1) * 2);
            Pixel[,] grandeMatrice = new Pixel[max, max];  //on crée une grande matrice carrée qui contiendra notre rotation
            int centre = max / 2;  //on relève le centre de la matrice

            for (int i = 0; i < grandeMatrice.GetLength(0); i++)
            {
                for (int j = 0; j < grandeMatrice.GetLength(1); j++)
                {
                    grandeMatrice[i, j] = new Pixel(255, 255, 255); //on crée un fond blanc pour notre rotation
                }
            }
            Pixel[,] copie = copieMatrice(grandeMatrice); //on crée une copie du fond, on on appliquera la rotation
            int ligne = 0;
            for (int i = centre - (origine.GetLength(0) / 2); i < (origine.GetLength(0) / 2) + centre; i++)
            {
                int colonne = 0;
                for (int j = centre - (origine.GetLength(1) / 2); j < centre + (origine.GetLength(1) / 2); j++)
                {
                    grandeMatrice[i, j] = origine[ligne, colonne]; //sur le milieu de notre fond original, on place la photo
                    colonne++;
                }
                ligne++;
            }
            //on applique les modifications sur le fichier
            this.hauteur = max;
            this.largeur = max;
            this.tabImage = new Pixel[max * max];
            this.tailleFichier = (max * max * 3) + 54;
            //on applique la rotation en appliquant la formule de la rotation vectorielle
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            for (int i = centre - (origine.GetLength(0) / 2); i < (origine.GetLength(0) / 2) + centre; i++)
            {

                for (int j = centre - (origine.GetLength(1) / 2); j < centre + (origine.GetLength(1) / 2); j++)
                {
                    int posX = Convert.ToInt32(Math.Ceiling(centre + ((i - centre) * cos - (j - centre) * sin)));
                    int posY = Convert.ToInt32(Math.Ceiling(centre + ((i - centre) * sin + (j - centre) * cos)));
                    copie[posX, posY] = grandeMatrice[i, j]; //on enregistre la rotation sur la copie
                }

            }
            //avec la rotation, certains index ne sont pas remplis à cause du fait que posX et PosY ne soient pas des entiers
            //on remplit donc ces vides en faisant la moyenne des pixels voisins

            Pixel[,] copie2 = copieMatrice(copie);

            for (int i = 3; i < copie.GetLength(0) - 3; i++)
            {
                for (int j = 3; j < copie.GetLength(1) - 3; j++)
                {
                    if ((copie[i, j].R == 255) && (copie[i, j].B == 255) && (copie[i, j].V == 255)) //on voit si le pixel est blanc, la couleur du fond
                    {


                        int compteur = 0;
                        Pixel[,] voisins = new Pixel[3, 3];
                        int lignes2 = 0;
                        for (int b = i - 1; b <= i + 1; b++)
                        {
                            int colonnes2 = 0;
                            for (int a = j - 1; a <= j + 1; a++)
                            {
                                voisins[lignes2, colonnes2] = copie[b, a];

                                colonnes2++;
                            }
                            lignes2++;
                        }
                        foreach (Pixel p in voisins) //on vérifie si ses voisins sont des pixels de couleurs pour vérifier qu'on ne soit pas sur le fond
                        {
                            if (p.R != 255 || p.V != 255 || p.B != 255)
                            {
                                compteur = compteur + 1;
                            }
                        }
                        if (compteur > 1)
                        {
                            copie2[i, j] = copie[i, j + 1]; //on lui donne la valeur de son voisin
                        }
                    }


                }
            }
            AppliquerModif(copie2);

        }
        /// <summary>
        /// agrandit une image selon un coefficient d'agrandisement
        /// </summary>
        /// <param name="coeff"></param> coefficient d'agrandissement souhaité
        public void AgrandirImage(int coeff)
        {
            //On modifie les dimensions de l'image
            //On créer une matrice de pixels faciles à manipuler
            Pixel[,] copie = CréationMatricePixel();

            int hauteur = Convert.ToInt32(this.hauteur * coeff);
            int largeur = Convert.ToInt32(this.largeur * coeff);
            this.hauteur = hauteur;
            this.largeur = largeur;
            //Puis on créer une matrice de pixels qui sera notre grande image
            Pixel[,] GrandeImage = new Pixel[hauteur, largeur];

            for (int a = 0; a <= copie.GetLength(0) - coeff; a++)
            {
                for (int b = 0; b <= copie.GetLength(1) - coeff; b++)
                {
                    int x = 0;
                    int y = 0;
                    //on gère les exceptions si on se trouve sur les bords
                    if (a == copie.GetLength(0) - coeff)
                    {
                        y = 1;
                    }
                    if (b == copie.GetLength(1) - coeff)
                    {
                        x = 1;
                    }
                    for (int i = a; i <= a + coeff + y; i++)
                    {
                        for (int j = b; j <= b + coeff + x; j++)
                        {
                            GrandeImage[i + a, j + b] = copie[a, b];
                        }
                    }
                }
            }

            this.tailleFichier = (hauteur * largeur * 3) + 54; //on ajoute la taille du header et on multiplie par trois car un pixel vaut 3 bytes
            this.tabImage = new Pixel[hauteur * largeur];
            AppliquerModif(GrandeImage);
        }
        /// <summary>
        /// retrecit une image selon un coefficient de rétrécissement
        /// </summary>
        /// <param name="coeff"></param> coefficient de rétrécissement souhaité
        public void RetrecirImage(int coeff)
        {
            //On modifie les dimensions de l'image
            int hauteur = Convert.ToInt32(this.hauteur / coeff);
            int largeur = Convert.ToInt32(this.largeur / coeff);
            this.hauteur = hauteur;
            this.largeur = largeur;
            Pixel[,] PetiteImage = new Pixel[hauteur, largeur];
            int R = 0;
            int V = 0;
            int B = 0;
            //On fait la moyenne des couleurs de chaque bloc de pixel (la taille dépend du coeff) et on l'applique à un seul pixel
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    for (int k = 0; k < coeff; k++)
                    {
                        for (int l = 0; l < coeff; l++)
                        {
                            R = R + tabImage[coeff * j + l + (coeff * i + k) * largeur * coeff].R;
                            B = B + tabImage[coeff * j + l + (coeff * i + k) * largeur * coeff].B;
                            V = V + tabImage[coeff * j + l + (coeff * i + k) * largeur * coeff].V;
                        }
                    }
                    R = R / (coeff * coeff);
                    B = B / (coeff * coeff);
                    V = V / (coeff * coeff);
                    PetiteImage[i, j] = new Pixel(Convert.ToByte(R), Convert.ToByte(V), Convert.ToByte(B));
                    R = 0;
                    B = 0;
                    V = 0;
                }
            }
            this.tailleFichier = (hauteur * largeur * 3) + 54; //on ajoute la taille du header et on multiplie par trois car un pixel vaut 3 bytes
            this.tabImage = new Pixel[hauteur * largeur];
            AppliquerModif(PetiteImage);

        }
        /// <summary>
        /// applique la formule de convolution sur une image pour un noyau donné
        /// </summary>
        /// <param name="kernel"></param> noyau en question, qui est une matrice d'entiers
        /// <param name="valeur"></param> entier permettant d'éclaircir une image en rajoutant de la valeur sur les pixels si besoin
        public void Convolution(int[,] kernel, int valeur)
        {
            //le paramètre valeur permet d'éclaircir une image si elle est trop sombre
            Pixel[,] origine = CréationMatricePixel(); //on crée une matrice de pixel à partir du tableau de pixels de l'image
            Pixel[,] extraMatrice = ExtraMatrice(origine);  //On agrandit la matrice afin que même les pixels au bord aient des pixels voisins
            Pixel[,] extraMatrice1 = copieMatrice(extraMatrice);  //on duplique la matrice précédente 
            int facteurPositif = 0;
            int[,] noyau = new int[7, 7];
            int ligne = 0;
            int colonne = 0;
            int sommeNoyau = 0;
            //qu'elle que soit la taille du kernel, on va en faire un noyau 7x7, s'il le faut avec des 0
            for (int a = 0; a < noyau.GetLength(0); a++)
            {
                for (int b = 0; b < noyau.GetLength(1); b++)
                {
                    noyau[a, b] = 0;
                }
            }
            if (kernel.GetLength(0) == 3)
            {
                for (int i = 2; i < noyau.GetLength(0) - 2; i++)
                {
                    colonne = 0;
                    for (int j = 2; j < noyau.GetLength(1) - 2; j++)
                    {
                        noyau[i, j] = kernel[ligne, colonne];
                        colonne++;
                    }
                    ligne++;
                }
            }
            colonne = 0;
            ligne = 0;
            if (kernel.GetLength(0) == 5)
            {
                for (int i = 1; i < noyau.GetLength(0) - 1; i++)
                {
                    colonne = 0;
                    for (int j = 1; j < noyau.GetLength(1) - 1; j++)
                    {
                        noyau[i, j] = kernel[ligne, colonne];
                        colonne++;
                    }
                    ligne++;
                }
            }
            if (kernel.GetLength(0) == 7) //si le kernel a déja une taille de 7x7, on ne modifie rien
            {
                for (int a = 0; a < noyau.GetLength(0); a++)
                {
                    for (int b = 0; b < noyau.GetLength(1); b++)
                    {
                        noyau[a, b] = kernel[a, b];
                    }
                }
            }
            //on définit la valeur de la somme des coefficients positifs du noyau et la somme des valeurs du noyau
            for (int i = 0; i < noyau.GetLength(0); i++)
            {
                for (int j = 0; j < noyau.GetLength(1); j++)
                {
                    sommeNoyau = sommeNoyau + noyau[i, j];
                    if (noyau[i, j] > 0)
                    {
                        facteurPositif = facteurPositif + noyau[i, j];
                    }


                }
            }
            //on définit de quel façon on va diviser pour faire la moyenne des pixels
            int diviseur = 0;
            if (sommeNoyau > 0)
            {
                diviseur = sommeNoyau;
            }
            else
            {
                diviseur = facteurPositif;
            }
            //on crée des matrices pour chaque couleur
            int[,] matriceRouges = new int[extraMatrice.GetLength(0), extraMatrice.GetLength(1)];
            int[,] matriceVerts = new int[extraMatrice.GetLength(0), extraMatrice.GetLength(1)];
            int[,] matriceBleus = new int[extraMatrice.GetLength(0), extraMatrice.GetLength(1)];
            for (int i = 0; i < extraMatrice.GetLength(0); i++)
            {
                for (int j = 0; j < extraMatrice.GetLength(1); j++)
                {
                    matriceBleus[i, j] = extraMatrice[i, j].B;
                    matriceRouges[i, j] = extraMatrice[i, j].R;
                    matriceVerts[i, j] = extraMatrice[i, j].V;
                }
            }
            for (int i = 3; i < extraMatrice.GetLength(0) - 3; i++)
            {
                for (int j = 3; j < extraMatrice.GetLength(1) - 3; j++)
                {
                    //on lit les voisins pour chaque matrice de couleurs
                    int[,] voisinBleu = new int[7, 7];
                    int[,] voisinVert = new int[7, 7];
                    int[,] voisinRouge = new int[7, 7];
                    int sommeRouge = 0;
                    int sommeVert = 0;
                    int sommeBleu = 0;
                    int ligne1 = 0;
                    //la matrice de voisins va prendre la valeur des voisins de la couleur multipliés par la valeur du noyau qui lui correspond
                    for (int a = i - 3; a <= i + 3; a++)
                    {
                        int colonne1 = 0;
                        for (int b = j - 3; b <= j + 3; b++)
                        {
                            voisinBleu[ligne1, colonne1] = matriceBleus[a, b] * noyau[ligne1, colonne1];
                            voisinRouge[ligne1, colonne1] = matriceRouges[a, b] * noyau[ligne1, colonne1];
                            voisinVert[ligne1, colonne1] = matriceVerts[a, b] * noyau[ligne1, colonne1];
                            colonne1++;
                        }
                        ligne1++;
                    }
                    //on additionne toutes les valeurs des voisins de chaque couleurs afin d'en faire la moyenne; 
                    for (int a = 0; a < voisinBleu.GetLength(0); a++)
                    {
                        for (int b = 0; b < voisinBleu.GetLength(1); b++)
                        {
                            sommeBleu = sommeBleu + voisinBleu[a, b];
                            sommeRouge = sommeRouge + voisinRouge[a, b];
                            sommeVert = sommeVert + voisinVert[a, b];
                        }
                    }
                    byte moyenneBleu = Convert.ToByte(Math.Min(Math.Abs((sommeBleu / diviseur) + valeur), 255));
                    byte moyenneRouge = Convert.ToByte(Math.Min(Math.Abs((sommeRouge / diviseur) + valeur), 255));
                    byte moyenneVert = Convert.ToByte(Math.Min(Math.Abs((sommeVert / diviseur) + valeur), 255));
                    extraMatrice1[i, j] = new Pixel(moyenneRouge, moyenneVert, moyenneBleu);
                }
            }
            Pixel[,] finale = extraireMatrice(extraMatrice1);//on récupère la matrice qui nous intéresse, au centre de la grande
            AppliquerModif(finale); //on rentre cette matrice dans le tableau de pixels de l'image
        }
        /// <summary>
        /// decode l'image cachée derrière une autre et la révèle
        /// </summary>
        public void DecoderImage()//fonction permettant de révéler si une image est cachée dans une autre
        {
            int x = 0;
            int[] RougeBinaire = new int[8];
            int[] VertBinaire = new int[8];
            int[] BleuBinaire = new int[8];
            foreach (Pixel i in this.tabImage)
            {
                int[] Rouge = Convertir_Couleur_To_Binaire(this.tabImage[x].R);
                int[] Vert = Convertir_Couleur_To_Binaire(this.tabImage[x].V);
                int[] Bleu = Convertir_Couleur_To_Binaire(this.tabImage[x].B);
                //on prend les quatre derniers bits de chaque couleur pour chaque pixel et on les met en premières positions
                for (int a = 0; a < 4; a++)
                {
                    RougeBinaire[a] = Rouge[a + 4];
                    VertBinaire[a] = Vert[a + 4];
                    BleuBinaire[a] = Bleu[a + 4];
                }
                //on applique les modifications sur notre tabImage
                tabImage[x].R = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(RougeBinaire), 1)[0]);
                tabImage[x].V = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(VertBinaire), 1)[0]);
                tabImage[x].B = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(BleuBinaire), 1)[0]);
                x++;
            }
        }
        /// <summary>
        /// fonction préliminaire pour l'affichage de l'histogramme
        /// </summary>
        /// <returns></returns> un tableau par couleur du nombre de représentations de chaque intensité de byte 
        public List<double[]> Histogramme()
        {
            
            //on crée un tableau pour chaque couleur avec pour taille 256
            double[] tabBleu = new double[256];
            double[] tabRouge = new double[256];
            double[] tabVert = new double[256];
            //on remplit ces tableaux de 0
            for (int i = 0; i < tabBleu.Length; i++)
            {
                tabBleu[i] = 0;
                tabRouge[i] = 0;
                tabVert[i] = 0;
            }
            //à chaque fois qu'on lit une certaine intensité pour une couleur, on incrémente 1 à la position de cette intensité dans le tableau de la couleur
            //intensite est comprise entre 0 et 255
            for (int i = 0; i < this.tabImage.Length; i++)
            {
                int intensiteB = this.tabImage[i].B;
                int intensiteR = this.tabImage[i].R;
                int intensiteV = this.tabImage[i].V;
                tabBleu[intensiteB] = tabBleu[intensiteB] + 1;
                tabRouge[intensiteR] = tabRouge[intensiteR] + 1;
                tabVert[intensiteV] = tabVert[intensiteV] + 1;
            }
            List<double[]> stock = new List<double[]>();
            //rentrer RVB dans l'ordre
            stock.Add(tabRouge);
            stock.Add(tabVert);
            stock.Add(tabBleu);
            return stock;

        }
        /// <summary>
        /// permet de cacher une image dans une autre
        /// </summary>
        /// <param name="ImageàCacher"></param> il s'agit de l'image qu'on désire cacher
        public void CoderUneImageDansUneImage(MyImage ImageàCacher) //fonction qui permet de cacher une image dans une autre
        {
            if (this.tabImage.Length != ImageàCacher.tabImage.Length)
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            int x = 0;
            //Ces trois tableaux réprésentent la valeur en binaire des nouvelles couleurs de chaque pixel. Elles seront ensuite appliquées à l'image d'origine qui changera donc un petit peu
            int[] RougeBinaire = new int[8];
            int[] VertBinaire = new int[8];
            int[] BleuBinaire = new int[8];
            foreach (Pixel i in ImageàCacher.tabImage)
            {
                //Les couleurs du pixel de l'image principale :
                int[] Rouge1 = Convertir_Couleur_To_Binaire(this.tabImage[x].R);
                int[] Vert1 = Convertir_Couleur_To_Binaire(this.tabImage[x].V);
                int[] Bleu1 = Convertir_Couleur_To_Binaire(this.tabImage[x].B);
                //Les couleurs du pixel de l'image à coder :
                int[] Rouge2 = Convertir_Couleur_To_Binaire(i.R);
                int[] Vert2 = Convertir_Couleur_To_Binaire(i.V);
                int[] Bleu2 = Convertir_Couleur_To_Binaire(i.B);
                //Les 4 premiers bits de la nouvelle image seront les 4 premiers bits de l'image d'origine :
                for (int a = 0; a < 4; a++)
                {
                    RougeBinaire[a] = Rouge1[a];
                    VertBinaire[a] = Vert1[a];
                    BleuBinaire[a] = Bleu1[a];
                }
                //Puis les 4 derniers bits de la nouvelle image seront les 4 premiers bits de l'image à cacher
                for (int b = 4; b < 8; b++)
                {
                    RougeBinaire[b] = Rouge2[b - 4];
                    VertBinaire[b] = Vert2[b - 4];
                    BleuBinaire[b] = Bleu2[b - 4];
                }
                //Enfin, on modifie le pixel de l'image d'origine avec ses nouvelles couleurs :
                tabImage[x].R = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(RougeBinaire), 1)[0]);
                tabImage[x].V = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(VertBinaire), 1)[0]);
                tabImage[x].B = Convert.ToByte(Convertir_Int_To_Byte(Convertir_Binaire_to_int(BleuBinaire), 1)[0]);
                x++;
            }
        }
        #endregion


        #region FONCTIONS PRATIQUES
        /// <summary>
        /// copie les valeurs d'une matrice 
        /// </summary>
        /// <param name="matrice"></param> prend en paramètres une matrice de pixel quelconque
        /// <returns></returns> une matrice de pixel qui est la copie
        public Pixel[,] copieMatrice(Pixel[,] matrice)//fonction permettant de dupliquer les valeurs d'une matrice dans une autre
        {
            Pixel[,] matrice2 = new Pixel[matrice.GetLength(0), matrice.GetLength(1)];
            for (int j = 0; j < matrice.GetLength(0); j++)
            {
                for (int k = 0; k < matrice.GetLength(1); k++)//on lit chaque valeur de la matrice initiale
                {
                    matrice2[j, k] = matrice[j, k];//on copie ces valeurs dans une matrice de taille identique qui sera retournée
                }
            }
            return matrice2;


        }
        /// <summary>
        /// affiche les valeurs d'une matrice d'entiers
        /// </summary>
        /// <param name="matrice"></param> matrice d'entier en question
        public void AfficherMatrice(int[,] matrice)
        {
            if (matrice != null || matrice.GetLength(0) > 0 || matrice.GetLength(1) > 0)
            {
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < matrice.GetLength(1); j++)
                    {
                        if (matrice[i, j] < 10)
                        {
                            Console.Write(" " + matrice[i, j] + " ");
                        }
                        else
                        {
                            Console.Write(matrice[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            if (matrice.GetLength(0) == 0 || matrice.GetLength(1) == 0)
            {
                Console.WriteLine("(matrice vide)");
            }
            if (matrice == null)
            {
                Console.WriteLine("(matrice null)");
            }
        }
        /// <summary>
        /// génère une matrice de pixel à partir du tableau de pixel d'une occurence de MyImage
        /// </summary>
        /// <returns></returns> la matrice de pixel en question
        public Pixel[,] CréationMatricePixel()
        {
            int largeur = Convert.ToInt32(this.largeur);
            int hauteur = Convert.ToInt32(this.hauteur);
            Pixel[,] copie = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++) // je crée une matrice de pixel à partir du tabImage qui sera plus facile à manipuler
            {
                for (int j = 0; j < largeur; j++)
                {
                    copie[i, j] = this.tabImage[j + i * largeur];
                }
            }
            return copie;

        }
        /// <summary>
        /// applique les valeurs d'une matrice de pixel sur le tableau de pixel d'une image
        /// </summary>
        /// <param name="matrice"></param> matrice de pixel à appliquer
        public void AppliquerModif(Pixel[,] matrice)//je pars d'une matrice de pixel et l'applique dans le tabImage
        {
            int largeur = matrice.GetLength(1);
            int hauteur = matrice.GetLength(0);
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    this.tabImage[j + i * largeur] = matrice[i, j];
                }
            }
        }
        /// <summary>
        /// genère une matrice plus grande avec les bords en double
        /// </summary>
        /// <param name="matrice"></param> matrice dont on souhaite dupliquer les bords
        /// <returns></returns> matrice de pixels plus grande avec les bords en double
        public Pixel[,] ExtraMatrice(Pixel[,] matrice) // cette fonction reprend la fonction Grande Matrice sauf que celle-ci permet de se servir des 49 voisins d'une cellule
        {

            int ligne = 0;
            Pixel[,] finale = new Pixel[matrice.GetLength(0) + 6, matrice.GetLength(1) + 6];
            for (int a = 0; a < finale.GetLength(0); a++)
            {
                Pixel vide = new Pixel(0, 0, 0);
                for (int b = 0; b < finale.GetLength(1); b++)
                {
                    finale[a, b] = vide;
                }
            }
            for (int i = 3; i < finale.GetLength(0) - 3; i++)
            {
                int colonne = 0;
                for (int j = 3; j < finale.GetLength(1) - 3; j++)
                {
                    finale[i, j] = matrice[ligne, colonne];
                    colonne++;
                }
                ligne++;
            }
            for (int j = 0; j < finale.GetLength(1); j++)
            {
                finale[2, j] = finale[3, j];
                finale[1, j] = finale[3, j];
                finale[0, j] = finale[3, j];
                finale[finale.GetLength(0) - 2, j] = finale[finale.GetLength(0) - 4, j];
                finale[finale.GetLength(0) - 1, j] = finale[finale.GetLength(0) - 4, j];
                finale[finale.GetLength(0) - 3, j] = finale[finale.GetLength(0) - 4, j];
            }
            for (int i = 0; i < finale.GetLength(0); i++)
            {
                finale[i, 0] = finale[i, 3];
                finale[i, 1] = finale[i, 3];
                finale[i, 2] = finale[i, 3];
                finale[i, finale.GetLength(1) - 1] = finale[i, finale.GetLength(1) - 4];
                finale[i, finale.GetLength(1) - 2] = finale[i, finale.GetLength(1) - 4];
                finale[i, finale.GetLength(1) - 3] = finale[i, finale.GetLength(1) - 4];
            }
            return finale;

        }
        /// <summary>
        /// récupère une matrice de pixel en enlevant la duplication des bords
        /// </summary>
        /// <param name="matrice"></param> matrice avec les bords en double
        /// <returns></returns> matrice avec bords uniques
        public Pixel[,] extraireMatrice(Pixel[,] matrice) // cette fonction permet de récupérer le centre de la grande matrice après modification des cellules, où se situe la matrice d'origine
        {
            int lignes = 0;
            Pixel[,] matrice2 = new Pixel[matrice.GetLength(0) - 6, matrice.GetLength(1) - 6]; // on enlève 4 lignes et 4 colonnes pour retrouver la taille initiale de la matrice
            for (int i = 3; i < matrice.GetLength(0) - 3; i++)
            {
                int colonnes = 0;
                for (int a = 3; a < matrice.GetLength(1) - 3; a++)
                {
                    matrice2[lignes, colonnes] = matrice[i, a];
                    colonnes++;
                }
                lignes++;
            }
            return matrice2;

        }

        #region CONVERSIONS
        /// <summary>
        /// Méthode qui permet de convertir l'intensité d'une couleur d'un pixel en une série de 0 et de 1
        /// </summary>
        /// <param name="couleur">valeur du byte qu'on souhaite convertir
        /// <returns></returns> un octet de bits
        public static int[] Convertir_Couleur_To_Binaire(byte couleur) //Méthode qui permet de convertir l'intensité d'une couleur d'un pixel en une série de 0 et de 1
        {
            double color = Convert.ToDouble(couleur);
            int compteur = 0;
            int[] total = new int[8];
            double[] tab = new double[8];
            double quotient = 0;
            for (int i = 0; i < total.Length; i++)
            {
                quotient = color % 2;
                tab[i] = quotient;
                color = Math.Floor(color / 2);
            }
            for (int a = 0; a < tab.Length; a++)
            {
                total[compteur] = Convert.ToByte(tab[a]);
                compteur++;
            }
            total = Inversion_Octet(total);

            return total;
        }
        /// <summary>
        /// convertit une suite de 0 et 1 en un entier
        /// </summary>
        /// <param name="binaire"></param> suite de 0 et 1
        /// <returns></returns> l'entier correspondant au tableau
        public static int Convertir_Binaire_to_int(int[] binaire)
        {
            double résultat = 0;
            double exposant = 0;
            for (int i = binaire.Length - 1; i >= 0; i--)
            {
                if (binaire[i] == 1)
                {
                    résultat = résultat + Math.Pow(2, exposant);
                }
                exposant++;
            }
            int retour = Convert.ToInt32(résultat);
            return retour;
        }
        /// <summary>
        /// convertit une valeur en une suite de coefficients multipliés par des puissances succesives de 256
        /// </summary>
        /// <param name="val"></param> valeur à convertir
        /// <param name="taille"></param> longueur de la suite de coefficients
        /// <returns></returns> liste de coefficients multipliés à 256
        public static int[] Convertir_Int_To_Byte(double val, int taille)
        {
            int compteur = 0;
            int[] total = new int[taille];
            double[] tab = new double[taille];
            double quotient = 0;
            for (int i = 0; i < total.Length; i++)
            {
                quotient = val % 256;
                tab[i] = quotient;
                val = Math.Floor(val / 256);
            }
            for (int a = 0; a < tab.Length; a++)
            {
                total[compteur] = Convert.ToByte(tab[a]);
                compteur++;
            }

            return total;
        }
        /// <summary>
        /// retourne un octet inversé
        /// </summary>
        /// <param name="tab"></param> octet à inverser
        /// <returns></returns> octet une fois inversé
        public static int[] Inversion_Octet(int[] tab)
        {
            int compteur = 0;
            int[] inverser = new int[tab.Length];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                inverser[compteur] = tab[i];
                compteur++;
            }
            return inverser;
        }
        /// <summary>
        /// prend un tableau au format little endian et retourne la valeur qui correspond
        /// </summary>
        /// <param name="tab"></param> tableau format little
        /// <returns></returns> entier correspondant
        public static int Little_to_int(int[] tab)
        {
            double sol = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                double a = Convert.ToDouble(tab[i]);
                sol = sol + a * Math.Pow(256, i);
            }
            int rep = Convert.ToInt32(sol);
            return rep;
        }
        /// <summary>
        /// prend un tableau au format big endian et retourne la valeur qui correspond
        /// </summary>
        /// <param name="tab"></param> tableau au format big endian
        /// <returns></returns> entier correspondant
        public static double Big_to_int(int[] tab)
        {
            int compteur = 0;
            double sol = 0;
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                double a = Convert.ToDouble(tab[compteur]);
                sol = sol + a * Math.Pow(256, i);
                compteur++;
            }
            double rep = Convert.ToDouble(sol);
            return rep;
        }
        /// <summary>
        /// convertit un entier en une suite de bits
        /// </summary>
        /// <param name="val"></param> entier à convertir
        /// <param name="taille"></param> taille de la chaine de bits désirée
        /// <returns></returns> la chaine de bits correspondant à l'entier
        public static int[] Convertir_Int_To_Binaire(double val, int taille)
        {
            int compteur = 0;
            int[] total = new int[taille];
            double[] tab = new double[taille];
            double quotient = 0;
            for (int i = 0; i < total.Length; i++)
            {
                quotient = val % 2;
                tab[i] = quotient;
                val = Math.Floor(val / 2);
            }
            for (int a = 0; a < tab.Length; a++)
            {
                total[compteur] = Convert.ToByte(tab[a]);
                compteur++;
            }
            total = Inversion_Octet(total);
            return total;
        }
        #endregion

        #endregion


        #region INNOVATION
        /// <summary>
        /// affiche une image en ne laissant qu'une des trois couleurs
        /// </summary>
        /// <param name="lettre"></param> lettre de la couleur qu'on souhaite conserver
        public void MonoCouleur(string lettre)
        {
            lettre = lettre.ToUpper();
            if (lettre == "R")
            {
                for (int i = 0; i < this.tabImage.Length; i++)
                {
                    tabImage[i].B = 0;
                    tabImage[i].V = 0;
                }
            }
            if (lettre == "V")
            {
                for (int i = 0; i < this.tabImage.Length; i++)
                {
                    tabImage[i].R = 0;
                    tabImage[i].B = 0;
                }
            }
            if (lettre == "B")
            {
                for (int i = 0; i < this.tabImage.Length; i++)
                {
                    tabImage[i].R = 0;
                    tabImage[i].V = 0;
                }
            }
        }
        /// <summary>
        /// fonction qui ne laisse que la couleur avec la plus forte intensité sur un pixel
        /// </summary>
        public void CouleurDominante()
        {
            for(int i=0;i<this.tabImage.Length;i++)
            {
                if(tabImage[i].R>=tabImage[i].B && tabImage[i].R >= tabImage[i].V)
                {
                    tabImage[i].B = 0;
                    tabImage[i].V = 0;
                }
                if (tabImage[i].B >= tabImage[i].R && tabImage[i].B >= tabImage[i].V)
                {
                    tabImage[i].R = 0;
                    tabImage[i].V = 0;
                }
                if (tabImage[i].V >= tabImage[i].B && tabImage[i].V >= tabImage[i].R)
                {
                    tabImage[i].B = 0;
                    tabImage[i].R = 0;
                }

            }
        }
        /// <summary>
        /// fonction qui éclaircit ou assombrit une image
        /// </summary>
        /// <param name="coeff"></param> coefficient d'assombrissement ou d'éclaircicessement
        public void Brillance(int coeff)
        {
            for(int i=0;i<this.tabImage.Length;i++)
            {
                int rouge = Math.Max(tabImage[i].R + coeff,0);
                int bleu = Math.Max(tabImage[i].B + coeff,0);
                int vert = Math.Max(tabImage[i].V + coeff,0);
                tabImage[i].R = Convert.ToByte(Math.Min(rouge, 255));
                tabImage[i].B = Convert.ToByte(Math.Min(bleu, 255));
                tabImage[i].V = Convert.ToByte(Math.Min(vert, 255));

            }
        }
        /// <summary>
        /// fonction qui ne laisse que le byte de plus faible intensite sur un pixel
        /// </summary>
        public void CouleurFaible()
        {
            Pixel[,] image = CréationMatricePixel();
            Pixel[,] copie = copieMatrice(image);
            for(int i=0;i<copie.GetLength(0);i++)
            {
                for(int j=0;j<copie.GetLength(1);j++)
                {
                    if(copie[i,j].R<=copie[i,j].V && copie[i,j].R<= copie[i,j].B)
                    {
                        image[i, j].V = 0;
                        image[i, j].B = 0;
                    }
                    if (copie[i, j].B <= copie[i, j].V && copie[i, j].B <= copie[i, j].R)
                    {
                        image[i, j].V = 0;
                        image[i, j].R = 0;
                    }
                    if (copie[i, j].V <= copie[i, j].B && copie[i, j].V <= copie[i, j].R)
                    {
                        image[i, j].R = 0;
                        image[i, j].B = 0;
                    }
                }
            }
            AppliquerModif(image);
        }
        /// <summary>
        /// fonction qui inverse l'intensité de chaque byte
        /// </summary>
        public void Inversion()
        {
            foreach (Pixel pix in tabImage)
            {
                pix.R = Convert.ToByte((255 - Convert.ToInt32(pix.R)));
                pix.V = Convert.ToByte((255 - Convert.ToInt32(pix.V)));
                pix.B = Convert.ToByte((255 - Convert.ToInt32(pix.B)));
            }
        }

        #endregion


        #region Lecteur QR Code

        /*    
        1 - Reconnaître les bits 1 ou 0.
        2 - Identifier la version du QR Code.
        3 - Découvrir la région à décoder.
        4 - Lire les données et le code correcteur.
        5 - Détecter/Corriger les erreurs.
        6 - Décoder les données.
        7 - Afficher le résultat.
        */

        /// <summary>
        /// lit la valeur d'un bit sur un qr code en fonction de sa couleur
        /// </summary>
        /// <param name="position"></param> index du tableau qu'on étudie
        /// <returns></returns>
        public int ReconnaissanceBit(int position)
        {
            //Si le pixel est blanc, le bit associé est 0. S'il est noir le bit associé est 1.
            int bit = 0;
            if (tabImage[position].R == 0 && tabImage[position].V == 0 && tabImage[position].B == 0)
            {
                bit = 1;
            }
            else
            {
                if (tabImage[position].R != 255 || tabImage[position].R != 255 || tabImage[position].R != 255)
                {
                    Console.WriteLine("Le pixel n'est ni blanc ni noir, on ne peut pas reconnaître le bit");
                }
            }
            return bit;
        }
        /// <summary>
        /// détecte la version d'un qr code en fonction de sa taille
        /// </summary>
        /// <returns></returns> la verision (1 ou 2)
        public int Version()
        {
            int version = 0;
            int taille = Convert.ToInt32(hauteur * largeur);
            if (taille % 21 == 0)
            {
                version = 1;
            }
            else
            {
                if (taille % 25 == 0)
                {
                    version = 2;
                }
                else
                {
                    Console.WriteLine("Erreur : ce n'est ni une version 1 ni une version 2.");
                }
            }
            return version;
            //On peut améliorer cette méthode en rajoutant une partie qui détecte s'il y a un détecteur d'alignement, afin de différencer la version 1 et 2 si la taille est un multiple de 21 et de 25 à la fois
        }

         
        /// <summary>
        /// La méthode qui suit retourne la taille des cases du QR Code. Elle est nécessaire car même si on sait que les versions 1 et 2 comprennent 21x21 et 25x25 cases, les cases n'ont pas forcément une taille d'un pixel 
        /// </summary>
        /// <param name="version"></param> la version du qr code qu'on lit
        /// <returns></returns> taille d'une case en pixels
        public int TailleCase(int version)
        {
            int taillecase = 0;
            if (version == 1)
            {
                taillecase = Convert.ToInt32(largeur) / 21;
            }
            else
            {
                taillecase = Convert.ToInt32(largeur) / 25;
            }
            if (taillecase == 0)
            {
                Console.WriteLine("Erreur : taille de la case");
            }
            return taillecase;
        }

        //Méthode qui permet de créer une matrice composée de 0 et de 1. C'est une copie simplifiée du QR Code que l'on étudie.
        /// <summary>
        /// Méthode qui permet de créer une matrice composée de 0 et de 1. C'est une copie simplifiée du QR Code que l'on étudie.
        /// </summary>
        /// <param name="version"></param> version du qr code qu'on étudie
        /// <param name="taillecase"></param> taille d'une case en pixel de notre qr code
        /// <returns></returns> matrice simplifiée de 0 et de 1
        public int[,] FromQrToMat(int version, int taillecase)
        {
            int[,] mat = new int[0, 0];
            if (version == 1)
            {
                mat = new int[21, 21];
            }
            if (version == 2)
            {
                mat = new int[25, 25];
            }
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(0); j++)
                {
                    if (ReconnaissanceBit((j * taillecase) + ((mat.GetLength(0) - 1 - i) * taillecase * Convert.ToInt32(largeur))) == 1)
                    {
                        mat[i, j] = 1;
                    }
                    else
                    {
                        mat[i, j] = 0;
                    }
                }
            }
            return mat;
        }

        
        /// <summary>
        /// Méthode qui retourne True si des motifs de recherche sont bien en haut à gauche, en bas à gauche et en haut à droite. Permet entre autre de détecter si l'image est bien un QR code ou s'il est bien orienté
        /// </summary>
        /// <param name="version"></param> version du qr code qu'on lit
        /// <param name="mat"></param> matrice d'entier du qr code
        /// <returns></returns> true si c'est bien la matrice est bien la représentation d'un qr code
        public bool MotifsRechercheBienPlacés(int version, int[,] mat)
        {
            bool retour = true;
            int termeI = 0; //Nombre que l'on va utiliser pour se placer dans le motif de recherche en haut à droite
            int termeJ = 0; //Nombre que l'on va utiliser pour se placer dans le motif de recherche en bas à gauche
            for (int MotifÉtudié = 1; MotifÉtudié <= 3; MotifÉtudié++) //On va étudier 3 motifs de recherche donc on doit faire le processus 3 fois
            {
                if (MotifÉtudié == 2) //On étudiera le motif en bas à gauche en deuxième
                {
                    if (version == 1) //Dans la version 1 il y a 14 cases horizontales entre le début du 1er motif de recherche et le début du 2ème
                    {
                        termeI = 14;
                    }
                    else //Dans la version 2, il y en a 18
                    {
                        termeI = 18;
                    }
                }
                if (MotifÉtudié == 3) //On étudiera le motif en haut à droite en 3ème
                {
                    if (version == 1) // Idem, 14 cases verticales entre le 1er et le 3ème
                    {
                        termeJ = 14;
                    }
                    else //Idem, 18 pour la version 2
                    {
                        termeJ = 18;
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        //Les valeurs de ce if paraissent complexes mais ce sont simplement les coordonnées de toutes les cases qui sont sensées appartenir au carré blanc de 5x5 à l'intérieur du motif de recherche 
                        if ((i == 1 && (j != 0 && j != 6)) || (i == 5 && (j != 0 && j != 6)) || (j == 1 && (i != 0 && i != 6)) || (j == 5 && (i != 0 && i != 6)))
                        {
                            if (mat[i + termeI, j + termeJ] != 0) //Si au moins une de ces cases n'est pas blanche, alors ce n'est pas un motif de recherche valide
                            {
                                retour = false;
                            }
                        }
                        else //Et si une des autres cases n'est pas noire, alors le motif de recherche n'est pas valide
                        {
                            if (mat[i + termeI, j + termeJ] != 1)
                            {
                                retour = false;
                            }
                        }
                    }
                }
                termeI = 0;
                termeJ = 0;
            }
            return retour;
        }
        //A rajouter : fonction qui trouve l'angle pour tourner l'image si le qr code est incliné

        /// <summary>
        /// Méthode qui créer une liste comprenant les coordonnées de toutes les cases qu'il faudra contourner pendant la lecture de code
        /// </summary>
        /// <param name="version"></param> version du qr code
        /// <returns></returns> les coeordonnées des zones à contourner lors de la lecture du qr code
        public List<int[]> ZoneNonDécodable(int version)
        {
            List<int[]> zone = new List<int[]>();
            int[] coordonnéesHorizontale = new int[2];
            int[] coordonnéesVerticale = new int[2];
            //On commence par les 3 motifs de recherche. On fait donc comme la méthode précédente.
            int largeur = 0;
            if (version == 1)
            {
                largeur = 21;
            }
            else
            {
                largeur = 25;
            }
            int termeI = 0;
            int termeJ = 0;
            for (int MotifÉtudié = 1; MotifÉtudié <= 3; MotifÉtudié++)
            {
                if (MotifÉtudié == 2) //motif en bas à gauche
                {
                    termeI = largeur - 7;
                }
                if (MotifÉtudié == 3) //motif en bas à droite
                {
                    termeJ = largeur - 7;
                }
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        int[] coordonnées = new int[2] { i + termeI, j + termeJ };
                        zone.Add(coordonnées);
                    }
                }
                termeI = 0;
                termeJ = 0;

                //On ajoute cette fois-ci les lignes blanches encadrant les motifs de recherche :
                for (int i = 0; i < 8; i++)
                {
                    if (MotifÉtudié == 1) //Séparateurs en haut à gauche
                    {
                        coordonnéesHorizontale = new int[2] { 7, i };
                        coordonnéesVerticale = new int[2] { i, 7 };
                    }
                    if (MotifÉtudié == 2) //Séparateurs en bas à gauce
                    {
                        coordonnéesHorizontale = new int[2] { largeur - 8, i };
                        coordonnéesVerticale = new int[2] { largeur - 1 - i, 7 };
                    }
                    if (MotifÉtudié == 3) //Séparateurs en haut à droite
                    {
                        coordonnéesHorizontale = new int[2] { 7, largeur - 1 - i };
                        coordonnéesVerticale = new int[2] { i, largeur - 8 };
                    }

                    zone.Add(coordonnéesHorizontale);
                    zone.Add(coordonnéesVerticale);
                }
                //On s'occupe maintenant des lignes bleues qui ne peuvent pas être utilisées pour insérer le texte
                if (MotifÉtudié == 1) //Motif en haut à gauche
                {
                    for (int i = 0; i < 9; i++)
                    {
                        coordonnéesHorizontale = new int[2] { 8, i };
                        coordonnéesVerticale = new int[2] { i, 8 };
                        zone.Add(coordonnéesHorizontale);
                        zone.Add(coordonnéesVerticale);
                    }
                }
                if (MotifÉtudié == 2) //Motif en bas à gauche
                {
                    for (int i = largeur - 8; i < largeur; i++) //Cette ligne comprend la case noire présente sur tous les QR code à côté du motif de recherche en bas à gauche
                    {
                        coordonnéesVerticale = new int[2] { i, 8 };
                        zone.Add(coordonnéesVerticale);
                    }
                }
                if (MotifÉtudié == 3) //Motif en haut à droite
                {
                    for (int i = largeur - 8; i < largeur; i++)
                    {
                        coordonnéesHorizontale = new int[2] { 8, i };
                        zone.Add(coordonnéesHorizontale);
                    }
                }
            }
            //On passe aux motifs de synchronisation (les deux lignes pointillées)
            {
                for (int i = 0; i < largeur; i++)
                {
                    int[] coordonnésHorizontale = new int[2] { 6, i };
                    zone.Add(coordonnésHorizontale);
                    int[] coordonnésVerticale = new int[2] { i, 6 };
                    zone.Add(coordonnésVerticale);
                }
            }



            //De plus, si c'est la deuxième version, il faut enlever le motif d'alignement
            if (version == 2)
            {
                for (int i = 16; i <= 20; i++)
                {
                    for (int j = 16; j <= 20; j++)
                    {
                        int[] coordonnées = new int[2] { i, j };
                        zone.Add(coordonnées);
                    }
                }
            }
            return zone;
        }
        /// <summary>
        /// methode qui retourne les valeurs des données à lire d'un qr code
        /// </summary>
        /// <param name="version"></param> version du qr code
        /// <param name="mat"></param> matrice de 0 et 1 à partir du qr code
        /// <param name="zoneàéviter"></param> liste de coordonées des zones à ne pas lire
        /// <returns></returns> retourne une liste de bits qui correspondent au message du qr code
        public List<int> FromMatToChaîne(int version, int[,] mat, List<int[]> zoneàéviter)
        {
            List<int> chaîne = new List<int>();
            int[] tab = new int[2];
            int changementMasque = 0;
            int valeur = 0;
            for (int c = 0; c < mat.GetLength(0); c += 4) //Nous allons traiter les colonnes 4 par 4, avec 2 montantes puis 2 descendantes
            {
                {
                    for (int i = 0; i < mat.GetLength(0); i++)
                    {
                        for (int x = 0; x < 2; x++) //On commence par les 2 colonnes montantes 
                        {
                            tab = new int[] { mat.GetLength(0) - 1 - i, mat.GetLength(1) - 1 - c };
                            bool boul = false;
                            valeur = mat[(mat.GetLength(0) - 1 - i), (mat.GetLength(1) - 1 - c)];
                            foreach (int[] a in zoneàéviter)
                            {
                                if (a[0] == tab[0] && a[1] == tab[1])
                                {
                                    boul = true;
                                }
                            }
                            changementMasque = ((mat.GetLength(0) - 1 - i) + (mat.GetLength(1) - 1 - c)) % 2; //Le masque inverse la valeur du bit si (i+j)%2 = 0 
                            if (changementMasque == 0)
                            {
                                if (valeur == 1)
                                {
                                    valeur = 0;
                                }
                                else
                                {
                                    valeur = 1;
                                }
                            }
                            if (boul == false)
                            {

                                chaîne.Add(valeur);
                            }
                            if (x == 0) //Si c'est la première des deux, on passe à la colonne suivante
                            {
                                c++;
                            }
                            else //Si c'est la seconde, on revient à la première
                            {
                                c--;
                            }
                        }
                    }
                    c += 2;
                    for (int i = mat.GetLength(0) - 1; i >= 0; i--) //Puis on fait la même chose avec les deux descendantes
                    {
                        if (mat.GetLength(0) - 1 - c == 6) //Cette colonne doit être sautée car elle ne contient que des valeurs à ne pas décoder
                        {
                            c++;
                        }
                        for (int x = 0; x < 2; x++)
                        {

                            tab = new int[] { mat.GetLength(0) - 1 - i, mat.GetLength(1) - 1 - c };
                            bool boul = false;
                            valeur = mat[(mat.GetLength(0) - 1 - i), (mat.GetLength(1) - 1 - c)];
                            foreach (int[] a in zoneàéviter)
                            {
                                if (a[0] == tab[0] && a[1] == tab[1])
                                {
                                    boul = true;
                                }
                            }
                            changementMasque = ((mat.GetLength(0) - 1 - i) + (mat.GetLength(1) - 1 - c)) % 2; //Le masque inverse la valeur du bit si (i+j)%2 = 0
                            if (changementMasque == 0)
                            {
                                if (valeur == 1)
                                {
                                    valeur = 0;
                                }
                                else
                                {
                                    valeur = 1;
                                }
                            }
                            if (boul == false)
                            {
                                chaîne.Add(valeur);
                            }
                            if (x == 0) //Si c'est la première des deux, on passe à la colonne suivante
                            {
                                c++;
                            }
                            else //Si c'est la seconde, on revient à la première
                            {
                                c--;
                            }
                        }
                    }
                    c -= 2;
                }
            }
            return chaîne;
        }
        /// <summary>
        /// retourne un carctère après avoir lu son poids
        /// </summary>
        /// <param name="poids"></param> poids du carctère
        /// <returns></returns> le caractère détecté
        public string FromPoidsToCaractère(int poids)
        {
            if (poids == 0) { return "0"; }
            if (poids == 1) { return "1"; }
            if (poids == 2) { return "2"; }
            if (poids == 3) { return "3"; }
            if (poids == 4) { return "4"; }
            if (poids == 5) { return "5"; }
            if (poids == 6) { return "6"; }
            if (poids == 7) { return "7"; }
            if (poids == 8) { return "8"; }
            if (poids == 9) { return "9"; }
            if (poids == 10) { return "A"; }
            if (poids == 11) { return "B"; }
            if (poids == 12) { return "C"; }
            if (poids == 13) { return "D"; }
            if (poids == 14) { return "E"; }
            if (poids == 15) { return "F"; }
            if (poids == 16) { return "G"; }
            if (poids == 17) { return "H"; }
            if (poids == 18) { return "I"; }
            if (poids == 19) { return "J"; }
            if (poids == 20) { return "K"; }
            if (poids == 21) { return "L"; }
            if (poids == 22) { return "M"; }
            if (poids == 23) { return "N"; }
            if (poids == 24) { return "0"; }
            if (poids == 25) { return "P"; }
            if (poids == 26) { return "Q"; }
            if (poids == 27) { return "R"; }
            if (poids == 28) { return "S"; }
            if (poids == 29) { return "T"; }
            if (poids == 30) { return "U"; }
            if (poids == 31) { return "V"; }
            if (poids == 32) { return "W"; }
            if (poids == 33) { return "X"; }
            if (poids == 34) { return "Y"; }
            if (poids == 35) { return "Z"; }
            if (poids == 36) { return " "; }
            if (poids == 37) { return "$"; }
            if (poids == 38) { return "%"; }
            if (poids == 39) { return "*"; }
            if (poids == 40) { return "+"; }
            if (poids == 41) { return "-"; }
            if (poids == 42) { return "."; }
            if (poids == 43) { return "/"; }
            if (poids == 44) { return ":"; }
            return "ERREUR FROM POIDS TO CARACTERE";
        }
        /// <summary>
        /// Vérifie que le mode est bien l'alphanumérique 
        /// </summary>
        /// <param name="chaîne"></param> liste de bits de données
        /// <returns></returns> true s'il s'agit bien du mode alphanumérique
        public bool VerifModeAlphanumérique(List<int> chaîne) //Vérifie que le mode est bien l'alphanumérique 
        {
            List<int> mode = new List<int>();
            bool good = true;
            for (int i = 0; i < 4; i++)
            {
                mode.Add(chaîne[i]);
            }
            if (mode[0] != 0 || mode[1] != 0 || mode[2] != 1 || mode[3] != 0)
            {
                good = false;
            }
            return good;
        }
        /// <summary>
        /// fonction qui retourne le nombre de caractères que va contenir le message du qr code
        /// </summary>
        /// <param name="chaîne"></param> message du qr sous forme de bits
        /// <returns></returns> nombre de caractères du message
        public int DécodageNombreCaractères(List<int> chaîne)
        {
            int nombrecaractères = -1;
            bool mode = VerifModeAlphanumérique(chaîne);
            if (mode == false)
            {
                return nombrecaractères;
            }
            int[] tab = new int[9];
            for (int i = 0; i < 9; i++)
            {
                tab[i] = chaîne[i + 4];
            }
            nombrecaractères = Convertir_Binaire_to_int(tab);
            return nombrecaractères;
        }
        /// <summary>
        /// méthode qui prend une liste d'entiers et la retourne en paquet de 8, des octets
        /// </summary>
        /// <param name="chaîne"></param> message du qr sous forme de bits
        /// <param name="nombreOctets"></param> le nombre d'octets qu'on désire
        /// <returns></returns> une liste d'octets
        public List<int[]> FromChaîneToOctets(List<int> chaîne, int nombreOctets)
        {
            List<int[]> listeOctets = new List<int[]>();
            int compteur = 0;
            for (int i = 0; i < nombreOctets; i++)
            {
                int[] tab = new int[8];
                for (int x = 0; x < 8; x++)
                {
                    tab[x] = chaîne[compteur];
                    compteur++;
                }
                listeOctets.Add(tab);
            }
            return listeOctets;
        }
        /// <summary>
        /// retourne le message du qr en partant de sa liste de bits
        /// </summary>
        /// <param name="version"></param> version du qr code lu
        /// <param name="chaîne"></param> message du qr sous forme de bits
        /// <returns></returns> le message du qr code
        public string DécodageChaîne(int version, List<int> chaîne)
        {
            bool mode = VerifModeAlphanumérique(chaîne);
            int NombreCaracteres = DécodageNombreCaractères(chaîne);
            if (mode == false || NombreCaracteres == -1)
            {
                return "Erreur, le mode n'est pas alphanumérique";
            }
            int nombreOctetsDonnées = 0;
            if (version == 1)
            {
                nombreOctetsDonnées = 19;
            }
            if (version == 2)
            {
                nombreOctetsDonnées = 34;
            }
            List<int[]> listeOctets = new List<int[]>();
            listeOctets = FromChaîneToOctets(chaîne, nombreOctetsDonnées);
            int[] octet236 = new int[8] { 1, 1, 1, 0, 1, 1, 0, 0 };
            int[] octet17 = new int[8] { 0, 0, 0, 1, 0, 0, 0, 1 };
            int index = 0;
            int compteur1 = 0;
            int compteur2 = 0;
            int EnleverBitChaine = chaîne.Count - 1;
            for (int i = listeOctets.Count - 1; i >= 0; i--)
            {
                /*if(listeOctets[i] != octet236 && listeOctets[i] != octet17)
                {
                    
                    break;
                }*/
                index = 0;
                compteur1 = 0;
                compteur2 = 0;
                foreach (int bit in listeOctets[i])
                {
                    if (bit == octet236[index])
                    {
                        compteur1++;
                    }
                    if (bit == octet17[index])
                    {
                        compteur2++;
                    }
                    index++;
                }
                if (compteur1 != 8 && compteur2 != 8)
                {
                    break;
                }
                listeOctets.RemoveAt(i);
            }
            List<int> chaineDonnées = new List<int>();
            foreach (int[] octet in listeOctets) //On met tous ces octets dans une liste comprenant toutes les données à traîter
            {
                foreach (int bit in octet)
                {
                    chaineDonnées.Add(bit);
                }
            }
            //On a maintenant une chaine qui ne détient que les informations à décoder (sans le correcteur)
            if (chaineDonnées.Count % 8 != 0)
            {
                Console.WriteLine("Erreur, la chaine de données n'est pas un multiple de 8");
            }
            for (int i = 0; i < 13; i++) //On enlève les 13 premiers bits qui correspondent au mode et au nombre de caractères
            {
                chaineDonnées.RemoveAt(0);
            }
            int nombreBitPhrase = 0; //Cette variable calcule le nombre de bits nécessaires pour coder le nombre de caractères de la phrase
            if (NombreCaracteres % 2 == 0)
            {
                nombreBitPhrase = (NombreCaracteres / 2) * 11;
            }
            else
            {
                nombreBitPhrase = ((NombreCaracteres - 1) / 2) * 11 + 6;
            }
            int tailleChaine = chaineDonnées.Count;
            for (int i = 0; i < tailleChaine - nombreBitPhrase; i++) //On enlève les 0 qui on été rajoutés pour compléter la chaîne
            {
                chaineDonnées.RemoveAt(tailleChaine - 1 - i);
            }
            string message = ""; //Le message que l'on va décoder 
            int poidsPremièreLettre = 0;
            int poidsDeuxièmeLettre = 0;
            string strpremièreLettre = "";
            string strdeuxièmeLettre = "";
            List<int> groupede11bit = new List<int>();
            for (int i = 0; i < chaineDonnées.Count; i++) //On va chercher quels lettres sont comprises dans le message, en les analysant paire par paire
            {
                //On remplis une liste de bits, on la traîte lorsqu'il y a 11 bits, puis on la vide afin de la re-remplir avec les 11 bits suivants.
                groupede11bit.Add(chaineDonnées[i]);
                if (groupede11bit.Count == 11)  //Si il y a 11 bits dans la liste on la traite
                {
                    int[] tab = new int[11]; //On passe cette liste en un tableau afin d'utiliser la méthode Convertir_Binaire_to_int
                    int compteur = 0;
                    foreach (int bit in groupede11bit)
                    {
                        tab[compteur] = bit;
                        compteur++;
                    }
                    int valeur = Convertir_Binaire_to_int(tab);
                    //On calcule le poids des deux lettres
                    poidsDeuxièmeLettre = valeur % 45;
                    poidsPremièreLettre = (valeur - poidsDeuxièmeLettre) / 45;
                    strpremièreLettre = FromPoidsToCaractère(poidsPremièreLettre);
                    strdeuxièmeLettre = FromPoidsToCaractère(poidsDeuxièmeLettre);
                    message += strpremièreLettre + strdeuxièmeLettre; //Et on ajoute chaque lettre dans le message

                    groupede11bit.Clear();
                }
                if (i == chaineDonnées.Count - 1 && (nombreBitPhrase % 11) != 0) //On le traite le cas où le nombre de lettres est impaire et que la dernière lettre est codée sur 6 bits et non 11
                {
                    int[] tab = new int[6];
                    int compteur = 0;
                    foreach (int bit in groupede11bit)
                    {
                        tab[compteur] = bit;
                        compteur++;
                    }
                    int valeur = Convertir_Binaire_to_int(tab);
                    strpremièreLettre = FromPoidsToCaractère(valeur);
                    message += strpremièreLettre;
                }
            }
            return message; //On affiche le message reçu
        }



        #endregion

    }
}
