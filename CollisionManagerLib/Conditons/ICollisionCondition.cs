namespace CollisionManagerLib.Conditons
{
    public interface ICollisionCondition
    {
        string Name { get; set; }
        CollisionConditionType ConditionType { get;}
    }
}