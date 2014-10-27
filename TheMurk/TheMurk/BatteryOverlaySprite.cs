using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    class BatteryOverlaySprite : Sprite
    {
         public BatteryOverlaySprite(Texture2D image, Vector2 position, GameState state)
             : base(new SpriteSheet(image, new  Point(0, 4), 0.15f), position,
            new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {

            Point frameSize = new Point(200, 445);
            
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(1, 0), new Point(1, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(2, 0), new Point(2, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(3, 0), new Point(3, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }

         public override void Update(GameTime gameTime, Rectangle clientBounds)
         {
             if (state.getGameTime() < state.getLosingTime())
             {
                 spriteSheet.setCurrentSegment(3);
             }
             if (state.getGameTime() < ((state.getLosingTime() / 4) * 3))
             {
                 spriteSheet.setCurrentSegment(2);
             }
             

             if (state.getGameTime() < ((state.getLosingTime() / 4) * 2))
             {
                 spriteSheet.setCurrentSegment(1);
             }
             if (state.getGameTime() < state.getLosingTime() / 4)
             {
                 spriteSheet.setCurrentSegment(0);
             }

             
             currentFrame = spriteSheet.currentSegment.startFrame;
         }

         public override Vector2 direction
         {
             get
             {
                 return new Vector2(0, 0);
             }
         }
    }
}
