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
    class StructButtons
    {
        public Texture2D Pic;
        public bool ShowArtibutes = false;
        public Rectangle Pos;
        public bool ButtonIsTrue;
        public String Name = "";
        public Vector2 NamePos = new Vector2(0, 0);
        public StructButtons()
        {
        
        }
    }
}
