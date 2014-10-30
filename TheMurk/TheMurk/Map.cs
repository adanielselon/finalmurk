using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheMurk
{
    class Map
    {
        public List<StationarySprite> sprites = new List<StationarySprite>();

        public Map()
        {
        }

        public void add(StationarySprite sprite)
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
            int howMany = 0;

            //player on the left
            if (player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) - player.collisionOffset.east <= sprite.position.X + (SpriteManager.speed.X + 6) + sprite.collisionOffset.west && player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) - player.collisionOffset.east > sprite.position.X + sprite.collisionOffset.west)
            {
                float difference = (player.position.X + (player.spriteSheet.currentSegment.frameSize.X * player.spriteSheet.scale) - player.collisionOffset.east) - (sprite.position.X + sprite.collisionOffset.west);
                foreach (Sprite spr in sprites)
                {
                    spr.position.X += difference;
                }
                howMany++;
            }
            if (player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) - player.collisionOffset.south <= sprite.position.Y + (SpriteManager.speed.Y + 6) + sprite.collisionOffset.north && player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) - player.collisionOffset.south > sprite.position.Y + sprite.collisionOffset.north)
            {
                float difference = (player.position.Y + (player.spriteSheet.currentSegment.frameSize.Y * player.spriteSheet.scale) - player.collisionOffset.south) - (sprite.position.Y + sprite.collisionOffset.north);       
                foreach (Sprite spr in sprites)
                {
                    spr.position.Y += difference;
                }
                howMany++;
            }
            if (player.position.X + player.collisionOffset.west < sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - sprite.collisionOffset.east && player.position.X + player.collisionOffset.west >= sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - (SpriteManager.speed.X + 6) - sprite.collisionOffset.east)
            {
                float difference = (sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - sprite.collisionOffset.east) - (player.position.X + player.collisionOffset.west);
                foreach (Sprite spr in sprites)
                {
                    spr.position.X -= difference;
                }
                howMany++;
            }
            if (player.position.Y + player.collisionOffset.north < sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - sprite.collisionOffset.south && player.position.Y + player.collisionOffset.north >= sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - (SpriteManager.speed.Y + 6) - sprite.collisionOffset.south)
            {
                float difference = (sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - sprite.collisionOffset.south) - (player.position.Y + player.collisionOffset.north);
                foreach (Sprite spr in sprites)
                {
                    spr.position.Y -= difference;
                }
                howMany++;
            }
            
            if (howMany > 1)
            {
                foreach (Sprite spr in sprites)
                {
                    spr.position.X += 1;
                }
            }
        }

    }
}
