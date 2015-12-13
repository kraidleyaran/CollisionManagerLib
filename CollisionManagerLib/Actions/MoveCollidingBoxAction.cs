using CollisionEngineLib.Objects;
using Microsoft.Xna.Framework;

namespace CollisionManagerLib.Actions
{
    public class MoveCollidingBoxAction : ICollisionAction
    {
        public MoveCollidingBoxAction()
        {
            
        }
        public MoveCollidingBoxAction(string name, Collidable collidable, Vector2 position)
        {
            Name = name;
            Collidable = collidable;
            PositionX = position.X;
            PositionY = position.Y;
        }
        public string Name { get; set; }
        public Collidable Collidable { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public CollisionActionType CollisionActionType { get { return CollisionActionType.Move;} }
    }
}