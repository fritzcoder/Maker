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

        public static bool BoundingCollision2(Objekt a, Objekt b)
        {
            // check if their bounding boxes touch
            if( b.Bounds.X + b.Bounds.Width < a.Bounds.X )
            {
                return false;
            }

            if( b.Bounds.X > a.Bounds.X + a.Bounds.Width )
            {
                return false;
            }
            
            if( b.Bounds.Y + b.Bounds.Height< a.Bounds.Y )
            {
                return false;
            }

            if( b.Bounds.Y > a.Bounds.Y + a.Bounds.Height )
            {
                return false;
            }
            
            // bounding boxes intersect
            return true;
        }

        public static bool BoundingCollision3(Objekt a, Objekt b)
        {
            if(a.Bounds.Intersects(b.Bounds))
                return true;

            return false;
        }
    }
}

