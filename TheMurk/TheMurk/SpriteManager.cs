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
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;

        //GAMEPLAY VARIABLES
        private int objectsCount = 200;
        private int zombieCount = 30;
        private int batteryCount = 10;
        public readonly static Point frameSize = new Point(1024, 1024);
        public readonly static Point playerSize = new Point(66, 66);
        public readonly static int mapSize = 2;//even numbers only
        public readonly static Vector2 speed = new Vector2(3, 3);

        private int currentTime = 0;

        protected bool collision = false;
        private int losingTime = 500000;
        private int batteryBonus = 10000;

        GameState state;
        Game game;

        Cue suspenseCue;

        SpriteBatch spriteBatch;

        List<MapBoundSprite> objects = new List<MapBoundSprite>();
        List<BackgroundSprite> backgrounds = new List<BackgroundSprite>();
        List<ZombieSprite> zombies = new List<ZombieSprite>();
        List<BatterySprite> batteries = new List<BatterySprite>();
        LostPlayer player;
        MapBoundSprite gps;
        OverlaySprite overlay;
        BatteryOverlaySprite batteryOverlay;

        Map map = new Map();

        public SpriteManager(Game game)
            : base(game)
        {
            this.game = game;
            
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");

            suspenseCue = soundBank.GetCue("suspense");
            soundBank.PlayCue("background");

            state = new GameState(soundBank);
            state.setLosingTime(losingTime);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            for (int i = (mapSize/2) * -1 ; i <= mapSize/2; i++)
            {
                for (int j = (mapSize / 2) * -1; j <= mapSize / 2; j++)
                {
                    backgrounds.Add(new BackgroundSprite(Game.Content.Load<Texture2D>(@"Images/background_v2"), new Vector2((Game.GraphicsDevice.Viewport.Width/2 - frameSize.X/2) + (j * frameSize.X), (Game.GraphicsDevice.Viewport.Height / 2 - frameSize.Y/2) + (i * frameSize.Y)), state));
                }
            }

            Random random = new Random();
            
            for (int i = 0; i < objectsCount; i++)
                objects.Add(new StumpSprite(Game.Content.Load<Texture2D>(@"Images/truestump"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width / 2 + (((frameSize.X * (mapSize / 2)) + (frameSize.X / 2)) * -1), Game.GraphicsDevice.Viewport.Width / 2 + (frameSize.X * (mapSize / 2)) + (frameSize.X / 2)), random.Next(Game.GraphicsDevice.Viewport.Height / 2 + (((frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2)) * -1), Game.GraphicsDevice.Viewport.Height / 2 + (frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2))), state));      

            for (int i = 0; i < zombieCount; i++)
                zombies.Add(new ZombieSprite(Game.Content.Load<Texture2D>(@"Images/ZombieSpriteSheet"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width / 2 + (((frameSize.X * (mapSize / 2)) + (frameSize.X / 2)) * -1), Game.GraphicsDevice.Viewport.Width / 2 + (frameSize.X * (mapSize / 2)) + (frameSize.X / 2)), random.Next(Game.GraphicsDevice.Viewport.Height / 2 + (((frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2)) * -1), Game.GraphicsDevice.Viewport.Height / 2 + (frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2))), state, soundBank));

            for (int i = 0; i < batteryCount; i++)
                batteries.Add(new BatterySprite(Game.Content.Load<Texture2D>(@"Images/battery"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width / 2 + (((frameSize.X * (mapSize / 2)) + (frameSize.X / 2)) * -1), Game.GraphicsDevice.Viewport.Width / 2 + (frameSize.X * (mapSize / 2)) + (frameSize.X / 2)), random.Next(Game.GraphicsDevice.Viewport.Height / 2 + (((frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2)) * -1), Game.GraphicsDevice.Viewport.Height / 2 + (frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2))), state, soundBank));

            gps = new GpsSprite(Game.Content.Load<Texture2D>(@"Images/gps_locator (1)"), new Vector2(random.Next(Game.GraphicsDevice.Viewport.Width / 2 + (((frameSize.X * (mapSize / 2)) + (frameSize.X / 2)) * -1), Game.GraphicsDevice.Viewport.Width / 2 + (frameSize.X * (mapSize / 2)) + (frameSize.X / 2)), random.Next(Game.GraphicsDevice.Viewport.Height / 2 + (((frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2)) * -1), Game.GraphicsDevice.Viewport.Height / 2 + (frameSize.Y * (mapSize / 2)) + (frameSize.Y / 2))), state);
            player = new LostPlayer(Game.Content.Load<Texture2D>(@"Images/PersonSpriteSheet"), new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - playerSize.X / 2, Game.GraphicsDevice.Viewport.Height / 2 - playerSize.Y / 2), state);
            overlay = new OverlaySprite(Game.Content.Load<Texture2D>(@"Images/trulytheoverlay"), new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - frameSize.X / 2) + (0 * frameSize.X), (Game.GraphicsDevice.Viewport.Height / 2 - frameSize.Y / 2) + (0 * frameSize.Y)), state);
            batteryOverlay = new BatteryOverlaySprite(Game.Content.Load<Texture2D>(@"Images/battery_game2"), new Vector2(Game.GraphicsDevice.Viewport.Width - 50, 20), state);

            //add all map objects to map
            foreach (ZombieSprite zombie in zombies)
                map.add(zombie);

            foreach (BackgroundSprite back in backgrounds)
                map.add(back);

            foreach (BatterySprite battery in batteries)
                map.add(battery);

            foreach (MapBoundSprite sprite in objects)
                map.add(sprite);
            
            map.add(gps);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            // call update on all map objects (backgrounds, map objects, batteries, gps, and zombies)
            map.update(gameTime, Game.Window.ClientBounds);

            //check for collision between map object and player
            foreach (MapBoundSprite sprite in objects)
            {
                if (sprite.collisionRect.Intersects(player.collisionRect))
                    map.collision((LostPlayer) player, (Sprite) sprite);              
            }

            //move zombies according to AI
            foreach (ZombieSprite sprite in zombies)
                sprite.AI(player, Game.Window.ClientBounds);

            //move player (only applies in gamestate.nsew)
            player.Update(gameTime, Game.Window.ClientBounds);

            //check again if any map objects intersect with player and zombies
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

            //check if zombies intersect with other zombies
            foreach (ZombieSprite zombie1 in zombies)
            {
                foreach (ZombieSprite zombie2 in zombies)
                {
                    if (zombie1.collisionRect.Intersects(zombie2.collisionRect))
                        zombie2.collision(zombie1);
                }
            }

            //check for zombie - player collision. Game end.
            foreach (ZombieSprite zombie in zombies)
            {
                if (zombie.collisionRect.Intersects(player.collisionRect))
                {
                    soundBank.GetCue("bite").Play();
                    game.Exit();
                }

                //if zombie is on screen make battery noise
                if (zombie.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height)) && !zombie.isCuePlaying())
                {
                    zombie.playCue();
                }
                if (!zombie.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height)))
                {
                    zombie.stopCue();
                }
            }

            //check for battery - player collision. Add time.
            foreach (BatterySprite battery in batteries)
            {
                if (battery.collisionRect.Intersects(player.collisionRect) && battery.isUsed == false)
                {
                    soundBank.GetCue("charging").Play();
                    state.setLosingTime(state.getLosingTime() + batteryBonus);
                    battery.isUsed = true;
                }

                //if battery is on screen make battery noise
                if (battery.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height)) && !battery.isCuePlaying())
                {
                    battery.playCue();
                }
                if (!battery.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height)))
                {
                    battery.stopCue();
                }
            }

            //check for gps - player collision. Add time.
            if (gps.collisionRect.Intersects(player.collisionRect))
            {
                soundBank.GetCue("gps_on").Play();
                game.Exit();
            }

            //if gps is on screen make gps noise
            if (gps.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height)) && !state.isCuePlaying(1))
            {
                state.playCue("gps_nearby", 1);
            }
            if(!gps.collisionRect.Intersects(new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height))) 
            {
                state.stopCue(1);
            }

            if (state.getGameTime() >= state.getLosingTime() - 15000)
            {
                if (!suspenseCue.IsPlaying)
                {
                    suspenseCue.Play();
                }
            }


            state.setGameTime(state.getGameTime() + gameTime.ElapsedGameTime.Milliseconds);
            if (state.getGameTime() >= state.getLosingTime())
                game.Exit();

            //update overlays
            batteryOverlay.Update(gameTime, Game.Window.ClientBounds);
            overlay.Update(gameTime, Game.Window.ClientBounds);

            audioEngine.Update();
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
            foreach (BatterySprite battery in batteries)
            {
                if(battery.isUsed == false)
                    battery.Draw(gameTime, spriteBatch);
            }
            gps.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            //overlay.Draw(gameTime, spriteBatch);
            batteryOverlay.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
