using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class ListNode
    {
        public ListNode(string data)
        {
            Data = data;
        }

        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
    }

    public class ListRandom : IEnumerable
    {
        ListNode Head;
        ListNode Tail;
        int _count;

        public int Count { get { return _count; } }
        public bool IsEmpty { get { return _count == 0; } }


        public void Serialize(Stream s)
        {
        }
        public void Deserialize(Stream s)
        {
        }

        public void Add(string data)
        {
            ListNode node = new ListNode(data);
            if (Head == null)
                Head = node;
            else
                Tail.Next = node;
            Tail = node;
            _count++;
        }

        public bool Remove(string data)
        {
            ListNode current = Head;
            ListNode previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            Tail = previous;
                    }
                    else
                    {
                        Head = Head.Next;
                        if (Head == null)
                            Tail = null;
                    }
                    _count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            _count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            ListNode current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
