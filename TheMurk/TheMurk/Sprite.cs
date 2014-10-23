using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheMurk
{
    abstract class Sprite
    {

        public SpriteSheet spriteSheet;
        protected Point currentFrame;
        protected Boolean pauseAnimation = false;
        protected SpriteEffects effects = SpriteEffects.None;

        protected GameState state;
        protected CollisionOffset collisionOffset;

        int timeSinceLastFrame = 0;

        protected Vector2 speed;
        public Vector2 position;

        public Sprite(SpriteSheet spriteSheet, Vector2 position, CollisionOffset collisionOffset, Vector2 speed, GameState state)
        {
            this.state = state;
            this.spriteSheet = spriteSheet;
            this.position = position;
            this.collisionOffset = collisionOffset;
            this.speed = speed;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds, Vector2 mod, bool inRange)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= spriteSheet.currentSegment.millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                if (!pauseAnimation)
                {
                    ++currentFrame.X;
                    // move the frame forward
                    if (currentFrame.X >= spriteSheet.sheetSize.X || currentFrame.X > spriteSheet.currentSegment.endFrame.X)
                    {
                        currentFrame.X = spriteSheet.currentSegment.startFrame.X;

                        ++currentFrame.Y;
                        if (currentFrame.Y >= spriteSheet.sheetSize.Y || currentFrame.Y > spriteSheet.currentSegment.endFrame.Y)
                        {
                            currentFrame.Y = spriteSheet.currentSegment.startFrame.Y;
                        }
                    }
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet.texture,
                position,
                new Rectangle(currentFrame.X * spriteSheet.currentSegment.frameSize.X,
                    currentFrame.Y * spriteSheet.currentSegment.frameSize.Y,
                    spriteSheet.currentSegment.frameSize.X, spriteSheet.currentSegment.frameSize.Y),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                effects,
                0);
        }

        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset.east,
                    (int)position.Y + collisionOffset.north,
                    spriteSheet.currentSegment.frameSize.X - (collisionOffset.east),
                    spriteSheet.currentSegment.frameSize.Y - (collisionOffset.north));
            }
        }
    }
}

