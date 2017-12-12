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

            Node head = CreateSinglyLinkedList(10000);

            CreatePalindrome(ref head);            
                                    
            CheckIfPalindrome_Reverse(head);

            Console.ReadLine();
        }

        private static void CreatePalindrome(ref Node passed_head)
        {
            Node runner = passed_head;
            Node rev_head = null;

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

        /// <summary>
        /// 
        /// 1. Create a new, reversed list from the original
        /// 2. Compare the two lists node by node
        /// 3. If any two nodes don't match, report false
        /// 4. If no mismatches found, report true
        /// 
        /// Complexity:     Algorithm runs in O(N) time        
        ///                 Each node is read once to create a node
        ///                 Each node (both lists) is read once to
        ///                 compare.
        ///                 
        ///                 Requires O(N) memory
        ///                 As input grows, memory requirement grows
        ///                 The input is copied, so the algo requires 
        ///                 as much memory as the input represents.
        /// 
        /// </summary>
        /// <param name="passed_head"></param>
        private static void CheckIfPalindrome_Reverse(Node passed_head)
        {
            Stopwatch sw = Stopwatch.StartNew();

            // build reversed list from passed_head
            Node runner = passed_head;            
            Node rev_head = null;           
            while (runner != null)
            {
                rev_head = new Node(runner.Data, rev_head);
                
                runner = runner.next;
            }    
            
            // compare original list to the new, reversed list
            runner = passed_head;            
            while (runner.next != null)
            {
                if (runner.Data != rev_head.Data)
                {
                    Console.WriteLine("List is not a palindrome.");
                    return;
                }

                runner = runner.next;
                rev_head = rev_head.next;
            }

            sw.Stop();

            Console.WriteLine("List is a palindrome. (" + sw.ElapsedTicks + " ticks)");
        }

        //private static bool CheckIfPalindrome(Node passed_node, int count_thisnode, ref int total_nodes)
        //{
        //    ++count_thisnode; // contains this node's position in the list (1-based)
        //    ++total_nodes; // during unspooling, contains total count of nodes (1-based)

        //    bool result = false;

        //    if (passed_node.next != null)
        //        result = CheckIfPalindrome(passed_node.next, count_thisnode, ref total_nodes);

        //    if (count_thisnode < (total_nodes / 2))
        //        return result;

        //    // compare this node to the mirrored node
        //    int value = GetNodeData(passed_total_nodes - count_thisnode);
        //}

        //private static int GetNodeData(node passed_head, int node_position)
        //{
        //    for (int i = 0; i < node_position; ++i)

        //}

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
}
