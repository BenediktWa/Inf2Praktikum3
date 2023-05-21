/*
 * Praktikum 3 RCZweipol
 * Autor: B.W
 * Letzte Änderung: 22.05.23
 */

using System;

namespace Praktikum
{

    internal class RCZweipol
    {

        private double r;
        private Kondensator ko;

        //Properties
        public double R
        {
            get => r;
            set
            {
                if (value == null || value <= 0)
                {
                    throw new ArgumentNullException("Fehler!, Widerstand muss positiv sein!");
                }
                r = value;
            }
        }

        internal Kondensator Ko
        {
            get => ko;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Fehler!, Kondensatorobjekt muss existieren!");
                }
                ko = value;
            }
        }

        //Konstruktor
        public RCZweipol(double rC, double cC, string bauForm)
        {
            ko = new Kondensator(bauForm, cC);
            r = rC;
        }


    }
}