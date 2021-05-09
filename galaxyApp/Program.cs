using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GalaxyCatalog.Modals;

namespace Kursova_rabota_po_PP

{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string commandTD, gName, sName, planetName, moonName;
            int commandLength;
            string[] comandVals;

            char[] separators = new char[] { '[', ']' };

            
            GalaxyBook galaxyBook = new GalaxyBook();

           
            

            do
            {
            
                commandTD = Console.ReadLine();

                if (String.IsNullOrEmpty(commandTD))
                { break; }
                else
                {
                    
                    string[] commandWords = commandTD.Split(' ');
                    commandLength = commandWords.Length;

                


                    
                    switch (commandLength)
                    {
                        case 1:
                            
                            switch (commandWords[0])
                            {
                                case "stats":                                   
                                    galaxyBook.printStat();                                    
                                    break;                              
                                
                                case "exit":
                                    Console.WriteLine("\n");
                                    break;

                                default:
                                    Console.WriteLine("Unknown Command");
                                    break; 
                            }
                            break;

                        default:
                            //
                            
                            switch (commandWords[0])
                            {
                                case "add":

                                    switch (commandWords[1])
                                    {
                                        
                                        
                                        case "galaxy":
                                            comandVals = commandTD.Split(']')[1].Trim(' ').Split(' ');

                                            string gt = commandTD.Split('[', ']')[2].Split(' ')[1].Trim();
                                            GalaxyTypes gTobj = (GalaxyTypes) Enum.Parse(typeof(GalaxyTypes), gt);
                                            float age = float.Parse(comandVals[1].Substring(0, comandVals[1].Length - 1), CultureInfo.InvariantCulture);
                                            char ageS = char.Parse(comandVals[1].Substring(comandVals[1].Length - 1));
                                            gName = commandTD.Split('[', ']')[1].Trim();
                                            

                                            Galaxy g = new Galaxy(gName, gTobj, age, ageS);
                                            
                                            galaxyBook.Galaxies.Add(g);

                                            break;
                                        
                                        case "star":
                                            comandVals = commandTD.Split('[', ']');
                                            gName = commandTD.Split('[', ']')[1];
                                            sName = commandTD.Split('[', ']')[3];
                                            string[] sParm = commandTD.Split('[', ']')[4].Trim().Split(" ");

                                            float mass = float.Parse(sParm[0], CultureInfo.InvariantCulture);
                                            float size = float.Parse(sParm[1], CultureInfo.InvariantCulture);
                                            int temp = int.Parse(sParm[2]);
                                            float luminosity = float.Parse(sParm[3], CultureInfo.InvariantCulture);

                                           Star currStar = new Star(sName, mass, size , temp, luminosity);

                                        

                                               if (galaxyBook.Galaxies.Any(g => g.GalaxyName == gName))
                                               {
                                                   galaxyBook.Galaxies.Find(g => g.GalaxyName == gName).addStar(currStar);
                                               }
                                               else
                                               {
                                                   galaxyBook.Galaxies.Add(new Galaxy(gName, new List<Star>() { currStar } ));
                                               } 


                                            break;
                                        
                                        case "planet":
                                            comandVals = commandTD.Split('[',']');
                                          
                                            sName = comandVals[1];
                                            planetName = comandVals[3].Trim();
                                            string[] planetParam = comandVals[4].Trim().Split(' ');
                                            
                                            bool life = planetParam.Last() == "yes" ? true : false;
                                            
                                            string pt = String.Join(" ", planetParam.SkipLast(1));

                                            

                                            Planet currPlanet = new Planet(planetName, pt, life);

                                           


                                            if (galaxyBook.Galaxies.Any(g => g.Stars.Any(s => s.StarName == sName) ) ) 
                                            {                                          
                                                foreach (Galaxy glx in galaxyBook.Galaxies)
                                                {
                                                    if ( glx.Stars.Any(s => s.StarName == sName))
                                                    {
                                                        glx.Stars.Find(s => s.StarName == sName).addPlanet(currPlanet);
                                                    }
                                                    
                                                }
                                            }
                                            else {
                                                Console.WriteLine($" Трябва да добавите Звездата първо към съответната галактика! ");  
                                            }
                                             
                                            break;
                                        
                                        case "moon":
                                            comandVals = commandTD.Split('[', ']');
                                            planetName = comandVals[1].Trim();
                                            moonName = comandVals[3].Trim();

                                            Moon currMoon = new Moon(moonName);

                                            
                                            if (galaxyBook.Galaxies.Any(g => g.Stars.Any(s => s.Planets.Any( p => p.PlanetName == planetName ))))
                                            {
                                                foreach (Galaxy glx in galaxyBook.Galaxies)
                                                {
                                                    foreach (Star s in glx.Stars)
                                                    {
                                                        if (s.Planets.Any(x => x.PlanetName == planetName) )
                                                        { s.Planets.Find(p => p.PlanetName == planetName).addMoon(currMoon);  }
                                                        
                                                     }
                                                }
                                            }
                                            else {
                                                Console.WriteLine("Необходимо е да добавите Планета, звезда към Галактиката първо");
                                            }

                                                break;
                                       
                                    }
                                    break;

                              
                                case "list": 
                                    switch (commandWords[1])
                                    {
                                        case "galaxies":
                                            galaxyBook.printGalaxies();
                                            break;

                                        case "stars":
                                            galaxyBook.printStars();

                                            break;

                                        case "planets":
                                            galaxyBook.printPlanets();
                                            break;

                                        case "moons":
                                            galaxyBook.printMoons();
                                            break;
                                    }
                                    break;
                             
                                case "print":

                                    gName = commandTD.Split('[', ']')[1];

                                    if (galaxyBook.Galaxies.Any(g => g.GalaxyName == gName))
                                    {
                                        Console.WriteLine( galaxyBook.Galaxies.Find(g => g.GalaxyName == gName).ToString() );
                                    }
                                    else
                                    { Console.WriteLine($"Няма въведена галактика: {gName}"); }

                                    break;
                                
                                default:
                                    Console.WriteLine("Unknown Command");
                                    break;

                            } 
                            break;                       
                    }
                }
            }
            while ( !commandTD.Equals("exit"));           
        
            
        
        
        } 
    } 
}
