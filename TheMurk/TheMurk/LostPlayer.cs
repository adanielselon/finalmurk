using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    class LostPlayer : UserControlledSprite
    {
        public LostPlayer(Texture2D image, Vector2 position, GameState state)
             : base(new SpriteSheet(image, new  Point(0, 0)), position, 
            new CollisionOffset(0, 0, 0, 0), new Vector2(1, 1), state)
        {
            Point frameSize = new Point(40, 42);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }
    }
}
