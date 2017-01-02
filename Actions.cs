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
    class Actions
    {
        
        public Actions()
        {

        }
        
       public void move_spaceship(Planet planet1, Planet planet2,Player player)
        {

            if (planet2.faction == Player.Faction.NoFaction)
            {
                for (int i = 0; i < planet1.spaceships.Count; i++)
                    planet2.spaceships.Add(planet1.spaceships[i]);
                planet1.spaceships.Clear();
                
                planet2.Tendency -= 10;
                planet2.faction = planet1.faction;
            }
            else
                if (planet2.faction != planet1.faction)
                {
                    Fight.ship_planet(planet1.spaceships, planet2.spaceships,planet1,planet2);
                    for (int i = 0; i < planet1.spaceships.Count; i++)
                        planet2.spaceships.Add(planet1.spaceships[i]);
                    for (int i = 0; i < planet1.spaceships.Count; i++)
                        planet2.spaceships.Remove(planet1.spaceships[i]);
                }
                else
                {
                    for (int i = 0; i < planet1.spaceships.Count; i++)
                        planet2.spaceships.Add(planet1.spaceships[i]);
                    planet1.spaceships.Clear();
                    
                }
       }
       
        
       public void sell_spaceship(SpaceShip spaceship,Player player) 
        {
            player.money +=((spaceship.Price * 50) / 100);
        }
       
        
        public void build_spaceship(SpaceShip spaceship,Player player, Planet planet)
        {
            if (player.facnumber != 0 && player.money>=spaceship.Price)
            {
                player.spaceships.Add(new SpaceShip(spaceship.Armor, spaceship.AttackPower, spaceship.Price, spaceship.Pic));
                planet.spaceships.Add(new SpaceShip(spaceship.Armor, spaceship.AttackPower, spaceship.Price, spaceship.Pic));
                player.money -= spaceship.Price;
            }
        }
       
        
        public void build_powerplant(PowerPlant powerplant, int number,Planet planet, Player player)
        {
            if (player.money >= powerplant.Price && planet.Tendency == 100)
            {
                
                planet.powerIsTrue = true;
                powerplant.powerIsTrue = true;
                player.powerIsTrue = true;
                player.money -= powerplant.Price;
            }
        }
       
        
        public void build_refinery(Refinery refinery, int number, Planet planet, Player player)
        {
            if (player.powerIsTrue && player.money >= refinery.Price && planet.Tendency == 100)
            {
                player.refnumber += number;
                planet.refnumber += number;
                refinery.RefineryIsTrue = true;
                player.money -= refinery.Price;
            }
        }
       
        
        public void build_factory(Factory factory, int number, Planet planet, Player player)
        {
            if (player.powerIsTrue && player.money>=factory.Price && planet.Tendency == 100)
            {

                planet.facnumber += number;
                factory.FactoryIsTrue = true;
                player.facnumber += number;
                player.money -= factory.Price;
            }
        
        }
        
        
        
        public void build_techcenter(TechCenter techcenter, Planet planet, Player player)
        {
            if (player.powerIsTrue && player.money >= techcenter.Price && planet.Tendency == 100)
            {
                planet.techIsTrue = true;
                player.techIsTrue = true;
                player.money -= techcenter.Price;
            }
        }

        public void build_turrent(Turrent turrent, Planet planet, Player player, int number)
        {
            if (player.powerIsTrue && player.money >= turrent.Price && planet.Tendency == 100)
            {
                turrent.TurrentIsTrue = true;
                planet.turrentnumber += number;
                player.turrentnumber += number;
                player.money -= turrent.Price;
            }
        }
        
        
        public void capture_planet(Planet planet,Player player)
        {
            player.planets.Add(new Planet(planet.Pic, planet.Pos, planet.Size, 50,planet.ID,player.faction));
            for(int i=0;i<planet.spaceships.Count;i++)
            {
                player.spaceships.Add(new SpaceShip(planet.spaceships[i].Armor, planet.spaceships[i].AttackPower, planet.spaceships[i].Price, planet.spaceships[i].Pic));

            }
            player.refnumber = planet.refnumber;
            player.facnumber = planet.facnumber;
            
        }

        public void send_resource(Planet planet1,Planet planet2 ,Player player)
        {
            if (planet1.Resource > 1000)
            {
                
                
                
                if (planet2.faction == Player.Faction.NoFaction)
                {
                    planet1.Resource -= 1000;
                    planet2.Resource += 1000;
                    planet2.Tendency += 10;
                    planet2.faction = planet1.faction;

                }
                else
                {
                    if (planet2.faction == planet1.faction && planet2.Tendency < 100)
                     {
                        planet1.Resource -= 1000;
                        planet2.Resource += 1000;
                        planet2.Tendency += 10;
                     }
                    if (planet2.faction != planet1.faction && planet2.Tendency < 100 && planet2.Tendency > 50)
                    {
                        planet1.Resource -= 1000;
                        planet2.Resource += 1000;
                        planet2.Tendency -= 10;
                    }
                    if (planet2.faction != planet1.faction && planet2.Tendency == 50)
                    {
                        planet1.Resource -= 1000;
                        planet2.Resource += 1000;
                        planet2.Tendency += 10;
                        planet2.faction = planet1.faction;
                    }

                }
            
            }
        }
    
    
    }
}
