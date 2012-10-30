using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
  public class Player : Objekt
  {
    public int Speed { get; set; }
    public Dictionary<string, bool> playerStates;
    public int JumpHeight { get; set; }
    private int jumpCounter;
    private int jumpSpeed = 10;

    public Player (MacGame game)
    {
        playerStates = new Dictionary<string, bool>();
        _sprites = new Dictionary<string, Sprite>();
        _spriteBatch = game.spriteBatch;
        _graphics = game.graphics;
        _camera = game.camera;
        Speed = 10;
        JumpHeight = 14;
        this.AddSprite("right",
                        new Sprite(game.Content, 
                        "maker_walk",1,4));
        this.AddSprite("left",
                           new Sprite(game.Content, 
                       "maker_walk_left",1,4));
      
        this.SelectedAction = "right";

        playerStates.Add("UP", false);
        playerStates.Add("DOWN", false);
        playerStates.Add("LEFT", false);
        playerStates.Add("RIGHT", false);
        playerStates.Add("JUMP", false);
        playerStates.Add("RUN", false);
        playerStates.Add("FALL", false);
        playerStates.Add("DEAD", false);
      //this.Position = new Vector2(this.Position.X - Speed, this.Position.Y);
    }

    private void MoveRight()
    {
      this.Position = new Vector2(this.Position.X + Speed, this.Position.Y);
      playerStates["RIGHT"] = false;
            this.SelectedAction = "right";
      this.Update();
    }

    private void MoveLeft()
    {
        this.Position = new Vector2(this.Position.X - Speed, this.Position.Y);
        this.SelectedAction = "left";
        playerStates["LEFT"] = false;
        this.Update();
    }

    private void Jump()
    {
      this.Position = new Vector2(this.Position.X, this.Position.Y - jumpSpeed);
      jumpCounter++;
      if(jumpCounter >= JumpHeight){
        jumpCounter = 0;
        playerStates["JUMP"] = false;
        playerStates["FALL"] = true;
      }
    }

    private void Fall()
    {
      this.Position = new Vector2(this.Position.X, this.Position.Y + jumpSpeed);
      jumpCounter = 0; 
      /*jumpCounter--;
      if(jumpCounter == JumpHeight){
        jumpCounter = 0;
        playerStates["FALL"] = false;
      }*/
    }

    public void Actions()
    {
      if(playerStates["LEFT"] == true){
        MoveLeft();
      }

      if(playerStates["RIGHT"] == true){
        MoveRight();
      }

      if(playerStates["JUMP"] == true){
        Jump();
      }

      if(playerStates["FALL"] == true){
        Fall();
      }
    }
  }
}

