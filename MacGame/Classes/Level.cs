using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
    public class Level
    {
        public Dictionary<string, Objekt> _objekts; 
        MacGame _game;

        public Level (MacGame game)
        {
            _objekts = new Dictionary<string, Objekt>();
            _game = game;
        }

        public void Load(string filename)
        {
          /*
      Load Background
      Load Tiles
      Load Enemies and Items
      Load Player
       */
            _objekts.Add("bg1", new Objekt(new Sprite(_game.Content, "Background01"), 
                                     _game.spriteBatch, _game.graphics, _game.camera,false));
            _objekts.Add("bg2", new Objekt(new Sprite(_game.Content, "Background02"), 
                                     _game.spriteBatch, _game.graphics, _game.camera, false));
            _objekts.Add("bg3", new Objekt(new Sprite(_game.Content, "Background03"),
                                     _game.spriteBatch, _game.graphics, _game.camera, false));
            _objekts.Add("bg4", new Objekt(new Sprite(_game.Content, "Background05"), 
                                     _game.spriteBatch, _game.graphics, _game.camera, false));
  
            _objekts["bg1"].Scale = 3.0f;
            _objekts["bg2"].Scale = 3.0f;
            _objekts["bg3"].Scale = 3.0f;
            _objekts["bg4"].Scale = 3.0f;

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
                         new Player(new Sprite(_game.Content, "WalkingSquare", 1, 5),
                                        _game.spriteBatch,
                                        _game.graphics,
                                        _game.camera));

            _objekts["hero"].Position = new Vector2(10, 100); 
            _objekts["bg1"].Position = new Vector2(0,-100);
            _objekts["bg2"].Position = new Vector2(_objekts["bg1"].Position.X + _objekts["bg1"].Size.Width, -100);
            _objekts["bg3"].Position = new Vector2(_objekts["bg2"].Position.X + _objekts["bg2"].Size.Width, -100);
            _objekts["bg4"].Position = new Vector2(_objekts["bg3"].Position.X + _objekts["bg3"].Size.Width, -100);

            _objekts.Add("mouse-pointer", 
                         new Objekt(new Sprite(_game.Content, "mouse-pointer"), 
                                        _game.spriteBatch, _game.graphics, _game.camera, false));

            _objekts["mouse-pointer"].Position = new Vector2(0,0);
        }
    }
}

