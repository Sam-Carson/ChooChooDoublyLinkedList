using System;
using System.Dynamic;

namespace CustomLinkedList
{
    public class LinkedListNode<T>
    {
        public T Data;
        public LinkedListNode<T> Next { get; internal set; }   //Internal set: Next can only be modified from within the assembly.  That way, CustomSinglyLinkedList methods
                                                               //can modify Next, but callers (such as the methods in the unit test and ChooChoo projects) can look
        public LinkedListNode<T> Prev { get; internal set; }                                                    //but no touch.

        public LinkedListNode(T data)
        {
            this.Data = data;
        }

    }

    public class CustomLinkedList<T>
    {
        public LinkedListNode<T> First { get; private set; }    //Private set: First can only be modified within the class.
        public LinkedListNode<T> Last { get; private set; }

        public int Count { get; private set; } = 0;
        public LinkedListNode<T> InsertFirst(LinkedListNode<T> newNode)
        {
            if (First == null || this.Count == 0)
            {
                First = newNode;
                Last = newNode; 
            }
            else
            {
                newNode.Next = First;
                newNode.Prev = null;
                newNode.Next.Prev = newNode;
                First = newNode;
            }
            Count++;
            return newNode;
        }

        public LinkedListNode<T> InsertLast(LinkedListNode<T> newNode)
        {
            if (Last == null || this.Count == 0)
            {
                First = newNode;
                Last = newNode;
            }
            else
            {
                newNode.Prev = Last;
                newNode.Next = null;
                newNode.Prev.Next = newNode;
                Last = newNode;
            }
            Count++;
            return newNode;
        }

        public LinkedListNode<T> InsertAfter(LinkedListNode<T> newNode, LinkedListNode<T> existingNode)
        {
            if (existingNode == Last)
            {
                InsertLast(newNode);
            }
            else
            {
                newNode.Next = existingNode.Next;
                newNode.Prev = existingNode;
                if (newNode.Next != null)
                {
                    newNode.Next.Prev = newNode;
                    existingNode.Next = newNode;
                }
            }
            Count++;

            return newNode;
        }

        public LinkedListNode<T> InsertBefore(LinkedListNode<T> newNode, LinkedListNode<T> existingNode)
        {
            if (existingNode == First)
            {
                InsertFirst(newNode);
            }
            else
            {
                newNode.Prev = existingNode.Prev;
                newNode.Next = existingNode;
                if (newNode.Prev != null)
                {
                    newNode.Prev.Next = newNode;
                    existingNode.Prev = newNode;
                }
            }
            Count++;
            return newNode;
        }

        public LinkedListNode<T> Find(T nodeValue)
        {
            LinkedListNode<T> currNode = First;

            while (currNode != null && !(currNode.Data.Equals(nodeValue)))
            {
                currNode = currNode.Next;
            }

            return currNode;
        }

        public LinkedListNode<T> RemoveFirst()
        {
            if (First == null || Count == 0) return null;

            LinkedListNode<T> doomedNode = First;
            First = First.Next;
            Count--;
            return doomedNode;
        }

        public LinkedListNode<T> RemoveLast()
        {
            if (First == null || Count == 0) return null;

            LinkedListNode<T> doomedNode = Last;
            Last = Last.Prev;
            Last.Next = null;
            Count--;
            return doomedNode;
        }

        public LinkedListNode<T> Remove(LinkedListNode<T> doomedNode)
        {
            if (First == null) return null;
            if (First == doomedNode)
            {
                RemoveFirst();
                Count++;
            }
            else if(Last == doomedNode)
            {
                RemoveLast();
            }
            else
            {
                doomedNode.Next.Prev = doomedNode.Prev;
                doomedNode.Prev.Next = doomedNode.Next;
            }
            Count--;
            return doomedNode;
        }

        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
        }
    }
}

