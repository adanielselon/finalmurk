using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheMurk
{
    class LostPlayer : UserControlledSprite
    {

        public LostPlayer(Texture2D image, Vector2 position, GameState state)
             : base(new SpriteSheet(image, new  Point(0, 8), 0.15f), position,
            new CollisionOffset(20, 20, 20, 20), SpriteManager.speed, state)
        {

            Point frameSize = new Point(438, 444);
            
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(1, 0), new Point(1, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(2, 0), new Point(2, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(3, 0), new Point(3, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(4, 0), new Point(4, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(5, 0), new Point(5, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(6, 0), new Point(6, 0), 50);
            spriteSheet.addSegment(new Point(436, 444), new Point(7, 0), new Point(7, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (direction.X == 0 && direction.Y < 0)           
            {
                spriteSheet.setCurrentSegment(0);
                //collisionOffset = new CollisionOffset(20, 20, 0, 0);
            }
            if (direction.X > 0 && direction.Y < 0)
            {
                spriteSheet.setCurrentSegment(1);
                //collisionOffset = new CollisionOffset(20, 20, 20, 20);
            }
            if (direction.X > 0 && direction.Y == 0)
            {
                spriteSheet.setCurrentSegment(2);
                //collisionOffset = new CollisionOffset(0, 0, 20, 20);
            }
            if (direction.X > 0 && direction.Y > 0)
            {
                spriteSheet.setCurrentSegment(3);
                //collisionOffset = new CollisionOffset(20, 20, 20, 20);
            }
            if (direction.X == 0 && direction.Y > 0)
            {
                spriteSheet.setCurrentSegment(4);
                //collisionOffset = new CollisionOffset(20, 20, 0, 0);
            }
            if (direction.X < 0 && direction.Y > 0)
            {
                spriteSheet.setCurrentSegment(5);
                //collisionOffset = new CollisionOffset(20, 20, 20, 20);
            }
            if (direction.X < 0 && direction.Y == 0)
            {
                spriteSheet.setCurrentSegment(6);
                //collisionOffset = new CollisionOffset(0, 0, 20, 20);
            }
            if (direction.X < 0 && direction.Y < 0)
            {
                spriteSheet.setCurrentSegment(7);
                //collisionOffset = new CollisionOffset(20, 20, 20, 20);
            }


            base.Update(gameTime, clientBounds);
        }
    }
}
