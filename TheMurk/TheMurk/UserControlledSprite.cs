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

        public void collision(Sprite sprite)
        {
            if (position.X + (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) <= sprite.position.X + SpriteManager.speed.X && position.X + (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) > sprite.position.X)
            {
                position.X = sprite.position.X - spriteSheet.currentSegment.frameSize.X * spriteSheet.scale;
            }
            if (position.Y + (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) <= sprite.position.Y + SpriteManager.speed.Y && position.Y + (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) > sprite.position.Y)
            {
                position.Y = sprite.position.Y - spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale;
            }
            if (position.X < sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) && position.X >= sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale )- SpriteManager.speed.X)
            {
                position.X = sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale);
            }
            if (position.Y < sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) && position.Y >= sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - SpriteManager.speed.Y)
            {
                position.Y = sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale);
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
                lastPosition = position;
                if (state.getState(GameState.WEST))
                {
                    position.X += direction.X;
                    if (position.X < 0)
                    {
                        position.X = 0;
                    }
                    if (position.X >= clientBounds.Width / 2 - (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) / 2)
                    {
                        state.setState(GameState.WEST, false);
                    }
                }
                if (state.getState(GameState.EAST))
                {
                    position.X += direction.X;
                    if (position.X > clientBounds.Width - (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale))
                    {
                        position.X = clientBounds.Width - (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale);
                    }
                    if (position.X <= clientBounds.Width / 2 - (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) / 2)
                    {
                        state.setState(GameState.EAST, false);
                    }
                }
                if (state.getState(GameState.NORTH))
                {
                    position.Y += direction.Y;
                    if (position.Y < 0)
                    {
                        position.Y = 0;
                    }
                    if (position.Y >= clientBounds.Height / 2 - (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) / 2)
                    {
                        state.setState(GameState.NORTH, false);
                    }
                }
                if (state.getState(GameState.SOUTH))
                {
                    position.Y += direction.Y;
                    if (position.Y > clientBounds.Height - (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale))
                    {
                        position.Y = clientBounds.Height - (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale);
                    }
                    if (position.Y <= clientBounds.Height / 2 - (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) / 2)
                    {
                        state.setState(GameState.SOUTH, false);
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
            base.Update(gameTime, clientBounds);
        }
    }
}
