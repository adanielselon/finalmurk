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
        private int zombieCount = 0;
        public readonly static Point frameSize =  new Point(512, 512);
        public readonly static Point playerSize = new Point(40, 42);
        public readonly static int mapSize = 2; //even numbers only
        public readonly static Vector2 speed = new Vector2(4, 4);

        private int currentTime = 0;

        protected bool collision = false;
        private int losingTime = 20000;

        GameState state;
        Game game;

        SpriteBatch spriteBatch;

        List<MapBoundSprite> objects = new List<MapBoundSprite>();
        List<BackgroundSprite> backgrounds = new List<BackgroundSprite>();
        List<ZombieSprite> zombies = new List<ZombieSprite>();
        LostPlayer player;
        BackgroundSprite overlay;

        Map map = new Map();

        public SpriteManager(Game game)
            : base(game)
        {
            this.game = game;
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
                    backgrounds.Add(new BackgroundSprite(Game.Content.Load<Texture2D>(@"Images/background_2048"), new Vector2((Game.GraphicsDevice.Viewport.Width/2 - frameSize.X/2) + (j * frameSize.X), (Game.GraphicsDevice.Viewport.Height / 2 - frameSize.Y/2) + (i * frameSize.Y)), state));
                }
            }

            Random random = new Random();

            for (int i = 0; i < stumpCount; i++)
                objects.Add(new StumpSprite(Game.Content.Load<Texture2D>(@"Images/truestump"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width/2 + (((frameSize.X * (mapSize/2)) + (frameSize.X/2)) * -1), Game.GraphicsDevice.Viewport.Width/2 + (frameSize.X * (mapSize/2)) + (frameSize.X/2)), random.Next(Game.GraphicsDevice.Viewport.Height/2 + (((frameSize.Y * (mapSize/2)) + (frameSize.Y/2)) * -1), Game.GraphicsDevice.Viewport.Height/2 + (frameSize.Y * (mapSize/2)) + (frameSize.Y/2))), state));
            
            for (int i = 0; i < zombieCount; i++)
                zombies.Add(new ZombieSprite(Game.Content.Load<Texture2D>(@"Images/character"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width / 2 + (((frameSize.X * (mapSize / 2)) + (frameSize.X / 2)) * -1), Game.GraphicsDevice.Viewport.Width / 2 + (frameSize.X * (mapSize / 2)) + (frameSize.X / 2)), random.Next(Game.GraphicsDevice.Viewport.Height / 2 + (((frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2)) * -1), Game.GraphicsDevice.Viewport.Height / 2 + (frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2))), state));

            foreach (ZombieSprite zombie in zombies)
                map.add(zombie);

            foreach (BackgroundSprite back in backgrounds)
                map.add(back);

            foreach (MapBoundSprite sprite in objects)
                map.add(sprite);

            //add zombies
            player = new LostPlayer(Game.Content.Load<Texture2D>(@"Images/PersonSpriteSheet"), new Vector2(Game.GraphicsDevice.Viewport.Width/2 - playerSize.X/2, Game.GraphicsDevice.Viewport.Height/2 - playerSize.Y/2), state);
            //overlay = new BackgroundSprite(Game.Content.Load<Texture2D>(@"Images/overlay"), new Vector2((Game.GraphicsDevice.Viewport.Width/2 - frameSize.X/2) + (0 * frameSize.X), (Game.GraphicsDevice.Viewport.Height / 2 - frameSize.Y/2) + (0 * frameSize.Y)), state);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            map.update(gameTime, Game.Window.ClientBounds);
            foreach (MapBoundSprite sprite in objects)
            {
                if (sprite.collisionRect.Intersects(player.collisionRect))
                    map.collision((LostPlayer) player, (Sprite) sprite);              
            }

            //move zombies
            foreach (ZombieSprite sprite in zombies)
                sprite.AI(player, Game.Window.ClientBounds);

            player.Update(gameTime, Game.Window.ClientBounds);

            foreach (MapBoundSprite sprite in objects)
            {
                if (sprite.collisionRect.Intersects(player.collisionRect))
                    player.collision(sprite);
                foreach (ZombieSprite zombie in zombies)
                {
                    if (sprite.collisionRect.Intersects(zombie.collisionRect))
                        zombie.collision(sprite);
                }
            }
            foreach (ZombieSprite zombie1 in zombies)
            {
                foreach (ZombieSprite zombie2 in zombies)
                {
                    if (zombie1.collisionRect.Intersects(zombie2.collisionRect))
                        zombie2.collision(zombie1);
                }
            }

            foreach (ZombieSprite zombie in zombies)
                if (zombie.collisionRect.Intersects(player.collisionRect))
                    game.Exit();

            state.setGameTime(state.getGameTime() + gameTime.ElapsedGameTime.Milliseconds);
            if (state.getGameTime() >= losingTime)
                game.Exit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Sprite sprite in backgrounds)
                sprite.Draw(gameTime, spriteBatch);
            foreach (Sprite sprite in objects)
                sprite.Draw(gameTime, spriteBatch);
            foreach (Sprite sprite in zombies)
                sprite.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            //overlay.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
