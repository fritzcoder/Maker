using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;


namespace maker
{
    public class Objekt
    {
        protected Sprite _sprite;
        //_sprites will be used to match a sprite or sprite sheet 
        //to actions. 

        //We can have sprite sheet for walking left and right 
        //We can add it to the dictionary
        //AddSprite("left", sprite_for_walking_left)
        //AddSprite("right", sprite_for_walking_right)
        //Then be able to select which one is going to be rendered
        //objekt.SelectedAction = "left";
        //This will render the left action;

        protected Dictionary<string, Sprite> _sprites;
        protected SpriteBatch _spriteBatch;
        protected GraphicsDeviceManager _graphics;
        public Rectangle _top; 
        public Rectangle _bottom;
        public Rectangle _left;
        public Rectangle _right;
        protected Camera _camera;
        protected Vector2 _position;

        public bool Collidable { set; get; }

        public Rectangle Top {
            get {
                _top.X = _sprite.Bounds.X + _sprite.Bounds.Width/4;
                _top.Y = _sprite.Bounds.Y;
               return _top;
            }
        }

        public Rectangle Bottom {
            get {
                _bottom.X = _sprite.Bounds.X + _sprite.Bounds.Width/4;
                _bottom.Y = _sprite.Bounds.Y+_sprite.Bounds.Height-_bottom.Height;
                return _bottom;
            }
        }

        public Rectangle Left {
            get {
                _left.X = _sprite.Bounds.X;
                _left.Y = _sprite.Bounds.Y+Top.Height;
                return _left;
            }
        }

        public Rectangle Right {
            get {
                _right.X = _sprite.Bounds.X+Left.Width;
                _right.Y = Top.Y+Top.Height;
                return _right;
            }
        }

        public bool WorldPosition { 
            get {
                return _sprite.WorldPosition;
            }
            set {
                _sprite.WorldPosition = value;
            }
        }

        public Vector2 Position{
            get{
                return _position;
            }

            set{
                _position = value;
                _sprite.Position = Position;
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

        public Objekt(
                  SpriteBatch spriteBatch,
                  GraphicsDeviceManager graphics,
                  Camera camera,
                  bool collidable){
            _top = new Rectangle();
            _bottom = new Rectangle();
            _right = new Rectangle();
            _left = new Rectangle();

           
            _sprites = new Dictionary<string, Sprite>();
            _spriteBatch = spriteBatch;
            _graphics = graphics;
            _camera = camera;
            Collidable = collidable;
        }

        public Objekt() { }

        private string _selectedAction; 
        public string SelectedAction {
            get {
                return _selectedAction;
            }
            set {
                _sprite = _sprites[value];
                _sprite.Position = _position;
                _selectedAction = value;
                _bottom.Width = _top.Width = _sprite.Bounds.Width/2;
                _top.Height = 10+5;
                _bottom.Height = 10+5;
                _right.Width = _left.Width = _sprite.Bounds.Width/2;
                _right.Height = _left.Height = _sprite.Bounds.Height-_top.Height-_bottom.Height;
            }
        }

        public void AddSprite (string actionName, Sprite sprite) {

            _sprites.Add(actionName, sprite);

        }

        public void Update(){
            //_sprite.Position = _position;
            _sprite.update();
        }
       
        public void Draw(){
           
            _sprite.Position = _position;
            _sprite.Draw(_spriteBatch,_camera.Position);

        }

        public bool InScreen(){
            _bottom.Width = _top.Width = _sprite.Bounds.Width/2;
            _top.Height = 10+5;
            _bottom.Height = 10+5;
            _right.Width = _left.Width = _sprite.Bounds.Width/2;
            _right.Height = _left.Height = _sprite.Bounds.Height-_top.Height-_bottom.Height;

            return _sprite.InScreen(_graphics.GraphicsDevice.DisplayMode.Width,
                              _graphics.GraphicsDevice.DisplayMode.Height,
                              _camera.Position);
        }

    }
}