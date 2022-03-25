using System;
using System.Threading;

namespace Survival_RPG
{
    class Enemy
    {
        #region Properties

        private int health;

        public int Health { get => health; set => health = value; }

        //Enemy Health Being Randomly Set Everyday
        public Enemy(int health)
        {
            this.Health = health;
        }
        #endregion

        #region Methods

        //Array For Enemy Types That Are Randomly Selected Each Day
        public string Type()
        {
            String[] types = { "Goblin", "Orc", "Imp", "Centaur", "Cyclops", "Lesser Demon", "Fairy" };

            Random random = new Random();
            int randomType = random.Next(0, 7);
            return types[randomType];
        }

        //Enemies Random Attack Generated In Main Game Loop Every Day. Attack DMG Returned To Be Updated With Combat();
        public int EnemyAttack(int enemyChoice, int day)
        {
            Random random = new Random();

            switch (enemyChoice)
            {
                case 1:
                    int magicAttk = random.Next(0, 25);
                    Console.WriteLine("                                     Enemy used magic!... They hit you for {0}", magicAttk);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);
                    return magicAttk;
                case 2:
                    int meleeAttk = random.Next(0, 15);
                    Console.WriteLine("                                     Enemy used melee!... They hit you for {0}", meleeAttk);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);
                    return meleeAttk;
                case 3:
                    int bowAttk = random.Next(0, 25);
                    Console.WriteLine("                                     Enemy used a bow!... They hit you for {0}", bowAttk);
                    Console.WriteLine(" ");
                    Thread.Sleep(1000);
                    return bowAttk;
            }
            return 0;
        }
        #endregion
    }
}
