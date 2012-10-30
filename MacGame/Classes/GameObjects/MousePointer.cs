using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace maker {
    public class MousePointer : Objekt {
        public enum MouseState {
            Point, 
            Erase
        }
        private MouseState _state;
        public MouseState State { 
            get {
                return _state;
            }

            set {
                _state = value;
                if(_state == MousePointer.MouseState.Point){
                    this.SelectedAction = "main";
                }
                else if(_state == MousePointer.MouseState.Erase){
                    this.SelectedAction = "erase";
                }
            }
        }

        public MousePointer (MacGame game) {
            _sprites = new Dictionary<string, Sprite>();
            _spriteBatch = game.spriteBatch;
            _graphics = game.graphics;
            _camera = game.camera;

            this.AddSprite("main", 
                           new Sprite(game.Content,
                                "mouse-pointer",
                                true));
            this.AddSprite("erase", 
                           new Sprite(game.Content, 
                                      "eraser",
                                      true));
            State = MouseState.Point;
        }


    }
}

