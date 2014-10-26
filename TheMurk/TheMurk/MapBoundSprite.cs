using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class MapBoundSprite : StationarySprite
    {
        protected Vector2 startingPosition;

        public MapBoundSprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset, Vector2 speed, GameState state)
            : base(spriteSheet, position, collisionOffset, speed, state)
        {
            startingPosition = position;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

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

            base.Update(gameTime, clientBounds);
        }
    }
}
