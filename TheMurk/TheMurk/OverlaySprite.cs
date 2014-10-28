using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class OverlaySprite : Sprite
    {

        public OverlaySprite(Texture2D image, Vector2 position, GameState state)
             : base(new SpriteSheet(image, new  Point(0, 0), 1), position,
            new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {

            Point frameSize = new Point(3072, 2340);
            
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }

         public override void Update(GameTime gameTime, Rectangle clientBounds)
         {
             spriteSheet.scale = (float) (((1 - (float) state.getGameTime() / (float) state.getLosingTime()) * 2) + 0.7);

             MouseState mouseState = Mouse.GetState();
             
             position.X = mouseState.X - (1536 * spriteSheet.scale);
             position.Y = mouseState.Y - (1170 * spriteSheet.scale);            
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

