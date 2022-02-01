using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInfo_Crochet_Watrigant
{
    /// <summary>
    /// classe facilitant l'écriture des fractales
    /// </summary>
    public class Complexe
    {
        private double reel;
        private double imaginaire;
        private double module;

        //classe qui permet de réaliser les fractales
        //elle a comme attributs la partie imaginaire, la partie réelle et le module d'un nombre complexe
        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="reel"></param> partie rélle
        /// <param name="imaginaire"></param> partie imaginaire
        public Complexe(double reel, double imaginaire)
        {
            this.reel = reel;
            this.imaginaire = imaginaire;
            this.module = Math.Sqrt((this.reel * this.reel) + (this.imaginaire * this.imaginaire));
        }

        #region PROPRIETES
        /// <summary>
        /// propriété partie reelle
        /// </summary>
        public double Reel
        {
            get => reel;
            set => reel = value;
        }
        /// <summary>
        /// propriété partie imaginaire
        /// </summary>
        public double Imaginaire
        {
            get => imaginaire;
            set => imaginaire = value;
        }
        /// <summary>
        /// propriété module
        /// </summary>
        public double Module
        {
            get => module;
            set => module = value;
        }
        #endregion

    }
}