using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInfo_Crochet_Watrigant
{
    /// <summary>
    /// classe qui met en lien 3 bytes pour former une couleur
    /// </summary>
    public class Pixel
    {
        /// <summary>
        /// byte rouge
        /// </summary>
        public byte R;
        /// <summary>
        /// byte vert
        /// </summary>
        public byte V;
        /// <summary>
        /// byte bleu
        /// </summary>
        public byte B;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="R"></param> byte rouge
        /// <param name="V"></param> byte vert
        /// <param name="B"></param> byte bleu
        public Pixel(byte R, byte V, byte B)
        {
            this.R = R;
            this.V = V;
            this.B = B;
        }
    }
}

