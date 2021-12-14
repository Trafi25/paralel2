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
        public Node<T> Temp;


        public MishelAndScoth()
        {
            Head = new Node<T>(default, null);
            Tail = Head;

        }

        public void Push(T value)
        {
            var NewNode = new Node<T>(value, null);
            do
            {
                Temp = Head;
                if (Temp != null)
                {
                    NewNode.Next = Temp;
                }
                else
                {
                    Interlocked.CompareExchange(ref Tail, Temp, NewNode);
                }
            } while (Interlocked.CompareExchange(ref Head, NewNode, Temp) != Temp);
           
        }

        public bool Delete(out T result)
        {           
            while (true)
            {
                var tempHead = Head;
                var tempTail = Tail;
               
                var next = tempHead.Next;                           

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
