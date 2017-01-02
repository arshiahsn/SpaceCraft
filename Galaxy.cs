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
    class Galaxy : Player
    {
        public string ID;
        public SolarSystem[] systems;
        public int resources;
        public Texture2D Pic;
        public Rectangle Pos;
        public Random rand;
        public int SolarSystemNumber, PlanetNumber;
        public int Width = 800, Height = 600;
        public bool ShowArtibutes = false;
        public bool GalaxyIsTrue = false;
        public int randsize;
        public Galaxy() { }
        public Galaxy(int solarsystemnumber)
         {
            resources=0;
            rand = new Random();
            SolarSystemNumber = solarsystemnumber;
            systems = new SolarSystem[solarsystemnumber];
            for (int i = 0; i < solarsystemnumber; i++)
            {
                randsize = rand.Next(1, 6);
                PlanetNumber = rand.Next(3, 10);
                systems[i] = new SolarSystem(PlanetNumber, new Rectangle(rand.Next(100, 700), rand.Next(100, 500), randsize * 35, randsize*35), Pic, "System " + (i+1).ToString());
            }
         }

        public void Draw(SpriteBatch spriteBatch, Texture2D BG2,Rectangle BGpos,Galaxy galaxy,Player player1,SpriteFont Font)
        {
            spriteBatch.Draw(BG2, BGpos, Color.White);
            for (int i = 0; i < galaxy.systems.Length; i++)
            {
                spriteBatch.Draw(galaxy.systems[i].Pic, galaxy.systems[i].Pos, Color.Yellow * 0.6f);

            }
            spriteBatch.DrawString(Font, "Money " + player1.money, new Vector2(600, 0), Color.White);

        }
    }
}
