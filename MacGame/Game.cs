using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace maker
{
    public class MacGame : Game {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public ScreenManager screenManager;
        public Camera camera;
      
        public MacGame() {
            graphics = new GraphicsDeviceManager (this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;       
        }

        protected override void Initialize () {
            base.Initialize();
            InputManager.Initialize();

            //graphics.ApplyChanges();
            //testscreen = new HelpScreen();
            Fonts.LoadContent(this.Content);

            camera = new Camera(new Vector2(0,0));
            spriteBatch = new SpriteBatch (GraphicsDevice);
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            screenManager.AddScreen(new GamePlayScreen(), "GamePLay");
        }

        //protected override void Draw(GameTime gameTime) {
        //    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
        //}

        protected override void Update (GameTime gameTime){
            InputManager.Update();

            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Q)){
                Exit ();    
            }

            // TODO: Add your update logic here 
            //screenManager.Update(gameTime);
            base.Update (gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw (GameTime gameTime){
            graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

            spriteBatch.Begin();
            screenManager.Draw(gameTime);
            spriteBatch.End();

            base.Draw (gameTime);
        }
    }
}
