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
using DebugTerminal;

namespace TheMurk
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("courier");
            Terminal.Init(this, spriteBatch, font, GraphicsDevice);
            
            Terminal.SetSkin(TerminalThemeType.FIRE);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Terminal.CheckOpen(Keys.P, Keyboard.GetState());
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Terminal.CheckDraw(true);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            
        }
    }
}
