using System;
using CollisionEngineLib;
using CollisionEngineLib.Objects;
using CollisionManagerLib;
using CollisionManagerLib.Actions;
using CollisionManagerLib.Conditons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using Assert = NUnit.Framework.Assert;

namespace CollisionManagerTests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(TestName = "Evaluating a Colliding Condition works correctly")]
        public void ExecuteCollidingCondition_ReplyTrue()
        {
            CollisionManager collisionManager =
                new CollisionManager(new CollisionEngine(new QuadTree(new Vector2(1000, 1000), 1)));
            collisionManager.CollisionEngine.Add(new QuadTreePositionItem(new Collidable("test object 1"),new Vector2(0, 0), new Vector2(10, 10)));
            collisionManager.CollisionEngine.Add(new QuadTreePositionItem(new Collidable("test object 2"), new Vector2(20, 20), new Vector2(10, 10)));

            CollidingCondition condition = new CollidingCondition
            {
                Name = "new",
                FirstObject = "test object 1",
                SecondObject = "test object 2",
                Colliding = Colliding.No
            };
            collisionManager.EvaluateCondition(condition);
            Assert.IsTrue(collisionManager.EvaluateCondition(condition));

            collisionManager.CollisionEngine.Move("test object 2", new Vector2(7, 7));
            Assert.IsFalse(collisionManager.EvaluateCondition(condition));
        }

        [TestCase(TestName = "Executing an AddCollidingBox action works correctly")]
        public void ExecuteAddCollidingBoxAction()
        {
            CollisionManager collisionManager = new CollisionManager(new CollisionEngine(new QuadTree(new Vector2(1000, 1000), 1)));
            AddCollidingBoxAction action = new AddCollidingBoxAction
            {
                Name = "create new box",
                Collidable = new Collidable("new box"),
                Height = 10,
                Width = 10,
                PositionX = 0,
                PositionY = 0
            };
            collisionManager.ExecuteAction(action);
            Assert.IsTrue(collisionManager.CollisionEngine.DoesItemExist("new box"));
        }

        [TestCase(TestName = "Executing a RemoveCollidingBox action works correctly")]
        public void ExecuteRemoveCollidingBoxAction()
        {
            CollisionManager collisionManager = new CollisionManager(new CollisionEngine(new QuadTree(new Vector2(1000, 1000), 1)));
            collisionManager.CollisionEngine.Add(new QuadTreePositionItem(new Collidable("new box"), new Vector2(0, 0), new Vector2(10, 10)));
            RemoveCollidingBoxAction action = new RemoveCollidingBoxAction
            {
                Name = "remove new box",
                Collidable = new Collidable("new box"),
            };
            collisionManager.ExecuteAction(action);
            Assert.IsFalse(collisionManager.CollisionEngine.DoesItemExist("new box"));
        }

        [TestCase(TestName = "Executing a MoveCollidingBox action works correctly")]
        public void ExecuteMoveCollidingBoxAction()
        {
            CollisionManager collisionManager = new CollisionManager(new CollisionEngine(new QuadTree(new Vector2(1000, 1000), 1)));
            collisionManager.CollisionEngine.Add(new QuadTreePositionItem(new Collidable("new box"), new Vector2(0, 0), new Vector2(10, 10)));
            MoveCollidingBoxAction action = new MoveCollidingBoxAction
            {
                Name = "move new box",
                Collidable = new Collidable("new box"),
                PositionX = 10,
                PositionY = 7
            };
            collisionManager.ExecuteAction(action);
            Vector2 position = collisionManager.CollisionEngine.GetPoisitionOfItem("new box");
            Assert.AreEqual(position.X, 10);
            Assert.AreEqual(position.Y, 7);
        }
    }
}
