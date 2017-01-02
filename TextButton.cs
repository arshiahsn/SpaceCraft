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
    class TextButton
    {
        public Vector2 pos;
        public String name;
        public Color color;
        public Rectangle rpos;
        public Texture2D pic;
        public TextButton()
        {
            name = "";
            rpos = new Rectangle(0, 0, 10, 10);
            color = Color.Gray;
            pos = new Vector2(10, 10);
        }

    }
}
