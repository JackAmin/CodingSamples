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

namespace CS2053Lab6
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector3 cameraPosition = new Vector3(0.0f, 00.0f, 1000.0f);
        float aspectRatio;

        GameObject[] gameObjects = new GameObject[4];
        GameControl gameControl;

        float lastUpdateTime = 0.0f;
        float timeStepInterval = 10.0f; //100 milliseconds

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

            Model model = Content.Load<Model>("Model\\soccer");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            Vector3 modelPosition = new Vector3(-100, 0, 0);
            Vector3 modelVelocity = new Vector3(-5f, 0.0f, 0.0f);
            gameObjects[0] = new GameObject(model, modelPosition, modelVelocity, cameraPosition, aspectRatio);

            modelPosition = new Vector3(100, 0, 0);
            modelVelocity = new Vector3(5f, 0, 0);
            gameObjects[1] = new GameObject(model, modelPosition, modelVelocity, cameraPosition, aspectRatio);

            modelPosition = new Vector3(-600, 0, 0);
            gameObjects[2] = new GameObject(model, modelPosition, Vector3.Zero, cameraPosition, aspectRatio);

            modelPosition = new Vector3(600, 0, 0);
            gameObjects[3] = new GameObject(model, modelPosition, Vector3.Zero, cameraPosition, aspectRatio);

            gameControl = new GameControl(gameObjects);

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

          // float timeStep;
           float currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
           if (currentTime - lastUpdateTime < timeStepInterval) return;
           lastUpdateTime = currentTime;

            for (int i = 0; i < 4; i++)
            {
                gameObjects[i].update();
            }
            gameControl.collisionControl();

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

            for (int i = 0; i < 4; i++)
            {
                gameObjects[i].draw();
            }

            base.Draw(gameTime);
        }
    }
}
