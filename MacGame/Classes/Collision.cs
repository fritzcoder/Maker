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

                
                if(o1.Right.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Right;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Right: " + o2.GetType().Name);
                    return collided;
                }

                if(o1.Left.Intersects(o2.Bounds))
                {
                    collided.Side = CollisionSide.Left;
                    collided.CollidedObjekt = o2;
                    System.Console.WriteLine("Left: " + o2.GetType().Name);
                    return collided;
                }
            }

            return null;
        }
    }
}

