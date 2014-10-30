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
        private bool gameStarted;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameStarted = false;
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
            spriteBatch = null;
        }

        protected override void Update(GameTime gameTime)
        {
           /* if (!gameStarted)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    //UnloadContent();
                    Initialize();
                    LoadContent();
                    gameStarted = true;
                }
            }*/
            //else
            {
                Terminal.CheckOpen(Keys.P, Keyboard.GetState());
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                if (spriteManager.gameOver || spriteManager.gameRunOutOfTime || spriteManager.gameWon)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                    {
                        this.Exit();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Y))
                    {
                        UnloadContent();
                        Initialize();
                        LoadContent();
                    }
                }
                if (!gameStarted)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        UnloadContent();
                        Initialize();
                        LoadContent();
                        gameStarted = true;
                    }
                }
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
           /* if (!gameStarted)
            {
                //spriteManager.audioEngine = null;
                GraphicsDevice.Clear(Color.AliceBlue);

                spriteBatch.Begin();
                spriteBatch.Draw(this.Content.Load<Texture2D>(@"Images/start"), new Vector2(0, 0), Color.AntiqueWhite);
                spriteBatch.End();


            }*/
            
            if (spriteManager.gameOver)
            {
                if (spriteManager.gameRunOutOfTime)
                {
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin();
                    spriteBatch.Draw(this.Content.Load<Texture2D>(@"Images/lose"), new Vector2(0, 0), Color.AntiqueWhite);
                    spriteBatch.End();
                }
                else
                {
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin();
                    spriteBatch.Draw(this.Content.Load<Texture2D>(@"Images/lose"), new Vector2(0, 0), Color.AntiqueWhite);
                    spriteBatch.End();
                }
            }
            else if (spriteManager.gameRunOutOfTime)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else if (spriteManager.gameWon)
            {
                GraphicsDevice.Clear(Color.AliceBlue);
                spriteBatch.Begin();
                spriteBatch.Draw(this.Content.Load<Texture2D>(@"Images/win"), new Vector2(0, 0), Color.AntiqueWhite);
                spriteBatch.End();
            }
            else
            {
                Terminal.CheckDraw(true);
                GraphicsDevice.Clear(Color.Black);
                base.Draw(gameTime);
            }

            
        }
    }
}
