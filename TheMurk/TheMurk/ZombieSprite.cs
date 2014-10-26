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

        protected bool chasing;

        public ZombieSprite(Texture2D image, Vector2 position, GameState state)
            : base(new SpriteSheet(image, new Point(0, 0), 1f), position, new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            Point frameSize = new Point(40, 42);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;

            chasing = false;
        }

        public void AI(Sprite player, Rectangle clientBounds)
        {
            if (position.X + spriteSheet.currentSegment.frameSize.X > 0 && position.Y + spriteSheet.currentSegment.frameSize.Y > 0 && position.X < clientBounds.Width && position.Y < clientBounds.Height)
            {
                if (player.position.X > position.X)
                    position.X += 1f;
                else
                    position.X -= 1f;
                if (player.position.Y > position.Y)
                    position.Y += 1f;
                else
                    position.Y -= 1f;
            }
        }

        public void collision(Sprite sprite)
        {
            if (position.X + spriteSheet.currentSegment.frameSize.X <= sprite.position.X + SpriteManager.speed.X && position.X + spriteSheet.currentSegment.frameSize.X > sprite.position.X)
            {
                position.X = sprite.position.X - spriteSheet.currentSegment.frameSize.X;
            }
            if (position.Y + spriteSheet.currentSegment.frameSize.Y <= sprite.position.Y + SpriteManager.speed.Y && position.Y + spriteSheet.currentSegment.frameSize.Y > sprite.position.Y)
            {
                position.Y = sprite.position.Y - spriteSheet.currentSegment.frameSize.Y;
            }
            if (position.X < sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X && position.X >= sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X - SpriteManager.speed.X)
            {
                position.X = sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X;
            }
            if (position.Y < sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y && position.Y >= sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y - SpriteManager.speed.Y)
            {
                position.Y = sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y;
            }
        }
    }
}
