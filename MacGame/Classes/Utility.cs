using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace maker
{
  public static class Utility
  {
    public static bool BoundingCollision(Objekt a, Objekt b)
    {

      Rectangle rectangleA = a.Bounds;
      Rectangle rectangleB = b.Bounds;
      // Find the bounds of the rectangle intersection
      int top = Math.Max(rectangleA.Top, rectangleB.Top);
      int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
      int left = Math.Max(rectangleA.Left, rectangleB.Left);
      int right = Math.Min(rectangleA.Right, rectangleB.Right);
      
      if (top >= bottom || left >= right)
        return false;
      
      return true;
    }
  }
}

