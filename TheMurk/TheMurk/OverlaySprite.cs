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

            Point frameSize = new Point(2157, 1172);
            
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }

         public override void Update(GameTime gameTime, Rectangle clientBounds)
         {

             if (state.getGameTime() < state.getLosingTime())
             {
                 spriteSheet.scale = .25f;
             }
             if (state.getGameTime() < ((state.getLosingTime() / 4) * 3))
             {
                 spriteSheet.scale = .5f;
             }
             if (state.getGameTime() < ((state.getLosingTime() / 4) * 2))
             {
                 spriteSheet.scale = .75f;
             }
             if (state.getGameTime() < state.getLosingTime() / 4)
             {
                 spriteSheet.scale = 1f;
             }
             MouseState mouseState = Mouse.GetState();
             
             position.X = mouseState.X - (982 * spriteSheet.scale);
             position.Y = mouseState.Y - (531 * spriteSheet.scale);

             
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

