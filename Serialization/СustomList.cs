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
        public int index;
        public string Data;
    }

    public class ListRandom : IEnumerable
    {
        ListNode Head;
        ListNode Tail;
        List<ListNode> listNodes = new List<ListNode>(50);
        Random random = new Random();
        int _count;

        public int Count { get { return _count; } }
        public bool IsEmpty { get { return _count == 0; } }


        public void Serialize(Stream s)
        {
            List<byte> value = new List<byte>();
            foreach (var item in listNodes)
            {                
                value.AddRange(Encoding.UTF8.GetBytes("\""+ item.Data.Replace("\"", "\\\"") + "\"" + Environment.NewLine + item.Random.index + Environment.NewLine));
            }
            s.Write(value.ToArray(), 0, value.Count);
        }

        public void Deserialize(Stream s)
        {
            List<ListNode> spareList = new List<ListNode>(listNodes);
            try
            {
                Clear();
                listNodes.Clear();
                using (var sr = new StreamReader(s, Encoding.UTF8))
                {
                    bool isOdd = false;
                    List<int> index = new List<int>();
                    while (!sr.EndOfStream)
                    {
                        string text = sr.ReadLine();
                        if (isOdd)
                        {
                            index.Add(int.Parse(text));
                            isOdd = false;
                        }
                        else
                        {
                            Add(text.Trim('\"'));
                            isOdd = true;
                        }
                    }
                    for (int i = 0; i < listNodes.Count; i++)
                    {
                        listNodes[i].Random = listNodes[index[i]];
                    }
                }
            }
            catch (Exception)
            {
                foreach (var node in spareList)
                {
                    listNodes.Add(node);
                    if (Head == null)
                    {
                        Head = node;
                        Head.Random = node;
                    }
                    else
                    {
                        Tail.Next = node;
                        node.Previous = Tail;
                        node.Random = listNodes[random.Next(0, _count)];
                    }
                    Tail = node;
                    node.index = _count;
                    _count++;
                }
            }

        }

        public void Add(string data)
        {
            ListNode node = new ListNode(data);
            listNodes.Add(node);
            if (Head == null)
            {
                Head = node;
                Head.Random = node;
            }
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
                node.Random = listNodes[random.Next(0, _count)];
            }
            Tail = node;
            node.index = _count;
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

        public IEnumerable BackEnumerator()
        {
            ListNode current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}
