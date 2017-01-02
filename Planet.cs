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
    class Planet : SolarSystem
    {
        public Planet() { }
        public Planet(Texture2D pic, Rectangle position, int size, int tendency,String id,Faction faction1) 
        { 
            Pic = pic; 
            Pos = position; 
            Size = size; 
            Resource = size * 10000;
            ID = id;
            faction = faction1;
            Tendency = tendency;
        }
        
        
        public int Size;
        public int Tendency;
        public int Resource;
        public bool PlanetIsTrue = false;
        public StructButtons[] structbuttons;
        public StructButtons[] structures;
        public TextButton[] FactoryButtons;
        public TextButton BuyOut; 
        public bool SSShopIstrue = false;
        public bool TCShopsIsTrue = false;
        public int SSCnt = 0;
        public bool TCShopIstrue { get; set; }
        public Factory factory;
        public Refinery refinery;
        public TechCenter techcenter;
        public Turrent turrent;
        public PowerPlant powerplant;
        public int TArmor = 0;
        public TextButton MoveButton;
        public TextButton SendResource;
        public bool IsPrinciple = false;
        

        public void Draw(Galaxy galaxy,Texture2D PlanetBG,SpriteBatch spriteBatch,SpaceShip[] kships,SpriteFont Font, Player player1)
        {
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    if (galaxy.systems[i].planets[j].PlanetIsTrue == true)
                    {
                        spriteBatch.Draw(PlanetBG, new Vector2(0, 0), Color.White);
                        for (int z = 0; z < 5; z++)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structbuttons[z].Pic, galaxy.systems[i].planets[j].structbuttons[z].Pos, Color.White);

                        if (galaxy.systems[i].planets[j].powerIsTrue)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[1].Pic, galaxy.systems[i].planets[j].structures[1].Pos, Color.White);
                        if (galaxy.systems[i].planets[j].techIsTrue)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[3].Pic, galaxy.systems[i].planets[j].structures[3].Pos, Color.White);
                        if (galaxy.systems[i].planets[j].refnumber > 0)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[2].Pic, galaxy.systems[i].planets[j].structures[2].Pos, Color.White);
                        if (galaxy.systems[i].planets[j].facnumber > 0)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[0].Pic, galaxy.systems[i].planets[j].structures[0].Pos, Color.White);
                        if (galaxy.systems[i].planets[j].turrentnumber > 0)
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[4].Pic, galaxy.systems[i].planets[j].structures[4].Pos, Color.White);
                        if (galaxy.systems[i].planets[j].SSShopIstrue && galaxy.systems[i].planets[j].PlanetIsTrue)
                        {

                            for (int z = 3; z >= 0; z--)
                                spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[z].pic, galaxy.systems[i].planets[j].FactoryButtons[z].rpos, Color.White);
                            spriteBatch.Draw(kships[galaxy.systems[i].planets[j].SSCnt].Pic, new Vector2(50, 350), Color.White);
                            spriteBatch.DrawString(Font, "Price : " + kships[galaxy.systems[i].planets[j].SSCnt].Price,
                                new Vector2(400, 300), Color.White * 0.6f);
                            spriteBatch.DrawString(Font, "Armor : " + kships[galaxy.systems[i].planets[j].SSCnt].Armor,
                                new Vector2(400, 350), Color.White * 0.6f);
                            spriteBatch.DrawString(Font, "Attack Power : " + kships[galaxy.systems[i].planets[j].SSCnt].AttackPower,
                                new Vector2(400, 400), Color.White * 0.6f);
                            spriteBatch.Draw(galaxy.systems[i].planets[j].BuyOut.pic, galaxy.systems[i].planets[j].BuyOut.rpos, Color.White);
                            spriteBatch.DrawString(Font, galaxy.systems[i].planets[j].BuyOut.name, galaxy.systems[i].planets[j].BuyOut.pos, Color.White);

                        }
                        if (galaxy.systems[i].planets[j].TCShopIstrue && galaxy.systems[i].planets[j].PlanetIsTrue)
                        {
                            for (int z = 3; z >= 0; z--)
                                spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[z].pic, galaxy.systems[i].planets[j].FactoryButtons[z].rpos, Color.White);

                        }

                        spriteBatch.DrawString(Font, "Turrents " + galaxy.systems[i].planets[j].turrentnumber.ToString(), new Vector2(0, 30), Color.White);
                        spriteBatch.DrawString(Font, "Refineries " + galaxy.systems[i].planets[j].refnumber.ToString(), new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(Font, "Factories " + galaxy.systems[i].planets[j].facnumber.ToString(), new Vector2(0, 60), Color.White);
                        spriteBatch.DrawString(Font, "Money " + player1.money, new Vector2(590, 0), Color.White);
                        spriteBatch.DrawString(Font, "Space Ships " + player1.spaceships.Count, new Vector2(590, 30), Color.White);
                    }

                }
        }
    
    }
}
