using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceCraft;

namespace SpaceCraft
{
    class SolarSystem : Galaxy
    {
        
        public bool SolarIsTrue = false;
        public Planet[] planets;
        public int CapitalIsTrue = 0;
        public SolarSystem() { }
        public int KlingonNumber, RomulanNumber;
        public SolarSystem(int PlanetNumber1,Rectangle position,Texture2D solarPic, String id)
        {
            rand = new Random();
            Pic = solarPic;
            Pos = position;
            planets = new Planet[PlanetNumber1];
            PlanetNumber = PlanetNumber1;
            ID = id;
            faction = Faction.NoFaction;

            KlingonNumber = rand.Next(0, PlanetNumber);
            RomulanNumber = rand.Next(0, PlanetNumber);
            if(RomulanNumber==KlingonNumber)
                while (RomulanNumber == KlingonNumber)
                    RomulanNumber = rand.Next(0, PlanetNumber);

            for (int i = 0; i < PlanetNumber1; i++)
            {
                
                randsize = rand.Next(1, 6);
                planets[i] = new Planet(Pic, new Rectangle(rand.Next(100, 700), rand.Next(100, 500), randsize * 25 , randsize * 25 ),
                randsize, (i == KlingonNumber || i == RomulanNumber) ? 100 : 50 , "Planet " + (i + 1).ToString(), (i == KlingonNumber) ? Faction.Klingon : (i == RomulanNumber) ? Faction.Romulan : Faction.NoFaction);
                planets[i].structures = new StructButtons[5];
                planets[i].structbuttons = new StructButtons[5];
                planets[i].FactoryButtons = new TextButton[4];
                planets[i].BuyOut = new TextButton();
                planets[i].factory = new Factory(1);
                planets[i].refinery = new Refinery(1);
                planets[i].techcenter = new TechCenter();
                planets[i].turrent = new Turrent(1);
                planets[i].powerplant = new PowerPlant();
                
                    if (i == KlingonNumber || i == RomulanNumber) { planets[i].turrentnumber = 4; }
                    
                
                }
            
        }
        public void draw(Galaxy galaxy, SpriteBatch spriteBatch, Player player1, SpriteFont Font,SpriteFont Font2,Texture2D SolarBG)
        {
            for (int i = 0; i < galaxy.systems.Length; i++)
            {
                if (galaxy.systems[i].SolarIsTrue)
                {
                    spriteBatch.Draw(SolarBG, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(Font, "Money " + player1.money, new Vector2(600, 0), Color.White);
                    for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                    {
                        spriteBatch.Draw(galaxy.systems[i].planets[j].Pic, galaxy.systems[i].planets[j].Pos, Color.White * 0.7f);
                        spriteBatch.DrawString(Font2, "Refineries " + galaxy.systems[i].planets[j].refnumber,
                            new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y), Color.White * 0.7f);
                        spriteBatch.DrawString(Font2, "Factories " + galaxy.systems[i].planets[j].facnumber,
                            new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 10), Color.White * 0.7f);
                        spriteBatch.DrawString(Font2, "Faction " + galaxy.systems[i].planets[j].faction + " " + galaxy.systems[i].planets[j].Tendency + "%",
                            new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 20), Color.White * 0.7f);
                        spriteBatch.DrawString(Font2, "Space Ships " + galaxy.systems[i].planets[j].spaceships.Count,
                            new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 30), Color.White * 0.7f);
                        spriteBatch.DrawString(Font2, "Turrents " + galaxy.systems[i].planets[j].turrentnumber,
                            new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 40), Color.White * 0.7f);
                    }
                }
            }


        }
          

    }
}

