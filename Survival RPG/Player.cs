using System;
using System.Threading;

namespace Survival_RPG
{
    class Player
    {
        #region Variables

        private int _hPot = 2;
        private int _sPot = 1;
        private int _mPot = 1;
        protected int health = 100;
        protected int stamina = 100;
        protected int mana = 100;

        #endregion

        #region Properties

        public int HPot { get => _hPot; set => _hPot = value; }
        public int SPot { get => _sPot; set => _sPot = value; }
        public int MPot { get => _mPot; set => _mPot = value; }
        public int Health { get => health; set => health = value; }
        public int Stamina { get => stamina; set => stamina = value; }
        public int Mana { get => mana; set => mana = value; }

        #endregion

        #region Methods

        //When Inventory Is Prompted In Main Game Loop Player.Inventory So Player Is Attached To Inventory
        public int UseInventory()
        {
            Sound sound = new Sound();

            Console.WriteLine("Users Current Inventory");
            Console.WriteLine("___________________");
            Console.WriteLine("");
            Console.WriteLine("[1]Health Potions:    " + _hPot);
            Console.WriteLine("[2]Stamina Potions:   " + _sPot);
            Console.WriteLine("[3]Mana Potions:      " + _mPot);
            Console.WriteLine("[4]Back to battle");
            Console.WriteLine("");
            Console.WriteLine("Use a item?");

            try
            {
                int userChoice = Convert.ToInt32(Console.ReadLine());
                sound.playSFX(5);
                switch (userChoice)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    default:
                        return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }

        //When Health Pot Is Chosen In Inventory
        public void Heal()
        {
            //Checks To Make Sure Player Is Not At Full Health And Has At least One Potion Available
            if (Health < 100 && _hPot >= 1)
            {
                Health = Health + 15;

                Console.WriteLine("You drink and recharge 15 health...");
                Thread.Sleep(1000);

                _hPot = _hPot - 1;
                if (_hPot < 0)
                    _hPot = 0;

                Console.WriteLine("Potions Remaining: " + _hPot);
                Thread.Sleep(1000);

                if (Health > 100)
                {
                    Health = 100;
                }
            }
            else if (Health < 100 && _hPot == 0)
            {
                Console.WriteLine("You have no potions left...");
                Thread.Sleep(1500);
            }
            else if (Health >= 100)
            {
                Console.WriteLine("You are already at full health...");
                Thread.Sleep(1500);
            }
        }
        //When Mana Pot Is Chosen In Inventory
        public void Drink()
        {
            //Checks To Make Sure Player Is Not At Full Mana And Has At least One Potion Available
            if (Mana < 100 && _mPot >= 1)
            {
                Mana = Mana + 15;

                Console.WriteLine("You drink and recharge 15 mana...");
                Thread.Sleep(1000);

                _mPot = _mPot - 1;
                if (_mPot < 0)
                    _mPot = 0;

                Console.WriteLine("Potions Remaining: " + _mPot);
                Thread.Sleep(1000);

                if (Mana > 100)
                {
                    Mana = 100;
                }
            }
            else if (Mana < 100 && _mPot == 0)
            {
                Console.WriteLine("You have no recharges left...");
                Thread.Sleep(1500);
            }
            else if (Mana >= 100)
            {
                Console.WriteLine("You are already at full mana...");
                Thread.Sleep(1500);
            }

        }
        //When Stamina Pot Is Chosen In Inventory
        public void Recharge()
        {
            //Checks To Make Sure Player Is Not At Full Stamina And Has At least One Potion Available
            if (Stamina < 100 && _sPot >= 1)
            {
                Stamina = Stamina + 15;

                Console.WriteLine("You drink and recharge 15 stamina...");
                Thread.Sleep(1000);

                _sPot = _sPot - 1;
                if (_sPot < 0)
                    _sPot = 0;

                Console.WriteLine("Potions Remaining: " + _sPot);
                Thread.Sleep(1000);

                if (Stamina > 100)
                {
                    Stamina = 100;
                }
            }
            else if (Stamina < 100 && _sPot == 0)
            {
                Console.WriteLine("You have no potions left...");
                Thread.Sleep(1500);
            }
            else if (Stamina >= 100)
            {
                Console.WriteLine("You are already at full stamina...");
                Thread.Sleep(1500);
            }

        }
        //End Of Every Day Sleep(); Recharges All Stats By A Small Amount
        public void Sleep()
        {
            Stamina = Stamina + 10;
            Mana = Mana + 10;
            Health = Health + 15;

            if (Mana > 100)
            {
                Mana = 100;
            }

            if (Stamina > 100)
            {
                Stamina = 100;
            }

            if (Health > 100)
            {
                Health = 100;
            }

            Console.WriteLine("You regain some health, stamina and mana!");
            Thread.Sleep(2000);
        }

        //When Player Uses Magic Attack Check If Mana Is > 0 Returns Updated Mana To Player.mana
        public int ManaConsume(int playerMana)
        {
            if (playerMana <= 0)
            {
                playerMana = 0;
                return playerMana;
            }
            else
                playerMana -= 10;

            return playerMana;
        }
        //When Player Uses Stamina Attack Check If Stamina Is > 0 Returns Updated Stamina To Player.stamina
        public int StaminaConsume(int playerStamina)
        {
            if (playerStamina <= 0)
            {
                playerStamina = 0;
                return playerStamina;
            }
            else
                playerStamina -= 5;

            return playerStamina;
        }
        //When Player Chooses Melee Attack. Random Number Between 0, 15 + dmgModifer (dmgModifer = game.Day)
        public int PlayerStaminaMeleeAttack(int playerStamina, int dmgModifer)
        {
            Random random = new Random();
            int maxStamDmg = dmgModifer + 15;

            if (playerStamina >= 5)
            {
                int playerAttk = random.Next(0, maxStamDmg);
                Console.WriteLine("You used melee!...You hit for {0}", playerAttk);
                Thread.Sleep(1000);
                return playerAttk;
            }
            else
                Console.WriteLine("You have no stamina!... You miss this turn!...");
            Thread.Sleep(1000);
            return 0;
        }
        //When Player Chooses Bow Attack. Random Number Between 0, 25 + dmgModifer (dmgModifer = game.Day)
        //Checks If Player.stamina is > 5 To Use Attack
        public int PlayerStaminaBowAttack(int playerStamina, int dmgModifer)
        {
            Random random = new Random();
            int maxStamDmg = dmgModifer + 25;

            if (playerStamina >= 5)
            {
                int playerAttk = random.Next(0, maxStamDmg);
                Console.WriteLine("You used a bow!...You hit for {0}", playerAttk);
                Thread.Sleep(1000);
                return playerAttk;
            }
            else
                Console.WriteLine("You have no stamina!... You miss this turn!...");
            Thread.Sleep(1000);
            return 0;
        }

        //When Player Chooses Magic Attack. Random Number Between 0, 25 + dmgModifer (dmgModifer = game.Day)
        //Checks If Player.stamina is > 10 To Use Attack
        public int PlayerManaAttack(int playerMana, int dmgModifer)
        {
            Random random = new Random();
            int maxManaDmg = dmgModifer + 25;

            if (playerMana >= 10)
            {
                int playerAttk = random.Next(0, maxManaDmg);
                Console.WriteLine("You used magic!...You hit for {0}", playerAttk);
                Thread.Sleep(1000);
                return playerAttk;
            }
            else
                Console.WriteLine("You have no mana!... You miss this turn!...");
            Thread.Sleep(2000);
            return 0;
        }
        //Every Day DmgModifer Increases By A Small Amount
        public int DmgModifer(int day)
        {
            int newModifer = 0;
            newModifer = newModifer + day;
            return newModifer;
        }
    }
    #endregion
}
