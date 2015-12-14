using System;
using System.Collections.Generic;
using CollisionEngineLib;
using CollisionEngineLib.Objects;
using CollisionManagerLib.Actions;
using CollisionManagerLib.Conditons;
using Microsoft.Xna.Framework;
using Direction = CollisionEngineLib.Objects.Direction;

namespace CollisionManagerLib
{
    public class CollisionManager
    {
        public CollisionEngine CollisionEngine;

        private Dictionary<string, ICollisionCondition> conditions = new Dictionary<string, ICollisionCondition>();
        private Dictionary<string, ICollisionAction> actions = new Dictionary<string, ICollisionAction>();

        public CollisionManager(CollisionEngine engine)
        {
            CollisionEngine = engine;
        }

        public bool AddCondition(ICollisionCondition condition)
        {
            if (conditions.ContainsKey(condition.Name)) return false;
            conditions.Add(condition.Name, condition);
            return true;
        }

        public bool RemoveCondition(string name)
        {
            return conditions.Remove(name);
        }

        public bool DoesConditionExist(string name)
        {
            return conditions.ContainsKey(name);
        }
        public void ClearConditions()
        {
            conditions = new Dictionary<string, ICollisionCondition>();
        }

        public bool AddAction(ICollisionAction action)
        {
            if (actions.ContainsKey(action.Name)) return false;
            actions.Add(action.Name, action);
            return true;
        }

        public bool RemoveAction(string name)
        {
            return actions.Remove(name);
        }

        public bool DoesActionExist(string name)
        {
            return actions.ContainsKey(name);
        }

        public void ClearActions()
        {
            actions = new Dictionary<string, ICollisionAction>();
        }

        public bool EvaluateCondition(string name)
        {
            if (!conditions.ContainsKey(name)) throw new Exception("Condition " + name + " does not exist");
            return EvaluateCondition(conditions[name]);
        }
        public bool EvaluateCondition(ICollisionCondition condition)
        {
            switch (condition.ConditionType)
            {
                case CollisionConditionType.Object:
                    CollidingCondition collidingCondition = (CollidingCondition) condition;
                    return EvaluateCollidingCondition(collidingCondition);
            }
            return false;
        }

        private bool EvaluateCollidingCondition(CollidingCondition condition)
        {
            CollisionResponse response = CollisionEngine.CheckCollisionResponse(condition.FirstObject, condition.SecondObject);
            switch (condition.Colliding)
            {
                case Colliding.No:
                    switch (response.Collided)
                    {
                        case true:
                            return false;
                        case false:
                            return true;
                    }
                    break;
                case Colliding.Yes:
                    switch (response.Collided)
                    {
                        case true:
                            switch (condition.Direction)
                            {
                                case Conditons.Direction.Any:
                                    return response.Sides.Count > 0;
                                default:
                                    return response.Sides.Contains(ConvertToEngineDirection(condition.Direction));
                            }
                        case false:
                            return false;
                    }
                    break;
            }
            return false;
        }


        public bool ExecuteAction(string name)
        {
            if (!actions.ContainsKey(name)) throw new Exception("Action " + name + " does not exist");
            return ExecuteAction(actions[name]);
        }
        public bool ExecuteAction(ICollisionAction action)
        {
            switch (action.CollisionActionType)
            {
                case CollisionActionType.Add:
                    AddCollidingBoxAction addAction = (AddCollidingBoxAction) action;
                    return ExecuteAddCollidingBoxAction(addAction);
                case CollisionActionType.Remove:
                    RemoveCollidingBoxAction removeAction = (RemoveCollidingBoxAction) action;
                    return ExecuteRemoveCollidingBoxAction(removeAction);
                case CollisionActionType.Move:
                    MoveCollidingBoxAction moveAction = (MoveCollidingBoxAction) action;
                    return ExecuteMoveCollidingBoxAction(moveAction);
            }
            return false;
        }

        private bool ExecuteAddCollidingBoxAction(AddCollidingBoxAction action)
        {
            if (CollisionEngine.DoesItemExist(action.Collidable.Name)) return false;
            QuadTreePositionItem item = new QuadTreePositionItem(action.Collidable, new Vector2(action.PositionX, action.PositionY), new Vector2(action.Width, action.Height));
            return CollisionEngine.Add(item);
        }

        private bool ExecuteRemoveCollidingBoxAction(RemoveCollidingBoxAction action)
        {
            return CollisionEngine.Remove(action.Collidable.Name);
        }

        private bool ExecuteMoveCollidingBoxAction(MoveCollidingBoxAction action)
        {
            return CollisionEngine.DoesItemExist(action.Collidable.Name) && CollisionEngine.Move(action.Collidable.Name, new Vector2(action.PositionX, action.PositionY));
        }

        private Direction ConvertToEngineDirection(Conditons.Direction direction)
        {
            switch (direction)
            {
                case Conditons.Direction.East:
                    return Direction.East;
                case Conditons.Direction.North:
                    return Direction.North;
                case Conditons.Direction.West:
                    return Direction.West;
                case Conditons.Direction.South:
                    return Direction.South;
                case Conditons.Direction.Surround:
                    return Direction.Surround;
                case Conditons.Direction.Inside:
                    return Direction.Inside;
                default:
                    throw new Exception("Cannot convert Any to a direction");
            }
        }
        
    }
}
