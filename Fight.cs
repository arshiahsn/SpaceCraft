using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceCraft
{
    class Fight
    {


        public static void ship_planet(List<SpaceShip> spaceships1, List<SpaceShip> spaceships2,Planet planet1,Planet planet2)
        {

            if (planet2.spaceships.Count == 0 && planet2.turrentnumber == 0)
            {
                planet2.faction = planet1.faction;
                planet2.Tendency = 50;

            }
            
            int SumArmor2 = 0;
            int SumAP1 = 0;
            int SumArmor1 = 0;
            int SumAP2 = 0;
            //Calculating Sum Armor and Sum Attack power of invaders and deffenders
            for (int i = 0; i < spaceships1.Count; i++)
            {
                SumAP1 += spaceships1[i].AttackPower;
                SumArmor1 += spaceships1[i].Armor;
            }
            for (int i = 0; i < spaceships2.Count; i++)
            {
                SumArmor2 += spaceships2[i].Armor;
                SumAP2 += spaceships2[i].AttackPower;
            }
            SumArmor2 += (planet2.turrentnumber * 3);
            SumAP2 += (planet2.turrentnumber * 5);
            //Fighting
            while(spaceships1.Count != 0 || spaceships2.Count!=0)
            {
                    for (int i = 0; i < spaceships2.Count; i++)
                    {
                        spaceships2[i].HitPoint -= ((SumAP1-SumArmor2) / spaceships2.Count);
                        if (spaceships2[i].HitPoint <= 0) spaceships2.Remove(spaceships2[i]);
                    }
                    if (spaceships2.Count == 0) break;
                    for (int i = 0; i < spaceships1.Count; i++)
                    {
                        spaceships1[i].HitPoint -= ((SumAP2-SumArmor1) / spaceships1.Count);
                        if (spaceships1[i].HitPoint <= 0) spaceships1.Remove(spaceships1[i]);
                    }
            }
            //If there are no Space Ships on the destiny planet and neither no turrents the planet will get captured by the Player
            
        }
       
        
    }
}
