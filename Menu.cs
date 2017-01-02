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
    class Menu : TextButton
    {
        public Menu() { }
        public bool MenuIsTrue = true;
        public void Draw(SpriteBatch spriteBatch, Texture2D BG, TextButton[] menuButtons, Texture2D title,Rectangle BGpos,Rectangle titlepos,SpriteFont Font)
        {
            spriteBatch.Draw(BG, BGpos, Color.White);
            spriteBatch.Draw(title, titlepos, Color.LightGray * 0.8f);
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(menuButtons[i].pic, menuButtons[i].rpos, menuButtons[i].color * 0.6f);
                spriteBatch.DrawString(Font, menuButtons[i].name, menuButtons[i].pos, menuButtons[i].color * 0.6f);
            }
        }
    }
}
