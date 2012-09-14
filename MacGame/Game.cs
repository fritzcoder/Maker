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
        Dictionary<string, Objekt> objekts; 
        public Camera camera;
        public bool _jump = false;
        Level level; 

        public MacGame() {
            graphics = new GraphicsDeviceManager (this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;       
            objekts = new Dictionary<string, Objekt>();
        }

        protected override void Initialize () {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            camera = new Camera(new Vector2(0,0));
            spriteBatch = new SpriteBatch (GraphicsDevice);
            level = new Level(this);
            level.Load("test");
            objekts = level._objekts;
            screenManager = new ScreenManager(this);

            base.Initialize();
        }

        //protected override void Draw(GameTime gameTime) {
        //    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
        //}

        protected override void Update (GameTime gameTime){
            Player player = ((Player)objekts["hero"]);
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
                Exit ();
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                int x = Mouse.GetState().X + (int)camera.Position.X;
                int y = Mouse.GetState().Y + (int)camera.Position.Y;
                if(!objekts.ContainsKey("tileB" + x.ToString() + y.ToString()))
                {
                    Objekt newO = new Tile(new Sprite(Content, "Ground"),
                                           spriteBatch,
                                           graphics,
                                           camera, true);

                    newO.Position = new Vector2(x,y);

                    if(Collided(newO) == null)
                    {
                        objekts.Add("tileB" + x.ToString() + y.ToString(), 
                                    new Tile(new Sprite(Content, "Ground"),
                                 spriteBatch,
                                 graphics,
                                 camera, true));
                        
                        objekts["tileB" + x.ToString() + 
                                y.ToString()].Position = new Vector2(x,y);
                    }
                }
            }
            
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape)){
                Exit ();    
            }
            
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left)){
                player.playerStates["LEFT"] = true; 
                player.playerStates["RIGHT"] = false; 
            }
            
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right)){
                player.playerStates["RIGHT"] = true; 
                player.playerStates["LEFT"] = false;
            }
            
            if(!_jump)
            {
                if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space)){
                    if(player.playerStates["FALL"] == false){
                        player.playerStates["JUMP"] = true; 
                        _jump = true;
                    }
                }
            }
            
            if(_jump)
            {
                if(Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Space)){
                    if(player.playerStates["JUMP"] == true){
                        player.playerStates["JUMP"] = false; 
                    }
                    _jump = false;
                }
            }

            if(Collided (player) != null){
                player.playerStates["FALL"] = false;
                //System.Console.WriteLine("Collided:" + kvp.Key);
            }
            else{
                if(player.playerStates["JUMP"] == false){
                    player.playerStates["FALL"] = true;
                }
            }

            player.Actions();

            objekts["mouse-pointer"].Position = 
                new Vector2(Mouse.GetState().X + (int)camera.Position.X, 
                            Mouse.GetState().Y + (int)camera.Position.Y); 

            if(player.Position.X > graphics.GraphicsDevice.DisplayMode.Width / 2)
                camera.Position = 
                    new Vector2(player.Position.X - graphics.GraphicsDevice.DisplayMode.Width / 2,0);
            
            // TODO: Add your update logic here         
            base.Update (gameTime);
        }

        protected Objekt Collided(Objekt o)
        {
            foreach(KeyValuePair<string, Objekt> kvp in objekts){
                if(kvp.Value.InScreen() && !kvp.Value.Equals(o) && kvp.Value.Collidable){
                    if(Utility.BoundingCollision(o, kvp.Value)){
                        return kvp.Value;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw (GameTime gameTime){
            graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
            spriteBatch.Begin();
            
            foreach(KeyValuePair<string, Objekt> kvp in objekts){
                if(kvp.Value.InScreen()){
                    kvp.Value.Draw();
                }
            }
            
            spriteBatch.End();
            
            base.Draw (gameTime);
        }
    }
}
