using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;


namespace maker {
    public class GamePlayScreen : GameScreen {

        public Dictionary<string, Objekt> _objekts; 
        bool _jump;
        public Tile selected_tile;
        Texture2D pixel;

        public GamePlayScreen () : base() {
            _objekts = new Dictionary<string, Objekt>();

        }

        protected List<Collision> Collided(Objekt o)
        {
            List<Collision> collisions = new List<Collision>();

            foreach(KeyValuePair<string, Objekt> kvp in _objekts){
                if(kvp.Value.InScreen() && !kvp.Value.Equals(o) && kvp.Value.Collidable){
                    Collision col = Collision.Collided(o, kvp.Value);
   
                    if(col != null){
                        collisions.Add(col);
                    }
                }
            }

            return collisions;
        }


        public override void LoadContent () {
            MacGame _game = (MacGame)ScreenManager.Game;
            pixel = new Texture2D(_game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it

            _objekts.Add("bg1", new Objekt( 
                                           _game.spriteBatch, _game.graphics, _game.camera,false));
            _objekts.Add("bg2", new Objekt(
                                           _game.spriteBatch, _game.graphics, _game.camera, false));
            _objekts.Add("bg3", new Objekt(
                                           _game.spriteBatch, _game.graphics, _game.camera, false));
            _objekts.Add("bg4", new Objekt(
                                           _game.spriteBatch, _game.graphics, _game.camera, false));

            _objekts["bg1"].AddSprite("bg1", new Sprite(_game.Content, "Background01"));
            _objekts["bg2"].AddSprite("bg2", new Sprite(_game.Content, "Background02"));
            _objekts["bg3"].AddSprite("bg3", new Sprite(_game.Content, "Background03"));
            _objekts["bg4"].AddSprite("bg4", new Sprite(_game.Content, "Background05"));

            _objekts["bg1"].SelectedAction = "bg1";
            _objekts["bg2"].SelectedAction = "bg2";
            _objekts["bg3"].SelectedAction = "bg3";
            _objekts["bg4"].SelectedAction = "bg4";

            _objekts["bg1"].Scale = 3.0f;
            _objekts["bg2"].Scale = 3.0f;
            _objekts["bg3"].Scale = 3.0f;
            _objekts["bg4"].Scale = 3.0f;

            _objekts["bg1"].Position = new Vector2(0,-100);
            _objekts["bg2"].Position = new Vector2(_objekts["bg1"].Position.X + _objekts["bg1"].Size.Width, -100);
            _objekts["bg3"].Position = new Vector2(_objekts["bg2"].Position.X + _objekts["bg2"].Size.Width, -100);
            _objekts["bg4"].Position = new Vector2(_objekts["bg3"].Position.X + _objekts["bg3"].Size.Width, -100);

            for(int i = 0; i < 32; i++){
                _objekts.Add("tile" + i.ToString(), 
                             new Tile(new Sprite(_game.Content, "Ground"),
                         _game.spriteBatch,
                         _game.graphics,
                         _game.camera, 
                         true));
                
                _objekts["tile" + i.ToString()].Position = new Vector2(i * 32,650);
                ((Tile)_objekts["tile" + i.ToString()]).SolidTop = true;
            }
            
            
            for(int i = 0; i < 32; i++){
                _objekts.Add("tileA" + i.ToString(), 
                             new Tile(new Sprite(_game.Content, "Ground"),
                         _game.spriteBatch,
                         _game.graphics,
                         _game.camera, true));
                
                _objekts["tileA" + i.ToString()].Position = new Vector2((i + 30) * 32,550);
                ((Tile)_objekts["tileA" + i.ToString()]).SolidTop = true;
            }
            
            _objekts.Add("hero", 
                         new Player(
                       _game.spriteBatch,
                       _game.graphics,
                       _game.camera));

            _objekts["hero"].AddSprite("right",new Sprite(_game.Content, "maker_walk", 1, 4));
            _objekts["hero"].AddSprite("left",new Sprite(_game.Content, "maker_walk_left", 1, 4));
            _objekts["hero"].SelectedAction = "right";
            _objekts["hero"].Position = new Vector2(10, 100); 


            
            _objekts.Add("mouse-pointer", 
                         new Objekt( 
                       _game.spriteBatch, _game.graphics, _game.camera, false));
            _objekts["mouse-pointer"].AddSprite("point", new Sprite(_game.Content, "mouse-pointer"));
            _objekts["mouse-pointer"].SelectedAction = "point";
            _objekts["mouse-pointer"].Position = new Vector2(0,0);
            Camera camera = ((MacGame)ScreenManager.Game).camera;

            selected_tile = new Tile(new Sprite(ScreenManager.Game.Content, "Ground2"),
                     ScreenManager.SpriteBatch,
                     ((MacGame)ScreenManager.Game).graphics,
                     camera, true);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                    bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            
            if (IsActive && !coveredByOtherScreen)
            {
                Camera camera = ((MacGame)ScreenManager.Game).camera;
                Player player = ((Player)_objekts["hero"]);
               
                if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                    int x = Mouse.GetState().X + (int)camera.Position.X;
                    int y = Mouse.GetState().Y + (int)camera.Position.Y;

                    if(!_objekts.ContainsKey("tileB" + x.ToString() + y.ToString()))
                    {
                        Objekt newO = (Tile)selected_tile.Clone();
                        
                        newO.Position = new Vector2(x,y);
                        newO.WorldPosition = true;
                        
                        if(Collided(newO).Count == 0)
                        {
                            _objekts.Add("tileB" + x.ToString() + y.ToString(), 
                                        newO);
                        }
                    }
                }
               
                if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Tab)){
                    TileScreen tileScreen = new TileScreen();
                    tileScreen.TitleText = "Maker";
                    ScreenManager.AddScreen(tileScreen, "Tile");
                }

                if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A)){
                    player.playerStates["LEFT"] = true; 
                    player.playerStates["RIGHT"] = false; 
                }
                
                if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D)){
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


                List<Collision> collisions = Collided(player);
                if(collisions.Count > 0){
                    foreach(Collision col in collisions)
                    {
                        if(col.Side == CollisionSide.Bottom)
                        {
                            player.playerStates["FALL"] = false;
                        }
                        if(col.Side == CollisionSide.Right)
                        {
                            player.playerStates["RIGHT"] = false;
                        }
                        if(col.Side == CollisionSide.Left)
                        {
                            player.playerStates["LEFT"] = false;
                        }
                        if(col.Side == CollisionSide.Top)
                        {
                            player.playerStates["FALL"] = true;
                            player.playerStates["JUMP"] = false;
                        }
                    }
                    //System.Console.WriteLine("Collided:" + kvp.Key);
                }
                else{
                    if(player.playerStates["JUMP"] == false){
                        player.playerStates["FALL"] = true;
                    }
                }
                
                player.Actions();
                
                _objekts["mouse-pointer"].Position = 
                    new Vector2(Mouse.GetState().X + (int)camera.Position.X, 
                                Mouse.GetState().Y + (int)camera.Position.Y); 
                
                if(player.Position.X > ((MacGame)ScreenManager.Game).graphics.GraphicsDevice.DisplayMode.Width / 2)
                    camera.Position = 
                        new Vector2(player.Position.X - ((MacGame)ScreenManager.Game).graphics.GraphicsDevice.DisplayMode.Width / 2,0);
                
                // TODO: Add your update logic here 
                //screenManager.Update(gameTime);
               
                //Session.Update(gameTime);
            }
        }


        private void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
            
            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            
            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                                  rectangleToDraw.Y,
                                                  thicknessOfBorder,
                                                  rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                                  rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                                  rectangleToDraw.Width,
                                                  thicknessOfBorder), borderColor);
        }

        private void DrawBorder2(SpriteBatch spriteBatch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            Camera camera = ((MacGame)ScreenManager.Game).camera;
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X - (int)camera.Position.X, 
                                                  rectangleToDraw.Y - (int)camera.Position.Y, 
                                                  rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X - (int)camera.Position.X, 
                                                  rectangleToDraw.Y - (int)camera.Position.Y, 
                                                  thicknessOfBorder, rectangleToDraw.Height), borderColor);
            
            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X - (int)camera.Position.X + rectangleToDraw.Width - thicknessOfBorder),
                                                  rectangleToDraw.Y - (int)camera.Position.Y,
                                                  thicknessOfBorder,
                                                  rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X - (int)camera.Position.X,
                                                  rectangleToDraw.Y - (int)camera.Position.Y + rectangleToDraw.Height - thicknessOfBorder,
                                                  rectangleToDraw.Width,
                                                  thicknessOfBorder), borderColor);
        }


        public override void Draw (GameTime gameTime) {
            //graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
           
            foreach(KeyValuePair<string, Objekt> kvp in _objekts){
                if(kvp.Value.InScreen()){
                    kvp.Value.Draw();
                    DrawBorder2(spriteBatch, kvp.Value.Top, 1, Color.Red);
                    DrawBorder2(spriteBatch, kvp.Value.Bottom, 1, Color.Red);
                    DrawBorder2(spriteBatch, kvp.Value.Left, 1, Color.Red);
                    DrawBorder2(spriteBatch, kvp.Value.Right, 1, Color.Red);

                }
            }
            // Create any rectangle you want. Here we'll use the TitleSafeArea for fun.
            Rectangle titleSafeRectangle = ((MacGame)ScreenManager.Game).GraphicsDevice.Viewport.TitleSafeArea;
            
            // Call our method (also defined in this blog-post)
            DrawBorder(spriteBatch, titleSafeRectangle, 2, Color.Red);
            //_objekts["mouse-pointer"].Draw();
            spriteBatch.End();
        }
    }
}

