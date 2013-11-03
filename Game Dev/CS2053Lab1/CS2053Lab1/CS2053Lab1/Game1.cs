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

namespace CS2053Lab1
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

        Texture2D gameObjectTexture;
        int gameObjectWidth = 50;
        int gameObjectHeight = 50;
        Vector2 gameObjectLocation;
        Rectangle gameObjectRectangle;

        Vector2 gameObjectVelocity = new Vector2(1f, 1f);

        Texture2D playerObjectTexture;
        int playerObjectWidth = 50;
        int playerObjectHeight = 50;
        Vector2 playerObjectLocation;
        Rectangle playerObjectRectangle;

        Vector2 playerObjectVelocity = new Vector2(5f, 5f);

        SpriteFont GameInfoFont;
        Vector2 gameInfoLocation;
        String gameInfo;
        double gameHour, gameMinute, gameSecond;

        SpriteFont GameTimerFont;
        Vector2 gameTimerLocation;
        String gameTimer;
        double second;
        double timer;

        SpriteFont GameScoreFont;
        Vector2 gameScoreLocation;
        String gameScore;
        int score = 0;

        SpriteFont WinLoseFont;
        Vector2 WinLoseLocation;
        String winLose;

        Boolean pressed = false;
        Boolean gameWon = false;


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
            backgroundTexture = this.Content.Load<Texture2D>("background4");
            backgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            gameObjectTexture = this.Content.Load<Texture2D>("redball");
            gameObjectLocation = new Vector2(screenWidth / 2 , screenHeight / 2);
            gameObjectRectangle = new Rectangle(0, 0, gameObjectWidth, gameObjectHeight);

            playerObjectTexture = this.Content.Load<Texture2D>("whiteball");
            playerObjectLocation = new Vector2(0, screenHeight - playerObjectHeight);
            playerObjectRectangle = new Rectangle(0, screenHeight - playerObjectHeight, gameObjectWidth, gameObjectHeight);

            GameInfoFont = this.Content.Load<SpriteFont>("SpriteFont1");
            gameInfoLocation = new Vector2(0, 0);

            GameScoreFont = this.Content.Load<SpriteFont>("SpriteFont2");
            gameScoreLocation = new Vector2(screenWidth - 100, 0);

            GameTimerFont = this.Content.Load<SpriteFont>("SpriteFont3");
            gameTimerLocation = new Vector2(0, 25);

            WinLoseFont = this.Content.Load<SpriteFont>("SpriteFont4");
            WinLoseLocation = new Vector2(screenWidth / 2 - 50, screenHeight / 2);

            // TODO: use this.Content to load your game content here
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (!gameWon)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // TODO: Add your update logic here


                gameHour = gameTime.TotalGameTime.Hours;
                gameMinute = gameTime.TotalGameTime.Minutes;
                gameSecond = gameTime.TotalGameTime.Seconds;
                gameInfo = "Game Time: " + gameHour + ":" + gameMinute + ":" + gameSecond;

                second = gameTime.TotalGameTime.Seconds - timer;
                gameTimer = "Need to score in: " + (10 - second);

                gameScore = "Score: " + score;


                gameObjectLocation = gameObjectLocation + gameObjectVelocity;
                if (gameObjectLocation.X < 0 || gameObjectLocation.X > screenWidth - gameObjectWidth)
                    gameObjectVelocity.X = -gameObjectVelocity.X;
                if (gameObjectLocation.Y < 0 || gameObjectLocation.Y > screenHeight - gameObjectHeight)
                    gameObjectVelocity.Y = -gameObjectVelocity.Y;
                gameObjectRectangle = new Rectangle((int)gameObjectLocation.X, (int)gameObjectLocation.Y, gameObjectWidth, gameObjectHeight);

                KeyboardState kstate = Keyboard.GetState();
                if (kstate.IsKeyDown(Keys.Left))
                {
                    if (playerObjectLocation.X > 0)
                        playerObjectLocation.X = playerObjectLocation.X - (int)playerObjectVelocity.X;
                }
                if (kstate.IsKeyDown(Keys.Right))
                {
                    if (playerObjectLocation.X < screenWidth - playerObjectWidth)
                        playerObjectLocation.X = playerObjectLocation.X + (int)playerObjectVelocity.X;
                }
                if (kstate.IsKeyDown(Keys.Up))
                {
                    if (playerObjectLocation.Y > 0)
                        playerObjectLocation.Y = playerObjectLocation.Y - (int)playerObjectVelocity.Y;
                }
                if (kstate.IsKeyDown(Keys.Down))
                {
                    if (playerObjectLocation.Y < screenHeight - playerObjectHeight)
                        playerObjectLocation.Y = playerObjectLocation.Y + (int)playerObjectVelocity.Y;
                }
                if (kstate.IsKeyDown(Keys.Space) && pressed == false)
                {
                    pressed = true;
                    if (Math.Abs(Math.Sqrt(Math.Pow((playerObjectLocation.X - gameObjectLocation.X), 2) + Math.Pow((playerObjectLocation.Y - gameObjectLocation.Y), 2))) < 50)
                    {
                        score++;
                        timer = gameTime.TotalGameTime.Seconds;
                    }
                    else
                    {
                        score--;
                    }
                }
                if ((10 - second) == 0)
                {
                    timer = gameTime.TotalGameTime.Seconds;
                    score--;
                }
                if (kstate.IsKeyUp(Keys.Space) && pressed == true)
                    pressed = false;

                playerObjectRectangle = new Rectangle((int)playerObjectLocation.X, (int)playerObjectLocation.Y, playerObjectWidth, playerObjectHeight);
                
                base.Update(gameTime);

                if (score == 10)
                {
                    winLose = "YOU WIN!!!";
                    gameWon = true;
                }
                if (score == -10)
                {
                    winLose= "YOU LOSE!!!";
                    gameWon = true;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(gameObjectTexture, gameObjectRectangle, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(gameObjectTexture, gameObjectRectangle, Color.White);
            spriteBatch.Draw(playerObjectTexture, playerObjectRectangle, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(gameObjectTexture, gameObjectRectangle, Color.White);
            spriteBatch.Draw(playerObjectTexture, playerObjectRectangle, Color.White);
            spriteBatch.DrawString(GameInfoFont, gameInfo, gameInfoLocation, Color.Orange);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(GameScoreFont, gameScore, gameScoreLocation, Color.Orange);
            spriteBatch.End();


            spriteBatch.Begin();
            spriteBatch.DrawString(GameTimerFont, gameTimer, gameTimerLocation, Color.Orange);
            spriteBatch.End();


            if (gameWon)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(WinLoseFont, winLose, WinLoseLocation, Color.Orange);
                spriteBatch.End();
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
