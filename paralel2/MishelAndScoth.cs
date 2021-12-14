using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace paralel2
{
    public class MishelAndScoth<T>
    {
        public class Node<T>
        {
            public Node<T> Next;

            int count;
            public T Data { get; set; }
            public Node(T data, Node<T> next)
            {
                this.Data = data;
                this.Next = next;
            }
        }

        public Node<T> Head;
        public Node<T> Tail;


       public MishelAndScoth()
        {
            Head = new Node<T>(default, null);
            Tail = Head;

        }

        public void Push(T value)
        {
            var NewNode = new Node<T>(value, null);

            while (true)
            {
                var tempTail = Tail;
               
                var tailNext = tempTail.Next;
                
                if (tailNext == null)
                {
                    if (Interlocked.CompareExchange(ref tempTail.Next, NewNode, tailNext) != tailNext)
                    {
                        continue;
                    }
                    Interlocked.CompareExchange(ref Tail, NewNode, tempTail);
                    return;
                }
                else
                {
                    Interlocked.CompareExchange(ref Tail, tailNext, tempTail);
                    continue;
                }
            }
        }

        public bool Delete(out T result)
        {           
            while (true)
            {
                var tempHead = Head;
                var tempTail = Tail;
               
                var next = tempHead.Next;

                if (tempHead != Head) continue;
                if (next == null)
                {
                    result = default(T);
                    return false;
                }

                if (tempHead == tempTail)
                {
                    Interlocked.CompareExchange(ref Tail, next, tempTail);
                }
                else
                {
                    result = next.Data;
                    if (Interlocked.CompareExchange(ref Head, next, tempHead) == tempHead)
                    {
                        return true;
                    }
                }
            }
        }
    }
    
}
