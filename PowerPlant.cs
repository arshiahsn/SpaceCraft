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
    class PowerPlant : Structure
    {
        public PowerPlant(int number) { Price = 1000; Number = number; PowerIsTrue = true; }

        public PowerPlant()
        {
            // TODO: Complete member initialization
        }
        bool PowerIsTrue=false;
        private int number;
    }
}
