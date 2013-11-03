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

namespace CS2053Lab3
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

        PlayerObject playerObject;
        List<GameObject> gameObjectList = new List<GameObject>();

        GameInfoObject gameInfoObject;

        // Health
        GameInfoObject score;
        int health;

        // Win Lose
        GameInfoObject gameOver;

        Boolean gameWon = false;
        String winLose;


        GameController gameController;

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

            playerObject = new PlayerObject(screenWidth, screenHeight + screenHeight);
            gameObjectList = new List<GameObject>();
            gameInfoObject = new GameInfoObject(screenWidth, screenHeight, 0, screenHeight / 10);

            score = new GameInfoObject(screenWidth, screenHeight, screenWidth - 170, screenHeight / 10);
            gameOver = new GameInfoObject(screenWidth, screenHeight / 10, screenWidth / 2, screenHeight / 2);

            gameController = new GameController(screenWidth, screenHeight, playerObject, gameObjectList, gameInfoObject, score);

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

            backgroundTexture = this.Content.Load<Texture2D>("background");
            backgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            gameController.setTexture(this.Content.Load<Texture2D>("redball"), this.Content.Load<Texture2D>("egg"));
           // gameController.setBombTexture(this.Content.Load<Texture2D>("redball"));

            playerObject.setTexture(this.Content.Load<Texture2D>("chomp"));
            playerObject.setVelocity(new Vector2(3f, 3f));

            gameInfoObject.setFont(this.Content.Load<SpriteFont>("SpriteFont1"));
          //  gameInfoObject.setLocation(new Vector2(0, screenHeight - 50));

            playerObject.setBulletTexture(this.Content.Load<Texture2D>("chomp"));

             // Score
            score.setFont(this.Content.Load<SpriteFont>("SpriteFont2"));
          //  score.setLocation(new Vector2(screenWidth - 100, screenHeight - 50));
           // health = 100;
   

            // Game over 
            gameOver.setFont(this.Content.Load<SpriteFont>("SpriteFont3"));
            gameOver.setLocation(new Vector2(screenWidth / 2 - 50, screenHeight / 2));

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
        /// 

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (!gameWon)
            {

                playerObject.update();

                foreach (GameObject g in gameObjectList) { g.update(); }

                gameController.control();
            }

            if (gameObjectList.Count == 0)
            {
                winLose = "YOU WIN!!!";
                gameOver.setGameInfo(winLose);
                gameWon = true;
            }
            if (playerObject.getHealth() < 0)
            {
                winLose = "YOU LOSE!!!";
                gameOver.setGameInfo(winLose);
                gameWon = true;
            }
           

            base.Update(gameTime);
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
            playerObject.draw(spriteBatch);
            gameInfoObject.draw(spriteBatch);
            score.draw(spriteBatch);
            if (gameWon)
            {
                // spriteBatch.Begin();
                playerObject.setHealth(0);
                score.draw(spriteBatch);
                gameOver.draw(spriteBatch);
                // spriteBatch.End();
            }

            foreach (GameObject g in gameObjectList)
            {
                g.draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
