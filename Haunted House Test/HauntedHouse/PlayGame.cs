using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HauntedHouse
{
    class PlayGame
    {
        private Room[] rooms;
        private Item[] items;
        private Enemy[] enemies;
        private Player P1;

        public PlayGame()
        {
            Setup();
            Console.WriteLine("Welcome to the Haunted House\n\n");
            Console.Write("Enter your player name: ");
            P1 = new Player(Console.ReadLine());

            Play();
        }

        private void Setup()
        {
            rooms = new Room[4];
            rooms[0] = new Room(0, "Front Door", "");
            rooms[1] = new Room(1, "Lounge", "");
            rooms[2] = new Room(2, "Library", "");
            rooms[3] = new Room(3, "Kitchen", "");

            items = new Item[5];
            items[0] = new Item(0, "Torch", -1, "shines in your eyes");
            items[1] = new Item(1, "Ghost spray", 2, "smells disgusting but may be useful", 100);
            items[2] = new Item(2, "goo", 3, "smells disgusting, looks disgusting and is disgusting", 1);
            items[3] = new Item(3, "teddy bear", 3, "has goo all over it, is missing an eye and an arm", 30);
            items[4] = new Item(4, "key", 2, "unlocks room 4", 0);

            enemies = new Enemy[2];
            enemies[0] = new Enemy("ghoul", rooms.Length);
            enemies[1] = new Enemy("ghost", rooms.Length);
        }

        public void Help()
        {
            Console.WriteLine("list" + "i" + "goto" + "pickup" + "drop");
        }

        public void Play()
        {
            bool leave = false;
            string command;
            string noun = "";
            while (!leave)
            {
                Console.Write(": ");
                command = Console.ReadLine().ToLower();

                if (command.Contains(" "))
                {
                    noun = command.Substring(command.IndexOf(" ") + 1);
                    command = command.Substring(0, command.IndexOf(" "));
                }

                switch (command)
                {
                    case "0":
                        EnterRoom(command);
                        Console.WriteLine("Press 'x' to exit");
                        break;
                    case "1":
                    case "2":
                    case "3":
                        EnterRoom(command);
                        break;
                    case "list":
                        List(P1.getLocation());
                        break;
                    case "i":
                        List();
                        break;
                    case "pickup":
                        Pickup(noun, P1.getLocation());
                        break;
                    case "drop":
                        Drop(noun, P1.getLocation());
                        break;
                    case "value":
                        int totalValue = 0;
                        totalValue = TotalValue(totalValue);
                        Console.WriteLine("You have {0}g worth of items in your inventory", totalValue);
                        break;
                    case "x":
                        leave = ExitHouse(leave);
                        break;
                    default:
                        Console.WriteLine("Invlid input");
                        break;
                }
            }

        }

        public void EnterRoom(string currentRoom)
        {
            P1.setLocation(Convert.ToInt32(currentRoom));
            Console.WriteLine();
            Console.WriteLine("You are at the {0}", rooms[Convert.ToInt32(currentRoom)].getRoomName());
            Console.WriteLine(rooms[Convert.ToInt32(currentRoom)].getRoomDescritpion());
            CheckEnemy();
        }

        public void CheckEnemy()
        {
            bool endCombat = false;
            int counter = 0;
            for(int i = 0; i < enemies.Length; i++)
            {
                if(P1.getLocation() == enemies[i].getLocation())
                {
                    Console.WriteLine("You meet {0} in the room", enemies[i].getName());
                    while(!endCombat)
                    {
                        if (counter % 2 == 0)
                            endCombat = Combat(enemies[i], P1);
                        else
                            endCombat = Combat(P1, enemies[i]);
                        counter++;
                    }
                }
            }
        }

        public bool Combat(Entity attacker, Entity defender)
        {
            if (attacker.getHealth() > 0)
            {
                attacker.Combat(defender);
                return false;
            }
            else
            {
                Console.WriteLine("{0} has killed {1}", defender.getName(), attacker.getName());
                return true;
            }
        }


        public void List(int location)
        {
            Console.WriteLine("\nIn this room there is...");
            for(int i = 0; i < items.Length; i++)
            {
                if(items[i].GetLocation() == location)
                {
                    Console.WriteLine("\t{0}", items[i].GetName());
                }
            }
        }

        public void List()
        {
            Console.WriteLine("\nIn your inventory there is... \n");
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetLocation() == -1)
                {
                    Console.WriteLine("\t{0}", items[i].GetName());
                }
            }
        }


        public void Pickup(string noun, int room)
        {
            bool found = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetLocation() == room && items[i].GetName().Equals(noun))
                {
                    items[i].SetLocation(-1);
                    found = true;
                }
                else if (noun.Equals("all") && items[i].GetLocation() == room)
                {
                    items[i].SetLocation(-1);
                    found = true;
                }
            }

            if (found == false)
            {
                Console.WriteLine("Item not found, either it doesn't exist or it isn't in the room");
            }
        }


        public void Drop(string noun, int room)
        {
            bool found = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetLocation() == -1 && items[i].GetName().Equals(noun))
                {
                    items[i].SetLocation(room);
                    found = true;
                }
                else if (noun.Equals("all") && items[i].GetLocation() == -1)
                {
                    items[i].SetLocation(room);
                    found = true;
                }
            }

            if (found == false)
            {
                Console.WriteLine("Item not found, either it doesn't exist or it isn't in the room");
            }
        }


        public bool ExitHouse(bool leave)
        {
            int totalValue = 0;
            if (P1.getLocation() == 0)
            {
                totalValue = TotalValue(totalValue);
                Console.WriteLine("You have found {0}g worth of items", totalValue);
                Console.WriteLine("Goodbye");
                leave = true;
                return true;
            }
            else
            {
                Console.WriteLine("You need to be in room 0 to leave");
                return false;
            }
        }

        public int TotalValue(int totalValue)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetLocation() == -1)
                {
                    totalValue += items[i].GetValue();
                }
            }
            return totalValue;
        }
    }
}
