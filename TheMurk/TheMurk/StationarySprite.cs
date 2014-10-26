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

        public StationarySprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset, Vector2 speed, GameState state)
            : base(spriteSheet, position, collisionOffset, speed, state)
        {
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

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
             if (!state.getState(GameState.WEST) && !state.getState(GameState.EAST))
                {
                    position.X += direction.X;
                }

                if (!state.getState(GameState.NORTH) && !state.getState(GameState.SOUTH))
                {
                    position.Y += direction.Y;
                }
            
        }
    }
}