using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
    public class Tile : Objekt, ICloneable
    {
        public bool SolidTop { get; set; }
        public bool SolidBottom { get; set; }
        public bool SolidRight { get; set; }
        public bool SolidLeft { get; set; }

        public Object Clone () {
            Tile c = new Tile(
                              this._spriteBatch,
                              this._graphics,
                              this._camera,
                              this.Collidable);
            c.AddSprite("tile",(Sprite)this._sprite.Clone());
            c.SelectedAction = "tile";
            return c;
        }


        bool Solid{
            get{
                if( SolidTop &&
                    SolidBottom &&
                    SolidRight &&
                    SolidLeft){
                        return true;
                    }
                    else{
                        return false;
                    }
            }

            set{
                SolidTop = true;
                SolidBottom = true;
                SolidRight = true;
                SolidLeft = true;
            }
        }   

        public Tile( 
                    SpriteBatch spriteBatch,
                    GraphicsDeviceManager graphics,
                    Camera camera,
                    bool collidable){
            _sprites = new Dictionary<string, Sprite>();
            //this.AddSprite("tile", sprite);
            //this.SelectedAction = "tile";
            //_sprite = sprite;
            _spriteBatch = spriteBatch;
            _graphics = graphics;
            _camera = camera;
            Collidable = collidable;

    }
  }
}

