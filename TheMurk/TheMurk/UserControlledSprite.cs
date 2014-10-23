using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class UserControlledSprite : Sprite
    {

        protected Vector2 lastPosition;

        public UserControlledSprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset, Vector2 speed, GameState state)
            : base(spriteSheet, position, collisionOffset, speed, state)
        {
            lastPosition = position;
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                /* keyboard input */
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    inputDirection.Y += 1;

                /* gamepad input */
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Vector2 mod, bool isInRange)
        {
            // (state.getState(GameState.WEST) || state.getState(GameState.EAST) || state.getState(GameState.NORTH) || state.getState(GameState.SOUTH))
            if (state.isCollision() && ((state.getCollisionLeftRight() && (state.getState(GameState.WEST) || state.getState(GameState.EAST))) || (state.getCollisionTopBottom() && (state.getState(GameState.NORTH) || state.getState(GameState.SOUTH)))))
            {
                
                if (state.getCollisionLeftRight() && (state.getState(GameState.WEST) || state.getState(GameState.EAST)))
                {
                    position.X = lastPosition.X;
                    if (state.getState(GameState.NORTH))
                    {
                        position.Y += direction.Y * mod.Y;
                        if (position.Y < 0)
                        {
                            position.Y = 0;
                        }
                        if (position.Y >= clientBounds.Height / 2 - SpriteManager.playerSize.Y / 2)
                        {
                            state.setState(GameState.NORTH, false);
                        }
                    }
                    if (state.getState(GameState.SOUTH))
                    {
                        position.Y += direction.Y * mod.Y;
                        if (position.Y > clientBounds.Height - SpriteManager.playerSize.Y)
                        {
                            position.Y = clientBounds.Height - SpriteManager.playerSize.Y;
                        }
                        if (position.Y <= clientBounds.Height / 2 - SpriteManager.playerSize.Y / 2)
                        {
                            state.setState(GameState.SOUTH, false);
                        }
                    }
                }
                if (state.getCollisionTopBottom() && (state.getState(GameState.NORTH) || state.getState(GameState.SOUTH)))
                {
                    position.Y = lastPosition.Y;
                    if (state.getState(GameState.WEST))
                    {
                        position.X += direction.X * mod.X;
                        if (position.X < 0)
                        {
                            position.X = 0;
                        }
                        if (position.X >= clientBounds.Width / 2 - SpriteManager.playerSize.X / 2)
                        {
                            state.setState(GameState.WEST, false);
                        }
                    }
                    if (state.getState(GameState.EAST))
                    {
                        position.X += direction.X * mod.X;
                        if (position.X > clientBounds.Width - SpriteManager.playerSize.X)
                        {
                            position.X = clientBounds.Width - SpriteManager.playerSize.X;
                        }
                        if (position.X <= clientBounds.Width / 2 - SpriteManager.playerSize.X / 2)
                        {
                            state.setState(GameState.EAST, false);
                        }
                    }
                }
            }
            else
            {
                lastPosition = position;
                if (state.getState(GameState.WEST))
                {
                    position.X += direction.X * mod.X;
                    if (position.X < 0)
                    {
                        position.X = 0;
                    }
                    if (position.X >= clientBounds.Width / 2 - SpriteManager.playerSize.X / 2)
                    {
                        state.setState(GameState.WEST, false);
                    }
                }
                if (state.getState(GameState.EAST))
                {
                    position.X += direction.X * mod.X;
                    if (position.X > clientBounds.Width - SpriteManager.playerSize.X)
                    {
                        position.X = clientBounds.Width - SpriteManager.playerSize.X;
                    }
                    if (position.X <= clientBounds.Width / 2 - SpriteManager.playerSize.X / 2)
                    {
                        state.setState(GameState.EAST, false);
                    }
                }
                if (state.getState(GameState.NORTH))
                {
                    position.Y += direction.Y * mod.Y;
                    if (position.Y < 0)
                    {
                        position.Y = 0;
                    }
                    if (position.Y >= clientBounds.Height / 2 - SpriteManager.playerSize.Y / 2)
                    {
                        state.setState(GameState.NORTH, false);
                    }
                }
                if (state.getState(GameState.SOUTH))
                {
                    position.Y += direction.Y * mod.Y;
                    if (position.Y > clientBounds.Height - SpriteManager.playerSize.Y)
                    {
                        position.Y = clientBounds.Height - SpriteManager.playerSize.Y;
                    }
                    if (position.Y <= clientBounds.Height / 2 - SpriteManager.playerSize.Y / 2)
                    {
                        state.setState(GameState.SOUTH, false);
                    }
                }
            }

            //position += direction;

            // If sprite is off the screen, move it back within the game window
            //TODO If sprite is on edge of screen stop it from moving
            /*
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - spriteSheet.currentSegment.frameSize.X)
                position.X = clientBounds.Width - spriteSheet.currentSegment.frameSize.X;
            if (position.Y > clientBounds.Height - spriteSheet.currentSegment.frameSize.Y)
                position.Y = clientBounds.Height - spriteSheet.currentSegment.frameSize.Y;
            */
            base.Update(gameTime, clientBounds, mod, isInRange);
        }
    }
}
