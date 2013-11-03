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

namespace CS2053Lab2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenWidth;
        int screenHeight;

        Texture2D backgroundTexture;
        Rectangle backgroundRectangle;

        // Game Object
        Texture2D gameObjectTexture;
        List<GameObject> gameObjectList = new List<GameObject>();

        Texture2D gameObjectTexture2;
        List<GameObject> gameObjectList2 = new List<GameObject>();

        List<GameObject> gameObjectsHit = new List<GameObject>();

       // Player Object
        PlayerObject  playerObject; 
        Vector2 playerObjectVelocity = new Vector2(1f, 1f);

        // Game Info
        GameInfoObject gameInfoObject;

        // Health
        GameInfoObject score;
        int health;

        // Win Lose
        GameInfoObject gameOver;

        Boolean gameWon = false;
        String winLose;



        int playerCount, enemyCount, i, j;

        String gameInfo;
        double gameHour, gameMinute, gameSecond;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Back Ground
            backgroundTexture = this.Content.Load<Texture2D>("background3");
            backgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            // Game Object
            gameObjectTexture = this.Content.Load<Texture2D>("egg");
            gameObjectTexture2 = this.Content.Load<Texture2D>("axe");

            // Player Object
            playerObject = new PlayerObject(screenWidth, screenHeight);
            playerObject.setTexture(this.Content.Load<Texture2D>("whiteball"));
            playerObject.setLocation(new Vector2(0, screenHeight - 50));
            playerObject.setBox(new Rectangle(0, screenHeight - 50, 50, 50));
            playerObject.setVelocity(new Vector2(5, 5));


            // Game Info
            gameInfoObject = new GameInfoObject(this.Content.Load<SpriteFont>("SpriteFont1"), new Vector2(0, 0));
            
            // Score
            score = new GameInfoObject(this.Content.Load<SpriteFont>("SpriteFont2"), new Vector2(screenWidth - 200, 0));
            health = 100;
            playerCount = 10;
            enemyCount = 12; 
            i = 0;
            j = 0;

            // Game over 
            gameOver = new GameInfoObject(this.Content.Load<SpriteFont>("SpriteFont3"), new Vector2(screenWidth / 2 - 50, screenHeight / 2 - 50));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

       // Boolean spaceKeyDown = false;
        Random random = new Random();

        protected override void Update(GameTime gameTime)
        {
            if (!gameWon)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // TODO: Add your update logic here


                KeyboardState kstate = Keyboard.GetState();

                gameHour = gameTime.TotalGameTime.Hours;
                gameMinute = gameTime.TotalGameTime.Minutes;
                gameSecond = gameTime.TotalGameTime.Seconds;
                gameInfo = "Game Time: " + gameHour + ":" + gameMinute + ":" + gameSecond;

                // update gameInfo
                gameInfoObject.setGameInfo(gameInfo);

                // score
                score.setGameInfo("Health: " + health);


                // update player
                playerObject.update(kstate);

                // update object
                foreach (GameObject g in gameObjectList)
                {
                    g.update();
                }

                foreach (GameObject g in gameObjectList2)
                {
                    g.update();
                }

                for (; i < playerCount; i++)
                {
                    GameObject g = new GameObject(screenWidth, screenHeight);
                    g.setTexture(gameObjectTexture);
                    int x = (int)random.Next(0, screenWidth - 50);
                    int y = (int)random.Next(0, screenHeight - 50);
                    g.setLocation(new Vector2(x, y));
                    g.setBox(new Rectangle(x, y, 50, 50));
                    float vx = random.Next(-2, 2);
                    float vy = (int)random.Next(-2, 2);
                    g.setVelocity(new Vector2(vx, vy));
                    gameObjectList.Add(g);
                    // spaceKeyDown = false;
                }

                //i = 0;

                for (; j < enemyCount; j++)
                {
                    GameObject g = new GameObject(screenWidth, screenHeight);
                    g.setTexture(gameObjectTexture2);
                    int x = (int)random.Next(0, screenWidth - 50);
                    int y = (int)random.Next(0, screenHeight - 50);
                    g.setLocation(new Vector2(x, y));
                    g.setBox(new Rectangle(x, y, 50, 50));
                    float vx = random.Next(-2, 2);
                    float vy = (int)random.Next(-2, 2);
                    g.setVelocity(new Vector2(vx, vy));
                    gameObjectList2.Add(g);
                    // spaceKeyDown = false;
                }

                foreach (GameObject g in gameObjectList)
                {
                    // if (Math.Abs(Math.Sqrt(Math.Pow((playerObject.getLocation().X - g.getLocation().X), 2) + Math.Pow(((playerObject.getLocation().Y - g.getLocation().Y)), 2))) < 5)
                    if (playerObject.getBox().Intersects(g.getBox()))
                    {
                        health = health + 10;
                        enemyCount = enemyCount + 2;
                        gameObjectsHit.Add(g);
                        //gameObjectList.Remove(g);

                    }
                }

                foreach (GameObject g in gameObjectList2)
                {
                    // if (Math.Abs(Math.Sqrt(Math.Pow((playerObject.getLocation().X - g.getLocation().X), 2) + Math.Pow(((playerObject.getLocation().Y - g.getLocation().Y)), 2))) < 5)
                    if (playerObject.getBox().Intersects(g.getBox()))
                    {
                        health = health - 20;
                        gameObjectsHit.Add(g);
                        //gameObjectList.Remove(g);

                    }
                }

                foreach (GameObject g in gameObjectsHit)
                {
                    // score++;
                    gameObjectList.Remove(g);

                }

                foreach (GameObject g in gameObjectsHit)
                {
                    // score++;
                    gameObjectList2.Remove(g);

                }

                gameObjectsHit = new List<GameObject>();



                if (gameObjectList.Count == 0)
                {
                    winLose = "YOU WIN!!!";
                    gameOver.setGameInfo(winLose);
                    gameWon = true;
                }
                if (health < 0)
                {
                    winLose = "YOU LOSE!!!";
                    gameOver.setGameInfo(winLose);
                    gameWon = true;
                }

                //  if (kstate.IsKeyDown(Keys.Space)) { spaceKeyDown = true; }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);

            foreach (GameObject g in gameObjectList)
            {
                g.draw(spriteBatch);
            }

            foreach (GameObject g in gameObjectList2)
            {
                g.draw(spriteBatch);
            }
            playerObject.draw(spriteBatch);

            gameInfoObject.draw(spriteBatch);
            score.draw(spriteBatch);

            if (gameWon)
            {
               // spriteBatch.Begin();
                health = 0;
                score.draw(spriteBatch);
                gameOver.draw(spriteBatch);
               // spriteBatch.End();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}