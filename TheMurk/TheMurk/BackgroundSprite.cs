using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    class BackgroundSprite : MapBoundSprite
    {
        public BackgroundSprite(Texture2D image, Vector2 position, GameState state)
             : base(new SpriteSheet(image, new  Point(0, 0), 1f), position, 
            new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            Point frameSize = new Point(512, 512);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }
    }
}
