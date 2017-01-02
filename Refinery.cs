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

//A structure used to turn resources to money
namespace SpaceCraft
{
    class Refinery : Structure
    {
        public Refinery(int number) { Price = 2000; Number = number; RefineryIsTrue = true; }
        public bool RefineryIsTrue = false;
        public void money_plus(Player player, Refinery refinery,Planet planet)
        {
            if (refinery.RefineryIsTrue==true && planet.refnumber>0 && planet.Resource>0)
            {
                player.money += planet.refnumber;
                planet.Resource -=  planet.refnumber;
            }
        }
    }
    
}
