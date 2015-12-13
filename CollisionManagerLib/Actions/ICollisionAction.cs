using CollisionEngineLib.Objects;

namespace CollisionManagerLib.Actions
{
    public interface ICollisionAction
    {
        string Name { get; set; }
        Collidable Collidable { get; set; }
        CollisionActionType CollisionActionType { get; }
    }
}