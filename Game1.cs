using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceCraft
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Button, BG, BG2, title, cursor, SolarBG, PlanetBG, FactionPic;
        
        Vector2 cursorpos;
        Rectangle titlepos, BGpos;
        SpriteFont Font, Font2;
        SoundEffect MenuSound, GameSound;
        SoundEffectInstance MenuSoundInstance, GameSoundInstance;
        
        
        Random rand = new Random();
        MouseState FirstMouse, LastMouse;
        KeyboardState CurrentKeyboard, LastKeyboard;
        
        public static int playerchecker = 0;
        public static TimeSpan lastTime = new TimeSpan();
        public static TimeSpan cycleTime = new TimeSpan();
        static int _Cycle = 5;
        public static int Cycle
        {
            get { return _Cycle; }
            set
            {
                _Cycle = value;
                cycleTime = new TimeSpan(0, 0, 2 * _Cycle);
            }
        }

        
        Player player1, player2, currentPlayer;
        Menu menu;
        Galaxy galaxy;

        SpaceShip[] kships;
        SpaceShip[] rships;

        GameTime time1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }
        TextButton[] menuButtons;
        
        

        protected override void Initialize()
        {

            
            int systemnumber = rand.Next(2, 8);
            int planetnumber = rand.Next(3, 10);
            player1 = new Player();
            player2 = new Player();
            player1.index = 0;
            player2.index = 1;
            currentPlayer = player1;
            player1.faction = Player.Faction.Klingon;
            player2.faction = Player.Faction.Romulan;
            menu = new Menu();

            int PicNumber = rand.Next(1, 10);
            
            galaxy = new Galaxy(systemnumber);
            player1.actions = new Actions();
            player2.actions = new Actions();
            
            kships = new SpaceShip[12];
            rships = new SpaceShip[12];
            
            //========================================================================================================================
            //Initializing spaceships for each faction
            for (int i = 0; i < 12; i++)
            {
                
                kships[i] = new SpaceShip();
                kships[i].Armor = (i < 6) ? i : i * 2;
                kships[i].AttackPower = (i < 6) ? i+2 : i * 3;
                kships[i].Price = (i < 6) ? i * 1000 + 1000 : i * i * 2000;

                rships[i] = new SpaceShip();
                rships[i].Armor = (i < 6) ? i : i * 2;
                rships[i].AttackPower = (i < 6) ? i + 2 : i * 3;
                rships[i].Price = (i < 6) ? i * 1000 + 1000 : i * i * 2000;

            }
            //========================================================================================================================
                menuButtons = new TextButton[3];
            for (int i = 0; i < 3; i++)
            {
                menuButtons[i] = new TextButton();
                menuButtons[i].rpos = new Rectangle(200, 100 * i + 270, 400, 80);
                menuButtons[i].color = Color.LightGray;
                menuButtons[i].pos = new Vector2(250, 120 * i + 150);
                menuButtons[i].pic = Content.Load<Texture2D>("Pictures/Buttons/b1");
            }
            
            menuButtons[0].name = "New Game";
            menuButtons[1].name = " Options";
            menuButtons[2].name = "    Exit";
            //========================================================================================================================
            //Initializing SpaceShip Shop buttons
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                    for (int z = 0; z < 4; z++)
            {
                galaxy.systems[i].planets[j].FactoryButtons[z] = new TextButton();
                galaxy.systems[i].planets[j].FactoryButtons[z].color = Color.White;
                galaxy.systems[i].planets[j].FactoryButtons[z].pic = Content.Load<Texture2D>("Pictures/Buttons/p" + z.ToString());

            }
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    galaxy.systems[i].planets[j].FactoryButtons[1].rpos = new Rectangle(0, 400, 50, 50);
                    galaxy.systems[i].planets[j].FactoryButtons[0].rpos = new Rectangle(750, 400, 50, 50);
                    galaxy.systems[i].planets[j].FactoryButtons[2].rpos = new Rectangle(750, 300, 50, 50);
                    galaxy.systems[i].planets[j].FactoryButtons[3].rpos = new Rectangle(0, 300, 800, 300);
                    galaxy.systems[i].planets[j].BuyOut.rpos = new Rectangle(450,500,141,47);
                    galaxy.systems[i].planets[j].BuyOut.pos = new Vector2(490, 510);
                    galaxy.systems[i].planets[j].BuyOut.name = "Buy";
                }
            //========================================================================================================================
            //Initializing Structure and Shop buttons
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                    for (int z = 0; z < 5; z++)
                {
                    galaxy.systems[i].planets[j].structbuttons[z] = new StructButtons();
                    galaxy.systems[i].planets[j].structures[z] = new StructButtons();
                    galaxy.systems[i].planets[j].structbuttons[z].Pos = new Rectangle(0, 100 + z * 100, 95, 95);
                    galaxy.systems[i].planets[j].structures[z].Pos = new Rectangle(rand.Next(100, 700), rand.Next(200, 500), 100, 100);
                }
            //========================================================================================================================
            //Initializing spaceship attributes for their shop picture
            for (int i = 0; i < kships.Length; i++)
            {
                if (i < 4) kships[i].Pos = new Rectangle(200 * i, 300, 200, 100);
                if (i >= 4 && i < 8) kships[i].Pos = new Rectangle(200 * i, 400, 200, 100);
                if (i >= 8 && i < 12) kships[i].Pos = new Rectangle(200 * i, 500, 200, 100);
                if (i < 4) rships[i].Pos = new Rectangle(200 * i, 300, 200, 100);
                if (i >= 4 && i < 8) rships[i].Pos = new Rectangle(200 * i, 400, 200, 100);
                if (i >= 8 && i < 12) rships[i].Pos = new Rectangle(200 * i, 500, 200, 100);


            }
            //========================================================================================================================
            //Initializing Move and Send Resource Buttons
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    galaxy.systems[i].planets[j].MoveButton = new TextButton();
                    galaxy.systems[i].planets[j].SendResource = new TextButton();
                    galaxy.systems[i].planets[j].MoveButton.rpos = new Rectangle(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y-25, 25, 25);
                    galaxy.systems[i].planets[j].SendResource.rpos = new Rectangle(galaxy.systems[i].planets[j].Pos.X+26, galaxy.systems[i].planets[j].Pos.Y-25, 25, 25);
                }
            BGpos = new Rectangle(0, 0, 800, 600);
            titlepos = new Rectangle(100, 50, 593, 133);
            cursor = Content.Load<Texture2D>("Pictures/buttons/Pointer");

            time1 = new GameTime();
            
                base.Initialize();
        }

        protected override void LoadContent()
        {
            PlanetBG = Content.Load<Texture2D>("Pictures/Planets/planetbg");
            SolarBG = Content.Load<Texture2D>("Pictures/Planets/space");
            BG = Content.Load<Texture2D>("Pictures/Planets/Galaxy");
            BG2 = Content.Load<Texture2D>("Pictures/Planets/Galaxy2");
            title = Content.Load<Texture2D>("Pictures/buttons/title");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/scorefont");
            Font2 = Content.Load<SpriteFont>("Fonts/scoreFont2");
            Button = Content.Load<Texture2D>("Pictures/Buttons/b1");
            MenuSound = Content.Load<SoundEffect>("Sounds/Star Wars");
            GameSound = Content.Load<SoundEffect>("Sounds/game");

            GameSoundInstance = GameSound.CreateInstance();
            MenuSoundInstance = MenuSound.CreateInstance();
            GameSoundInstance.IsLooped = true;
            MenuSoundInstance.IsLooped = true;
            MenuSoundInstance.Play();
            //========================================================================================================================
           //Loading Sctructure and their buttons pictures
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    for (int z = 2; z < 11; z += 2)
                        galaxy.systems[i].planets[j].structures[z / 2 - 1].Pic = Content.Load<Texture2D>("Pictures/Structures/k" + z.ToString());
                    for (int z = 1; z < 11; z += 2)
                        galaxy.systems[i].planets[j].structbuttons[z / 2].Pic = Content.Load<Texture2D>("Pictures/Structures/k" + z.ToString());
                    galaxy.systems[i].planets[j].BuyOut.pic = Content.Load<Texture2D>("Pictures/Buttons/aqua1");
                }
            //========================================================================================================================
            //Loading planet pictures and system pictures
            for (int i = 0; i < galaxy.systems.Length; i++)
                {
                    galaxy.systems[i].Pic = Content.Load<Texture2D>("Pictures/Planets/solar_system2");
                    for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                        galaxy.systems[i].planets[j].Pic = Content.Load<Texture2D>("Pictures/Planets/" + rand.Next(1, 19).ToString());
                }
            //========================================================================================================================
            //Loading space ship pictures
            for (int i = 0; i < 12; i++)
            {
                kships[i].Pic = Content.Load<Texture2D>("Pictures/SpaceShips/k" + i.ToString());
                rships[i].Pic = Content.Load<Texture2D>("Pictures/SpaceShips/r" + i.ToString());
            }
            //========================================================================================================================
            //Loading Resource and Move button pictures
            for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    galaxy.systems[i].planets[j].MoveButton.pic = Content.Load<Texture2D>("Pictures/Buttons/move");
                    galaxy.systems[i].planets[j].SendResource.pic = Content.Load<Texture2D>("Pictures/Buttons/sendr");
                }
        }

       
       
        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {

            FirstMouse = Mouse.GetState();
            cursorpos = new Vector2(FirstMouse.X, FirstMouse.Y);
            CurrentKeyboard = Keyboard.GetState();
            
            double MoneyTime = time1.TotalGameTime.Seconds;
            //============================================================================================================================
            if (currentPlayer.index == 0) FactionPic = Content.Load<Texture2D>("Pictures/Buttons/index2");
            if (currentPlayer.index == 1) FactionPic = Content.Load<Texture2D>("Pictures/Buttons/index");
            //Going to the menu from the Galaxy screen
            if (CurrentKeyboard.IsKeyDown(Keys.Escape) && LastKeyboard.IsKeyUp(Keys.Escape) && galaxy.GalaxyIsTrue)
            {
                menu.MenuIsTrue = true;
                galaxy.GalaxyIsTrue = false;
                MenuSoundInstance.Play();
                GameSoundInstance.Stop();
            }

            //============================================================================================================================
            //Going to the galaxy screen from the system screen
            for (int j = 0; j < galaxy.systems.Length; j++)
                if (galaxy.systems[j].SolarIsTrue && CurrentKeyboard.IsKeyDown(Keys.Escape) && LastKeyboard.IsKeyUp(Keys.Escape))
                {
                    galaxy.systems[j].SolarIsTrue = false;
                    galaxy.GalaxyIsTrue = true;

                }
            
            
            
            //============================================================================================================================
            //Going to the System screen from the planet screen
            for (int j = 0; j < galaxy.systems.Length; j++)
                for (int i = 0; i < galaxy.systems[j].planets.Length; i++)
            if (CurrentKeyboard.IsKeyDown(Keys.Escape) && LastKeyboard.IsKeyUp(Keys.Escape) && galaxy.systems[j].planets[i].PlanetIsTrue)
            {
                galaxy.systems[j].planets[i].PlanetIsTrue = false;
                galaxy.systems[j].SolarIsTrue = true;

            }


                    //Planets
                    //============================================================================================================================ 
            //Changing the turn duo to the time wich we put infront of playerchecker in miliseconds
            if (gameTime.TotalGameTime - lastTime >= cycleTime && !menu.MenuIsTrue)
                {
                    lastTime = gameTime.TotalGameTime;
                    playerchecker++;
                    if (playerchecker == 3000)
                    {
                        if (currentPlayer.index == 0) currentPlayer = player2;
                        else currentPlayer = player1;
                        playerchecker = 0;
                    }
                }

            
            
            //============================================================================================================================
            //Going to the planet screen from the system screen
            for (int j = 0; j < galaxy.systems.Length; j++)
                for (int i = 0; i < galaxy.systems[j].planets.Length; i++)
                {
                    for (int z = 0; z < galaxy.systems[j].planets.Length; z++)
                        if (!galaxy.systems[j].planets[z].MoveButton.rpos.Contains(FirstMouse.X, LastMouse.Y))
                        {
                            if (galaxy.systems[j].planets[i].Pos.Contains(FirstMouse.X, FirstMouse.Y) && LastMouse.LeftButton == ButtonState.Released
                                && FirstMouse.LeftButton == ButtonState.Pressed && galaxy.systems[j].SolarIsTrue
                                && galaxy.systems[j].planets[i].faction == currentPlayer.faction)
                            {
                                galaxy.systems[j].planets[i].PlanetIsTrue = true;
                                galaxy.systems[j].SolarIsTrue = false;
                                break;
                            }
                        }
                    //============================================================================================================================
                    //If all planets in a system is owned by an empire then the solar goes to its faction
                    bool flag = true;
                    for (int z = 0; z < galaxy.systems[j].planets.Length; z++)
                        for (int k = 0; k < galaxy.systems[j].planets.Length; k++)
                            if (galaxy.systems[j].planets[z].faction != galaxy.systems[j].planets[k].faction){flag = false; break;}
                    if (flag) galaxy.systems[j].faction = galaxy.systems[j].planets[0].faction;      
                    //============================================================================================================================
                    //Showing attributes for the planet when the mouse is on the planet position
                    if (galaxy.systems[j].planets[i].Pos.Contains(FirstMouse.X, FirstMouse.Y)
                    && galaxy.systems[j].SolarIsTrue == true)
                        galaxy.systems[j].planets[i].ShowArtibutes = true;
                    else
                        galaxy.systems[j].planets[i].ShowArtibutes = false;

                    //============================================================================================================================
                    //Sending spaceships from the principle to the destination planet and calling the fight function within
                    if (galaxy.systems[j].planets[i].IsPrinciple && galaxy.systems[j].planets[i].faction == currentPlayer.faction)
                        for (int z = 0; z < galaxy.systems[j].planets.Length; z++)
                        {
                            if (galaxy.systems[j].planets[z].MoveButton.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                                && z != i
                                && FirstMouse.LeftButton == ButtonState.Released
                                && LastMouse.LeftButton == ButtonState.Pressed
                                && galaxy.systems[j].SolarIsTrue)
                            {
                                currentPlayer.actions.move_spaceship(galaxy.systems[j].planets[i], galaxy.systems[j].planets[z], currentPlayer);
                                galaxy.systems[j].planets[i].IsPrinciple = false;
                            }
                        }
                    //============================================================================================================================
                    //Sending resources from principle to the destination planet
                    if (galaxy.systems[j].planets[i].IsPrinciple && galaxy.systems[j].planets[i].faction == currentPlayer.faction)
                        for (int z = 0; z < galaxy.systems[j].planets.Length; z++)
                        {
                            if (galaxy.systems[j].planets[z].SendResource.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                                && z != i
                                && FirstMouse.LeftButton == ButtonState.Released
                                && LastMouse.LeftButton == ButtonState.Pressed
                                && galaxy.systems[j].SolarIsTrue)
                            {
                                currentPlayer.actions.send_resource(galaxy.systems[j].planets[i], galaxy.systems[j].planets[z], currentPlayer);
                                galaxy.systems[j].planets[i].IsPrinciple = false;
                            }
                        }
                    //============================================================================================================================
                    //Determines wich planet is Principle for sending resource or spaceships
                    if (galaxy.systems[j].planets[i].MoveButton.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                            || galaxy.systems[j].planets[i].SendResource.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                            && LastMouse.LeftButton == ButtonState.Released
                            && FirstMouse.LeftButton == ButtonState.Pressed
                            && galaxy.systems[j].SolarIsTrue)
                    {
                        galaxy.systems[j].planets[i].IsPrinciple = true;
                        break;
                    }
                    //============================================================================================================================
                    //If a player is in his planet and his turn time has already timed out,it jumps to the system screen
                    if (galaxy.systems[j].planets[i].PlanetIsTrue && currentPlayer.faction != galaxy.systems[j].planets[i].faction)
                    {
                        galaxy.systems[j].planets[i].PlanetIsTrue = false;
                        galaxy.systems[j].SolarIsTrue = true;
                    }

                    //============================================================================================================================
                    //Adding each player ,money equal to resources wich each player's refineries are turning
                    if (MoneyTime % 100 == 0)
                    {
                        if (player1.faction == galaxy.systems[j].planets[i].faction)
                            galaxy.systems[j].planets[i].refinery.money_plus(player1, galaxy.systems[j].planets[i].refinery,
                            galaxy.systems[j].planets[i]);
                        if (player2.faction == galaxy.systems[j].planets[i].faction)
                            galaxy.systems[j].planets[i].refinery.money_plus(player2, galaxy.systems[j].planets[i].refinery,
                            galaxy.systems[j].planets[i]);

                    }
                    //============================================================================================================================
                    //Builing deffrent structures
                    if (galaxy.systems[j].planets[i].PlanetIsTrue)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            if (LastMouse.LeftButton == ButtonState.Released
                                && FirstMouse.LeftButton == ButtonState.Pressed
                                && galaxy.systems[j].planets[i].structbuttons[z].Pos.Contains(FirstMouse.X, FirstMouse.Y)
                                && galaxy.systems[j].planets[i].TCShopsIsTrue == false
                                && galaxy.systems[j].planets[i].SSShopIstrue == false
                                && currentPlayer.faction == galaxy.systems[j].planets[i].faction)
                            {


                                if (z == 0) currentPlayer.actions.build_factory(galaxy.systems[j].planets[i].factory, 1, galaxy.systems[j].planets[i], currentPlayer);
                                if (z == 1) currentPlayer.actions.build_powerplant(galaxy.systems[j].planets[i].powerplant, 1, galaxy.systems[j].planets[i], currentPlayer);
                                if (z == 2) currentPlayer.actions.build_refinery(galaxy.systems[j].planets[i].refinery, 1, galaxy.systems[j].planets[i], currentPlayer);
                                if (z == 3) currentPlayer.actions.build_techcenter(galaxy.systems[j].planets[i].techcenter, galaxy.systems[j].planets[i], currentPlayer);
                                if (z == 4) currentPlayer.actions.build_turrent(galaxy.systems[j].planets[i].turrent, galaxy.systems[j].planets[i], currentPlayer, 1);
                            }

                        }
                    }

                    for (int z = 2; z < 11; z += 2)
                    {
                        if (galaxy.systems[j].planets[i].faction == Player.Faction.Klingon) galaxy.systems[j].planets[i].structures[z / 2 - 1].Pic = Content.Load<Texture2D>("Pictures/Structures/r" + z.ToString());
                        if (galaxy.systems[j].planets[i].faction == Player.Faction.Romulan) galaxy.systems[j].planets[i].structures[z / 2 - 1].Pic = Content.Load<Texture2D>("Pictures/Structures/k" + z.ToString());
                    }
                    for (int z = 1; z < 11; z += 2)
                    {

                        if (galaxy.systems[j].planets[i].faction == Player.Faction.Klingon) galaxy.systems[j].planets[i].structbuttons[z / 2].Pic = Content.Load<Texture2D>("Pictures/Structures/r" + z.ToString());
                        if (galaxy.systems[j].planets[i].faction == Player.Faction.Romulan) galaxy.systems[j].planets[i].structbuttons[z / 2].Pic = Content.Load<Texture2D>("Pictures/Structures/k" + z.ToString());
                    }
                    //============================================================================================================================               
                    //Structure Attributes
                    for (int z = 0; z < 5; z++)
                    {
                        if (galaxy.systems[j].planets[i].structures[z].Pos.Contains(FirstMouse.X, LastMouse.Y))
                            galaxy.systems[j].planets[i].structures[z].ShowArtibutes = true;
                        if (!galaxy.systems[j].planets[i].structures[z].Pos.Contains(FirstMouse.X, LastMouse.Y))
                            galaxy.systems[j].planets[i].structures[z].ShowArtibutes = false;
                    }
                    for (int z = 0; z < 5; z++)
                    
                        if (galaxy.systems[j].planets[i].structures[z].ShowArtibutes)
                        {
                            galaxy.systems[j].planets[i].structures[z].NamePos = new Vector2(FirstMouse.X, FirstMouse.Y);
                            if (z == 0) galaxy.systems[j].planets[i].structures[z].Name = "Factory";
                            if (z == 1) galaxy.systems[j].planets[i].structures[z].Name = "Power Plant";
                            if (z == 2) galaxy.systems[j].planets[i].structures[z].Name = "Refinery";
                            if (z == 3) galaxy.systems[j].planets[i].structures[z].Name = "Tech Center";
                            if (z == 4) galaxy.systems[j].planets[i].structures[z].Name = "Turrent";
                        }
                        
                    
                    //============================================================================================================================
                    //Structure Button Artibutes
                    for (int z = 0; z < galaxy.systems[j].planets[i].structbuttons.Length;z++ )
                    {
                        if (galaxy.systems[j].planets[i].structbuttons[z].Pos.Contains(FirstMouse.X, FirstMouse.Y))
                            galaxy.systems[j].planets[i].structbuttons[z].ShowArtibutes = true;
                        if (!(galaxy.systems[j].planets[i].structbuttons[z].Pos.Contains(FirstMouse.X, FirstMouse.Y)))
                            galaxy.systems[j].planets[i].structbuttons[z].ShowArtibutes = false;
                    }
                    for (int z = 0; z < galaxy.systems[j].planets[i].structbuttons.Length; z++)
                    {
                        if (galaxy.systems[j].planets[i].structbuttons[z].ShowArtibutes)
                        {
                            galaxy.systems[j].planets[i].structbuttons[z].NamePos =new Vector2(FirstMouse.X,FirstMouse.Y);
                            if (z == 0) galaxy.systems[j].planets[i].structbuttons[z].Name = "Build Factory";
                            if (z == 1) galaxy.systems[j].planets[i].structbuttons[z].Name = "Build Power Plant";
                            if (z == 2) galaxy.systems[j].planets[i].structbuttons[z].Name = "Build Refinery";
                            if (z == 3) galaxy.systems[j].planets[i].structbuttons[z].Name = "Build Tech Center";
                            if (z == 4) galaxy.systems[j].planets[i].structbuttons[z].Name = "Build Turrent";
                        }
                    }

                    //============================================================================================================================
                    //Space ship shop open
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].structures[0].Pos.Contains(FirstMouse.X, FirstMouse.Y))
                        galaxy.systems[j].planets[i].SSShopIstrue = true;
                    //============================================================================================================================
                    //Space ship shop close
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[2].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        galaxy.systems[j].planets[i].SSShopIstrue = false;
                        galaxy.systems[j].planets[i].SSCnt = 0;
                    }
                    //============================================================================================================================
                    //Tech Center shop open 
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].structures[3].Pos.Contains(FirstMouse.X, FirstMouse.Y))
                        galaxy.systems[j].planets[i].TCShopIstrue = true;
                    //============================================================================================================================
                    //Tech Center shop close
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed && galaxy.systems[j].planets[i].FactoryButtons[2].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].TCShopIstrue)
                        galaxy.systems[j].planets[i].TCShopIstrue = false;
                    //============================================================================================================================
                    //Space ship shop Next
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[0].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        if (galaxy.systems[j].planets[i].SSCnt < 11) galaxy.systems[j].planets[i].SSCnt++;
                        else galaxy.systems[j].planets[i].SSCnt = 0;

                    }
                    //============================================================================================================================
                    //Space ship shop Previous
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].FactoryButtons[1].rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue)
                    {
                        if (galaxy.systems[j].planets[i].SSCnt > 0) galaxy.systems[j].planets[i].SSCnt--;
                        else galaxy.systems[j].planets[i].SSCnt = 11;

                    }
                    //============================================================================================================================
                    //Building space ships
                    if (LastMouse.LeftButton == ButtonState.Released
                        && FirstMouse.LeftButton == ButtonState.Pressed
                        && galaxy.systems[j].planets[i].BuyOut.rpos.Contains(FirstMouse.X, FirstMouse.Y)
                        && galaxy.systems[j].planets[i].SSShopIstrue
                        && galaxy.systems[j].planets[i].faction == currentPlayer.faction)
                    {
                        if (currentPlayer.index == 0)
                            currentPlayer.actions.build_spaceship(kships[galaxy.systems[j].planets[i].SSCnt], currentPlayer, galaxy.systems[j].planets[i]);
                        if (currentPlayer.index == 1)
                            currentPlayer.actions.build_spaceship(rships[galaxy.systems[j].planets[i].SSCnt], currentPlayer, galaxy.systems[j].planets[i]);
                    }

                }
            //Systems
            //============================================================================================================================
            //Showing artibutes of a system
            for (int j = 0; j < galaxy.systems.Length; j++)
            {
                if (galaxy.systems[j].Pos.Contains(FirstMouse.X, FirstMouse.Y)
                    && galaxy.GalaxyIsTrue)
                    galaxy.systems[j].ShowArtibutes = true;
                else
                    galaxy.systems[j].ShowArtibutes = false;
                //============================================================================================================================
                //Entering a System
                if (galaxy.systems[j].Pos.Contains(FirstMouse.X, FirstMouse.Y) && LastMouse.LeftButton == ButtonState.Released
                    && FirstMouse.LeftButton == ButtonState.Pressed && galaxy.GalaxyIsTrue)
                {
                    galaxy.systems[j].SolarIsTrue = true;
                    galaxy.GalaxyIsTrue = false;
                    break;
                }

                
            }




            //Menu
            //============================================================================================================================    

            for (int i = 0; i < 3; i++)
            {

                if (menuButtons[i].rpos.Contains(FirstMouse.X, FirstMouse.Y) && menu.MenuIsTrue)
                {

                    if (LastMouse.LeftButton == ButtonState.Released && FirstMouse.LeftButton == ButtonState.Pressed)
                    {

                        if (i == 2) Exit();
                        if (i == 1) { menu.MenuIsTrue = false; }
                        if (i == 0) { menu.MenuIsTrue = false; MenuSoundInstance.Stop(); galaxy.GalaxyIsTrue = true; GameSoundInstance.Play(); }
                        menuButtons[i].pic = Content.Load<Texture2D>("Pictures/Buttons/b1");
                        menuButtons[i].pos = new Vector2(330, 100 * i + 292);
                    }
                    else
                    {
                        menuButtons[i].pic = Content.Load<Texture2D>("Pictures/Buttons/b2");
                        menuButtons[i].pos = new Vector2(330, 100 * i + 290);
                    }
                }
                else
                {
                    menuButtons[i].pic = Content.Load<Texture2D>("Pictures/Buttons/b1");
                    menuButtons[i].pos = new Vector2(330, 100 * i + 292);
                }
            }
                
          

            LastKeyboard = Keyboard.GetState();
            LastMouse = Mouse.GetState();
            base.Update(gameTime);
        }
            
            protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Navy);
            Random rand = new Random();
            spriteBatch.Begin();
            spriteBatch.Draw(BG, BGpos, Color.White);


            //============================================================================================================================  
                //Drawing galaxy and all thing related to it
                if (galaxy.GalaxyIsTrue)
                {
                    spriteBatch.Draw(BG2, BGpos, Color.White);
                    for (int i = 0; i < galaxy.systems.Length; i++)
                    {
                        spriteBatch.Draw(galaxy.systems[i].Pic, galaxy.systems[i].Pos, Color.Yellow*0.6f);
                        if (galaxy.systems[i].ShowArtibutes)
                        {

                            spriteBatch.DrawString(Font2, galaxy.systems[i].ID.ToString(), 
                                new Vector2(galaxy.systems[i].Pos.X, galaxy.systems[i].Pos.Y), Color.Yellow );
                            spriteBatch.DrawString(Font2, "Planets : " + galaxy.systems[i].PlanetNumber.ToString(), 
                                new Vector2(galaxy.systems[i].Pos.X, galaxy.systems[i].Pos.Y+10), Color.Yellow );
                            spriteBatch.DrawString(Font2, "Faction : " + galaxy.systems[i].faction.ToString(),
                               new Vector2(galaxy.systems[i].Pos.X, galaxy.systems[i].Pos.Y + 20),
                               (galaxy.systems[i].faction == Player.Faction.NoFaction) ? Color.White : (galaxy.systems[i].faction == Player.Faction.Klingon)?Color.Red : Color.Green);
                        }
                    }
                    spriteBatch.Draw(galaxy.systems[0].planets[0].FactoryButtons[3].pic, new Rectangle(700, 0, 100, 180), Color.White * 0.6f);
                    spriteBatch.DrawString(Font2, "Money " + currentPlayer.money, new Vector2(700, 15), Color.Cyan);
                    spriteBatch.DrawString(Font2, (currentPlayer.index == 0) ? "Turn : Player1" : "Turn : Player2", new Vector2(700, 0), (currentPlayer.index == 0) ? Color.Red : Color.Green);
                    spriteBatch.Draw(FactionPic, new Vector2(700, 60), Color.White * 0.6f);
                }
                //============================================================================================================================  
                //Drawing systems and all things related to them
                for (int i = 0; i < galaxy.systems.Length; i++)
                {
                    if (galaxy.systems[i].SolarIsTrue)
                    {
                        spriteBatch.Draw(SolarBG, new Vector2(0, 0), Color.White);
                        spriteBatch.Draw(galaxy.systems[i].planets[0].FactoryButtons[3].pic, new Rectangle(700, 0, 100, 180), Color.White * 0.6f);
                        spriteBatch.DrawString(Font2, "Money " + currentPlayer.money, new Vector2(700, 15), Color.Cyan);
                        spriteBatch.DrawString(Font2, (currentPlayer.index == 0) ? "Turn : Player1" : "Turn : Player2", new Vector2(700, 0), (currentPlayer.index == 0) ? Color.Red : Color.Green);
                        spriteBatch.Draw(FactionPic, new Vector2(700, 60), Color.White * 0.6f);
                        for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                        {
                            //Planet artibutes
                            spriteBatch.Draw(galaxy.systems[i].planets[j].Pic, galaxy.systems[i].planets[j].Pos, Color.White*0.7f);
                            spriteBatch.Draw(galaxy.systems[i].planets[j].MoveButton.pic, galaxy.systems[i].planets[j].MoveButton.rpos, Color.White);
                            spriteBatch.Draw(galaxy.systems[i].planets[j].SendResource.pic, galaxy.systems[i].planets[j].SendResource.rpos, Color.White);
                            if (galaxy.systems[i].planets[j].ShowArtibutes)
                            {
                                spriteBatch.DrawString(Font2, "Refineries : " + galaxy.systems[i].planets[j].refnumber,
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 10),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction)? Color.White :
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon)?Color.Red : Color.Green );
                                spriteBatch.DrawString(Font2, "Factories : " + galaxy.systems[i].planets[j].facnumber,
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 20), 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White : 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                                spriteBatch.DrawString(Font2, "Faction : " + galaxy.systems[i].planets[j].faction + " : " + galaxy.systems[i].planets[j].Tendency + "%",
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 30),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White : 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                                spriteBatch.DrawString(Font2, "Space Ships : " + galaxy.systems[i].planets[j].spaceships.Count,
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 40),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White : 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                                spriteBatch.DrawString(Font2, "Resource : " + galaxy.systems[i].planets[j].Resource,
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 50),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White : 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                                spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].ID.ToString(),
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White : 
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                                spriteBatch.DrawString(Font2, "Turrents " + galaxy.systems[i].planets[j].turrentnumber,
                                    new Vector2(galaxy.systems[i].planets[j].Pos.X, galaxy.systems[i].planets[j].Pos.Y + 60),
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.NoFaction) ? Color.White :
                                    (galaxy.systems[i].planets[j].faction == Player.Faction.Klingon) ? Color.Red : Color.Green);
                            }
                        }
                    }
                }

                //============================================================================================================================  
                //Drawing Menu
                if(menu.MenuIsTrue)
            {
                spriteBatch.Draw(BG, BGpos, Color.White);
                spriteBatch.Draw(title, titlepos, Color.LightGray * 0.8f);
                for (int i = 0; i < 3; i++)
                {
                    spriteBatch.Draw(menuButtons[i].pic, menuButtons[i].rpos, menuButtons[i].color * 0.6f);
                    spriteBatch.DrawString(Font, menuButtons[i].name, menuButtons[i].pos, menuButtons[i].color * 0.6f);
                }
            }
                //============================================================================================================================  
                //Drawing planets and all things related to them
                for (int i = 0; i < galaxy.systems.Length; i++)
                for (int j = 0; j < galaxy.systems[i].planets.Length; j++)
                {
                    
                    if (galaxy.systems[i].planets[j].PlanetIsTrue == true)
                    {
                        spriteBatch.Draw(PlanetBG, new Vector2(0, 0), Color.White);
                        spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[3].pic, new Rectangle(0, 0, 100, 600), Color.White * 0.6f);
                        spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[3].pic, new Rectangle(700, 0, 100,180), Color.White * 0.6f);

                        for (int z = 0; z < 5; z++)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structbuttons[z].Pic, galaxy.systems[i].planets[j].structbuttons[z].Pos, Color.White * 0.8f);
                            if (galaxy.systems[i].planets[j].structbuttons[z].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structbuttons[z].Name, galaxy.systems[i].planets[j].structbuttons[z].NamePos, Color.Tan);
                        }
                        //Drawing structures
                        if (galaxy.systems[i].planets[j].powerIsTrue)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[1].Pic, galaxy.systems[i].planets[j].structures[1].Pos, Color.White);
                            if (galaxy.systems[i].planets[j].structures[1].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[1].Name, galaxy.systems[i].planets[j].structures[1].NamePos, Color.Tan);
                        }
                        if (galaxy.systems[i].planets[j].techIsTrue)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[3].Pic, galaxy.systems[i].planets[j].structures[3].Pos, Color.White);
                            if (galaxy.systems[i].planets[j].structures[3].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[3].Name, galaxy.systems[i].planets[j].structures[3].NamePos, Color.Tan);
                        }
                        if (galaxy.systems[i].planets[j].refnumber > 0)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[2].Pic, galaxy.systems[i].planets[j].structures[2].Pos, Color.White);
                            if (galaxy.systems[i].planets[j].structures[2].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[2].Name, galaxy.systems[i].planets[j].structures[2].NamePos, Color.Tan);
                        }
                        if (galaxy.systems[i].planets[j].facnumber > 0)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[0].Pic, galaxy.systems[i].planets[j].structures[0].Pos, Color.White);
                            if (galaxy.systems[i].planets[j].structures[0].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[0].Name, galaxy.systems[i].planets[j].structures[0].NamePos, Color.Tan);
                        }
                        if (galaxy.systems[i].planets[j].turrentnumber > 0)
                        {
                            spriteBatch.Draw(galaxy.systems[i].planets[j].structures[4].Pic, galaxy.systems[i].planets[j].structures[4].Pos, Color.White);
                            if (galaxy.systems[i].planets[j].structures[4].ShowArtibutes) spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[4].Name, galaxy.systems[i].planets[j].structures[4].NamePos, Color.Tan);
                        }
                                            if (galaxy.systems[i].planets[j].SSShopIstrue && galaxy.systems[i].planets[j].PlanetIsTrue)
                        {
                            //Space Ship Attributes
                            for (int z = 3; z >= 0; z--)
                            {
                                spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[z].pic, galaxy.systems[i].planets[j].FactoryButtons[z].rpos, Color.White * 0.8f);
                                spriteBatch.DrawString(Font2, galaxy.systems[i].planets[j].structures[1].Name, galaxy.systems[i].planets[j].structures[1].NamePos, Color.White);
                            }
                                if(currentPlayer.index == 0)
                            spriteBatch.Draw(kships[galaxy.systems[i].planets[j].SSCnt].Pic, new Vector2(50,350), Color.White);
                            if(currentPlayer.index == 1)
                                spriteBatch.Draw(rships[galaxy.systems[i].planets[j].SSCnt].Pic, new Vector2(50, 350), Color.White);
                            spriteBatch.DrawString(Font, "Price : " + kships[galaxy.systems[i].planets[j].SSCnt].Price,
                                new Vector2(400,300), Color.White * 0.6f);
                            spriteBatch.DrawString(Font, "Armor : " + kships[galaxy.systems[i].planets[j].SSCnt].Armor, 
                                new Vector2(400, 350), Color.White * 0.6f);
                            spriteBatch.DrawString(Font, "Attack Power : " + kships[galaxy.systems[i].planets[j].SSCnt].AttackPower, 
                                new Vector2(400, 400), Color.White * 0.6f);
                            spriteBatch.Draw(galaxy.systems[i].planets[j].BuyOut.pic, galaxy.systems[i].planets[j].BuyOut.rpos, Color.White*0.8f);
                            spriteBatch.DrawString(Font, galaxy.systems[i].planets[j].BuyOut.name, galaxy.systems[i].planets[j].BuyOut.pos, Color.White*0.8f);
                        
                        }
                        if (galaxy.systems[i].planets[j].TCShopIstrue && galaxy.systems[i].planets[j].PlanetIsTrue)
                        {
                            for (int z = 3; z >= 0; z--)
                                spriteBatch.Draw(galaxy.systems[i].planets[j].FactoryButtons[z].pic, galaxy.systems[i].planets[j].FactoryButtons[z].rpos, Color.White*0.8f);
                            
                        }
                        //Player Attributes
                        spriteBatch.DrawString(Font2, "Turrents " + galaxy.systems[i].planets[j].turrentnumber.ToString(), new Vector2(0, 30), Color.Orange);
                        spriteBatch.DrawString(Font2, "Refineries " + galaxy.systems[i].planets[j].refnumber.ToString(), new Vector2(0, 0), Color.Violet);
                        spriteBatch.DrawString(Font2, "Factories " + galaxy.systems[i].planets[j].facnumber.ToString(), new Vector2(0, 60), Color.Yellow);
                        spriteBatch.DrawString(Font2, "Money " + currentPlayer.money, new Vector2(700, 30), Color.Cyan);
                        spriteBatch.DrawString(Font2, (currentPlayer.index==0)? "Turn : Player1"  :"Turn : Player2" , new Vector2(700, 0), (currentPlayer.index==0)? Color.Red:Color.Green);
                        spriteBatch.DrawString(Font2, "Space Ships " + currentPlayer.spaceships.Count, new Vector2(700, 15), Color.White);
                        spriteBatch.Draw(FactionPic, new Vector2(700, 60), Color.White * 0.6f);
                    }

                }
                
                
                        MouseState mouse = Mouse.GetState();
                        spriteBatch.Draw(cursor, cursorpos, Color.White);  

          
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
