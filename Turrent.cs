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
    class Turrent : Structure
    {
        public Turrent(int number) { Number = number; Price = 10000; TurrentIsTrue = true; }
        public bool TurrentIsTrue = false;
    }
}
