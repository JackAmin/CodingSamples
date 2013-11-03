using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS2053Lab7
{
    class GameController
    {
        Vector2 screenSize;
        GroundObject ground;
        BallObject ball;

        PlatformObject platform;

        
        float x = 60f, y = 60f;
        float platformAngle = 1.0f;
        int sY = 480;
        int sX = 800;
        float ballVelocityX = 2 * (float)Math.Cos(1.0f);

        float ballVelocityY = 2 * (float)Math.Sin(1.0f);

        public GameController(Vector2 s, GroundObject g, PlatformObject p, BallObject b) { screenSize = s; ground = g; platform = p; ball = b; }

        Vector2 platformPosition = new Vector2(200.0f, 480 / 2);

        public void initialize()
        {

            Vector2 platformPosition = new Vector2(200.0f, screenSize.Y / 2);

            Vector2 platformSize = new Vector2(screenSize.X / 4, 20.0f);

            platform.setSize(platformSize);

            float platformAngle = 1.0f;


            platform.setPosition(platformPosition);
            platform.setAngle(platformAngle);

            int groundHeight = 40;

            ground.setPosition(new Vector2(0, screenSize.Y - groundHeight));

            ground.setSize(new Vector2((int)screenSize.X, groundHeight));
            
            Vector2 ballSize = new Vector2(60, 60);

            Vector2 ballPosition = new Vector2(platformPosition.X + ballSize.X / 2, platformPosition.Y - ballSize.Y / 2);

            ball.setPosition(ballPosition);

            ball.setSize(ballSize);

            float ballVelocityX = 2 * (float)Math.Cos(platformAngle);

            float ballVelocityY = 2 * (float)Math.Sin(platformAngle);

            Vector2 ballVelocity = new Vector2(ballVelocityX, ballVelocityY);

            ball.setVelocity(ballVelocity);  
    
                   
        }

        public void initializeSpace()
        {

            Vector2 ballSize = new Vector2(x, y);

            Vector2 ballPosition = new Vector2(platformPosition.X + ballSize.X / 2, platformPosition.Y - ballSize.Y / 2);

            ball.setPosition(ballPosition);

            ball.setSize(ballSize);



            Vector2 ballVelocity = new Vector2(ballVelocityX, ballVelocityY);

            ball.setVelocity(ballVelocity);

        }


        public void control(float timeInterval)
        {
            KeyboardState kstate = Keyboard.GetState();

            if((kstate.IsKeyDown(Keys.Left) && platform.getPosition().X > 0))   // Move Platform to the left
            {
                Vector2 newPosX = new Vector2(platform.getPosition().X - 10, platform.getPosition().Y);
                platform.setPosition(newPosX);
                platformPosition.X = newPosX.X;
            }

            if ((kstate.IsKeyDown(Keys.Right) && platform.getPosition().X <= screenSize.X)) // Move Platform to the right
            {
                Vector2 newPosX = new Vector2(platform.getPosition().X + 10, platform.getPosition().Y);
                platform.setPosition(newPosX);
                platformPosition.X = newPosX.X;
            }

            if ((kstate.IsKeyDown(Keys.Up) && platform.getPosition().Y > 0))   // Move Platform to the up
            {
                Vector2 newPosY = new Vector2(platform.getPosition().X, platform.getPosition().Y - 10);
                platform.setPosition(newPosY);
                platformPosition.Y = newPosY.Y;
            }

            if ((kstate.IsKeyDown(Keys.Down) && platform.getPosition().Y <= screenSize.Y)) // Move Platform to the down
            {
                Vector2 newPosY = new Vector2(platform.getPosition().X, platform.getPosition().Y + 10);
                platform.setPosition(newPosY);
                platformPosition.Y = newPosY.Y;
            }

            if ((kstate.IsKeyDown(Keys.D)))  // Rotate Platform to the right
            {
                platformAngle += .10f;
                platform.setAngle(platformAngle);
            }

            if ((kstate.IsKeyDown(Keys.A)))  // Rotate Platform to the left
            {
                platformAngle -= .10f;
                platform.setAngle(platformAngle);
            }

            if ((kstate.IsKeyDown(Keys.W)))  // Increase ball size
            {
                x += 10;
                y += 10;
                Vector2 ballSize = new Vector2(x, y);

                ball.setSize(ballSize);
            }

            if ((kstate.IsKeyDown(Keys.S)))  // Decrease ball size
            {
                x -= 10;
                y -= 10;
                Vector2 ballSize = new Vector2(x, y);

                ball.setSize(ballSize);
            }

            if ((kstate.IsKeyDown(Keys.Q)))  // Increase ball speed
            {
               

                ballVelocityX += 10;
                ballVelocityY += 10;

                Vector2 ballVelocity = new Vector2(ballVelocityX, ballVelocityY);

                ball.setVelocity(ballVelocity);
            }

            if ((kstate.IsKeyDown(Keys.E)))  // Decrease ball speed
            {
                ballVelocityX -= 10;
                ballVelocityY -= 10;

                Vector2 ballVelocity = new Vector2(ballVelocityX, ballVelocityY);

                ball.setVelocity(ballVelocity);
            }


            if (kstate.IsKeyDown(Keys.Space))
            {
                initializeSpace();
            }
            else
            {

                if (isBallOutRange()) { ball.setVelocity(Vector2.Zero); return; }

                Vector2 ballVelocity = ball.getVelocity();

                float gravityAcc = 0.00981f;

                if (isBallLeavePlatform()) { ballVelocity.Y += gravityAcc * timeInterval; }

                else
                {

                    float platformAngle = platform.getAngle();

                    ballVelocity.Y += (float)Math.Sin(platformAngle) * gravityAcc * timeInterval;

                    if (platformAngle != 0)

                        ballVelocity.X = ballVelocity.Y / (float)Math.Tan(platformAngle);

                }

                ball.setVelocity(ballVelocity);

                if (isBallHitGround())
                {

                    ballVelocity = ball.getVelocity();

                    ballVelocity.Y = 0f;

                    ball.setVelocity(ballVelocity);

                }
            }

        }

        bool isBallOutRange()
        {

            Vector2 ballPosition = ball.getPosition();

            return ballPosition.X < 0 || ballPosition.X > screenSize.X ||

                   ballPosition.Y < 0 || ballPosition.Y > screenSize.Y;

        }



        bool isBallHitGround()
        {

            Vector2 ballPosition = ball.getPosition();

            Vector2 ballSize = ball.getSize();

            Vector2 groundPosition = ground.getPosition();

            return (ballPosition.Y + ballSize.Y / 2 > groundPosition.Y);

        }



        bool isBallLeavePlatform()
        {

            Vector2 ballPosition = ball.getPosition();

            Vector2 ballSize = ball.getSize();

            Vector2 platformPosition = platform.getPosition();

            Vector2 platformSize = platform.getSize();

            return ballPosition.X + ballSize.X > platformPosition.X + platformSize.X;

        }


    }
}
