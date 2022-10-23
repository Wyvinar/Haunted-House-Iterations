using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse2
{
    class PlayGame
    {
        private Room[] rooms;
        private Item[] items;
        private List<Enemy> enemies = new List<Enemy>();
        private Player P1;
        private bool leave = false;

        static private Random rndNum = new Random();

        public PlayGame()
        {
            SetUp();
            Console.WriteLine("Welcome to the haunted house");
            Console.Write("Please enter your name: ");
            P1 = new Player(Console.ReadLine());
            Console.Clear();
            EnterRoom(0);
            Play();
        }

        private void SetUp()
        {
            rooms = new Room[8];
            rooms[0] = new Room(0, "driveway", false, true, new int[] {1, 2, 4},
                 "In front of you is the 'front door', to your right a 'fountain' and your left, a 'forest'\nType 'x' to leave");
            rooms[1] = new Room(1, "fountain", false, true, new int[] {0, 3},
                 "A dilapidated and ugly little cherub  is sitting on a pedastal dribbling a red liquid into a basin... not creepy at all...\nYou can see the 'driveway' behind you and the 'garden' in the distance");
            rooms[2] = new Room(2, "forest", false, false, new int[] {0},
                 "there sure are a lot of trees\nyou can barely make out the outline of the 'driveway'");
            rooms[3] = new Room(3, "garden", false, true, new int[] {1},
                 "not much to it, be careful of the roses though, they might prick you\nbeyond the rickety house you can see the cherub 'fountain' looking as lamely creepy as ever");
            rooms[4] = new Room(4, "front door", false, true, new int[] {0, 1, 2, 5},
                "In front of you is a door\nglancing behind you is the 'driveway', looking through the window you can see a 'lobby' area");
            rooms[5] = new Room(5, "lobby", true, true, new int[] {4},
                "the 'front door' stands in front of the 'stairs', the 'kitchen' and the 'dining room' peel off to the left and what seems to be a 'lounge' and 'piano room' to the right");
            rooms[6] = new Room(6, "kitchen", false, true, new int[2] {5, 7},
                "pots and pans plague the place and steam rises from a cluncky oven/hob\nan archway leads smoothly to the 'dining room'");
            rooms[7] = new Room(7, "dining room", false, false, new int[2] {5, 6},
                "ancient portraits line the walls, their eyes seem to be following you, bet you wished you never turned the torch on now :)\nan archway leads to the 'kitchen' and rickety door to the 'lobby'");


            items = new Item[8];
            items[0] = new Torch(0, "torch", -1,
                "can be used to light an area, does have a battery life, so take care", 10, 5);
            items[1] = new Item(1, "map", 4,
                "unlocks 'm' command, lists all the areas in the house and if they can be currently accessed", 10);
            items[2] = new Key(2, "front door key", 4,
                "found in a rusty looking lion's mouth door knocker at the porch, unlocks the front door", 5, 1, 5);
            items[3] = new Consumable(3, "ghost spray", -1,
                "extra effective against ghosts, use in a battle instance", 50, 0);
            items[4] = new Consumable(4, "battery", -1,
                "use to add on more battery time to your torch, each one is worth one more use of the torch", 15, 0);
            items[5] = new Consumable(5, "bandage", -1,
                "use to restore +20 health", 45, 10);
            items[6] = new Consumable(6, "bandage", 1);
            items[7] = new Consumable(7, "battery", 1);
        }

        public void Play()
        {
            string command;
            string noun = "";
            while (!leave)
            {
                Console.Write(": ");
                command = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (command.Contains(" "))
                {
                    noun = command.Substring(command.IndexOf(" ") + 1);
                    command = command.Substring(0, command.IndexOf(" "));
                }

                switch (command)
                {
                    case "goto":
                        Goto(noun);
                        break;
                    case "x":
                        leave = ExitHouse(leave);
                        break;
                    case "search":
                        Search(P1.GetLocation());
                        break;
                    case "pickup":
                        Pickup(noun, P1.GetLocation());
                        break;
                    case "drop":
                        Drop(noun, P1.GetLocation());
                        break;
                    case "use":
                        Use(noun);
                        break;
                    case "open":
                        break;
                    case "value":
                        int totalValue = 0;
                        totalValue = TotalValue(totalValue);
                        Console.WriteLine("You have {0}g worth of items in your inventory", totalValue);
                        break;
                    case "i":
                        Inventory();
                        break;
                    case "stats":
                        Console.Write(P1);
                        Console.WriteLine(rooms[P1.GetLocation()].GetRoomName());
                        break;
                    case "inspect":
                        Inspect(noun);
                        break;
                    case "description":
                        if (rooms[P1.GetLocation()].GetLit())
                            Console.WriteLine(rooms[P1.GetLocation()].GetRoomDesc());
                        else
                            Console.WriteLine("You can't see a thing");
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "help":
                        Help();
                        break;
                    default:
                        Console.WriteLine("Invalid command, type 'help' for more information on input");
                        break;
                }
                Console.WriteLine();
            }
        }



        public void Goto(string noun)
        {
            bool found = false;
            int currentRoom = P1.GetLocation();
            Room accessibles;
            for (int i = 0;  i < rooms[currentRoom].GetAccessibles().Length; i++)
            {
                accessibles = rooms[rooms[currentRoom].GetAccessibles()[i]];
                if (accessibles.GetRoomName().Equals(noun) && !accessibles.GetLocked())
                {
                    EnterRoom(accessibles.GetRoomNum());
                    found = true;
                }
                else if (accessibles.GetRoomName().Equals(noun) && accessibles.GetLocked())
                {
                    Console.WriteLine("Room is locked");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Room doesn't exist or you can't access it from your current position");
        }

        public void EnterRoom(int currentRoom)
        {
            int num;

            P1.SetLocation(currentRoom);
            Console.WriteLine("You are at the {0}", rooms[currentRoom].GetRoomName());
            if (rooms[currentRoom].GetLit() == true)
                Console.WriteLine(rooms[currentRoom].GetRoomDesc());
            else
                Console.WriteLine("You can't see a thing");

            num = rndNum.Next(0, 5);
            if (num == 0)
            {
                num = rndNum.Next(0, 8);
                switch (num)
                {
                    case 0:
                        enemies.Add(new Ghost(currentRoom));
                        break;
                    case 1:
                        enemies.Add(new Ghoul(currentRoom));
                        break;
                    case 2:
                        enemies.Add(new Zombie(currentRoom));
                        break;
                    case 3:
                    case 4:
                        enemies.Add(new Slime(currentRoom));
                        break;
                    default:
                        enemies.Add(new Enemy("average joe", currentRoom));
                        break;
                }
            }
            CheckEnemy(currentRoom);
        }

        public void CheckEnemy(int currentRoom)
        {
            bool endCombat = false;
            int counter = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (P1.GetLocation() == enemies[i].GetLocation())
                {
                    Console.WriteLine("You meet {0} in the room", enemies[i].GetName());
                    while (!endCombat)
                    {
                        if (counter % 2 == 0)
                            endCombat = Combat(enemies[i], P1);
                        else
                            endCombat = Combat(P1, enemies[i]);

                        counter++;

                        if (endCombat)
                            enemies.Remove(enemies[i]);
                    }

                    if (P1.GetHealth() <= 0 && P1.GetLives()>1)
                    {
                        P1.PlayerDeath();
                        Drop("all", currentRoom);
                        EnterRoom(0);
                    }
                    else if (P1.GetHealth() <=0 && P1.GetLives() <= 1)
                    {
                        P1.PlayerDeath();
                        Console.WriteLine("You have lost all your lives");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Game Over...");
                        leave = true;
                    }
                }
            }
        }

        public bool Combat(Entity attacker, Entity defender)
        {
            if (attacker.GetHealth() > 0)
            {
                attacker.Combat(defender);
                return false;
            }
            else
            {
                Console.WriteLine("{0} is dead", attacker.GetName());
                return true;
            }
        }

        public void Search(int location)
        {
            if (rooms[location].GetLit())
            {
                Console.WriteLine("In this room there is...\n");
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItemLoc() == location)
                    {
                        Console.WriteLine("\t{0}", items[i].GetItemName());
                    }
                }
            }
            else
                Console.WriteLine("You don't have any light to search by");
        }

        public void Inventory()
        {
            Console.WriteLine("In your inventory there is... \n");
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemLoc() == -1 && items[i].GetItemDura() != 0)
                {
                    Console.WriteLine("\t{0} {1, 0}", items[i].GetItemName() + ":", items[i].GetItemDesc());
                }
            }
        }

        public void Pickup(string noun, int room)
        {
            bool found = false;
            Console.WriteLine("You have picked up:\n");
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemLoc() == room && items[i].GetItemName().Equals(noun) || noun.Equals("all"))
                {
                    if (items[i].GetItemDura() >= 0)
                    {
                        switch (items[i].GetItemName())
                        {
                            case "torch":
                                items[i].SetItemLoc(-1);
                                items[i].SetItemLoc(-2);
                                break;
                            case "ghost spray":
                                PickupConsumables(3);
                                items[i].SetItemLoc(-2);
                                break;
                            case "bandage":
                                PickupConsumables(5);
                                items[i].SetItemLoc(-2);
                                break;
                            case "battery":
                                PickupConsumables(4);
                                items[i].SetItemLoc(-2);
                                break;
                        }
                    }
                    else
                        items[i].SetItemLoc(-1);

                    Console.WriteLine("\t{0}", items[i].GetItemName());
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Nothing. Item not found, either it doesn't exist or it isn't in the room");
        }

        public void PickupConsumables(int i)
        {
            items[i].SetItemDura(items[i].GetItemDura() + 1);
        }

        public void Drop(string noun, int room)
        {
            bool found = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemLoc() == -1 && items[i].GetItemName().Equals(noun))
                {
                    items[i].SetItemLoc(room);
                    found = true;
                }
                else if (noun.Equals("all") && items[i].GetItemLoc() == -1)
                {
                    items[i].SetItemLoc(room);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Item not found, either it doesn't exist or it isn't in your inventory");
            }
            else
                Console.WriteLine("Drop successful");
        }

        public void Use(string noun)
        {
            bool found = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemLoc() == -1 && items[i].GetItemName().Equals(noun) && items[i].GetItemDura() > 0)
                {
                    Console.WriteLine("You have used a {0}", items[i].GetItemName());
                    switch (noun)
                    {
                        case "bandage":
                            P1.SetHealth(P1.GetHealth() + 20);
                            Console.WriteLine("health + 20\nhealth: {0}", P1.GetHealth());
                            break;
                        case "battery":
                            items[0].SetItemDura(items[i].GetItemDura() + 1);
                            break;
                        case "torch":
                            rooms[P1.GetLocation()].SetLit(true);
                            Console.WriteLine(rooms[P1.GetLocation()].GetRoomDesc());
                            break;
                    }
                    found = true;
                    items[i].SetItemDura(items[i].GetItemDura() - 1);
                }
                else if(items[i].GetItemLoc() == -1 && items[i].GetItemName().Equals(noun) && items[i].GetItemDura() <= 0)
                {
                    Console.WriteLine("Item cannot be used or you don't have any to use");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Item not in inventory or doesn't exist");
        }

        public bool ExitHouse(bool leave)
        {
            int totalValue = 0;
            if (P1.GetLocation() == 0)
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

        public void Inspect(string noun)
        {
            bool found = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemName().Equals(noun) && items[i].GetItemLoc() == -1)
                {
                    Console.WriteLine(items[i]);
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Item either doesn't exist or isn't in your inventory");
        }

        public int TotalValue(int totalValue)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItemLoc() == -1)
                {
                    if(items[i].GetItemDura() >= 0 && items[i].GetItemName() != "torch")
                    {
                        totalValue += items[i].GetItemVal() * items[i].GetItemDura();
                    }
                    totalValue += items[i].GetItemVal();
                }
            }
            return totalValue;
        }

        public void Help()
        {
            Console.WriteLine("List of commands you can use:\n");
            Console.WriteLine("\tgoto:\t\tmove to a different room");
            Console.WriteLine("\tsearch:\t\tsearches your current location for all items");
            Console.WriteLine("\tpickup:\t\tallows you to pickup a specified item");
            Console.WriteLine("\tdrop:\t\tallows you to drop a specified item");
            Console.WriteLine("\tuse:\t\tuses a specific item");
            Console.WriteLine("\tunlock:\t\tused to unlock a door, you have to have the key though");
            Console.WriteLine("\tvalue:\t\ttotals the worth of the items in inventory");
            Console.WriteLine("\ti:\t\tlists all items in inventory");
            Console.WriteLine("\tstats:\t\tshows your stats");
            Console.WriteLine("\tinspect:\tgives you a description of an item in your inventory");
            Console.WriteLine("\tdescription:\tgives a description of your current area");
            Console.WriteLine("\tclear:\t\tclears the screen from all previous text");
            Console.WriteLine("\thelp\t\tprints the help page");
            Console.WriteLine("\nMore commands can be unlocked after finding specific items");
        }
    }
}
