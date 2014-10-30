using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TheMurk
{
    class ZombieSprite : StationarySprite
    {

        public SoundBank bank;
        public Cue sound;

        private int isConfused;
        protected Vector2 previousPosition;
        protected bool chasing;

        public ZombieSprite(Texture2D image, Vector2 position, GameState state, SoundBank bank)
            : base(new SpriteSheet(image, new Point(0, 8), .3f), position, new CollisionOffset(5, 10, 15, 5), SpriteManager.speed, state)
        {
            this.bank = bank;
            this.sound = null;
            this.isConfused = 0;

            Point frameSize = new Point(208, 202);

            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(1, 0), new Point(1, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(2, 0), new Point(2, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(3, 0), new Point(3, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(4, 0), new Point(4, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(5, 0), new Point(5, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(6, 0), new Point(6, 0), 50);
            spriteSheet.addSegment(new Point(210, 202), new Point(7, 0), new Point(7, 0), 50);

            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;

            chasing = false;
        }

        public void AI(Sprite player, Rectangle clientBounds)
        {
            previousPosition = position;
            
            if (position.X + spriteSheet.currentSegment.frameSize.X > 0 && position.Y + spriteSheet.currentSegment.frameSize.Y > 0 && position.X < clientBounds.Width && position.Y < clientBounds.Height)
            {
                if (isConfused <= 0)
                {
                    if (Math.Abs(player.position.X - position.X) > 1)
                    {
                        if (player.position.X > position.X)
                        {
                            position.X += 1;
                        }
                        else
                        {
                            position.X -= 1;
                        }
                    }
                    if (Math.Abs(player.position.Y - position.Y) > 1)
                    {
                        if (player.position.Y > position.Y)
                        {
                            position.Y += 1;
                        }
                        else
                        {
                            position.Y -= 1;
                        }
                    }
                }
                else
                {
                    Random random = new Random();
                    if (Math.Abs(player.position.X - position.X) > 1)
                    {
                        int a = random.Next(-5, 5);
                        if (a >= 0)
                        {
                            position.X -= 1;
                        }
                        else
                        {
                            position.X += 1;
                        }
                    }
                    if (Math.Abs(player.position.Y - position.Y) > 1)
                    {
                        int a = random.Next(-5, 5);
                        if (a < 0)
                        {
                            position.Y += 1;
                        }
                        else
                        {
                            position.Y -= 1;
                        }
                    }
                }
            }

            Vector2 difference = new Vector2(previousPosition.X - position.X, previousPosition.Y - position.Y);


            if (previousPosition.X - position.X == 0 && previousPosition.Y - position.Y > 0)
            {
               spriteSheet.setCurrentSegment(4);
            }

            if (previousPosition.X - position.X == 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(6);
            }

            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y == 0)
            {
                spriteSheet.setCurrentSegment(5);
            }

            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y == 0)
            {
                spriteSheet.setCurrentSegment(7);
            }
              
            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y > 0)
            {
                spriteSheet.setCurrentSegment(0);
            }
            if (previousPosition.X - position.X < 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(1);
            }
            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y < 0)
            {
                spriteSheet.setCurrentSegment(2);
            }
            if (previousPosition.X - position.X > 0 && previousPosition.Y - position.Y > 0)
            {
                spriteSheet.setCurrentSegment(3);
            }

            isConfused--;
            if(isConfused <= 0)
                currentFrame = spriteSheet.currentSegment.startFrame;
          
        }

        public void collision(Sprite sprite)
        {
            int howMany = 0;
            int modifier;

            if (sprite is ZombieSprite)
            {
                modifier = 25;
            }
            else
            {
                modifier = 0;
            }

            if (position.X + (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) - collisionOffset.east <= sprite.position.X + (SpriteManager.speed.X + modifier) + sprite.collisionOffset.west && position.X + (spriteSheet.currentSegment.frameSize.X * spriteSheet.scale) - collisionOffset.east > sprite.position.X + sprite.collisionOffset.west)
            {
                position.X = sprite.position.X + sprite.collisionOffset.west - spriteSheet.currentSegment.frameSize.X * spriteSheet.scale + collisionOffset.east;
                howMany++;
            }
            if (position.Y + (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) - collisionOffset.south <= sprite.position.Y + (SpriteManager.speed.Y + modifier) + sprite.collisionOffset.north && position.Y + (spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale) - collisionOffset.south > sprite.position.Y + sprite.collisionOffset.north)
            {
                position.Y = sprite.position.Y + sprite.collisionOffset.north - spriteSheet.currentSegment.frameSize.Y * spriteSheet.scale + collisionOffset.south;
                howMany++;
            }
            if (position.X + collisionOffset.west < sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - sprite.collisionOffset.east && position.X + collisionOffset.west >= sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - (SpriteManager.speed.X + modifier) - sprite.collisionOffset.east)
            {
                position.X = sprite.position.X + (sprite.spriteSheet.currentSegment.frameSize.X * sprite.spriteSheet.scale) - sprite.collisionOffset.east - collisionOffset.west;
                howMany++;
            }
            if (position.Y + collisionOffset.north < sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - sprite.collisionOffset.south && position.Y + collisionOffset.north >= sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - (SpriteManager.speed.Y + modifier) - sprite.collisionOffset.south)
            {
                position.Y = sprite.position.Y + (sprite.spriteSheet.currentSegment.frameSize.Y * sprite.spriteSheet.scale) - sprite.collisionOffset.south - collisionOffset.north;
                howMany++;
            }
            if (howMany > 1)
            {
                isConfused = 5; 
            }
        }

        public bool isCuePlaying()
        {
            if (sound == null)
            {
                return false;
            }
            return true;
        }

        public void playCue()
        {
            if (sound == null)
            {
                sound = bank.GetCue("groan");
                sound.Play();
            }
        }

        public void stopCue()
        {
            if (sound != null)
            {
                sound.Stop(AudioStopOptions.Immediate);
                sound = null;
            }
        }

    }
}
