using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
  public class TestEnemy : Objekt
  {
    public int Speed { get; set; }
    public Dictionary<string, bool> playerStates;
    public int JumpHeight { get; set; }
    //private int jumpCounter;
    //private int jumpSpeed = 10;

    public TestEnemy (Sprite sprite, 
                      SpriteBatch spriteBatch,
                      GraphicsDeviceManager graphics,
                      Camera camera)
    {
        playerStates = new Dictionary<string, bool>();
        _sprite = sprite;
        _spriteBatch = spriteBatch;
        _graphics = graphics;
        _camera = camera;
        Speed = 5;
        JumpHeight = 30;
        
        playerStates.Add("UP", false);
        playerStates.Add("DOWN", false);
        playerStates.Add("LEFT", false);
        playerStates.Add("RIGHT", false);
        playerStates.Add("JUMP", false);
        playerStates.Add("RUN", false);
        playerStates.Add("FALL", false);
        playerStates.Add("DEAD", false);
        
        this.Position = new Vector2(this.Position.X - Speed, this.Position.Y);
    }
  }
}

