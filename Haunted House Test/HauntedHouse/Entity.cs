using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse
{
    class Entity
    {
        protected string name;
        protected int health = 100;
        protected int attack = 50;
        protected int location = 0;

        protected static Random rndNum = new Random(); // class variable, only ever be one of them

        public Entity(string name)
        {
            this.name = name;
        }

        public int getHealth()
        {
            return health;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public int getLocation()
        {
            return location;
        }

        public void setLocation(int location)
        {
            this.location = location;
        }

        public string getName()
        {
            return name;
        }

        public int getAttack()
        {
            return attack;
        }

        public void setAttack(int attack)
        {
            this.attack = attack;
        }



        public virtual void Combat(Entity defender)
        {
            if (health > 0)
            {
                Console.WriteLine();
                Console.WriteLine("{0} has attacked {1}", name, defender.getName());
                Console.WriteLine("{0} takes away some of {1}'s health", name, defender.getName());
                defender.setHealth(defender.getHealth() - attack);

                Console.WriteLine();
                Console.WriteLine("{0} now has {1} health", defender.getName(), defender.getHealth());
                Console.WriteLine("{0} now has {1} health", name, health);
                Console.ReadKey();
            }
        }
    }

    class Player : Entity
    {
        public Player(string name):base(name)
        {

        }

        public override void Combat(Entity defender)
        {
            if (rndNum.Next(0, 4) != 0)
            {
                base.Combat(defender);
            }
            else
            {
                Console.WriteLine("you try to hit {0}, but miss", name);
            }
        }
    }

    class Enemy : Entity
    {
        public Enemy(string name, int numRooms) : base(name)
        {
            location = rndNum.Next(1, numRooms);
            health = rndNum.Next(50, 101);
            attack = rndNum.Next(10, 20);
        }

        public void ResetHealth()
        {
            health = rndNum.Next(50, 101);
        }

        public override void Combat(Entity defender)
        {
            if (rndNum.Next(0, 2) != 0)
            {
                base.Combat(defender);
            }
            else
            {
                Console.WriteLine("{0} tries to hit you, but misses", name);
            }
        }
    }
}
