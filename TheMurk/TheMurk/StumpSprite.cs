using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    class StumpSprite : MapBoundSprite
    {
        public StumpSprite(Texture2D image, Vector2 position, GameState state)
            : base(new SpriteSheet(image, new Point(0, 0), 0.05f), position,
           new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            Point frameSize = new Point(887, 822);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }
    }
}
