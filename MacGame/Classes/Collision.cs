using System;

namespace maker {
    public enum CollisionSide
    {
        Top, 
        Left, 
        Right, 
        Bottom
    }

    public class Collision {
        public Objekt CollidedObjekt{ get; set; }
        public CollisionSide Side{ get; set; }


        public Collision () {

        }

        public static Collision Collided(Objekt o1, Objekt o2)
        {
            if(Utility.BoundingCollision3(o1, o2)){
                Collision collided = new Collision();
                o1._bottom.X = o1._top.X = o1.Bounds.X + o1.Bounds.Width/4;
                o1._left.X = o1.Bounds.X;
                o1._top.Y = o1.Bounds.Y;
                o1._bottom.Y = o1.Bounds.Y+o1.Bounds.Height-o1.Bottom.Height;
                o1._right.Y = o1._left.Y = o1._top.Y+o1._top.Height;
                o1._right.X = o1.Bounds.X+o1._left.Width;

                //o1.Bottom.Location = o1.Bounds.Location;

                if(o1.Bottom.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Bottom;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Bottom: " + o2.GetType().Name);
                    return collided;
                }

                if(o1.Top.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Top;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Top: " + o2.GetType().Name);
                    return collided;
                }

                
                if(o1._right.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Right;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Right: " + o2.GetType().Name);
                    return collided;
                }

                if(o1._left.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Left;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Left: " + o2.GetType().Name);
                    return collided;
                }
                
                //return collided;
                //top
//                if( o1.Bounds.Y - 10 >= o2.Bounds.Y + o2.Bounds.Height && 
//                   o1.Bounds.X < o2.Bounds.X + o2.Bounds.Width && 
//                   o1.Bounds.X + o1.Bounds.Width > o2.Bounds.X )
//                {
//                    collided.Side = CollisionSide.Top;
//                    collided.CollidedObjekt = o2;
//                    System.Console.WriteLine("Top: " + o2.GetType().Name);
//                    return collided;
//                }
//                //bottom
//                if((o1.Bounds.Y + o1.Bounds.Height - 10) <= o2.Bounds.Y && 
//                   o1.Bounds.X < (o2.Bounds.X + o2.Bounds.Width) && 
//                   (o1.Bounds.X + o1.Bounds.Width) > o2.Bounds.X)
//                {
//                    collided.Side = CollisionSide.Bottom;
//                    collided.CollidedObjekt = o2;
//                    System.Console.WriteLine("Bottom: " + o2.GetType().Name);
//                    return collided;
//                }
//                //left
//                if(o1.Bounds.X + 1 >= o2.Bounds.X + o2.Bounds.Width && 
//                   o1.Bounds.Y < o2.Bounds.Y + o2.Bounds.Height && 
//                   o1.Bounds.Y + o1.Bounds.Height > o2.Bounds.Y )
//                {
//                    collided.Side = CollisionSide.Left;
//                    collided.CollidedObjekt = o2;
//                System.Console.WriteLine("Left: " + o2.GetType().Name);
//                    return collided;
//                }
//                //right
//                if( o1.Bounds.X + o1.Bounds.Width - 1 <= o2.Bounds.X && 
//                   o1.Bounds.Y < o2.Bounds.Y + o2.Bounds.Height && 
//                   o1.Bounds.Y + o1.Bounds.Height > o2.Bounds.Y )
//                {
//                    collided.Side  = CollisionSide.Right;
//                    collided.CollidedObjekt = o2;
//                System.Console.WriteLine("Right: " + o2.GetType().Name);
//                    return collided;
//                }

            }
            return null;
        }
    }
}

