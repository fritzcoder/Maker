using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace maker {
    public class TileScreen : GameScreen
    {
        #region Graphics Data
        
        private Dictionary<string, Tile> tiles; 
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private Vector2 titlePosition;
        private Objekt mouse; 

        #endregion
        
        
        #region Text Data
        
        
        /// <summary>
        /// The title text shown at the top of the screen.
        /// </summary>
        private string titleText;
        
        /// <summary>
        /// The title text shown at the top of the screen.
        /// </summary>
        public string TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }
        
        /// <summary>
        /// Construct a new DialogueScreen object.
        /// </summary>
        /// <param name="mapEntry"></param>
        public TileScreen()
        {
            this.IsPopup = true;
        }
        
        
        /// <summary>
        /// Load the graphics content
        /// </summary>
        /// <param name="batch">SpriteBatch object</param>
        /// <param name="screenWidth">Width of the screen</param>
        /// <param name="screenHeight">Height of the screen</param>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            tiles = new Dictionary<string, Tile>();

            backgroundTexture =
                content.Load<Texture2D>(@"Textures\GameScreens\PopupScreen");
           
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            
            backgroundPosition.X = (viewport.Width - backgroundTexture.Width) / 2;
            backgroundPosition.Y = (viewport.Height - backgroundTexture.Height) / 2;

            titlePosition.X = (viewport.Width -
                               Fonts.HeaderFont.MeasureString(titleText).X) / 2;
            titlePosition.Y = backgroundPosition.Y + 70f;

            tiles.Add("Ground", new Tile((MacGame)ScreenManager.Game, true));

            tiles["Ground"].AddSprite("tile", new Sprite(content, "Ground", false));
            tiles["Ground"].Position = new Vector2(backgroundPosition.X + 20, 
                                                   backgroundPosition.Y + 100);
            tiles["Ground"].SelectedAction = "tile";

            tiles.Add("Ground2", new Tile((MacGame)ScreenManager.Game, true));

            tiles["Ground2"].AddSprite("tile", new Sprite(content, "Ground2", false));

            tiles["Ground2"].Position = new Vector2(backgroundPosition.X + 60, 
                                                   backgroundPosition.Y + 100);

            tiles["Ground2"].SelectedAction = "tile";


            tiles.Add("Eraser", new Tile(
                (MacGame)ScreenManager.Game, true));
            
            tiles["Eraser"].AddSprite("eraser", new Sprite(content, "eraser", false));
            tiles["Eraser"].Position = new Vector2(backgroundPosition.X + 550, 
                                                   backgroundPosition.Y + 400);
            tiles["Eraser"].SelectedAction = "eraser";


            mouse = new Objekt((MacGame)ScreenManager.Game, true);

            mouse.AddSprite("point",new Sprite(content, "mouse-pointer",false));
            mouse.SelectedAction = "point";
            mouse.Position = new Vector2(0,0);
        }
        
        public override void UnloadContent () {

        }
        #endregion

        
        #region Updating
        protected Collision Collided(Objekt o)
        {
            foreach(KeyValuePair<string, Tile> kvp in tiles){
                if(!kvp.Value.Equals(o) && kvp.Value.Collidable){
                    Collision clicked = Collision.Collided(o, kvp.Value);
                    if(clicked != null){
                        return clicked;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Handles user input to the dialog.
        /// </summary>
        public override void HandleInput () {

            if (Mouse.GetState ().LeftButton == ButtonState.Pressed) {

                System.Console.WriteLine("Mouse X: " + Mouse.GetState().X + 
                                         " Mouse Y: " + Mouse.GetState().Y);
                System.Console.WriteLine("Tile X: " + tiles["Ground"].Position.X + 
                                         " Tile Y: " + tiles["Ground"].Position.Y);

                Collision selected = Collided(mouse);
                if(selected != null)
                {
                    Tile tile = (Tile)selected.CollidedObjekt;

                    if(tile.SelectedAction == "eraser")
                    {
                        ((GamePlayScreen)ScreenManager.screens["GamePlay"]).selected_tile = null;
                    }
                    else
                    {
                        ((GamePlayScreen)ScreenManager.screens["GamePlay"]).selected_tile = (Tile)tile.Clone();
                    }
                }
            }

            if(InputManager.CurrentKeyboardState.IsKeyDown(Keys.Escape))
            {
                if(this.IsActive)
                {
                    ExitScreen();
                }
              
                return;
            }

        }
    
        /// <summary>
        /// draws the dialog.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            mouse.Position = new Vector2(Mouse.GetState().X, 
                        Mouse.GetState().Y); 
//            System.Console.WriteLine("Mouse X: " + Mouse.GetState().X + 
//                                     " Mouse Y: " + Mouse.GetState().Y);
//            // draw the fading screen
//            //spriteBatch.Draw(fadeTexture, new Rectangle(0, 0, 1280, 720), Color.White);
//            
            // draw popup background
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            
           

            spriteBatch.DrawString(Fonts.HeaderFont, titleText, titlePosition,
                                   Fonts.CountColor);
            
          
            tiles["Ground"].Draw();
            tiles["Ground2"].Draw();
            tiles["Eraser"].Draw();
            mouse.Draw();
            spriteBatch.End();
        }
        
        
#endregion
    }
}

