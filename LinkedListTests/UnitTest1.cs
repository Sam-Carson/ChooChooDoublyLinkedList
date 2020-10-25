using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomLinkedList;

namespace LinkedListTests
{
    [TestClass]
    public class InsertFirst_Should
    {
        [TestMethod]
        public void ReplaceExistingFrontNode_WhenAFrontNodeExists()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;
            LinkedListNode<string> nodeOld;

            nodeOld = list.InsertFirst(new LinkedListNode<string>("FirstFirst"));

            //Act
            nodeNew = list.InsertFirst(new LinkedListNode<string>("NewFirst"));

            //Assert
            Assert.AreEqual(nodeNew.Next, nodeOld);
        }

        [TestMethod]
        public void AddNewFrontNode_WhenListIsEmpty()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;

            //Act
            nodeNew = list.InsertFirst(new LinkedListNode<string>("FirstFirst"));

            //Assert
            Assert.AreEqual(list.First, nodeNew);
        }

    }

    [TestClass]
    public class InsertLast_Should
    {
        [TestMethod]
        public void ReplaceExistingLastNode_WhenALastNodeExists()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;
            LinkedListNode<string> nodeOld;

            nodeOld = list.InsertFirst(new LinkedListNode<string>("OriginalLast"));

            //Act
            nodeNew = list.InsertLast(new LinkedListNode<string>("LastLast"));

            //Assert
            Assert.AreEqual(nodeNew.Prev, nodeOld);
        }

        [TestMethod]
        public void AddNewFirstNode_WhenListIsEmpty()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;

            //Act
            nodeNew = list.InsertLast(new LinkedListNode<string>("Last"));

            //Assert
            Assert.AreEqual(list.First, nodeNew);
        }

        [TestMethod]
        public void AddNewBackNode_WhenListIsEmpty()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;

            //Act
            nodeNew = list.InsertLast(new LinkedListNode<string>("Last"));

            //Assert
            Assert.AreEqual(list.Last, nodeNew);
        }
    }

    [TestClass]
    public class InsertAfter_Should
    {
        [TestMethod]
        public void ShouldBePointedToFromTargetNode_WhenInsertSucceeds()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeNew;
            LinkedListNode<string> existingNode;

            existingNode = list.InsertFirst(new LinkedListNode<string>("FirstFirst"));

            //Act
            nodeNew = list.InsertAfter(new LinkedListNode<string>("Next"), existingNode);

            //Assert
            Assert.AreEqual(existingNode.Next, nodeNew);
        }

        [TestMethod]
        public void ShouldPointToPreviousNext_WhenInsertSucceeds()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> nodeInBetween;
            LinkedListNode<string> nodeAhead = list.InsertFirst(new LinkedListNode<string>("Ahead"));
            LinkedListNode<string> nodeBehind = list.InsertAfter(new LinkedListNode<string>("Behind"), nodeAhead);

            //Act
            nodeInBetween = list.InsertAfter(new LinkedListNode<string>("InBetween"), nodeAhead);

            //Assert
            Assert.AreEqual(nodeInBetween.Next, nodeBehind);
        }

    }

    [TestClass]
    public class FindShould
    {
        [TestMethod]
        public void ReturnValue_WhenFound()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;
            LinkedListNode<string> expectedNode;
            LinkedListNode<string> actualNode;


            workNode = list.InsertFirst(new LinkedListNode<string>("One"));
            workNode = list.InsertAfter(new LinkedListNode<string>("Two"), workNode);
            expectedNode = list.InsertAfter(new LinkedListNode<string>("Three"), workNode);

            //Act
            actualNode = list.Find("Three");

            //Assert
            Assert.AreEqual(expectedNode, actualNode);
        }

        [TestMethod]
        public void ReturnNull_WhenNotFound()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;
            LinkedListNode<string> actualNode;


            workNode = list.InsertFirst(new LinkedListNode<string>("One"));
            workNode = list.InsertAfter(new LinkedListNode<string>("Two"), workNode);
            list.InsertAfter(new LinkedListNode<string>("Three"), workNode);

            //Act
            actualNode = list.Find("Four");

            //Assert
            Assert.AreEqual(null, actualNode);
        }
    }

    [TestClass]
    public class RemoveFirst_Should
    {
        [TestMethod]
        public void ChangeFrontPointer_WhenThereIsSomethingToRemove()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;

            workNode = list.InsertFirst(new LinkedListNode<string>("One"));
            workNode = list.InsertAfter(new LinkedListNode<string>("Two"), workNode);
            list.InsertAfter(new LinkedListNode<string>("Three"), workNode);
            int count = list.Count;

            //Act
            list.RemoveFirst();

            //Assert
            Assert.AreEqual(list.First.Data, "Two");
            Assert.AreEqual(count - 1, list.Count);

        }

        [TestMethod]
        public void ReturnNull_WhenListEmpty()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> result;

            //Act
            result = list.RemoveFirst();

            //Assert
            Assert.AreEqual(null, result);
            Assert.AreEqual(0, list.Count);

        }
    }

    [TestClass]
    public class RemoveLast_Should
    {
        [TestMethod]
        public void ChangeLastPointer_WhereThereIsSomethingToRemove()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;

            workNode = list.InsertFirst(new LinkedListNode<string>("One"));
            workNode = list.InsertAfter(new LinkedListNode<string>("Two"), workNode);
            list.InsertAfter(new LinkedListNode<string>("Three"), workNode);
            int count = list.Count;

            //Act
            list.RemoveLast();

            //Assert
            Assert.AreEqual(list.Last.Data, "Two");
            Assert.AreEqual(count - 1, list.Count);
        }
    }

    [TestClass]
    public class Remove_Should
    {
        [TestMethod]
        public void ChangePrevNextPointer_WhenThereSomethingToRemove()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;

            workNode = list.InsertFirst(new LinkedListNode<string>("Zero"));
            LinkedListNode<string> previousNode = list.InsertAfter(new LinkedListNode<string>("One"), workNode);
            LinkedListNode<string> doomedNode = list.InsertAfter(new LinkedListNode<string>("Two"), previousNode);
            list.InsertAfter(new LinkedListNode<string>("Three"), doomedNode);
            list.InsertAfter(new LinkedListNode<string>("Four"), workNode);
            int count = list.Count;

            //Act
            list.Remove(doomedNode);

            //Assert
            Assert.AreEqual(previousNode.Next.Data, "Three");
            Assert.AreEqual(count - 1, list.Count);
        }

        [TestMethod]
        public void RemoveFirst_WhenOnlyOne()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;

            workNode = list.InsertFirst(new LinkedListNode<string>("Zero"));

            //Act
            workNode = list.Remove(workNode);

            //Assert
            Assert.AreEqual(null, list.First);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ReturnNull_WhenNotFound()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> test = new LinkedListNode<string>("Help me!");
            LinkedListNode<string> result;
            int count = list.Count;

            //Act
            result = list.Remove(test);

            //Assert
            Assert.AreEqual(null, result);
            Assert.AreEqual(count, list.Count);
        }
    }


    [TestClass]
    public class Clear_Should
    {
        [TestMethod]
        public void ResetTheList()
        {
            //Arrange
            CustomLinkedList<string> list = new CustomLinkedList<string>();
            LinkedListNode<string> workNode;

            workNode = list.InsertFirst(new LinkedListNode<string>("One"));
            workNode = list.InsertAfter(new LinkedListNode<string>("Two"), workNode);
            list.InsertAfter(new LinkedListNode<string>("Three"), workNode);
            //Act
            list.Clear();

            //Assert
            Assert.IsNull(list.First, "First node should be null");
            Assert.AreEqual(0, list.Count, "Count should be zero.");
        }
    }
}
