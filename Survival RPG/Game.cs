using System;
using System.IO;

namespace Survival_RPG
{
    class Game
    {
        #region Variables
        private int _day = 1;
        #endregion

        #region Properties
        public int Day { get => _day; set => _day = value; }
        #endregion

        #region Methods

        //Current Status Of Player and Enemy Updating Every Turn In Combat
        public void Status(int playerHealth, int playerMana, int playerStamina, int enemyHealth)
        {
            Console.WriteLine("Current Health: " + playerHealth);
            Console.WriteLine("Current Mana: " + playerMana);
            Console.WriteLine("Current Stamina: " + playerStamina);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ");
            Console.WriteLine("Enemy Current Health: " + enemyHealth + " ");
            Console.WriteLine(" ");
        }
        #endregion
    }
}
