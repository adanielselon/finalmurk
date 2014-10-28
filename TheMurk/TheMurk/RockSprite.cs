using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    class RockSprite : MapBoundSprite
    {
        public RockSprite(Texture2D image, Vector2 position, GameState state)
            : base(new SpriteSheet(image, new Point(0, 0), 0.25f), position,
           new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            Point frameSize = new Point(455, 441);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }
    }
}
