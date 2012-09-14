using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
	public class AnimatedSprite : Sprite
	{
		//public int Rows { get; set; }
		//public int Columns { get; set; }
		private int _currentFrame;
		private int _totalFrames;

		public AnimatedSprite (ContentManager content, 
		                       string asset, int rows, int columns) {
			Texture = content.Load<Texture2D>(asset);
			//this.Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), 
			//                     (int)(Texture.Height * Scale));
			Rows = rows;
			Columns = columns; 
			_currentFrame = 0;
			_totalFrames = Rows * Columns;
		}

		public void update(){
			_currentFrame++;
			if(_currentFrame == _totalFrames)
				_currentFrame = 0; 
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 camera){
			int width = Texture.Width / Rows;
			int height = Texture.Height / Columns;
			int row = (int)((float)_currentFrame / (float)Columns);
			int column = _currentFrame % Columns;
			
			Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
      Rectangle destinationRectangle = new Rectangle((int)this.ScreenPosition(camera).X, 
                                                     (int)this.ScreenPosition(camera).Y, width, height);

			spriteBatch.Draw(Texture, destinationRectangle, 
                        sourceRectangle, Color.White);
		}
	}
}

