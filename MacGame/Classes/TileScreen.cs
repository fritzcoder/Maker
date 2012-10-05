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

            tiles.Add("Ground", new Tile(new Sprite(content, "Ground", false),
                               ScreenManager.SpriteBatch,
                               ((MacGame)ScreenManager.Game).graphics,
                               ((MacGame)ScreenManager.Game).camera, true));
            tiles["Ground"].Position = new Vector2(backgroundPosition.X + 20, 
                                                   backgroundPosition.Y + 100);

            tiles.Add("Ground2", new Tile(new Sprite(content, "Ground2", false),
                                         ScreenManager.SpriteBatch,
                                         ((MacGame)ScreenManager.Game).graphics,
                                         ((MacGame)ScreenManager.Game).camera, true));
            tiles["Ground2"].Position = new Vector2(backgroundPosition.X + 60, 
                                                   backgroundPosition.Y + 100);

            mouse = new Objekt( 
                               ScreenManager.SpriteBatch,
                               ((MacGame)ScreenManager.Game).graphics,
                               ((MacGame)ScreenManager.Game).camera, true);

            mouse.AddSprite("point",new Sprite(content, "mouse-pointer",false));
            mouse.SelectedAction = "point";
            mouse.Position = new Vector2(0,0);
        }
        
        public override void UnloadContent () {

        }
        #endregion

        
        #region Updating
        protected Objekt Collided(Objekt o)
        {
            foreach(KeyValuePair<string, Tile> kvp in tiles){
                if(kvp.Value.InScreen() && !kvp.Value.Equals(o) && kvp.Value.Collidable){
                    if(Utility.BoundingCollision(o, kvp.Value)){
                        return (Objekt)kvp.Value;
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
                Tile selected = (Tile)Collided(mouse);
                if(selected != null)
                {
                    ((GamePlayScreen)ScreenManager.screens["GamePlay"]).selected_tile = 
                        (Tile)selected.Clone(); 
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
        
        
#endregion
        
        
        #region Drawing
        
        
        /// <summary>
        /// draws the dialog.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            mouse.Position = new Vector2(Mouse.GetState().X, 
                        Mouse.GetState().Y); 
//            // draw the fading screen
//            //spriteBatch.Draw(fadeTexture, new Rectangle(0, 0, 1280, 720), Color.White);
//            
            // draw popup background
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            
           

            spriteBatch.DrawString(Fonts.HeaderFont, titleText, titlePosition,
                                   Fonts.CountColor);
            
          
            tiles["Ground"].Draw();
            tiles["Ground2"].Draw();
            mouse.Draw();
            spriteBatch.End();
        }
        
        
#endregion
    }
}

