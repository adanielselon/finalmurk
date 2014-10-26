using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheMurk
{
    class Map
    {
        public List<Sprite> sprites = new List<Sprite>();

        public Map()
        {
        }

        public void add(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public void update(GameTime gameTime, Rectangle clientBounds)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Update(gameTime, clientBounds);
            }
        }

        public void collision(LostPlayer player, Sprite sprite)
        {

            player.position = new Vector2(0, 0);
            if (player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) <= sprite.position.X + SpriteManager.speed.X && player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) > sprite.position.X)
            {
                float difference = player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) - sprite.position.X;
                foreach (Sprite spr in sprites)
                {
                    spr.position.X += difference;
                }
            }
            if (player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) <= sprite.position.Y + SpriteManager.speed.Y && player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) > sprite.position.Y)
            {
                float difference = player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) - sprite.position.Y;       
                foreach (Sprite spr in sprites)
                {
                    spr.position.Y += difference;
                }
            }
            if (player.position.X < sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) && player.position.X >= sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - SpriteManager.speed.X)
            {
                float difference = sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - player.position.X;
                foreach (Sprite spr in sprites)
                {
                    spr.position.X -= difference;
                }
            }
            if (player.position.Y < sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) && player.position.Y >= sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - SpriteManager.speed.Y)
            {
                float difference = sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - player.position.Y;
                foreach (Sprite spr in sprites)
                {
                    spr.position.Y -= difference;
                }
            }
        }

    }
}
