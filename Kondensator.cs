/*
 * Praktikum 3 Kondensatorklass
 * Autor: B.W
 * Letzte Änderung: 22.05.23
 */

using System;

namespace Praktikum
{

    internal class Kondensator
    {
        /// <summary>
        /// konstructor 
        /// </summary>
        /// <param name="bauformC">build style</param>
        /// <param name="materialDielektrikumC">material dieelektrrikum</param>
        /// <param name="kapazitaetC">capacity</param>
        /// <param name="relDielektrizKonstC">relative dieelectric const</param>
        public Kondensator(string bauformC, string materialDielektrikumC, double kapazitaetC, double relDielektrizKonstC)
        {
            bauform = bauformC;
            materialDielektrikum = materialDielektrikumC;
            kapazitaet = kapazitaetC;
            relDielektrizKonst = relDielektrizKonstC;
        }

        public Kondensator()
        {
        }

        public Kondensator(Kondensator koObjekt)
        {
            bauform = koObjekt.bauform;
            materialDielektrikum = koObjekt.materialDielektrikum;
            kapazitaet = koObjekt.kapazitaet;
            relDielektrizKonst = koObjekt.relDielektrizKonst;
        }

        /// <summary>
        /// konstructor 
        /// </summary>
        /// <param name="bauformC">build style</param>
        /// <param name="kapazitaetC">capacity</param>
        public Kondensator(string bauformC, double kapazitaetC)
        {
            bauform = bauformC;
            materialDielektrikum = "default";
            kapazitaet = kapazitaetC;
            relDielektrizKonst = 1;
        }

        private string bauform;
        private string materialDielektrikum;
        private double kapazitaet;
        private double relDielektrizKonst;

        //Getter/Setter
        public string Bauform
        {
            get => bauform;
            set
            {
                if (value != null || value.Length > 0) bauform = value;
                else bauform = "default";
            }
        }

        public string MaterialDielektrikum
        {
            get => materialDielektrikum;
            set
            {
                if (value != null || value.Length > 0) materialDielektrikum = value;
                else materialDielektrikum = "default";
            }
        }

        public double Kapazitaet
        {
            get => kapazitaet;
            set
            {
                if (value < 0)
                {
                    kapazitaet = (value * (-1));
                }
                if (value == 0)
                {
                    kapazitaet = 1;
                }
            }
        }

        public double RelDielektrizKonst
        {
            get => relDielektrizKonst;
            set
            {
                if (value < 0)
                {
                    relDielektrizKonst = (value * (-1));
                }
                if (value == 0)
                {
                    relDielektrizKonst = 1;
                }
            }
        }
    }

}