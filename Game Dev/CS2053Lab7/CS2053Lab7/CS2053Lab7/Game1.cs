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

namespace CS2053Lab7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlatformObject platform;

        Vector2 screenSize;
        GroundObject ground;
        GameController controller;

        BallObject ball;

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

            screenSize = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            ground = new GroundObject();
            
            platform = new PlatformObject();

            ball = new BallObject();

            controller = new GameController(screenSize, ground, platform, ball);

           

            controller.initialize();

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

            ground.setTexture(this.Content.Load<Texture2D>("background2"));

            ball.setTexture(this.Content.Load<Texture2D>("redball"));

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            texture.SetData(new[] { Color.White });

            platform.setTexture(texture);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            float timeInterval = 100f; //millisecond

            float lastTime = 0;

            float currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            if (currentTime - lastTime < timeInterval) return;

            lastTime = currentTime;

            controller.control(timeInterval);

            ball.update();

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
            platform.draw(spriteBatch);
            ground.draw(spriteBatch);
            agent.draw(spriteBatch);
            ball.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
