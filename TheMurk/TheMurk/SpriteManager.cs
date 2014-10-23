using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TheMurk
{
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //GAMEPLAY VARIABLES
        private int stumpCount = 100;
        public readonly static Point frameSize =  new Point(512, 512);
        public readonly static Point playerSize = new Point(40, 42);
        public readonly static int mapSize = 2; //even numbers only
        public readonly static double tspeed = 3;

        protected bool collision = false;

        GameState state;

        SpriteBatch spriteBatch;
        List<Sprite> spriteList = new List<Sprite>();
        List<BackgroundSprite> backgrounds = new List<BackgroundSprite>();
        Sprite player;

        List<Sprite> inRange = new List<Sprite>();
        List<Sprite> outRange = new List<Sprite>();

        public SpriteManager(Game game)
            : base(game)
        {
            state = new GameState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            for (int i = (mapSize/2) * -1 ; i <= mapSize/2; i++)
            {
                for (int j = (mapSize / 2) * -1; j <= mapSize / 2; j++)
                {
                    backgrounds.Add(new BackgroundSprite(Game.Content.Load<Texture2D>(@"Images/background"), new Vector2((Game.GraphicsDevice.Viewport.Width/2 - frameSize.X/2) + (j * frameSize.X), (Game.GraphicsDevice.Viewport.Height / 2 - frameSize.Y/2) + (i * frameSize.Y)), state));
                }
            }

            Random random = new Random();

            for (int i = 0; i < stumpCount; i++)
            {
                spriteList.Add(new StumpSprite(Game.Content.Load<Texture2D>(@"Images/stump"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width/2 + (((frameSize.X * (mapSize/2)) + (frameSize.X/2)) * -1), Game.GraphicsDevice.Viewport.Width/2 + (frameSize.X * (mapSize/2)) + (frameSize.X/2)), random.Next(Game.GraphicsDevice.Viewport.Height/2 + (((frameSize.Y * (mapSize/2)) + (frameSize.Y/2)) * -1), Game.GraphicsDevice.Viewport.Height/2 + (frameSize.Y * (mapSize/2)) + (frameSize.Y/2))), state));
            }

            player = new LostPlayer(Game.Content.Load<Texture2D>(@"Images/character"), new Vector2(Game.GraphicsDevice.Viewport.Width/2 - playerSize.X/2, Game.GraphicsDevice.Viewport.Height/2 - playerSize.Y/2), state);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {



            foreach (Sprite sprite in spriteList)
            {
                if (player.position.X > sprite.position.X - player.spriteSheet.currentSegment.frameSize.X - (40) && player.position.X < sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X + player.spriteSheet.currentSegment.frameSize.X + (1.5 * tspeed) && player.position.Y > sprite.position.Y - player.spriteSheet.currentSegment.frameSize.Y - (40) && player.position.Y < sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y + player.spriteSheet.currentSegment.frameSize.Y + (1.5 * tspeed))
                {
                    inRange.Add(sprite);
                }
                else
                {
                    outRange.Add(sprite);
                }
            }

            Vector2 mod = new Vector2((int)tspeed, (int)tspeed);

            if (inRange.Count() > 0)
                mod = inRange.ElementAt(0).position;
            
            for (int i = 0; i < tspeed; i++)
            {
                foreach (Sprite sprite in inRange)
                {
                    if (sprite.collisionRect.Intersects(player.collisionRect) && sprite is StumpSprite)
                    {
                        state.setCollision(true);
                        state.collisionOccured(player, sprite);
                    }
                }

                player.Update(gameTime, Game.Window.ClientBounds, new Vector2(1, 1), true);

                foreach (Sprite sprite in inRange)
                    sprite.Update(gameTime, Game.Window.ClientBounds, new Vector2(1, 1), true);
            }

            if (inRange.Count() > 0)
            {
                mod.X = Math.Abs(mod.X - inRange.ElementAt(0).position.X);
                mod.Y = Math.Abs(mod.Y - inRange.ElementAt(0).position.Y);
            }


            foreach (Sprite sprite in outRange)
                sprite.Update(gameTime, Game.Window.ClientBounds, mod, false);

            foreach (BackgroundSprite background in backgrounds)
                background.Update(gameTime, Game.Window.ClientBounds, mod, false);

            inRange.Clear();
            outRange.Clear();

            state.setCollisionTopBottom(false);
            state.setCollisionLeftRight(false);
            state.setCollision(false);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (BackgroundSprite sprite in backgrounds)
                sprite.Draw(gameTime, spriteBatch);
            foreach (Sprite sprite in spriteList)
                sprite.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
