using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class StationarySprite : Sprite
    {
        protected Vector2 startingPosition;
        protected Vector2 lastPosition;


        public StationarySprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset, Vector2 speed, GameState state)
            : base(spriteSheet, position, collisionOffset, speed, state)
        {
            startingPosition = position;
            lastPosition = position;
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                /* keyboard input */
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    inputDirection.Y += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    inputDirection.Y -= 1;



                /* gamepad input */
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X -= gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y += gamepadState.ThumbSticks.Left.Y;

                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, Vector2 mod, bool isInRange)
        {

            if (state.isCollision() && isInRange)
            {
                if (state.getCollisionLeftRight() == true)
                {
                    position.X = lastPosition.X;
                    if (!state.getState(GameState.NORTH) && !state.getState(GameState.SOUTH))
                    {
                        position.Y += direction.Y;
                    }
                    
                }
                if (state.getCollisionTopBottom() == true)
                {
                    position.Y = lastPosition.Y;
                    if (!state.getState(GameState.WEST) && !state.getState(GameState.EAST))
                    {
                        position.X += direction.X;
                    }
                    
                }
            }
            else
            {
                lastPosition = position;

                if (position.X > startingPosition.X + ((SpriteManager.frameSize.X - (clientBounds.Width / 2 - SpriteManager.frameSize.X / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.X)))
                {
                    position.X = startingPosition.X + ((SpriteManager.frameSize.X - (clientBounds.Width / 2 - SpriteManager.frameSize.X / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.X));
                    state.setState(GameState.WEST, true);
                }
                if (position.X < startingPosition.X - ((SpriteManager.frameSize.X - (clientBounds.Width / 2 - SpriteManager.frameSize.X / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.X)))
                {
                    position.X = startingPosition.X - ((SpriteManager.frameSize.X - (clientBounds.Width / 2 - SpriteManager.frameSize.X / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.X));
                    state.setState(GameState.EAST, true);
                }
                if (position.Y > startingPosition.Y + ((SpriteManager.frameSize.Y - (clientBounds.Height / 2 - SpriteManager.frameSize.Y / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.Y)))
                {
                    position.Y = startingPosition.Y + ((SpriteManager.frameSize.Y - (clientBounds.Height / 2 - SpriteManager.frameSize.Y / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.Y));
                    state.setState(GameState.NORTH, true);
                }
                if (position.Y < startingPosition.Y - ((SpriteManager.frameSize.Y - (clientBounds.Height / 2 - SpriteManager.frameSize.Y / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.Y)))
                {
                    position.Y = startingPosition.Y - ((SpriteManager.frameSize.Y - (clientBounds.Height / 2 - SpriteManager.frameSize.Y / 2)) + (((SpriteManager.mapSize / 2) - 1) * SpriteManager.frameSize.Y));
                    state.setState(GameState.SOUTH, true);
                }

                if (!state.getState(GameState.WEST) && !state.getState(GameState.EAST))
                {
                    position.X += direction.X * mod.X;
                }

                if (!state.getState(GameState.NORTH) && !state.getState(GameState.SOUTH))
                {
                    position.Y += direction.Y * mod.Y;
                }
            }
            base.Update(gameTime, clientBounds, mod, isInRange);
        }
    }
}
