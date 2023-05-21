/*
 * Praktikum 3 Program
 * Autor: B.W
 * Letzte Änderung: 22.05.23
 */

using System;

namespace Praktikum
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string eingabe;
            RCZweipolParallel zppObjekt = null;

            while (true)
            {
                Console.Write("\n\n Was möchten Sie tun?\n");
                Console.Write("\n <Erzeugen> (e/E) Ein neues RC-Zweipolobjekt erzeugen");
                Console.Write("\n <Speichern> (s/S) Aktuelles RC-Zweipolobjekt speichern");
                Console.Write("\n <Beenden> (b/B) Programm beenden.");
                Console.Write("\n ? ");
                eingabe = Console.ReadLine();
                switch (eingabe.ToLower().Trim())
                {
                    case "e":
                        zppObjekt = GetDataFromUser();
                        PrintData(zppObjekt);
                        break;

                    case "s":
                        if (zppObjekt != null && SaveDataToFile(zppObjekt))
                            Console.Write("\n\n ---> OK!\n");
                        else
                            Console.WriteLine("\n Fehler beim Speichern!");
                        break;

                    case "b":
                        return;
                }
            }
        }

        /// <summary>
        /// Dateineinlesen vom Benutzer
        /// </summary>
        /// <returns>RCZweipolobjekt</returns>
        static RCZweipolParallel GetDataFromUser()
        {
            //Variablendeklaration
            double r;
            double c;
            double f;
            string bauForm;
            bool ok;
            bool tmp;
            string buffer;
            RCZweipolParallel zppObjekt; 

            do
            {

                
                do
                {
                    //Eingabe Widerstand mit Überprüfung der Dezimaltrennstelle und double

                    Console.Write("\nZweipol-Widerstand R [Ohm]: ");
                    buffer = Console.ReadLine();
                    buffer = buffer.Replace('.', ',');

                    ok = Double.TryParse(buffer, out r);
                    if (!ok || (r < 0))
                    {
                        Console.WriteLine("Bitte einen korrekten Widerstandswert angeben!");
                        ok = false;
                    }

                } while (!ok);

                do
                {
                    //Eingabe Kapazität

                    Console.Write("\nZweipol-Kondensator-Kapazitaet [uF]: ");
                    buffer = Console.ReadLine();
                    buffer = buffer.Replace('.', ',');

                    ok = Double.TryParse(buffer, out c);
                    if (!ok || (c < 0))
                    {
                        Console.WriteLine("Bitte einen korrekten Kapazitätswert angeben!");
                        ok = false;
                    }
                } while (!ok);

                //Umrechnung in SI Einheiten
                c = c / 1e-09;

                do
                {
                    //Eingabe Bauform

                    ok = true;
                    Console.Write("\nKondensator-Bauform: ");
                    bauForm = Console.ReadLine();
                    if (bauForm == null || bauForm == " " )
                    {
                        ok = false;
                        Console.WriteLine("Bitte eine korrekte Bauform angeben!");
                    }

                } while (!ok);

                do
                {
                    //Eingabe Frequenz
                    Console.Write("\n\nZweipol-Frequenz f [Hz]: ");
                    buffer = Console.ReadLine();
                    buffer = buffer.Replace('.', ',');

                    ok = Double.TryParse(buffer, out f);
                    if (!ok || (f < 0))
                    {
                        Console.WriteLine("Bitte eine korrekte Frequenz angeben!");
                        ok = false;
                    }
                } while (!ok);

                zppObjekt = new RCZweipolParallel(r, c, bauForm, f); //neues Objekt erstelleen
                tmp = true;

                //Excptions
                try
                {
                    zppObjekt.GetZImag();
                    zppObjekt.GetZReal();
                }

                catch (ArgumentNullException e)
                {
                    Console.Write(e.Message);
                    tmp = false;
                }

            } while (!tmp);


            return zppObjekt; //Gibt Objekt zurück
        }

        /// <summary>
        /// Ausgabee der Daten
        /// </summary>
        /// <param name="z"></param>
        static void PrintData(RCZweipolParallel z)
        {
            string ohm = "Ohm";

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Komplexer Widerstand des RC-Parallel-Zweipols");
            Console.WriteLine("---------------------------------------------");

            //Console.WriteLine("Betrag :{0,33:F3}{1,4}", (z.GetZBetrag()), ohm);

            if (z.GetZBetrag() < 1)
            {
                Console.WriteLine("Betrag:{0,33:E3}{1,4}", (z.GetZBetrag()), ohm); //E= Exponential
            }
            else
            {
                Console.WriteLine("Betrag:{0,33:F3}{1,4}", (z.GetZBetrag()), ohm); //F= Festkomma
            }

            if (z.GetZReal() < 1)
            {
                Console.WriteLine("Re-Teil:{0,33:E3}{1,4}", (z.GetZReal()), ohm);
            }
            else
            {
                Console.WriteLine("Re-Teil:{0,33:F3}{1,4}", (z.GetZReal()), ohm);
            }

            if (z.GetZReal() < 1)
            {
                Console.WriteLine("Im-Teil:{0,33:E3}{1,4}", (z.GetZImag()), ohm);
            }
            else
            {
                Console.WriteLine("Im-Teil:{0,33:F3}{1,4}", (z.GetZImag()), ohm);
            }

            Console.WriteLine();

        }

        /// <summary>
        /// RCZweipoldaten in Datei speichern
        /// </summary>
        /// <param name="zpp">the objekt to be saved</param>
        /// <returns>was saving sucssessful</returns>
        static bool SaveDataToFile(RCZweipolParallel zpp)
        {
            bool saved = false;
            string path = @"..\..\SavedData.txt";

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {


                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(" {0} {1} {2} {3} ", zpp.R, zpp.Ko.Kapazitaet, zpp.Ko.Bauform, zpp.F);
                    sw.Flush(); //jeder Datensatz wird sofort geschrieben, ansonsten wird erst der Puffer gefüllt und dann erst geschrieben
                    saved = true;
                }
            }

            return saved;
            /*
            FileStream fs;
            if (!File.Exists(path))
            {

                return false;
            }
            fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(" {0} {1} {2} {3} ", zpp.R, zpp.Ko.Kapazitaet, zpp.Ko.Bauform, zpp.F);
                sw.Flush();
                saved = true;
            }

            return saved;
            */
        }

    }
}
