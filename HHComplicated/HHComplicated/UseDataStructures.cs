using System;

namespace HHComplicated
{
    class UseDataStructures
    {
        Graph house = new Graph();
        public void Menu()
        {
            string option = "";
            while (option != "3")
            {
                Console.WriteLine();
                Console.WriteLine("1. Graphs");
                Console.WriteLine("2. Trees");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Enter Option: ");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        GraphMenu();
                        break;
                    case "2":
                        treeMenu();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                }
            }
        }

        public void GraphMenu()
        {
            int start = 0;
            int finish = 0;
            string option = "";


            while (option != "5")

            {

                Console.WriteLine();

                Console.WriteLine("1. Depth First Search - recursive solution");

                Console.WriteLine("2. Depth First Search - iterative solution");

                Console.WriteLine("3. Breadth First Search");

                Console.WriteLine("4. Dijkstra Shortest Path");

                Console.WriteLine("5. Exit");

                Console.WriteLine();

                Console.Write("Enter Option: ");

                option = Console.ReadLine();

                if (option != "5")

                {

                    Console.WriteLine();

                    Console.Write("Enter the start room: ");

                    start = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter the finish room: ");

                    finish = Convert.ToInt32(Console.ReadLine());

                }

                switch (option)

                {

                    case "1":

                        Console.WriteLine("DFS Recursively");

                        break;

                    case "2":

                        Console.WriteLine("DFS Iteratively");

                        break;

                    case "3":

                        Console.WriteLine("DFS Iteratively");

                        break;

                    case "4":

                        Console.WriteLine("BFS");

                        break;

                    case "5":

                        break;

                    default:

                        Console.WriteLine("Please enter a valid option");

                        break;

                }

            }

        }



        public void treeMenu()
        {
            string option = "";
            string name;
            int value;

            while (option != "6")
            {
                Console.WriteLine();
                Console.WriteLine("1. Find Item");
                Console.WriteLine("2. Add to Tree");
                Console.WriteLine("3. InOrder Traversal");
                Console.WriteLine("4. PreOrder Traversal");
                Console.WriteLine("5. PostOrder Traversal");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Enter option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Find Item");
                        break;
                    case "2":
                        Console.WriteLine("Insert Item");
                        Console.Write("Enter name of Item: ");
                        name = Console.ReadLine().ToLower();
                        Console.Write("Enter value of Item: ");
                        value = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Item Added");
                        break;
                    case "3":
                        Console.WriteLine("InOrder Traversal/n");
                        break;
                    case "4":
                        Console.WriteLine("PreOrder Traversal/n");
                        break;
                    case "5":
                        Console.WriteLine("PostOrder Traversal/n");
                        break;
                    case "6":
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                }
            }
        }
    }
}
