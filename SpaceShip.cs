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
    class SpaceShip
    {
        public SpaceShip(int armor, int attackpower, int price, Texture2D pic) { Armor = armor; AttackPower = attackpower; Price = price; Pic = pic; }
        public SpaceShip() { }
        public int Armor;
        public int AttackPower;
        public int HitPoint=100;
        public Texture2D Pic;
        public int Price;
        public Vector2 Position;
        public Rectangle Pos;
        public bool SSIsTrue = false;

    }
}
