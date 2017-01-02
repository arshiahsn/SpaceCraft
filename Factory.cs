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
    class Factory : Structure
    {
        public Factory(int number) { Number = number; FactoryIsTrue = true; Price = 4000; }
        public Factory() { }
        public bool FactoryIsTrue = false;
        public void Factory_update(Galaxy galaxy,MouseState LastMouse,MouseState FirstMouse,Player player1,SpaceShip[] kships,Actions actions)
        {
            for (int j = 0; j < galaxy.systems.Length; j++)
                for (int i = 0; i < galaxy.systems[j].planets.Length; i++)
                {
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].structures[0].Pos.Contains(FirstMouse.X, FirstMouse.Y))
                        galaxy.systems[j].planets[i].SSShopIstrue = true;
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[2].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        galaxy.systems[j].planets[i].SSShopIstrue = false;
                        galaxy.systems[j].planets[i].SSCnt = 0;
                    }
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].structures[3].Pos.Contains(FirstMouse.X, FirstMouse.Y))
                        galaxy.systems[j].planets[i].TCShopIstrue = true;
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed && galaxy.systems[j].planets[i].FactoryButtons[2].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].TCShopIstrue)
                        galaxy.systems[j].planets[i].TCShopIstrue = false;

                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[0].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        if (galaxy.systems[j].planets[i].SSCnt < 11) galaxy.systems[j].planets[i].SSCnt++;
                        else galaxy.systems[j].planets[i].SSCnt = 0;

                    }
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[1].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        if (galaxy.systems[j].planets[i].SSCnt > 0) galaxy.systems[j].planets[i].SSCnt--;
                        else galaxy.systems[j].planets[i].SSCnt = 11;

                    }
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].BuyOut.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                        actions.build_spaceship(kships[galaxy.systems[j].planets[i].SSCnt], player1, galaxy.systems[j].planets[i]);

                }
        }
    }

    

}
