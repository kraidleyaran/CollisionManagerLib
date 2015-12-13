using CollisionEngineLib.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollisionManagerLib.Actions
{
    public class AddCollidingBoxAction : ICollisionAction
    {
        public AddCollidingBoxAction()
        {
            
        }

        public AddCollidingBoxAction(string name, Collidable collidable, Vector2 position, Vector2 size)
        {
            Name = name;
            Collidable = collidable;
            PositionX = position.X;
            PositionY = position.Y;
            Height = size.Y;
            Width = size.X;
        }
        public string Name { get; set; }
        public Collidable Collidable { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public CollisionActionType CollisionActionType { get { return CollisionActionType.Add;} }


    }
}