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
    /// classe permettant la création de nouveau fichier bitmap
    /// </summary>
    public class NewImage
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
        /// <param name="nom"></param> nom à donner au fichier
        /// <param name="l"></param> largeur à donner à l'image
        /// <param name="h"></param> hauteur à donner à l'image
        public NewImage(string nom, int l, int h)
        {
            //on rentre les paramètres en dur
            this.nomFichier = nom;
            this.hauteur = h;
            this.largeur = l;
            this.tailleOffset = 40;
            this.nbrBits = 24;
            this.typeFichier = "BM";
            this.tailleFichier = this.hauteur * this.largeur * 3 + 54;
            this.tabImage = new Pixel[Convert.ToInt32(this.hauteur * this.largeur)];
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

            for (int i = 0; i < tabImage.Length; i++)
            {
                this.tabImage[i] = new Pixel(0, 0, 0);
            }
            for (int x = 54; x < fichier.Length; x = x + 3)
            {
                //j'ai été obligé d'intervertir le bleu et le Rouge afin qu'on ait RVB dans l'ordre
                fichier[x] = this.tabImage[compteur].B;
                fichier[x + 1] = this.tabImage[compteur].V;
                fichier[x + 2] = this.tabImage[compteur].R;
                compteur++;
            }

            File.WriteAllBytes(nom, fichier);
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

        #region PRATIQUE
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
        /// applique les valeurs d'une matrice de pixel sur le tableau de pixel d'une image (spécifiquement pour les qrcode)
        /// </summary>
        /// <param name="matrice"></param> matrice de pixel à appliquer
        public void AppliquerModifQR(Pixel[,] matrice)//je pars d'une matrice de pixel et l'applique dans le tabImage
        {
            int largeur = matrice.GetLength(1);
            int hauteur = matrice.GetLength(0);
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    this.tabImage[j + i * largeur] = matrice[hauteur - i - 1, j];
                }
            }
        }
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

        #endregion
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
        /// affiche l'histogramme d'une image pour une couleur
        /// </summary>
        /// <param name="intensite"></param> valeurs des intensites de bytes pour chaque couleur
        /// <param name="lettre"></param> lettre de la couleur qu'on désire afficher
        public void AffichageHisto(List<double[]> intensite, string lettre)
        {
            lettre = lettre.ToUpper();
            double[] tabRouge = intensite.ElementAt(0);
            double[] tabVert = intensite.ElementAt(1);
            double[] tabBleu = intensite.ElementAt(2);
            //on normalise les valeurs trouvées afin de les mettre dans un graphique
            //le 9000 est trouvé de façon expérimentale
            for (int i = 0; i < tabBleu.Length; i++)
            {
                tabBleu[i] = (tabBleu[i] / tabImage.Length) * 9000;
                tabRouge[i] = (tabRouge[i] / tabImage.Length) * 9000;
                tabVert[i] = (tabVert[i] / tabImage.Length) * 9000;

            }
            int largeur = Convert.ToInt32(this.largeur);
            int hauteur = Convert.ToInt32(this.hauteur);
            this.tabImage = new Pixel[hauteur * largeur];
            this.tailleFichier = (hauteur * largeur * 3) + 54; //on ajoute la taille du header et on multiplie par trois car un pixel vaut 3 bytes
            Pixel[,] image = new Pixel[hauteur, largeur];
            //on crée un fond blanc
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    image[i, j] = new Pixel(255, 255, 255);
                }

            }
            //on s'occupe des cas où on aurait des valeurs qui auraient des ordonnées trop importantes
            List<double> supR = new List<double>();
            List<double> supV = new List<double>();
            List<double> supB = new List<double>();
            for (int i = 0; i < 256; i++)
            {
                if (tabRouge[i] > hauteur)
                {
                    supR.Add(tabRouge[i]);
                }
                if (tabBleu[i] > hauteur)
                {
                    supB.Add(tabBleu[i]);
                }
                if (tabVert[i] > hauteur)
                {
                    supV.Add(tabVert[i]);
                }
            }

            if (supR.Count() != 0)//cas où une valeur sort du cadre
            {
                //on regarde le max et on divise toutes les valeurs pas le nombre qu'il faut pour que le max rentre dans le cadre
                double max = supR.Max();
                double diviseurR = (max / 300) + 1;
                for (int i = 0; i < 256; i++)
                {
                    tabRouge[i] = tabRouge[i] / diviseurR;
                }
            }
            if (supV.Count() != 0)
            {
                double max = supV.Max();
                double diviseurV = (max / 300) + 1;
                for (int i = 0; i < 256; i++)
                {
                    tabVert[i] = tabVert[i] / diviseurV;
                }
            }
            if (supB.Count() != 0)
            {
                double max = supB.Max();
                double diviseurB = (max / 300) + 1;
                for (int i = 0; i < 256; i++)
                {
                    tabBleu[i] = tabBleu[i] / diviseurB;
                }
            }
            Pixel[,] copie = copieMatrice(image); //on duplique le fond blanc
            //on crée l'histogramme de la couleur sélectionnée
            for (int j = 0; j < largeur; j++)
            {
                if (lettre == "R")
                {
                    for (int i = Convert.ToInt32(hauteur - tabRouge[j]); i < hauteur; i++)
                    {
                        image[i, j] = new Pixel(Convert.ToByte(j), 0, 0); //on met j comme valeur d'intensité pour que ca donne un joli dégradé sur l'histogramme
                    }
                }
                if (lettre == "V")
                {
                    for (int b = Convert.ToInt32(hauteur - tabVert[j]); b < hauteur; b++)
                    {
                        image[b, j] = new Pixel(0, Convert.ToByte(j), 0);
                    }
                }
                if (lettre == "B")
                {
                    for (int a = Convert.ToInt32(hauteur - tabBleu[j]); a < hauteur; a++)
                    {
                        image[a, j] = new Pixel(0, 0, Convert.ToByte(j));
                    }
                }


            }
            //on retourne notre histogramme pour le mettre à l'endroit
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    copie[i, j] = image[image.GetLength(0) - 1 - i, j];
                }
            }
            AppliquerModif(copie);

        }


        #region FRACTALES
        /// <summary>
        /// retourne une fractale sans nom qui a servit de test
        /// </summary>
        public void fractaleQuelconque()
        {
            //première fractale réalisée, un test peu concluant
            int centreX = Convert.ToInt32(this.largeur / 2);
            int centreY = Convert.ToInt32(this.hauteur / 2);
            for (int y = 0; y < this.hauteur; y++)
            {
                for (int x = 0; x < this.largeur; x++)
                {
                    Complexe z = new Complexe((centreX - x) * 0.007, (centreY - y) * 0.007);
                    Complexe z0 = z;

                    for (int i = 0; i < 15; i++)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + z0.Reel, z0.Imaginaire + (2 * z0.Reel * z0.Imaginaire));
                        if (z.Module < 2 && i == 14)
                        {
                            this.tabImage[Convert.ToInt32(x * this.largeur + y)] = new Pixel(255, 0, 0);
                        }
                        else
                        {
                            this.tabImage[Convert.ToInt32(x * this.largeur + y)] = new Pixel(255, 255, 255);
                        }

                    }
                }
            }
        }
        /// <summary>
        /// retourne l'ensemble de mandelbrot
        /// </summary>
        public void Mandelbrot()
        {
            Pixel[,] image = CréationMatricePixel(); //on met notre image sous forme de matrice
                                                     //les fractales sont des suites qui convergent que sur un certain intervalle, différent pour chaque fractale
            double x1 = -2.1;
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;
            int iMax = 50; //nombre d'itérations max pour la boucle
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            //sachant que nos pixels ont pour index des index, on doit appliquer un zoom si on veut voir quelque chose sur notre image
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    //les complexes Z et C sont donnés
                    Complexe c = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    Complexe z = new Complexe(0, 0);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        //on applique la formule z = z^2 + c
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        //si i a atteint le nombre iMax, on met le pixel en noir
                        image[y, x] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        //sinon on crée un dégradé en fonction de i, ce qui montre à quel point on été près de converger en ce point
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);//on applique les modifications sur le tableau d'image
        }
        //même procédé pour chaque fractale suivante
        //le c est différent et donné
        //on trouve expérimentalement l'intervalle sur lequel la fractale est visible et le nombre iMax le plus pertinent
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia1()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.1;
            double x2 = 1.1;
            double y1 = -1.2;
            double y2 = 1.2;
            int iMax = 50;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(0.3, 0.5);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia2()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.7;
            double x2 = 1.7;
            double y1 = -1.5;
            double y2 = 1.5;
            int iMax = 17;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(-1.476, 0);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia3()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.5;
            double x2 = 1.5;
            double y1 = -1.5;
            double y2 = 1.5;
            int iMax = 50;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(-0.8, 0.156);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia4()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.4;
            double x2 = 1.4;
            double y1 = -1.5;
            double y2 = 1.5;
            int iMax = 50;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(-0.4, 0.6);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia5()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.1;
            double x2 = 1.1;
            double y1 = -1.2;
            double y2 = 1.2;
            int iMax = 150;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(0.285, 0.013);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }
        /// <summary>
        /// affiche un ensemble de julia
        /// </summary>
        public void EnsembleDeJulia6()
        {
            Pixel[,] image = CréationMatricePixel();
            double x1 = -1.1;
            double x2 = 1.1;
            double y1 = -1.2;
            double y2 = 1.2;
            int iMax = 20;
            double tailleX = this.hauteur;
            double tailleY = this.largeur;
            double zoomX = tailleX / (x2 - x1);
            double zoomY = tailleY / (y2 - y1);
            for (int x = 0; x < tailleX; x++)
            {
                for (int y = 0; y < tailleY; y++)
                {
                    Complexe c = new Complexe(-0.038088, 0.9754);
                    Complexe z = new Complexe(x / zoomX + x1, y / zoomY + y1);
                    int i = 0;
                    while (i < iMax && z.Module < 2)
                    {
                        z = new Complexe((z.Reel * z.Reel) - (z.Imaginaire * z.Imaginaire) + c.Reel, (2 * z.Imaginaire * z.Reel) + c.Imaginaire);
                        i = i + 1;
                    }
                    if (i == iMax)
                    {
                        image[y, x] = new Pixel(255, 255, 255);
                    }
                    else
                    {
                        image[y, x] = new Pixel(Convert.ToByte(i * 255 / iMax), Convert.ToByte(i * 255 / iMax), 0);
                    }
                }
            }
            AppliquerModif(image);

        }

        #endregion

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
        /// <summary>
        /// convertit un octet de bits en entier
        /// </summary>
        /// <param name="tab"></param> octet de bits
        /// <returns></returns> entier correspondant à l'octet
        public static byte Convertir_octet_to_byte(int[] tab)
        {
            double résultat = 0;
            double exposant = 0;
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                if (tab[i] == 1)
                {
                    résultat = résultat + Math.Pow(2, exposant);
                }
                exposant++;
            }
            byte retour = Convert.ToByte(résultat);
            return retour;
        }
        #endregion

        #region QR CODE
        /// <summary>
        /// fait correspondre chaque caractère à son poids
        /// </summary>
        /// <param name="chaine"></param> chaine de mots qu'on veut peser
        /// <returns></returns> le poids en entier la chaine
        public int PoidsCaractères(List<string> chaine)
        {
            string[] mot = new string[chaine.Count()];
            for (int i = 0; i < mot.Length; i++)
            {
                mot[i] = Convert.ToString(chaine.ElementAt(i));
                mot[i] = mot[i].ToUpper();
            }
            List<int> poids = new List<int>();
            for (int i = 0; i < mot.Length; i++)
            {
                if (mot[i] == "0") { poids.Add(0); }
                if (mot[i] == "1") { poids.Add(1); }
                if (mot[i] == "2") { poids.Add(2); }
                if (mot[i] == "3") { poids.Add(3); }
                if (mot[i] == "4") { poids.Add(4); }
                if (mot[i] == "5") { poids.Add(5); }
                if (mot[i] == "6") { poids.Add(6); }
                if (mot[i] == "7") { poids.Add(7); }
                if (mot[i] == "8") { poids.Add(8); }
                if (mot[i] == "9") { poids.Add(9); }
                if (mot[i] == "A") { poids.Add(10); }
                if (mot[i] == "B") { poids.Add(11); }
                if (mot[i] == "C") { poids.Add(12); }
                if (mot[i] == "D") { poids.Add(13); }
                if (mot[i] == "E") { poids.Add(14); }
                if (mot[i] == "F") { poids.Add(15); }
                if (mot[i] == "G") { poids.Add(16); }
                if (mot[i] == "H") { poids.Add(17); }
                if (mot[i] == "I") { poids.Add(18); }
                if (mot[i] == "J") { poids.Add(19); }
                if (mot[i] == "K") { poids.Add(20); }
                if (mot[i] == "L") { poids.Add(21); }
                if (mot[i] == "M") { poids.Add(22); }
                if (mot[i] == "N") { poids.Add(23); }
                if (mot[i] == "O") { poids.Add(24); }
                if (mot[i] == "P") { poids.Add(25); }
                if (mot[i] == "Q") { poids.Add(26); }
                if (mot[i] == "R") { poids.Add(27); }
                if (mot[i] == "S") { poids.Add(28); }
                if (mot[i] == "T") { poids.Add(29); }
                if (mot[i] == "U") { poids.Add(30); }
                if (mot[i] == "V") { poids.Add(31); }
                if (mot[i] == "W") { poids.Add(32); }
                if (mot[i] == "X") { poids.Add(33); }
                if (mot[i] == "Y") { poids.Add(34); }
                if (mot[i] == "Z") { poids.Add(35); }
                if (mot[i] == " ") { poids.Add(36); }
                if (mot[i] == "$") { poids.Add(37); }
                if (mot[i] == "%") { poids.Add(38); }
                if (mot[i] == "*") { poids.Add(39); }
                if (mot[i] == "+") { poids.Add(40); }
                if (mot[i] == "-") { poids.Add(41); }
                if (mot[i] == ".") { poids.Add(42); }
                if (mot[i] == "/") { poids.Add(43); }
                if (mot[i] == ":") { poids.Add(44); }

            }
            int compteur = poids.Count() - 1;
            int res = 0;
            for (int i = 0; i < poids.Count(); i++)
            {
                res = res + poids.ElementAt(i) * Convert.ToInt32(Math.Pow(45, compteur));
                compteur--;
            }
            return res;

        }
        /// <summary>
        /// affiche nombre de caractères d'une chaine
        /// </summary>
        /// <param name="chaine"></param> chaine de caractères à mesurer
        /// <returns></returns> longueur de la chaine
        public int NombreCaractères(string chaine)
        {
            return chaine.Length;
        }
        /// <summary>
        /// choisit la version en fonction de la taille de la chaine
        /// </summary>
        /// <param name="chaine"></param> chaine qu'on traite
        /// <returns></returns> la version (1 ou 2)
        public int ChoixVersion(string chaine)
        {
            int taille = NombreCaractères(chaine);
            if (taille <= 25)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        /// <summary>
        /// calcule le message d'erreur
        /// </summary>
        /// <param name="chaine"></param> chaine dont on veut mesurer l'erreur
        /// <returns></returns> une liste d'octet dont chacun coresspond à un byte d'erreur
        public List<int[]> CodeErreur(string chaine)
        {
            Encoding u8 = Encoding.UTF8;
            //string a = "HELLO WORLD";
            int iBC = u8.GetByteCount(chaine);
            //byte[] bytesa = u8.GetBytes(chaine);
            byte[] bytesa = new byte[36];
            byte[] result = new byte[10];
            if (ChoixVersion(chaine) == 1)
            {
                bytesa = new byte[19];
                result = new byte[7];
            }
            int compteur = 0;
            foreach (int[] tab in ChaineOctets(chaine))
            {
                byte valeur = Convertir_octet_to_byte(tab);
                bytesa[compteur] = valeur;
                
                compteur++;
            }
            if (ChoixVersion(chaine) == 1)
            {
                result = ReedSolomonAlgorithm.Encode(bytesa, 7, ErrorCorrectionCodeType.QRCode);
            }
            else
            {
                result = ReedSolomonAlgorithm.Encode(bytesa, 10, ErrorCorrectionCodeType.QRCode);
            }
            List<int[]> erreur = new List<int[]>();
            foreach (byte b in result)
            {
                
                int[] octet = Convertir_Int_To_Binaire(Convert.ToInt32(b), 8);
                erreur.Add(octet);
            }
            return erreur;
        }
        /// <summary>
        /// met les caractères d'une chaine par paire
        /// </summary>
        /// <param name="chaine"></param> chaine qu'on traite
        /// <returns></returns> une list de binome ou monome de caractère si un caractère est seul
        public List<List<string>> PaireCaractères(string chaine)
        {
            //fonction qui met les caractères par paire sauf le dernier si nécessaire
            List<List<string>> lot = new List<List<string>>();
            for (int i = 0; i < chaine.Length; i = i + 2)
            {
                List<string> paire = new List<string>();
                if (i == chaine.Length - 1)
                {
                    paire.Add(Convert.ToString(chaine[i]));
                }
                else
                {
                    paire.Add(Convert.ToString(chaine[i]));
                    paire.Add(Convert.ToString(chaine[i + 1]));

                }
                lot.Add(paire);
            }
            return lot;
        }
        /// <summary>
        /// affiche nombre de bits totaux pour unecertaine version
        /// </summary>
        /// <param name="version"></param> version qu'on traite
        /// <returns></returns> nombre de bits totaux pour la version en question
        public int TotalBits(int version)
        {
            //fonction qui retourne le nombre de bits nécessaires pour les données en fonction de la version
            int totalBits = 0;
            if (version == 1)
            {
                totalBits = 19 * 8;
            }
            else
            {
                totalBits = 34 * 8;
            }
            return totalBits;
        }
        /// <summary>
        /// retourne les bits de données d'une chaine de carctères sous forme d'octets
        /// </summary>
        /// <param name="chaine"></param>chaine qu'on traite
        /// <returns></returns> octets de données
        public List<int[]> ChaineOctets(string chaine)
        {
            //fonction qui retourne tous les octets par blocs de 8
            List<int> vrac = new List<int>();
            //indicateur de mode
            vrac.Add(0);
            vrac.Add(0);
            vrac.Add(1);
            vrac.Add(0);
            //indicateur nombre de caractères
            int[] tab = Convertir_Int_To_Binaire(NombreCaractères(chaine), 9);
            for (int i = 0; i < tab.Length; i++)
            {
                vrac.Add(tab[i]);
            }
            //mettre les caractères de la chaine par paire
            List<List<string>> paires = PaireCaractères(chaine);
            //définir le poids des caractères
            foreach (List<string> l in paires)
            {
                if (l.Count() == 2)
                {
                    int[] tab1 = Convertir_Int_To_Binaire(PoidsCaractères(l), 11);
                    for (int i = 0; i < tab1.Length; i++)
                    {
                        vrac.Add(tab1[i]);
                    }
                }
                else
                {
                    int[] tab1 = Convertir_Int_To_Binaire(PoidsCaractères(l), 6);
                    for (int i = 0; i < tab1.Length; i++)
                    {
                        vrac.Add(tab1[i]);
                    }
                }

            }
            //nombre de bits requis pour le qr
            int version = ChoixVersion(chaine);
            int totalBits = TotalBits(version);
            //terminaison
            int diff = totalBits - vrac.Count();
            int iMax = Math.Min(diff, 4);
            for (int i = 0; i < iMax; i++)
            {
                vrac.Add(0);
            }
            //mettre en octets
            //on comble avec des 0
            if (vrac.Count() % 8 != 0)
            {
                while (vrac.Count() % 8 != 0)
                {
                    vrac.Add(0);
                }
            }
            List<int[]> allOctets = new List<int[]>();
            int quotient = vrac.Count() / 8;
            int moins = 0;
            for (int i = 0; i < quotient; i++)
            {
                int[] octet = new int[8];
                for (int a = 0; a < octet.Count(); a++)
                {
                    octet[a] = vrac.ElementAt(i * 8 + a);
                }
                if (version == 1)
                {
                    allOctets.Add(octet);
                }
                if (version == 2)
                {
                    int val = 0;
                    for (int j = 0; j < octet.Count(); j++)
                    {
                        if (octet[j] == 0)
                        {
                            val++;
                        }
                    }
                    if (val == 8)
                    {
                        moins++;
                    }
                    if (val < 8)
                    {
                        allOctets.Add(octet);
                    }
                }

            }
            //combler jusqu'au nombre de bits total par les octets suivants
            int restant = totalBits - vrac.Count() + 8 * moins;
            int octetsManquants = restant / 8;
            for (int i = 0; i < octetsManquants; i++)
            {
                int[] octet = new int[8];
                if (i % 2 == 0)
                {
                    octet = new int[] { 1, 1, 1, 0, 1, 1, 0, 0 };

                }
                else
                {
                    octet = new int[] { 0, 0, 0, 1, 0, 0, 0, 1 };

                }
                allOctets.Add(octet);
            }
            return allOctets;

        }
        /// <summary>
        /// fond gris qui délimite les zones où les données s'écriront
        /// </summary>
        /// <param name="version"></param> version que le qr code va afficher
        /// <returns></returns> le fond gris d'une certaine taille en fonction de la version
        public Pixel[,] FondGris(int version)
        {
            //fonction qui crée un fond gris avec des limites pour les zones où on rentre les données
            int diviseur = 0;
            //on détermine des tailles pour chaque version
            if (version == 1)
            {
                this.hauteur = 840;
                this.largeur = 840;
                diviseur = 21;
            }
            else
            {
                this.hauteur = 1000;
                this.largeur = 1000;
                diviseur = 25;
            }
            this.tailleFichier = (this.hauteur * this.largeur * 3) + 54; //on ajoute la taille du header et on multiplie par trois car un pixel vaut 3 bytes
            this.tabImage = new Pixel[Convert.ToInt32(Convert.ToInt32(this.hauteur * this.largeur))];
            //on crée un fond gris
            for (int i = 0; i < this.tabImage.Length; i++)
            {
                this.tabImage[i] = new Pixel(180, 180, 180);
            }
            Pixel[,] image = CréationMatricePixel();
            int coeff = Convert.ToInt32(this.hauteur / diviseur); //coeff va permettre de passer de case en case et sera différent pour chaque version
            int taille = Convert.ToInt32(this.hauteur);
            //on crée des lignes rouges réservées pour le masque qui servent à délimiter les emplacement pour les donées
            //de façon verticale
            for (int i = taille - 1; i > 0; i = i - coeff)
            {
                if (i > taille - 7 * coeff || (i > coeff * 7 && i < coeff * 9) || (i < coeff * 6))
                {
                    for (int a = coeff * 8; a < coeff * 9; a++)
                    {
                        for (int b = i; b > i - coeff; b--)
                        {
                            image[b, a] = new Pixel(255, 0, 0);
                        }
                    }

                }

            }
            //de façon horizontale

            for (int b = 0; b < taille; b = b + coeff)
            {

                if (b < coeff * 6 || (b < coeff * 8 && b >= coeff * 7) || (b > taille - coeff * 9))
                {

                    for (int i = coeff * 8; i < coeff * 9; i++)
                    {
                        for (int a = b; a < b + coeff; a++)
                        {
                            image[i, a] = new Pixel(255, 0, 0);
                        }
                    }

                }

            }
            return image;
        }
        /// <summary>
        /// affichage des paterns, modules d'alignement et de synchronisation et le masque
        /// </summary>
        /// <param name="version"></param> version du qr code en cours
        /// <param name="image"></param> matrice sur laquelle on applique les modules
        public void AppliquerFond(int version, Pixel[,] image)
        {
            int taille = image.GetLength(0);
            int coeff = 0;
            if (version == 1)
            {
                coeff = taille / 21;
            }
            else
            {
                coeff = taille / 25;
            }
            //création des trois patern dans les angles correspondants
            for (int i = 2 * coeff; i < 5 * coeff; i++)
            {
                for (int j = 2 * coeff; j < 5 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(0, 0, 0);
                    //patern en bas à gauche
                    image[taille - 1 - i, j] = new Pixel(0, 0, 0);
                    //patern en haut à droite
                    image[i, taille - j - 1] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 0; i < 1 * coeff; i++)
            {
                for (int j = 0; j < 7 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(0, 0, 0);
                    image[j, i] = new Pixel(0, 0, 0);
                    //patern en bas à gauche
                    image[taille - 1 - i, j] = new Pixel(0, 0, 0);
                    image[taille - j - 1, i] = new Pixel(0, 0, 0);
                    //patern en haut à droite
                    image[i, taille - j - 1] = new Pixel(0, 0, 0);
                    image[j, taille - i - 1] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 6 * coeff; i < 7 * coeff; i++)
            {
                for (int j = 0; j < 7 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(0, 0, 0);
                    image[j, i] = new Pixel(0, 0, 0);
                    //patern en bas à gauche
                    image[taille - 1 - i, j] = new Pixel(0, 0, 0);
                    image[taille - j - 1, i] = new Pixel(0, 0, 0);
                    //patern en haut à droite
                    image[i, taille - j - 1] = new Pixel(0, 0, 0);
                    image[j, taille - i - 1] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 1 * coeff; i < 2 * coeff; i++)
            {
                for (int j = 1 * coeff; j < 6 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(255, 255, 255);
                    image[j, i] = new Pixel(255, 255, 255);
                    //patern en bas à gauche
                    image[taille - j - 1, i] = new Pixel(255, 255, 255);
                    image[taille - i - 1, j] = new Pixel(255, 255, 255);
                    //patern en haut à droite
                    image[j, taille - i - 1] = new Pixel(255, 255, 255);
                    image[i, taille - j - 1] = new Pixel(255, 255, 255);
                }
            }
            for (int i = 5 * coeff; i < 6 * coeff; i++)
            {
                for (int j = 1 * coeff; j < 6 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(255, 255, 255);
                    image[j, i] = new Pixel(255, 255, 255);
                    //patern en bas à gauche
                    image[taille - i - 1, j] = new Pixel(255, 255, 255);
                    image[taille - j - 1, i] = new Pixel(255, 255, 255);
                    //patern en haut à droite
                    image[j, taille - i - 1] = new Pixel(255, 255, 255);
                    image[i, taille - j - 1] = new Pixel(255, 255, 255);
                }
            }
            if (version == 2) //création du module d'alignement
            {

                for (int i = taille - coeff * 9; i < taille - coeff * 4; i++)
                {
                    for (int z = taille - coeff * 9; z < taille - coeff * 4; z++)
                    {
                        image[i, z] = new Pixel(0, 0, 0);
                    }
                }
                for (int i = taille - coeff * 8; i < taille - coeff * 5; i++)
                {
                    for (int z = taille - coeff * 8; z < taille - coeff * 5; z++)
                    {
                        image[i, z] = new Pixel(255, 255, 255);
                    }
                }
                for (int i = taille - coeff * 7; i < taille - coeff * 6; i++)
                {
                    for (int z = taille - coeff * 7; z < taille - coeff * 6; z++)
                    {
                        image[i, z] = new Pixel(0, 0, 0);
                    }
                }
            }
            //lignes bleues
            for (int i = 8 * coeff; i < 9 * coeff; i++)
            {
                for (int j = 0; j < 9 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(0, 0, 255);
                    image[j, i] = new Pixel(0, 0, 255);

                    if (j < 8 * coeff)
                    {
                        //patern en bas à gauche
                        image[taille - j - 1, i] = new Pixel(0, 0, 255);
                        //patern en haut à droite
                        image[i, taille - j - 1] = new Pixel(0, 0, 255);
                    }


                }
            }
            //séparateurs
            for (int i = 7 * coeff; i < 8 * coeff; i++)
            {
                for (int j = 0; j < 8 * coeff; j++)
                {
                    //patern en haut à gauche
                    image[i, j] = new Pixel(255, 255, 255);
                    image[j, i] = new Pixel(255, 255, 255);
                    //patern en bas à gauche
                    image[taille - i - 1, j] = new Pixel(255, 255, 255);
                    image[taille - j - 1, i] = new Pixel(255, 255, 255);
                    //patern en haut à droite
                    image[j, taille - i - 1] = new Pixel(255, 255, 255);
                    image[i, taille - j - 1] = new Pixel(255, 255, 255);


                }
            }
            //motif synchronisation horizontal
            for (int i = 6 * coeff; i < 7 * coeff; i++)
            {
                int compteur = 0;
                for (int j = 8 * coeff; j < taille - 8 * coeff; j = j + coeff)
                {
                    for (int a = j; a < j + coeff; a++)
                    {
                        if (compteur % 2 == 0)
                        {
                            image[i, a] = new Pixel(0, 0, 0);
                        }
                        else
                        {
                            image[i, a] = new Pixel(255, 255, 255);

                        }
                    }
                    compteur++;
                }
            }
            //motif synchronisation vertical
            for (int j = coeff * 6; j < coeff * 7; j++)
            {
                int compteur = 0;
                for (int i = coeff * 8; i < taille - coeff * 8; i = i + coeff)
                {
                    for (int a = i; a < i + coeff; a++)
                    {
                        if (compteur % 2 == 0)
                        {
                            image[a, j] = new Pixel(0, 0, 0);
                        }
                        else
                        {
                            image[a, j] = new Pixel(255, 255, 255);

                        }
                    }
                    compteur++;
                }
            }
            //module sombre
            int posY = (4 * version) + 9;
            for (int i = posY * coeff; i < (posY + 1) * coeff; i++)
            {
                for (int j = 8 * coeff; j < 9 * coeff; j++)
                {
                    image[i, j] = new Pixel(0, 0, 0);
                }
            }
            //remplissage des zones prévues pour le masque
            //de façon verticale
            int[] masque = new int[] { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 };
            int v = 0;

            for (int i = taille - 1; i > 0; i = i - coeff)
            {
                if (i > taille - 7 * coeff || (i > coeff * 7 && i < coeff * 9) || (i < coeff * 6))
                {
                    for (int a = coeff * 8; a < coeff * 9; a++)
                    {
                        for (int b = i; b > i - coeff; b--)
                        {
                            if (masque[v] == 0)
                            {
                                image[b, a] = new Pixel(255, 255, 255);
                            }
                            if (masque[v] == 1)
                            {
                                image[b, a] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    v++;
                }

            }
            //de façon horizontale
            v = 0;

            for (int b = 0; b < taille; b = b + coeff)
            {
                if (b == coeff * 10)
                {
                    v = 7; //pour que le septième index du masque soit à nouveau copié
                }
                if (b < coeff * 6 || (b < coeff * 8 && b >= coeff * 7) || (b > taille - coeff * 9))
                {

                    for (int i = coeff * 8; i < coeff * 9; i++)
                    {
                        for (int a = b; a < b + coeff; a++)
                        {
                            if (masque[v] == 0)
                            {
                                image[i, a] = new Pixel(255, 255, 255);
                            }
                            if (masque[v] == 1)
                            {
                                image[i, a] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    v++;
                }

            }
        }
        /// <summary>
        /// affiche le qr code définitif
        /// </summary>
        /// <param name="chaine"></param> message qu'on veut afficher
        public void AffichageQR(string chaine)
        {
            int version = ChoixVersion(chaine);
            //Pixel[,] image = Fond_QR(version);
            Pixel[,] image = FondGris(version);
            int diviseur = 0;
            if (version == 1)
            {
                this.hauteur = 840;
                this.largeur = 840;
                diviseur = 21;
            }
            else
            {
                this.hauteur = 1000;
                this.largeur = 1000;
                diviseur = 25;
            }
            this.tailleFichier = (this.hauteur * this.largeur * 3) + 54; //on ajoute la taille du header et on multiplie par trois car un pixel vaut 3 bytes
            this.tabImage = new Pixel[Convert.ToInt32(Convert.ToInt32(this.hauteur * this.largeur))];
            int coeff = Convert.ToInt32(this.hauteur / diviseur);
            int taille = Convert.ToInt32(this.hauteur);
            List<int[]> octets = ChaineOctets(chaine);
            List<int> bits = new List<int>();
            //on rentre toutes les données
            for (int i = 0; i < octets.Count(); i++)
            {
                foreach (int a in octets.ElementAt(i))
                {
                    bits.Add(a);
                }
            }
            //on ajoute à la fin les données du code d'erreur
            List<int[]> erreur = CodeErreur(chaine);
            for (int i = 0; i < erreur.Count(); i++)
            {
                foreach (int a in erreur.ElementAt(i))
                {
                    bits.Add(a);
                }
            }
            //on affiche ces données sur le qr code
            int index = taille - 1;
            int plus = 0;
            int ajout = 0;
            int j = taille - 1;
            if (version == 1)
            {
                int nombreBoucle = 0;
                int itération = 192;

                while (plus < itération)
                {
                    //mouvement vers le haut
                    index = taille - 1;

                    if (nombreBoucle % 2 == 0)
                    {

                        if (nombreBoucle == 0)
                        {
                            j = taille - 1;
                        }
                        else
                        {
                            j = taille - 2 * nombreBoucle * coeff - 1;
                        }
                        if (nombreBoucle >= 6)
                        {
                            index = taille - 8 * coeff - 1;
                        }


                        while (index >= 0 && nombreBoucle % 2 == 0 && image[index, j].B == 180 && image[index, j].V == 180 && image[index, j].R == 180 && plus < itération && j >= 0)
                        {

                            if (nombreBoucle == 0)
                            {
                                j = taille - 1;
                            }
                            else
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1;
                            }
                            int moins = 0;
                            if (nombreBoucle >= 7)
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1 - coeff;
                                moins = coeff;
                            }

                            while (j >= taille - (2 * nombreBoucle + 1) * coeff - moins && j >= 0)
                            {
                                ajout = index;
                                while (image[ajout, j].B == 180 && image[ajout, j].V == 180 && image[ajout, j].R == 180 && ajout > index - coeff && j >= 0)
                                {
                                    if (bits.ElementAt(plus) == 1)
                                    {
                                        image[ajout, j] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus) == 0)
                                    {
                                        image[ajout, j] = new Pixel(255, 255, 255);
                                    }
                                    if (bits.ElementAt(plus + 1) == 1)
                                    {
                                        image[ajout, j - coeff] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus + 1) == 0)
                                    {
                                        image[ajout, j - coeff] = new Pixel(255, 255, 255);
                                    }
                                    ajout--;
                                    //permet de passer le module de synchronisation
                                    if (plus == 124 && nombreBoucle == 4)
                                    {
                                        index = coeff * 6 - 1;
                                    }

                                }

                                j--;
                            }


                            plus = plus + 2;
                            index = index - coeff;
                            //permet de passer le module de synchronisation
                            if (plus == 124 && nombreBoucle == 4)
                            {
                                index = coeff - 1;
                                j = j = taille - 2 * nombreBoucle * coeff - 1;
                            }
                        }

                        if ((nombreBoucle < 7 && (nombreBoucle % 2 != 0 || image[index, j].B != 180 || image[index, j].V != 180 || image[index, j].R != 180)) || (nombreBoucle >= 7 && index < coeff * 13 && plus < itération))
                        {
                            nombreBoucle = nombreBoucle + 1;
                        }

                    }


                    index = index + 1;
                    //ce if permet de remplir les 4 cellules au centre en haut
                    if (nombreBoucle == 5)
                    {
                        for (int a = taille - 8 * coeff; a > 9 * coeff; a = a - coeff)
                        {
                            for (int i = 0; i < coeff; i++)
                            {
                                for (int b = a; b > a - coeff; b--)
                                {
                                    if (bits.ElementAt(plus) == 1)
                                    {
                                        image[i, b - 1] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus) == 0)
                                    {
                                        image[i, b - 1] = new Pixel(255, 255, 255);
                                    }

                                }

                            }
                            plus = plus + 1;
                        }

                    }
                    //cas pour éviter les problèmes d'affichage après le module de synchronisation
                    int s = 125;
                    for (int a = taille - 8 * coeff - 1; a > taille - 10 * coeff; a = a - coeff)
                    {
                        for (int i = coeff * 5; i < coeff * 6; i++)
                        {
                            for (int b = a; b > a - coeff; b--)
                            {
                                if (bits.ElementAt(s) == 1)
                                {
                                    image[i, b] = new Pixel(0, 0, 0);
                                }
                                if (bits.ElementAt(s) == 0)
                                {
                                    image[i, b] = new Pixel(255, 255, 255);
                                }

                            }

                        }
                        s++;
                    }

                    //mouvemant vers le bas
                    int x = 0;
                    j = taille - 2 * nombreBoucle * coeff;
                    if (nombreBoucle % 2 != 0)
                    {
                        while (nombreBoucle % 2 != 0 && ((nombreBoucle < 7 && index <= taille - coeff && plus < itération) || (nombreBoucle >= 7 && index < coeff * 13 && plus < itération)))
                        {
                            j = taille - 2 * nombreBoucle * coeff - 1;
                            int moins = 0;
                            if (nombreBoucle >= 7)
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1 - coeff;
                                moins = coeff;
                            }

                            while (j >= taille - (2 * nombreBoucle + 1) * coeff - moins)
                            {
                                ajout = index;

                                while (ajout < index + coeff && ajout < taille)
                                {

                                    if (bits.ElementAt(plus) == 1)
                                    {
                                        image[ajout, j] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus) == 0)
                                    {
                                        image[ajout, j] = new Pixel(255, 255, 255);
                                    }
                                    if (bits.ElementAt(plus + 1) == 1)
                                    {
                                        image[ajout, j - coeff] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus + 1) == 0)
                                    {
                                        image[ajout, j - coeff] = new Pixel(255, 255, 255);
                                    }
                                    ajout++;
                                    //permet de passer le module de synchronisation
                                    if (plus == 148 && nombreBoucle == 5)
                                    {
                                        index = coeff * 7;
                                    }


                                }
                                j--;
                            }
                            x = index + coeff;
                            plus = plus + 2;
                            if (index == taille - coeff)
                            {
                                x = index + coeff - 1;
                            }
                            index = x;


                        }
                        if (image[index, j].B != 180 || image[index, j].V != 180 || image[index, j].R != 180 || (ajout == taille && index == taille - 1))
                        {
                            nombreBoucle = nombreBoucle + 1;
                        }


                    }



                }
                int pos = 192;
                for (int i = coeff * 9; i < coeff * 13; i = i + coeff)
                {
                    for (int a = coeff * 4; a > coeff * 3; a--)
                    {
                        for (int b = i; b < i + coeff; b++)
                        {

                            if (bits.ElementAt(pos + 8) == 0)
                            {
                                image[b, a - 2 * coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 8) == 1)
                            {
                                image[b, a - 2 * coeff - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(pos + 9) == 0)
                            {
                                image[b, a - 3 * coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 9) == 1)
                            {
                                image[b, a - 3 * coeff - 1] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    pos = pos + 2;
                }
                pos = 192;
                for (int i = coeff * 13; i > coeff * 9; i = i - coeff)
                {
                    for (int a = coeff * 4; a > coeff * 3; a--)
                    {
                        for (int b = i; b > i - coeff; b--)
                        {

                            if (bits.ElementAt(pos) == 0)
                            {
                                image[b - 1, a - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos) == 1)
                            {
                                image[b - 1, a - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(pos + 1) == 0)
                            {
                                image[b - 1, a - coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 1) == 1)
                            {
                                image[b - 1, a - coeff - 1] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    pos = pos + 2;
                }
            }
            if (version == 2)
            {
                int nombreBoucle = 0;
                int itération = 327;
                while (plus < itération)
                {
                    if (nombreBoucle % 2 == 0)
                    {
                        index = taille - 1;
                        if (nombreBoucle == 0)
                        {
                            j = taille - 1;
                        }
                        else
                        {
                            j = taille - 2 * nombreBoucle * coeff - 1;
                        }

                        if (nombreBoucle >= 8)
                        {
                            index = taille - coeff * 8 - 1;
                        }
                        while (index >= 0 && nombreBoucle % 2 == 0 && image[index, j].B == 180 && image[index, j].V == 180 && image[index, j].R == 180 && plus < itération && j >= 0)
                        {
                            int moins = 0;
                            if (nombreBoucle >= 9)
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1 - coeff;
                                moins = coeff;
                            }
                            if (nombreBoucle == 0)
                            {
                                j = taille - 1;
                            }
                            else
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1;
                            }
                            while (j >= taille - (2 * nombreBoucle + 1) * coeff - moins && j >= 0)
                            {
                                ajout = index;
                                while (image[ajout, j].B == 180 && image[ajout, j].V == 180 && image[ajout, j].R == 180 && ajout > index - coeff && j >= 0 && ajout >= 0 && index > coeff)
                                {
                                    if (bits.ElementAt(plus) == 1)
                                    {
                                        image[ajout, j] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus) == 0)
                                    {
                                        image[ajout, j] = new Pixel(255, 255, 255);
                                    }
                                    if (bits.ElementAt(plus + 1) == 1)
                                    {
                                        image[ajout, j - coeff] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus + 1) == 0)
                                    {
                                        image[ajout, j - coeff] = new Pixel(255, 255, 255);
                                    }
                                    ajout--;

                                    if (nombreBoucle == 2 && plus == 70)
                                    {
                                        //premier passage sur le module d'alignement
                                        for (int u = taille - coeff * 4; u < taille - coeff * 3; u++)
                                        {
                                            for (int y = taille - 4 * coeff; y > taille - 5 * coeff; y--)
                                            {
                                                if (bits.ElementAt(70) == 1)
                                                {
                                                    image[u, y - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(70) == 0)
                                                {
                                                    image[u, y - 1] = new Pixel(255, 255, 255);
                                                }
                                                if (bits.ElementAt(71) == 1)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(71) == 0)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                                                }
                                            }
                                        }
                                        plus = plus + 2;
                                        index = taille - 9 * coeff - 1;
                                        j = j + 1;
                                    }
                                    //cas où on longe par la gauche le module d'alignement
                                    if (nombreBoucle == 4 && plus == 114)
                                    {
                                        int place = 116;
                                        for (int u = taille - coeff * 4; u > taille - coeff * 9; u = u - coeff)
                                        {
                                            for (int y = taille - coeff * 10; y < taille - coeff * 9; y++)
                                            {
                                                for (int t = u; t > u - coeff; t--)
                                                {
                                                    if (bits.ElementAt(place) == 1)
                                                    {
                                                        image[t - 1, y] = new Pixel(0, 0, 0);
                                                    }
                                                    if (bits.ElementAt(place) == 0)
                                                    {
                                                        image[t - 1, y] = new Pixel(255, 255, 255);
                                                    }


                                                }

                                            }
                                            place++;
                                        }
                                        for (int u = taille - coeff * 4; u < taille - coeff * 3; u++)
                                        {
                                            for (int y = taille - 8 * coeff; y > taille - 9 * coeff; y--)
                                            {
                                                if (bits.ElementAt(112) == 1)
                                                {
                                                    image[u, y - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(112) == 0)
                                                {
                                                    image[u, y - 1] = new Pixel(255, 255, 255);
                                                }
                                                if (bits.ElementAt(113) == 1)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(113) == 0)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                                                }
                                            }
                                        }
                                        index = taille - 8 * coeff - 1;
                                        j = taille - 9 * coeff - 1;
                                        plus = 119;
                                    }
                                    //premier passage du module de synchronisation
                                    if (plus == 137)
                                    {
                                        index = coeff * 7 - 1;
                                        j = taille - 8 * coeff - 1;
                                        for (int u = coeff * 7; u < coeff * 8; u++)
                                        {
                                            for (int y = taille - 8 * coeff; y > taille - 9 * coeff; y--)
                                            {
                                                if (bits.ElementAt(130) == 1)
                                                {
                                                    image[u, y - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(130) == 0)
                                                {
                                                    image[u, y - 1] = new Pixel(255, 255, 255);
                                                }
                                                if (bits.ElementAt(131) == 1)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(131) == 0)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                                                }
                                            }
                                        }
                                    }
                                    //toisième passage du module de synchronisation
                                    if (plus == 233)
                                    {
                                        for (int u = coeff * 7; u < coeff * 8; u++)
                                        {
                                            for (int y = taille - 12 * coeff; y > taille - 13 * coeff; y--)
                                            {
                                                if (bits.ElementAt(231) == 1)
                                                {
                                                    image[u, y - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(231) == 0)
                                                {
                                                    image[u, y - 1] = new Pixel(255, 255, 255);
                                                }
                                                if (bits.ElementAt(232) == 1)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                                                }
                                                if (bits.ElementAt(232) == 0)
                                                {
                                                    image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                                                }
                                            }
                                        }
                                        index = coeff * 6 - 1;
                                        plus = plus + 2;
                                        j = taille - 12 * coeff;
                                    }

                                }

                                j--;
                            }
                            int p = index - coeff;
                            if (plus == 148)
                            {
                                p = index - coeff + 1;
                            }
                            plus = plus + 2;
                            index = p;

                        }
                        if ((nombreBoucle < 10 && (nombreBoucle % 2 != 0 || index <= 0 || image[index, j].B != 180 || image[index, j].V != 180 || image[index, j].R != 180)) || (nombreBoucle >= 10 && index < taille - coeff * 7 - 1 && plus < itération))
                        {
                            nombreBoucle = nombreBoucle + 1;
                        }
                    }

                    //combler vide pour transition entre boucles 4 et 5
                    for (int u = 0; u < coeff; u++)
                    {
                        for (int y = taille - 8 * coeff; y > taille - 9 * coeff; y--)
                        {
                            if (bits.ElementAt(149) == 1)
                            {
                                image[u, y - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(149) == 0)
                            {
                                image[u, y - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(150) == 1)
                            {
                                image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(150) == 0)
                            {
                                image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                            }
                        }
                    }
                    if (nombreBoucle == 5)
                    {
                        index = -1;
                        plus = plus + 2;
                    }

                    int x = 0;
                    j = taille - 2 * nombreBoucle * coeff;

                    index = index + 1;
                    if (nombreBoucle % 2 != 0)
                    {
                        while (nombreBoucle % 2 != 0 && ((index <= taille - coeff && plus < itération)))
                        {
                            j = taille - 2 * nombreBoucle * coeff - 1;
                            int moins = 0;
                            if (nombreBoucle >= 9)
                            {
                                j = taille - 2 * nombreBoucle * coeff - 1 - coeff;
                                moins = coeff;
                            }
                            while (j >= taille - (2 * nombreBoucle + 1) * coeff - moins)
                            {
                                ajout = index;

                                while (ajout < index + coeff && ajout < taille)
                                {

                                    if (bits.ElementAt(plus) == 1)
                                    {
                                        image[ajout, j] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus) == 0)
                                    {
                                        image[ajout, j] = new Pixel(255, 255, 255);
                                    }
                                    if (bits.ElementAt(plus + 1) == 1)
                                    {
                                        image[ajout, j - coeff] = new Pixel(0, 0, 0);
                                    }
                                    if (bits.ElementAt(plus + 1) == 0)
                                    {
                                        image[ajout, j - coeff] = new Pixel(255, 255, 255);
                                    }
                                    ajout++;
                                    //permet de passer le module de synchronisation
                                    if (plus == 148 && nombreBoucle == 5)
                                    {
                                        index = coeff * 7;
                                    }
                                    //deuxième passage du module d'alignement
                                    if (nombreBoucle == 3 && plus == 100)
                                    {
                                        index = taille - 4 * coeff;

                                    }
                                    if (plus == 161)
                                    {

                                        index = coeff * 7;
                                        plus = plus + 2;
                                    }





                                }
                                j--;
                            }
                            if (plus == 257)
                            {
                                index = coeff * 7;
                                plus = plus + 2;
                            }
                            x = index + coeff;
                            plus = plus + 2;
                            if (index == taille - coeff)
                            {
                                x = index + coeff - 1;
                            }
                            index = x;


                        }
                        if (image[index, j].B != 180 || image[index, j].V != 180 || image[index, j].R != 180 || (ajout == taille && index == taille - 1))
                        {
                            nombreBoucle = nombreBoucle + 1;
                        }


                    }
                }
                //permet d'afficher les derniers octets
                int pos = 327;
                for (int i = coeff * 9; i < taille - coeff * 8; i = i + coeff)
                {
                    for (int a = coeff * 4; a > coeff * 3; a--)
                    {
                        for (int b = i; b < i + coeff; b++)
                        {

                            if (bits.ElementAt(pos + 8) == 0)
                            {
                                image[b, a - 2 * coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 8) == 1)
                            {
                                image[b, a - 2 * coeff - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(pos + 9) == 0)
                            {
                                image[b, a - 3 * coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 9) == 1)
                            {
                                image[b, a - 3 * coeff - 1] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    pos = pos + 2;
                }
                pos = 327;
                for (int i = taille - coeff * 8; i > coeff * 9; i = i - coeff)
                {
                    for (int a = coeff * 4; a > coeff * 3; a--)
                    {
                        for (int b = i; b > i - coeff; b--)
                        {

                            if (bits.ElementAt(pos) == 0)
                            {
                                image[b - 1, a - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos) == 1)
                            {
                                image[b - 1, a - 1] = new Pixel(0, 0, 0);
                            }
                            if (bits.ElementAt(pos + 1) == 0)
                            {
                                image[b - 1, a - coeff - 1] = new Pixel(255, 255, 255);
                            }
                            if (bits.ElementAt(pos + 1) == 1)
                            {
                                image[b - 1, a - coeff - 1] = new Pixel(0, 0, 0);
                            }
                        }
                    }
                    pos = pos + 2;
                }
                //second passage du module de synchronisation
                for (int u = coeff * 5; u < coeff * 6; u++)
                {
                    for (int y = taille - 10 * coeff; y > taille - 11 * coeff; y--)
                    {
                        if (bits.ElementAt(161) == 1)
                        {
                            image[u, y - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(161) == 0)
                        {
                            image[u, y - 1] = new Pixel(255, 255, 255);
                        }
                        if (bits.ElementAt(162) == 1)
                        {
                            image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(162) == 0)
                        {
                            image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                        }
                    }
                }
                //éléments du centre dans la septième boucle
                for (int u = 0; u < coeff; u++)
                {
                    for (int y = taille - 12 * coeff; y > taille - 13 * coeff; y--)
                    {
                        if (bits.ElementAt(245) == 1)
                        {
                            image[u, y - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(245) == 0)
                        {
                            image[u, y - 1] = new Pixel(255, 255, 255);
                        }
                        if (bits.ElementAt(246) == 1)
                        {
                            image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(246) == 0)
                        {
                            image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                        }
                    }
                }
                for (int u = coeff * 7; u < coeff * 8; u++)
                {
                    for (int y = taille - 14 * coeff; y > taille - 15 * coeff; y--)
                    {
                        if (bits.ElementAt(259) == 1)
                        {
                            image[u, y - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(259) == 0)
                        {
                            image[u, y - 1] = new Pixel(255, 255, 255);
                        }
                        if (bits.ElementAt(260) == 1)
                        {
                            image[u, y - coeff - 1] = new Pixel(0, 0, 0);
                        }
                        if (bits.ElementAt(260) == 0)
                        {
                            image[u, y - coeff - 1] = new Pixel(255, 255, 255);
                        }
                    }
                }

            }

            //on recopie biende gris les zones qui ne le seraient pas totalement
            for (int i = coeff * 6; i < coeff * 7; i++)
            {
                for (int z = 0; z < taille - 1; z++)
                {
                    image[i, z] = new Pixel(180, 180, 180);
                }
            }
            if (version == 2)
            {
                for (int i = taille - coeff * 9; i < taille - coeff * 4; i++)
                {
                    for (int z = taille - coeff * 9; z < taille - coeff * 4; z++)
                    {
                        image[i, z] = new Pixel(180, 180, 180);
                    }
                }
            }

            ModifMasque(image, version);
            AppliquerFond(version, image);
            AppliquerModifQR(image);
        }
        /// <summary>
        /// applique les opérations dues au masque
        /// </summary>
        /// <param name="image"></param> matrice sur laquelle on appliqueles modifications
        /// <param name="version"></param> version du qr code en cours
        public void ModifMasque(Pixel[,] image, int version)
        {
            int diviseur = 0;
            if (version == 1)
            {

                diviseur = 21;
            }
            else
            {

                diviseur = 25;
            }

            int coeff = Convert.ToInt32(image.GetLength(0) / diviseur);
            int taille = Convert.ToInt32(image.GetLength(0));
            Pixel[,] copie = copieMatrice(image);
            Pixel noir = new Pixel(0, 0, 0);
            Pixel blanc = new Pixel(255, 255, 255);
            for (int i = 0; i <= taille - coeff; i = i + coeff)
            {
                for (int j = 0; j <= taille - coeff; j = j + coeff)
                {
                    //on ne regarde que les cases blanches ou noires
                    if ((copie[i, j].R == 255 && copie[i, j].B == 255 && copie[i, j].V == 255) || (copie[i, j].R == 0 && copie[i, j].V == 0 && copie[i, j].B == 0))
                    {
                        if (SommePosition(i, j, coeff) % 2 == 0) //on regarde si la somme des index est paire
                        {
                            //on applique les modifications sur la matrice d'affichage
                            if (copie[i, j].R == 0 && copie[i, j].V == 0 && copie[i, j].B == 0)
                            {

                                Remplissage(i, j, image, coeff, blanc);
                            }
                            if (copie[i, j].R == 255 && copie[i, j].B == 255 && copie[i, j].V == 255)
                            {

                                Remplissage(i, j, image, coeff, noir);
                            }

                        }

                    }

                }
            }


        }
        /// <summary>
        /// retourne la somme des index à l'échelle 1
        /// </summary>
        /// <param name="i"></param> hauteur
        /// <param name="j"></param> largeur
        /// <param name="coeff"></param>coefficient d'agrandissement de chaque cellule du qr code
        /// <returns></returns>
        public int SommePosition(int i, int j, int coeff)
        {
            return (i + j) / coeff;
        }
        /// <summary>
        /// remplit une zone d'un pixel désiré
        /// </summary>
        /// <param name="i"></param> hauteur
        /// <param name="j"></param> largeur
        /// <param name="image"></param> matrice sur laquelle on applique cette modification
        /// <param name="coeff"></param> coefficient d'agrandissement de chaque cellule du qr code
        /// <param name="couleur"></param> pixel qu'on souhaite appliquer
        public void Remplissage(int i, int j, Pixel[,] image, int coeff, Pixel couleur)
        {
            for (int a = i; a < i + coeff; a++)
            {
                for (int b = j; b < j + coeff; b++)
                {
                    image[a, b] = couleur;
                }
            }
        }
        #endregion

        /// <summary>
        /// fonction qui crée une image à partir de l'addition de 2 autres
        /// </summary>
        /// <param name="image1"></param> première image à additionner
        /// <param name="image2"></param> deuxième image à additionner
        public void Addition(MyImage image1, MyImage image2)
        {
            if (image1.TabImage.Length != image2.TabImage.Length)
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            int compteur = 0;
            foreach (Pixel pix in this.tabImage)
            {
                pix.R = Convert.ToByte(Math.Min(255, (image1.TabImage[compteur].R + image2.TabImage[compteur].R)));
                pix.V = Convert.ToByte(Math.Min(255, (image1.TabImage[compteur].V + image2.TabImage[compteur].V)));
                pix.B = Convert.ToByte(Math.Min(255, (image1.TabImage[compteur].B + image2.TabImage[compteur].B)));
                compteur++;
            }
        }
    }


}
