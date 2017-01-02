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
    class Player
    {
        public Player() { }
        public int money = 10000;
        public List<Planet> planets = new List<Planet>();
        public List<SpaceShip> spaceships = new List<SpaceShip>();
        public int refnumber = 0;
        public int facnumber = 0;
        public int turrentnumber = 0;
        public bool techIsTrue = false;
        public bool powerIsTrue = false;
        public int index;
        public int TAmor = 0;
        public Actions actions;
        public enum Faction
        {
            Klingon = 0,
            Romulan = 1,
            NoFaction = 2
        }
        public Faction faction;
        

    }
}
