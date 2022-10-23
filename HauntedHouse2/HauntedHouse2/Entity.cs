using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HauntedHouse2
{
    class Entity
    {
        protected string name;
        protected int health = 100;
        protected int attack = 20;
        protected int armour = 1;
        protected int location = 0;

        protected static Random rndNum = new Random();      // class variable
        public Entity(string name)
        {
            this.name = name;
        }

        public Entity(string name, int location)
        {
            this.name = name;
            this.location = location;
        }

        public Entity(int location)
        {
            this.location = location;
        }


        public string GetName()
        {
            return name;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetAttack()
        {
            return attack;
        }

        public int GetArmour()
        {
            return armour;
        }

        public int GetLocation()
        {
            return location;
        }

        public void SetHealth(int health)
        {
            this.health = health;
        }

        public void SetAttack(int attack)
        {
            this.attack = attack;
        }

        public void SetArmour(int armour)
        {
            this.armour = armour;
        }

        public void SetLocation(int location)
        {
            this.location = location;
        }


        public virtual void Combat(Entity defender)
        {
            int damage;
            Console.WriteLine();
            Console.WriteLine("{0} has attacked {1}", name, defender.GetName());
            Console.ReadKey();
            Console.WriteLine("{0} takes away some of {1}'s health", name, defender.GetName());
            Console.ReadKey();
            damage = TotalDamage(Convert.ToDouble(attack), Convert.ToDouble(defender.GetArmour()));
            defender.SetHealth(defender.GetHealth() - damage);

            Console.WriteLine();
            Console.WriteLine("{0} now has {1} health", defender.GetName(), defender.GetHealth());
            Console.ReadKey();
            Console.WriteLine("{0} now has {1} health", name, health);
            Console.ReadKey();
            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        public int TotalDamage(double attack, double armour)
        {
            double damage = attack * (100 / (100 + armour));
            return Convert.ToInt32(damage);
        }



        public override string ToString()
        {
            return String.Format("Name: {0}\nHealth: {1}\nAttack: {2}\nArmour: {3}\nLocation: ", name, health, attack, armour);
        }
    }

    class Player : Entity
    {
        private int lives = 3;
        public Player(string name) : base(name)
        {

        }

        public int GetLives()
        {
            return lives;
        }

        public void SetLives(int lives)
        {
            this.lives = lives;
        }

        public override void Combat(Entity defender)
        {
            bool endLoop = false;
            string command;
            Console.WriteLine("\nYour action");

            while (!endLoop)
            {
                Console.Write("\n: ");
                command = Console.ReadLine();
                Console.WriteLine();

                switch (command)
                {
                    case "basic":
                        if (rndNum.Next(0, 4) != 0)
                            base.Combat(defender);
                        else
                            Console.WriteLine("You try to hit {0} and fail", defender.GetName());
                        endLoop = true;
                        break;
                    case "use":
                        endLoop = true;
                        break;
                    case "run":
                        endLoop = true;
                        break;
                    case "help":
                        endLoop = true;
                        break;
                }
            }
        }

        public bool PlayerDeath()
        {
            Console.Clear();
            Console.WriteLine("Your head begins to swim and your last sight is the ground as it rushes up to meet you.");
            Thread.Sleep(1000);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You have died, all your items have been dropped in the room you died in");
            Console.ReadKey();
            Console.Clear();
            health = 100;
            lives--;

            if (lives > 0)
                return false;
            else
                return true;
        }
    }

    class Enemy : Entity
    {
        private int enemyNum; // needed because I need a unique way to access individual enemies once they're in a list

        private static int enemeyCounter;

        public Enemy(string name, int location) : base(name, location)
        {
            health = rndNum.Next(10, 51);
            attack = rndNum.Next(5, 26);
            armour = rndNum.Next(1, 6);
            enemyNum = enemeyCounter;
            enemeyCounter++;
        }

        public Enemy(int location):base(location)
        {
            enemyNum = enemeyCounter;
            enemeyCounter++;
        }

        public int GetNum()
        {
            return enemyNum;
        }


        public void Drop()
        {

        }


        public override void Combat(Entity defender)
        {
            if (rndNum.Next(0, 2) != 0)
                base.Combat(defender);
            else
                Console.WriteLine("{0} tries to hit you, but misses", name);
        }

        public override string ToString()
        {
            return "Can't view the stats of a dead thing dummy, or can you...";
        }
    }

    class Ghost : Enemy
    {
        public Ghost(int location):base(location)
        {
            name = "ghost";
            health = 50;
            attack = 10;
            armour = 20;
        }
    }

    class Slime : Enemy
    {
        public Slime(int location) : base(location)
        {
            name = "slime";
            health = 10;
            attack = 10;
            armour = 50;
        }
    }

    class Ghoul : Enemy
    {
        public Ghoul(int location) : base(location)
        {
            name = "ghoul";
            health = 25;
            attack = 25;
            armour = 10;
        }
    }

    class Zombie : Enemy
    {
        public Zombie(int location) : base(location)
        {
            name = "zombie";
            health = 40;
            attack = 20;
            armour = 20;
        }
    }
}
