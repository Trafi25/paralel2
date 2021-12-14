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
            public Node<T> Next { get; set; }

            int count;
            public T Data { get; set; }
            public Node(T data)
            {
                this.Data = data;
                Next = null; ;
            }
        }

        public Node<T> Head;
        public Node<T> Tail;
        public Node<T> Temp;


        public MishelAndScoth()
        {
            Head = new Node<T>(default);
            Tail = Head;

        }

        public void Push(T value)
        {
            var NewNode = new Node<T>(value);
            do
            {
                Temp = Head;
                if (Temp != null)
                {
                    NewNode.Next = Temp;
                }
                else
                {
                    CAS(ref Tail, Temp, NewNode);
                }
            } while (!CAS(ref Head, NewNode, Temp));
           
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
                    CAS(ref Tail, next, tempTail);
                }
                else
                {
                    result = next.Data;
                    if (CAS(ref Head, next, tempHead))
                    {
                        return true;
                    }
                }
            }
        }

        private bool CAS(ref Node<T> compare, Node<T> swapVal, Node<T> compareVal)
        {
            return Interlocked.CompareExchange<Node<T>>(ref compare, swapVal, compareVal) == compareVal;
        }


    }
    
}
