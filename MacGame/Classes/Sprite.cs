using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
	public class Sprite
	{
		public Vector2 Position { get; set; }
		public Texture2D Texture { get; set; }

        public Rectangle Size { 
            get 
            { 
                return new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale)); 
            }
        }

		public float Angle {get; set; }
		public float Scale { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        //public bool Collidable { get; set; }
        private int _currentFrame;
        private int _totalFrames;
        private int _width;
        private int _height;


        public Rectangle Bounds{
            get
            {
                return new Rectangle((int)Position.X,(int)Position.Y,_width, _height);
            }
        }

		public Sprite() { }

		public Sprite(ContentManager content, string assetName){
      Scale = 1;
			Texture = content.Load<Texture2D>(assetName);
			//Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
      _width = Texture.Width;
      _height = Texture.Height; 
		}

    public Sprite (ContentManager content,
                           string asset, int rows, int columns) {
      Scale = 1; 
      Texture = content.Load<Texture2D>(asset);

      //Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), 
        //                   (int)(Texture.Height * Scale));
      Rows = rows;
      Columns = columns; 
      _currentFrame = 0;
      _totalFrames = Rows * Columns;
      _width = Texture.Width / Columns;
      _height = Texture.Height / Rows;
      //ColorData = new Color[_width, _height];


    }

		public virtual void Draw(SpriteBatch spriteBatch, Vector2 camera){

      if(_totalFrames > 0){
        int row = 0;

        if(Rows > 1){
          row = (int)((float)_currentFrame / (float)Rows);
        }

        int column = _currentFrame % Columns;

        //System.Console.WriteLine("row: " + row + " col:" + column);
        Rectangle sourceRectangle = new Rectangle(_width * column, _height * row, _width, _height);
        Rectangle destinationRectangle = new Rectangle((int)this.ScreenPosition(camera).X, 
                                                       (int)this.ScreenPosition(camera).Y, _width, _height);
        
        spriteBatch.Draw(Texture, destinationRectangle, 
                         sourceRectangle, Color.White);

      }
      else{
        spriteBatch.Draw(Texture, ScreenPosition(camera), 
			                 new Rectangle(0, 0, Texture.Width, Texture.Height),Color.White, 
			                 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
      }
		}


    public void update(){
      _currentFrame++;
      if(_currentFrame == (_totalFrames))
        _currentFrame = 0; 
    }

    public bool InScreen(int width, int height, Vector2 camera)
    {
      Vector2 screenPosition = this.ScreenPosition(camera);

      //System.Console.WriteLine("position - X: " + screenPosition.X);

      if (((screenPosition.X >= 0) && (screenPosition.X < width))
          || (screenPosition.X + (Texture.Width * Scale)  >= 0) && 
            (screenPosition.X + (Texture.Width * Scale) < width)){
        //if ((screenPosition.Y >= 0) && (screenPosition.Y < height))
        //{
          return true;
        //}
      }

      return false;
    }

    public Vector2 ScreenPosition(Vector2 camera)
    {
      return new Vector2(Position.X - camera.X,
                         Position.Y - camera.Y);
    }

	}
}