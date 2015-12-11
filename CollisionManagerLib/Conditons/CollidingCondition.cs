namespace CollisionManagerLib.Conditons
{
    public class CollidingCondition : ICollisionCondition
    {
        public string Name { get; set; }
        public CollisionConditionType ConditionType { get { return CollisionConditionType.Object;} }
        public string FirstObject { get; set; }
        public Colliding Colliding { get; set; }
        public string SecondObject { get; set; }
        public Direction Direction { get; set; }
    }
}