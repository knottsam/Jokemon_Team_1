﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Jokemon_Team_1
{
    public class Game1 : Game
    { //Plrease work
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont fontP;
        private SpriteFont battlingfont;
        private SpriteFont statsfont;

        private const int screenWidth = 800;
        private const int screenHeight = 800;

        private Tree[,] bigTreeTypeSide = new Tree[2, 14];
        private Tree[,] bigTreeTypeBottom = new Tree[2, 16];
        private Tree[,] smallTrees = new Tree[2, 6];
        private List<Tree> treeObjectList = new List<Tree>();


        private Building[] houses = new Building[2];
        private Building laboratory;
        private List<Building> buildingObjectList = new List<Building>();

        private Player player;

        private Sprite flowers = new Sprite();
        private Grass[,] jokemonGrass = new Grass[2, 6];
        private List<Grass> grassObjectList = new List<Grass>();

        private ReadableObject[] signPosts = new ReadableObject[2];
        private ReadableObject[] postBoxes = new ReadableObject[3];
        private List<ReadableObject> readablesObjectList = new List<ReadableObject>();

        private Jokemon[] showJokemonInBattle = new Jokemon[2];

        private BattleSystem battlesystem = new BattleSystem();

        private StartMenu startMenu = new StartMenu();
        private Sprite playButton = new Sprite();
        private Text playText = new Text();
        private Text skill1text = new Text();
        private Text skill2text = new Text();
        private Text skill3text = new Text();
        private Text skill4text = new Text();
        private Text eskill1text = new Text();
        private Text eskill2text = new Text();
        private Text eskill3text = new Text();
        private Text eskill4text = new Text();
        private Text ownhealth = new Text();
        private Text ownattack = new Text();
        private Text enemyhealth = new Text();
        private Text enemyattack = new Text();
        private Text showattackorder = new Text();

        private PhysicsManager pManager = new PhysicsManager();
        private InputManager iManager = new InputManager();

        private Texture2D labTexture;
        private Texture2D bigTreeTexture;
        private Texture2D houseTexture;
        private Texture2D playerTexture;
        private Texture2D smallTreeTexture; //Comment here please work oh my god
        private Texture2D signTextureWood;
        private Texture2D postBoxTexture;
        private Texture2D grassTexture;
        private Texture2D squareTexture;
        private Texture2D skillbox;

        public bool inJokemonBattle = false;
        private bool inPauseMenu = false;
        private int countFrames = 0;
        private bool userattacking = true, enemyattacking = false;
        private bool encounterenemy = false;

        private Jokemon PikaAchu;
        private Jokemon Enemy;
        private Stream music;
        private Texture2D pikaachuback;
        private Texture2D pikaachufront;

        private Sprite skillbox1,skillbox2,skillbox3,skillbox4, eskillbox1, eskillbox2, eskillbox3, eskillbox4;

        private HealthBar healthbar;
        private HealthBar enemyhealthbar;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            //It's supposed to be 800, 800 // is now a constant go got line 14 & 15
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            fontP = Content.Load<SpriteFont>("File");
            labTexture = Content.Load<Texture2D>("LabFixed");
            bigTreeTexture = Content.Load<Texture2D>("TreeFixed");
            houseTexture = Content.Load<Texture2D>("HouseFixed");
            playerTexture = Content.Load<Texture2D>("PlayerFixed");
            smallTreeTexture = Content.Load<Texture2D>("TreeFixed");
            grassTexture = Content.Load<Texture2D>("GrassFixed");
            squareTexture = Content.Load<Texture2D>("square");
            //pausemenuTexture = Content.Load<Texture2D>("PauseMenuBox");
            //signTextureWood = Content.Load<Texture2D>("Sign_Little");
            //postBoxTexture = Content.Load<Texture2D>("Postbox");
            pikaachufront = Content.Load<Texture2D>("Pika-A-chu-fixed");
            pikaachuback= Content.Load<Texture2D>("Pika_Back");
            skillbox = Content.Load<Texture2D>("box");
            battlingfont = Content.Load<SpriteFont>("Battling font");
            SpriteFont fontPika = Content.Load<SpriteFont>("File");
            statsfont = Content.Load<SpriteFont>("FontbyCharles");
            startMenu.hasStarted = false; //makes start menu show when game starts
            playButton = new Sprite(squareTexture, new Vector2((screenWidth / 2) - 200, screenHeight / 3), new Vector2(400, 100));
            playText = new Text(fontPika, "Play", new Vector2((screenWidth / 2) - 50, (screenHeight / 3) + 25), Color.Black);




            //The following are TREES
            for (int i = 0; i <= bigTreeTypeSide.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= bigTreeTypeSide.GetUpperBound(1); j++)
                {
                    if (i == 0)
                    {
                        bigTreeTypeSide[i, j] = new Tree(bigTreeTexture, new Vector2(0, j * Window.ClientBounds.Height / bigTreeTypeSide.GetUpperBound(1)), new Vector2(bigTreeTexture.Width * 2, bigTreeTexture.Height * 2));
                    }
                    else
                    {
                        bigTreeTypeSide[i, j] = new Tree(bigTreeTexture, new Vector2(Window.ClientBounds.Width - bigTreeTexture.Width * 2, j * Window.ClientBounds.Height / bigTreeTypeSide.GetUpperBound(1)), new Vector2(bigTreeTexture.Width * 2, bigTreeTexture.Height * 2));
                    }

                    treeObjectList.Add(bigTreeTypeSide[i, j]);
                }
            }

            for (int i = 0; i <= bigTreeTypeBottom.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= bigTreeTypeBottom.GetUpperBound(1); j++)
                {
                    if (i == 0)
                    {
                        bigTreeTypeBottom[i, j] = new Tree(bigTreeTexture, new Vector2(j * Window.ClientBounds.Width / bigTreeTypeBottom.GetUpperBound(1), 0), new Vector2(bigTreeTexture.Width * 2, bigTreeTexture.Height * 2));

                    }
                    else
                    {
                        bigTreeTypeBottom[i, j] = new Tree(bigTreeTexture, new Vector2(j * Window.ClientBounds.Width / bigTreeTypeBottom.GetUpperBound(1), Window.ClientBounds.Height - bigTreeTexture.Height * 2), new Vector2(bigTreeTexture.Width * 2, bigTreeTexture.Height * 2));
                    }

                    if(j != 8)
                    {
                        treeObjectList.Add(bigTreeTypeBottom[i, j]);
                    }
                }
            }

            for (int i = 0; i <= smallTrees.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= smallTrees.GetUpperBound(1); j++)
                {
                    if (i == 0)
                    {
                        smallTrees[i, j] = new Tree(smallTreeTexture, new Vector2(140 + j * smallTreeTexture.Width * 2, 460), new Vector2(smallTreeTexture.Width * 2, smallTreeTexture.Height));
                    }
                    else
                    {
                        smallTrees[i, j] = new Tree(smallTreeTexture, new Vector2(440 + j * smallTreeTexture.Width * 2, 660), new Vector2(smallTreeTexture.Width * 2, smallTreeTexture.Height));
                    }

                    treeObjectList.Add(smallTrees[i, j]);
                }
            }
            //Trees end HERE
            // Jokemon                            - by charles(just in case of merging error, ignore)
            PikaAchu = new Jokemon(pikaachuback, new Vector2(-100, 400), new Vector2(500, 500),100,10,5,10,5,5,"Normal Attack","Iron Tail","Nuzzle","Sneeze");
            Enemy = new Jokemon(pikaachufront, new Vector2(450, -75), new Vector2(500, 500), 100, 10, 5, 10, 5, 5, "Normal Attack", "Iron Tail", "Nuzzle", "Sneeze");
            ownhealth = new Text(statsfont,"health =" + PikaAchu.health.ToString(), new Vector2(0, 550), Color.White);
            ownattack = new Text(statsfont, "atk =" + PikaAchu.attack.ToString(), new Vector2(0, 500), Color.White);
            enemyhealth = new Text(statsfont, "health =" + Enemy.health.ToString(), new Vector2(600, 300), Color.White);
            enemyattack = new Text(statsfont, "atk =" + Enemy.attack.ToString(), new Vector2(600, 350), Color.White);












            //The following are BUILDINGS
            laboratory = new Building(labTexture, new Vector2(400, 500), new Vector2(labTexture.Width * 2, labTexture.Height * 2));
            buildingObjectList.Add(laboratory);

            for (int i = 0; i <= houses.GetUpperBound(0); i++)
            {
                houses[i] = new Building(houseTexture, new Vector2(), new Vector2(houseTexture.Width * 2, houseTexture.Height * 2));
                buildingObjectList.Add(houses[i]);
            }
            houses[0].spritePosition = new Vector2(Window.ClientBounds.Width / 3 - houses[0].spriteTexture.Width, 200);
            houses[1].spritePosition = new Vector2(2 * Window.ClientBounds.Width / 3 - houses[1].spriteTexture.Width, 200);
            //Buildings end HERE -- RIGHT HERE!

            //The following are READABLE OBJECTS
            //for (int i = 0; i <= signPosts.GetUpperBound(0); i++)
            //{
            //    signPosts[i] = new ReadableObject(signTextureWood, new Vector2(), new Vector2(signTextureWood.Width * 2, signTextureWood.Height * 2));
            //    readablesObjectList.Add(signPosts[i]);
            //}
            //signPosts[0].spritePosition = new Vector2(smallTrees[0, smallTrees.GetUpperBound(1)].spritePosition.X + signPosts[0].spriteTexture.Width * 2, smallTrees[0, smallTrees.GetUpperBound(1)].spritePosition.Y);
            //signPosts[1].spritePosition = new Vector2(smallTrees[1, smallTrees.GetUpperBound(1)].spritePosition.X + signPosts[1].spriteTexture.Width * 2, smallTrees[1, smallTrees.GetUpperBound(1)].spritePosition.Y);

            //for (int i = 0; i <= postBoxes.GetUpperBound(0); i++)
            //{
            //    postBoxes[i] = new ReadableObject(postBoxTexture, new Vector2(), new Vector2(postBoxTexture.Width * 2, postBoxTexture.Height * 2));
            //    readablesObjectList.Add(postBoxes[i]);
            //}
            //postBoxes[0].spritePosition = new Vector2(houses[1].spritePosition.X - postBoxes[1].spriteTexture.Width);
            //Readable Objects end HERE

            //Grass goes HERE

            for(int i = 0; i <= jokemonGrass.GetUpperBound(0); i++)
            {
                for(int j = 0; j <= jokemonGrass.GetUpperBound(1); j++)
                {
                    jokemonGrass[i, j] = new Grass(grassTexture, new Vector2(Window.ClientBounds.Width / 2 - grassTexture.Width * i, j * grassTexture.Height), new Vector2(grassTexture.Width, grassTexture.Height));
                    grassObjectList.Add(jokemonGrass[i,j]);
                }
            }

            //Grass ends HERE

                for(int i = 0; i <= showJokemonInBattle.GetUpperBound(0); i++)
                {
                    showJokemonInBattle[i] = new Jokemon(playerTexture, new Vector2(), new Vector2(100, 100), 0, 0, 0, 0, 0, 0, "this", "might", "break", "everything");
                }

            player = new Player(playerTexture, new Vector2(200, 100), new Vector2(playerTexture.Width * 2, playerTexture.Height * 2));
            //pausemenu = new PauseMenu(pausemenuTexture, new Vector2(400-pausemenuTexture.Width/2, 400-pausemenuTexture.Height/2), new Vector2(pausemenuTexture.Width, pausemenuTexture.Height), false);


            skillbox1 = new Sprite(skillbox, new Vector2(300, 500), new Vector2(150, 50));
            skillbox2 = new Sprite(skillbox, new Vector2(300, 650), new Vector2(150, 50));
            skillbox3 = new Sprite(skillbox, new Vector2(500, 500), new Vector2(150, 50));
            skillbox4 = new Sprite(skillbox, new Vector2(500, 650), new Vector2(150, 50));

            skill1text = new Text(battlingfont, "Iron Tail", new Vector2(300, 500), Color.Black);
            skill2text = new Text(battlingfont, "Nuzzle", new Vector2(300, 650), Color.Black);
            skill3text = new Text(battlingfont, "Normal Attack", new Vector2(500, 500), Color.Black);
            skill4text = new Text(battlingfont, "Sneeze", new Vector2(500, 650), Color.Black);


            eskillbox1 = new Sprite(skillbox, new Vector2(50, 50), new Vector2(150, 50));
            eskillbox2 = new Sprite(skillbox, new Vector2(50, 200), new Vector2(150, 50));
            eskillbox3 = new Sprite(skillbox, new Vector2(250, 50), new Vector2(150, 50));
            eskillbox4 = new Sprite(skillbox, new Vector2(250, 200), new Vector2(150, 50));

            eskill1text = new Text(battlingfont, "Iron Tail", new Vector2(50, 50), Color.Black);
            eskill2text = new Text(battlingfont, "Nuzzle", new Vector2(50, 200), Color.Black);
            eskill3text = new Text(battlingfont, "Normal Attack", new Vector2(250, 50), Color.Black);
            eskill4text = new Text(battlingfont, "Sneeze", new Vector2(250, 200), Color.Black);

            showattackorder = new Text(fontPika, "test", new Vector2(300, 350), Color.White);
            healthbar = new HealthBar(skillbox, PikaAchu, new Vector2(10, 400));
            enemyhealthbar = new HealthBar(skillbox, Enemy, new Vector2(690, 400));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.G))
                encounterenemy = true;
                // TODO: Add your update logic here
            if (startMenu.hasStarted == false) //wont show anything until space bar is pressed
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    startMenu.hasStarted = true;
                }
            }
            else if (startMenu.hasStarted == true) //start menu will disappear
            {
                if (encounterenemy == false)
                {
                    if (inJokemonBattle == false)
                    {

                        iManager.checkKeyboard(player, PikaAchu);

                        foreach (Tree t in treeObjectList)
                        {
                            pManager.checkCollision(player, t);
                        }

                        foreach (Building b in buildingObjectList)
                        {
                            pManager.checkCollision(player, b);
                        }
                        if (!inJokemonBattle)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.T))
                            {
                                inJokemonBattle = true;
                            }
                        }
                        else if (inJokemonBattle)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Y))
                            {
                                inJokemonBattle = false;
                            }

                            battlesystem.Battling(PikaAchu, Enemy, true, skillbox1, skillbox2, skillbox3, skillbox4);
                        }

                        //foreach (ReadableObject r in readablesObjectList)
                        //{
                        //    pManager.checkCollision(player, r);
                        //}

                        //Semi-broken, for now.

                        foreach (Grass g in grassObjectList)
                        {
                            if (countFrames % 10 == 0)
                            {
                                if (player.goingDown == true || player.goingLeft == true || player.goingRight == true || player.goingUp == true)
                                {
                                    if (pManager.checkCollision(player, g) == true)
                                    {
                                        inJokemonBattle = true;
                                    }
                                }
                            }
                        }

                        countFrames = countFrames + 1;

                        if (countFrames >= 60)
                        {
                            countFrames = 0;
                        }
                    }

                    else if (inJokemonBattle == true)
                    {
                        enemyhealth.textContent = "health =" + Enemy.health.ToString();
                        ownhealth.textContent = "health =" + PikaAchu.health.ToString();
                        Rectangle mouserec = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                        Rectangle skillbox1rec = new Rectangle((int)skillbox1.spritePosition.X, (int)skillbox1.spritePosition.Y, (int)skillbox1.spriteSize.X, (int)skillbox1.spriteSize.Y);
                        Rectangle skillbox2rec = new Rectangle((int)skillbox2.spritePosition.X, (int)skillbox2.spritePosition.Y, (int)skillbox2.spriteSize.X, (int)skillbox2.spriteSize.Y);
                        Rectangle skillbox3rec = new Rectangle((int)skillbox3.spritePosition.X, (int)skillbox3.spritePosition.Y, (int)skillbox3.spriteSize.X, (int)skillbox3.spriteSize.Y);
                        Rectangle skillbox4rec = new Rectangle((int)skillbox4.spritePosition.X, (int)skillbox4.spritePosition.Y, (int)skillbox4.spriteSize.X, (int)skillbox4.spriteSize.Y);
                        Rectangle eskillbox1rec = new Rectangle((int)eskillbox1.spritePosition.X, (int)eskillbox1.spritePosition.Y, (int)eskillbox1.spriteSize.X, (int)eskillbox1.spriteSize.Y);
                        Rectangle eskillbox2rec = new Rectangle((int)eskillbox2.spritePosition.X, (int)eskillbox2.spritePosition.Y, (int)eskillbox2.spriteSize.X, (int)eskillbox2.spriteSize.Y);
                        Rectangle eskillbox3rec = new Rectangle((int)eskillbox3.spritePosition.X, (int)eskillbox3.spritePosition.Y, (int)eskillbox3.spriteSize.X, (int)eskillbox3.spriteSize.Y);
                        Rectangle eskillbox4rec = new Rectangle((int)eskillbox4.spritePosition.X, (int)eskillbox4.spritePosition.Y, (int)eskillbox4.spriteSize.X, (int)eskillbox4.spriteSize.Y);
                        if (Keyboard.GetState().IsKeyDown(Keys.X))
                        {
                            inJokemonBattle = false;
                        }

                        if (userattacking)
                        {
                            showattackorder = new Text(fontP, "User turn", new Vector2(300, 400), Color.White);
                            if (mouserec.Intersects(skillbox1rec) || mouserec.Intersects(skillbox2rec) || mouserec.Intersects(skillbox3rec) || mouserec.Intersects(skillbox4rec))
                            {
                                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                {
                                    Enemy.health -= 10;

                                    userattacking = false;
                                    enemyattacking = true;
                                }
                            }
                        }
                        else if (enemyattacking)
                        {
                            showattackorder = new Text(fontP, "Enemy turn", new Vector2(300, 400), Color.White);
                            if (mouserec.Intersects(eskillbox1rec) || mouserec.Intersects(eskillbox2rec) || mouserec.Intersects(eskillbox3rec) || mouserec.Intersects(eskillbox4rec))
                            {
                                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                {
                                    PikaAchu.health -= 10;
                                    userattacking = true;
                                    enemyattacking = false;

                                }
                            }
                        }
                        if (PikaAchu.health <= 0 || Enemy.health <= 0)
                        {
                            inJokemonBattle = false;
                        }


                    }
                }
                else if (encounterenemy == true)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.G))
                    {
                        encounterenemy = false;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (startMenu.hasStarted == true) //only drwas everything else when the tart menu disappears
            {
                if (encounterenemy == false)
                {
                    if (inJokemonBattle == false)
                    {
                        GraphicsDevice.Clear(Color.LawnGreen);

                        foreach (Tree t in bigTreeTypeSide)
                        {
                            t.DrawSprite(_spriteBatch, t.spriteTexture);
                        }

                        foreach (Tree t in bigTreeTypeBottom)
                        {
                            t.DrawSprite(_spriteBatch, t.spriteTexture);
                        }

                        foreach (Building b in houses)
                        {
                            b.DrawSprite(_spriteBatch, b.spriteTexture);
                        }

                        foreach (Tree t in smallTrees)
                        {
                            t.DrawSprite(_spriteBatch, t.spriteTexture);
                        }

                        //foreach (ReadableObject r in signPosts)
                        //{
                        //    r.DrawSprite(_spriteBatch, r.spriteTexture);
                        //}

                        foreach (Grass g in jokemonGrass)
                        {
                            g.DrawSprite(_spriteBatch, grassTexture);
                        }

                        laboratory.DrawSprite(_spriteBatch, laboratory.spriteTexture);

                        player.DrawSprite(_spriteBatch, player.spriteTexture);
                    }
                    else if (inJokemonBattle == true)
                    {
                        GraphicsDevice.Clear(Color.Black);
                        PikaAchu.DrawJokemon(_spriteBatch, pikaachuback);
                        Enemy.DrawJokemon(_spriteBatch, pikaachufront);

                        ownhealth.DrawText(_spriteBatch);
                        ownattack.DrawText(_spriteBatch);
                        enemyhealth.DrawText(_spriteBatch);
                        enemyattack.DrawText(_spriteBatch);
                        skillbox1.DrawSprite(_spriteBatch, skillbox);
                        skillbox2.DrawSprite(_spriteBatch, skillbox);
                        skillbox3.DrawSprite(_spriteBatch, skillbox);
                        skillbox4.DrawSprite(_spriteBatch, skillbox);
                        skill4text.DrawText(_spriteBatch);
                        skill3text.DrawText(_spriteBatch);
                        skill2text.DrawText(_spriteBatch);
                        skill1text.DrawText(_spriteBatch);
                        eskillbox1.DrawSprite(_spriteBatch, skillbox);
                        eskillbox2.DrawSprite(_spriteBatch, skillbox);
                        eskillbox3.DrawSprite(_spriteBatch, skillbox);
                        eskillbox4.DrawSprite(_spriteBatch, skillbox);
                        eskill4text.DrawText(_spriteBatch);
                        eskill3text.DrawText(_spriteBatch);
                        eskill2text.DrawText(_spriteBatch);
                        eskill1text.DrawText(_spriteBatch);
                        healthbar.DrawHealth(_spriteBatch, PikaAchu);
                        enemyhealthbar.DrawHealth(_spriteBatch, Enemy);
                        showattackorder.DrawText(_spriteBatch);
                    }
                }
                else if (encounterenemy == true)
                {
                    {
                        GraphicsDevice.Clear(Color.Green);

                    }
                }
            }
            if (startMenu.hasStarted == false) //draws start menu
            {
                GraphicsDevice.Clear(Color.Purple);
                startMenu.DrawStartMenu(_spriteBatch);
                playButton.DrawSprite(_spriteBatch, squareTexture);
                playText.DrawText(_spriteBatch);
            }
            // TODO: Add your drawing code here




            base.Draw(gameTime);
        }
    }
}
