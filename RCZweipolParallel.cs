/*
 * Praktikum 3 RCZweipolParallel
 * Autor: B.W
 * Letzte Änderung: 22.05.23
 */

using System;

namespace Praktikum
{

    internal class RCZweipolParallel : RCZweipol //Vererbung
    {
        private double f;


        public double F
        {
            get => f;
            set
            {
                if (value == null || value <= 0)
                {
                    throw new ArgumentNullException("Fehler!, Frequenz muss positiv sein!");
                }

                f = value;
            }
        }

        //Konstruktor
        public RCZweipolParallel(double r, double c, string bauForm, double fC) : base(r, c, bauForm) //Vererbung, Nutzung des RCZweipol Konstruktors
        {
            f = fC;
        }

        //methods

        /// <summary>
        /// Berechnet die Kreisfrequenz
        /// </summary>
        /// <returns>double, die berechnete Kreisfrequenz</returns>
        public double GetKreisFrequenz()
        {
            double w;

            w = 2 * Math.PI * f;

            return w;
        }

        /// <summary>
        /// berechnet den realen Widerstandsanteil
        /// </summary>
        /// <returns>double, der berechnete Widerstandsanteil</returns>
        public double GetZReal()
        {
            double ZReal;
            double w = GetKreisFrequenz();
            double r = base.R;
            double c = base.Ko.Kapazitaet;

            double Nenner = 1 + ((Math.Pow(w, 2)) * (Math.Pow(r, 2)) * (Math.Pow(c, 2)));

            if (Nenner == 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Realteils darf nicht Null sein!");
            }
            if (Nenner < 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Realteils darf nicht negativ sein!");
            }

            ZReal = (r / Nenner);

            return ZReal;
        }

        /// <summary>
        /// berechnet den imaginären Widerstandsanteil
        /// </summary>
        /// <returns>double, der berechnete Widerstandsanteil</returns>
        public double GetZImag()
        {
            double zImag;

            double w = GetKreisFrequenz();
            double r = base.R;
            double c = base.Ko.Kapazitaet;

            double nenner = 1 + Math.Pow(w, 2) * Math.Pow(r, 2) * Math.Pow(c, 2);

            if (nenner == 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Imaginaerteils darf nicht Null sein!");
            }
            if (nenner < 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Imaginaerteils darf nicht negativ sein!");
            }

            double zaeler = (-1 * w) * Math.Pow(r, 2) * c;

            zImag = (zaeler / nenner);

            return zImag;
        }

        /// <summary>
        /// berechnet den Betrag der Widerstandsanteile
        /// </summary>
        /// <returns>double, der berechnete Berag</returns>
        public double GetZBetrag()
        {
            double zBetrag;

            double zImag = GetZImag();
            double zReal = GetZReal();

            double tmp = Math.Pow(zImag, 2) + Math.Pow(zReal, 2);

            zBetrag = Math.Sqrt(tmp);

            return zBetrag;
        }
    }
}