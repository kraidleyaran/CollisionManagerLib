using CollisionEngineLib.Objects;

namespace CollisionManagerLib.Actions
{
    public class RemoveCollidingBoxAction : ICollisionAction
    {
        public RemoveCollidingBoxAction()
        {
            
        }

        public RemoveCollidingBoxAction(string name, Collidable collidable)
        {
            Name = name;
            Collidable = collidable;
        }
        public string Name { get; set; }
        public Collidable Collidable { get; set; }
        public CollisionActionType CollisionActionType { get { return CollisionActionType.Remove;} }
    }
}