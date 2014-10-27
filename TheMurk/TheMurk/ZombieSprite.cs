using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class ZombieSprite : StationarySprite
    {

        protected Vector2 previousPosition;
        protected bool chasing;

        public ZombieSprite(Texture2D image, Vector2 position, GameState state)
            : base(new SpriteSheet(image, new Point(0, 8), .3f), position, new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            Point frameSize = new Point(208, 202);

            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(1, 0), new Point(1, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(2, 0), new Point(2, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(3, 0), new Point(3, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(4, 0), new Point(4, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(5, 0), new Point(5, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(6, 0), new Point(6, 0), 50);
            spriteSheet.addSegment(new Point(210, 202), new Point(7, 0), new Point(7, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;

            chasing = false;
        }

        public void AI(Sprite player, Rectangle clientBounds)
        {
            previousPosition = position;
            
            if (position.X + spriteSheet.currentSegment.frameSize.X > 0 && position.Y + spriteSheet.currentSegment.frameSize.Y > 0 && position.X < clientBounds.Width && position.Y < clientBounds.Height)
            {
                if (player.position.X > position.X)
                {
                    position.X += 1f;           
                }
                else
                {
                    position.X -= 1f;
                }
                if (player.position.Y > position.Y)
                {
                    position.Y += 1f;
                }
                else
                {
                    position.Y -= 1f;
                }
            }
            
            if (previousPosition.X - position.X == 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(4);
                currentFrame = new Point(4, 0);
            }
            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(0);
                currentFrame = new Point(0, 0);
            }
            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y == 0)
            {
                spriteSheet.setCurrentSegment(2);
            }
            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y > 0)
            {
                spriteSheet.setCurrentSegment(3);
            }
            if (previousPosition.X - position.X == 0 && previousPosition.Y - position.Y > 0)
            {
                spriteSheet.setCurrentSegment(4);
            }
            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y > 0)
            {
                spriteSheet.setCurrentSegment(5);
            }
            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y == 0)
            {
                spriteSheet.setCurrentSegment(6);
            }
            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(7);
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
            if (position.X < sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) && position.X >= sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - SpriteManager.speed.X)
            {
                position.X = sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale;
            }
            if (position.Y < sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) && position.Y >= sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - SpriteManager.speed.Y)
            {
                position.Y = sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale;
            }
        }

    }
}
