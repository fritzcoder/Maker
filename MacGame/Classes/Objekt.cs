using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;


namespace maker
{
    public class Objekt
    {
        protected Sprite _sprite;
        protected SpriteBatch _spriteBatch;
        protected GraphicsDeviceManager _graphics;
        protected Camera _camera;
        public bool Collidable { set; get; }

        public Vector2 Position{
            get{
                return _sprite.Position;
            }

            set{
                _sprite.Position = value;
            }
        }
    
        public float Scale{
            get{
                return _sprite.Scale;
            }
            set{
                _sprite.Scale = value;
            }
        }

        public float Angle{
            get{
                return _sprite.Angle;
            }
            set{
                _sprite.Angle = value;
            }
        }

        public Rectangle Size{
            get{
                return _sprite.Size;
            }
        }

        public Rectangle Bounds{
            get{
                return _sprite.Bounds;
            }
        }

        public Objekt(Sprite sprite, 
                  SpriteBatch spriteBatch,
                  GraphicsDeviceManager graphics,
                  Camera camera,
                  bool collidable){
            _sprite = sprite;
            _spriteBatch = spriteBatch;
            _graphics = graphics;
            _camera = camera;
            Collidable = collidable;
        }

        public Objekt() { }

        public void Update(){
            _sprite.update();
        }

        public void Draw(){
            _sprite.Draw(_spriteBatch,_camera.Position);
        }

        public bool InScreen(){
            return _sprite.InScreen(_graphics.GraphicsDevice.DisplayMode.Width,
                              _graphics.GraphicsDevice.DisplayMode.Height,
                              _camera.Position);
        }
    }
}