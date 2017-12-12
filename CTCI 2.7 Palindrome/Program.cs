using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_2._7_Palindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(2, 7, "Check if a linked list is a palindrome");

            Node head = CreateSinglyLinkedList(1000);

            CreatePalindrome(ref head);

            CheckIfPalindrome_Reverse(head);

            CheckIfPalindrome_Iterative(head);

            CheckIfPalindrome_Recursive(head);

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// 1. Push values from the 1st half of the list onto a stack
        /// 2. If the original list has odd number of nodes, skip the middle node
        /// 2. Compare the 2nd half of the original to values on the stack
        /// 3. If any two values don't match, report false
        /// 4. If no mismatches found, report true
        /// 
        /// Complexity:     Algorithm runs in O(N) time        
        ///                 Half the nodes are read & copied
        ///                 Half the nodes are read & compared
        ///                 
        ///                 Requires O(N) memory
        ///                 As input grows, memory requirement grows
        ///                 Half of the input list is copied. Note that only 
        ///                 values are copied, and not node objects. This should
        ///                 be much smaller than the "reverse" algorithm which 
        ///                 builds a new list of objects.
        /// 
        /// </summary>
        /// <param name="passed_head"></param>
        private static void CheckIfPalindrome_Iterative(Node head)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Stack<int> stack = new Stack<int>();

            // Push the first half of the list onto a stack
            Node runner_slow = head;
            Node runner_fast = head;
            while ((runner_fast != null) && (runner_fast.next != null))
            {
                stack.Push(runner_slow.Data);

                runner_slow = runner_slow.next;
                runner_fast = runner_fast.next.next;
            }

            // check for odd-numbered list. skip middle node if so.
            if (runner_fast != null)
            {
                runner_slow = runner_slow.next;
            }

            // Pop the list off the stack, compare to last half of list
            while (runner_slow != null)
            {
                if (runner_slow.Data != stack.Pop())
                {
                    sw.Stop();

                    Console.WriteLine("Iterative: Linked list is not a palindrome. (" + sw.ElapsedTicks + " ticks)");
                    return;
                }

                runner_slow = runner_slow.next;
            }

            sw.Stop();

            Console.WriteLine("Iterative: Linked list is a palindrome. (" + sw.ElapsedTicks + " ticks)");
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// 1. Create a new, reversed list from the 1st half of the original
        /// 2. If the original list has odd number of nodes, skip the middle node
        /// 2. Compare the 2nd half of the original to the new, reversed list
        /// 3. If any two nodes don't match, report false
        /// 4. If no mismatches found, report true
        /// 
        /// Complexity:     Algorithm runs in O(N) time        
        ///                 Half the nodes are read & copied
        ///                 Half the nodes are read & compared
        ///                 
        ///                 Requires O(N) memory
        ///                 As input grows, memory requirement grows
        ///                 Half of the input list is copied, so the algo requires 
        ///                 half as much memory as the input represents.
        /// 
        /// </summary>
        /// <param name="passed_head"></param>
        private static void CheckIfPalindrome_Reverse(Node passed_head)
        {
            Stopwatch sw = Stopwatch.StartNew();

            // build new, reversed list from first half of passed_head
            Node runner_slow = passed_head;
            Node runner_fast = passed_head;
            Node rev_head = null;
            while ((runner_fast != null) && (runner_fast.next != null))
            {
                rev_head = new Node(runner_slow.Data, rev_head);

                runner_slow = runner_slow.next;
                runner_fast = runner_fast.next.next;
            }

            // check for odd-numbered list. skip middle node if so.
            if (runner_fast != null)
            {
                runner_slow = runner_slow.next;
            }

            // compare the 2nd half of the original list
            // to the new, reversed list (which is the 1st half reversed)            
            while (runner_slow != null)
            {
                if (runner_slow.Data != rev_head.Data)
                {
                    sw.Stop();

                    Console.WriteLine("Reverse: List is not a palindrome. (" + sw.ElapsedTicks + " ticks)");
                    return;
                }

                runner_slow = runner_slow.next;
                rev_head = rev_head.next;
            }

            sw.Stop();

            Console.WriteLine("Reverse: List is a palindrome. (" + sw.ElapsedTicks + " ticks)");
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// This recursive algorithm is the book solution
        /// 
        /// 1. Get the length of the list
        /// 2. Call the recursive function with the head node and length value
        /// 3. Recursive function calls itself with the next node and (length - 2)
        /// 4. When length is 1 or 2, the algo has reached the middle of the list.
        ///    Return true (if middle node) or compare the current node value to 
        ///    the next value (if no middle node)
        ///    Also return the next (or next-next) node.
        /// 5. During the stack unspool, compare the current node to the returned 
        ///    node. Return result and the next node.
        /// 
        /// NOTE: the return value is an object containing a node object and 
        ///       a boolean value.
        /// 
        /// Complexity:     Runs in 0(N) time
        ///                 Every node is touched once
        ///                 
        ///                 Requires O(N) memory
        ///                 Every recursive call requires a frame on the stack
        ///                 Stack overflow will occur if there are too many nodes
        /// 
        /// </summary>
        /// <param name="head"></param>
        private static void CheckIfPalindrome_Recursive(Node head)
        {
            int length = GetLength(head);

            Stopwatch sw = Stopwatch.StartNew();

            Result rslt = Recurse(head, length);

            sw.Stop();

            if (rslt.result == true)            
                Console.WriteLine("Recursive: list is palindrome. (" + sw.ElapsedTicks + " ticks)");            
            else
                Console.WriteLine("Recursive: is not palindrome. (" + sw.ElapsedTicks + " ticks)");
        }

        private static Result Recurse(Node head, int length)
        {
            if ((head == null) || (length == 0))
                return new Result(null, true);
            else if (length == 1)
                // middle node (if it exists)
                // no need to compare it to itself. Move to next node  & return true
                return new Result(head.next, true); 
            else if (length == 2)
                // no middle node, last node of 1st half
                // compare to the next node, the 1st node of 2nd half
                // return node after next
                return new Result(head.next.next, head.Data == head.next.Data); 

            // call Recurse() for every node in the first half of the list
            Result rslt = Recurse(head.next, length - 2);

            if ((rslt.result == false) || (rslt.node == null))
                return rslt;

            // compare the current node (1st half of list) 
            // to the returned node (from 2nd half of list)
            rslt.result = (head.Data == rslt.node.Data);

            // set the node for the next return while unspooling the stack
            rslt.node = rslt.node.next; 
            return rslt;
        }

        private static int GetLength(Node head)
        {
            Node runner = head;
            int counter = 0;
            while (runner != null)
            {
                ++counter;
                runner = runner.next;
            }
            return counter;
        }

        private static Node CreateSinglyLinkedList(int count)
        {
            if (count < 1)
                return null;

            Random rnd = new Random();

            Node head = new Node(rnd.Next(0, 1000));

            Node n = head;

            for (int i = 0; i < count - 1; ++i)
            {
                n.next = new Node(rnd.Next(0, 1000));
                n = n.next;
            }

            Console.WriteLine("List created with " + count + " nodes");
            Console.WriteLine();

            return head;
        }

        private static void CreatePalindrome(ref Node passed_head)
        {
            Node runner = passed_head;
            Node rev_head = null;

            // create odd-numbered list
            //rev_head = new Node(9999, rev_head);

            // build reversed list from passed_head
            while (runner != null)
            {
                rev_head = new Node(runner.Data, rev_head);
                runner = runner.next;
            }

            while (rev_head != null)
            {
                passed_head.ApppendToTail(rev_head.Data);
                rev_head = rev_head.next;
            }
        }

        private static void PrintNodes(Node passed_n)
        {
            while (passed_n != null)
            {
                Console.Write(passed_n.Data + ", ");

                passed_n = passed_n.next;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    class Node
    {
        public Node next = null;

        public int Data;

        public Node(int d) => Data = d;

        public Node(int d, Node n)
        {
            Data = d;
            next = n;
        }

        public void ApppendToTail(int d)
        {
            Node n = this;

            while (n.next != null)
            {
                n = n.next;
            }

            n.next = new Node(d);
        }
    }

    class Result
    {
        public Node node;
        public bool result;

        public Result(Node n, bool b)
        {
            node = n;
            result = b;
        }
    }
}
