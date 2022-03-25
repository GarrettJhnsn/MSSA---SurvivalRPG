using System;
using System.Threading;

namespace Survival_RPG
{
    class Program
    {
        #region Main Game Loop
        static void Main(string[] args)
        {
            Sound sound = new Sound();
            Intro();

            //Starting Game, Player Not Playing
            bool gameState = true;
            bool isPlaying = false;
            do
            {
                bool currentGame = true;

                //Creating Game
                //Creating Player   
                Game game = new Game();
                Player player = new Player();

                //Music
                sound.playMusic(0);

                //Menu
                Menu();

                //Starting New Game
                do
                {
                    Console.Clear();
                    sound.playMusic(2, isPlaying);
                    //If game.Day == 1 Intro Story Plays
                    CurrentPhase(game.Day);

                    //Displays Current Day
                    Console.WriteLine("Current Day: {0}", game.Day);

                    Console.WriteLine("");
                    Random random = new Random();

                    //Checking If There Is An Encounter Today // 5% Chance To Not Encounter //Returns True False
                    bool dailyEncounter = IsThereEncounter();

                    //Checking If Player Wants To Continue Or Leave
                    bool userDecision = DailyDecision();


                    //If True Next Day Starts
                    if (userDecision == true)
                    {
                        if (dailyEncounter == true)
                        {

                            sound.playMusic(1);
                            isPlaying = false;

                            //Generates New Enemy With Random Health Based On Game Day
                            Enemy enemy = new Enemy(NewEnemyHealth(game.Day));
                            Console.WriteLine("A {0} has just appeared in front of you with {1} health!", enemy.Type(), enemy.Health);
                            Console.WriteLine("");
                            Thread.Sleep(1000);

                            //Currently In A Encounter. Combat Loop Begins
                            bool currentEncounter = true;
                            do
                            {
                                //Displays Game Status. Player Health, Mana, Stamina and Health
                                game.Status(player.Health, player.Mana, player.Stamina, enemy.Health);
                                Thread.Sleep(1000);
                                //Randomly Chooses Enemy Attack
                                int enemyChoice = random.Next(1, 4);
                                //User Choosing Their Attack
                                int userChoice = UserAttack();

                                //If Loop To Determine Players Chosen Attack Or Inventory Or Main Menu/Quit
                                if (userChoice == 1)
                                {
                                    sound.playSFX(userChoice);
                                    userChoice = player.PlayerManaAttack(player.Mana, player.DmgModifer(game.Day));
                                    player.Mana = player.ManaConsume(player.Mana);
                                    Thread.Sleep(1000);
                                }
                                else if (userChoice == 2)
                                {
                                    sound.playSFX(userChoice);
                                    userChoice = player.PlayerStaminaMeleeAttack(player.Stamina, player.DmgModifer(game.Day));
                                    player.Stamina = player.StaminaConsume(player.Stamina);
                                    Thread.Sleep(1000);
                                }
                                else if (userChoice == 3)
                                {
                                    sound.playSFX(6);
                                    userChoice = player.PlayerStaminaBowAttack(player.Stamina, player.DmgModifer(game.Day));
                                    player.Stamina = player.StaminaConsume(player.Stamina);
                                    Thread.Sleep(1000);
                                }
                                else if (userChoice == 4)
                                {
                                    Console.Clear();
                                    //Player Chooses Item From Inventory
                                    int item = player.UseInventory();

                                    if (item == 1)
                                    {
                                        player.Heal();
                                        Console.Clear();
                                    }
                                    else if (item == 2)
                                    {
                                        player.Recharge();
                                        Console.Clear();
                                    }
                                    else if (item == 3)
                                    {
                                        player.Drink();
                                        Console.Clear();
                                    }
                                    else
                                        Console.Clear();
                                    continue;

                                }
                                else if (userChoice == 5)
                                {
                                    //False False == End Of Game/ Main Menu
                                    currentEncounter = false;
                                    currentGame = false;
                                }

                                //Updates Enemy Health After Being Attacked
                                enemy.Health = Combat(userChoice, enemy.Health);

                                //Checks To See If Enemy Was Killed
                                if (enemy.Health == 0)
                                {
                                    sound.playSFX(3);

                                    Console.WriteLine("You have defeated the enemy!");
                                    Thread.Sleep(2000);

                                    //Day Increases By 1
                                    game.Day++;
                                    player.Sleep();
                                    //Ends Current Day
                                    currentEncounter = false;
                                }

                                //Enemy Attacks With Random Choice Through enemyChoice
                                else if (userChoice != 6)
                                {
                                    sound.playSFX(enemyChoice);
                                    int enemyAttk = enemy.EnemyAttack(enemyChoice, game.Day);

                                    //Players Health Is Updated After Attack
                                    player.Health = Combat(enemyAttk, player.Health);
                                    Thread.Sleep(1000);
                                }

                                //Checks To See If Player Health Is Equal To Zero
                                if (player.Health == 0)
                                {
                                    sound.playSFX(4);
                                    Console.WriteLine("You have been defeated... Game Over...");
                                    Console.WriteLine("You survived for {0} days...", game.Day);
                                    Thread.Sleep(2000);
                                    //If Player Is Dead Game Ends // False False
                                    currentEncounter = false;
                                    currentGame = false;
                                }
                                Console.Clear();
                                //While Loop To Continue Fight Until Either Enemy Or Player Health Is Equal To Zero
                            } while (currentEncounter != false);
                        }
                        //If Player Encounters No Enemy Day Ends
                        else if (dailyEncounter == false)
                        {
                            Console.WriteLine("You luckily encountered no enemies today!");
                            Thread.Sleep(2000);
                            Console.WriteLine("");
                            Console.WriteLine("You set up camp and sleep till the next morning");
                            Thread.Sleep(2000);
                            isPlaying = true;
                            player.Sleep();
                            game.Day++;
                        }
                    }
                    //If User Wants To Either Quit Or Go To Main Menu Current Game Ends
                    else if (userDecision == false)
                    {
                        currentGame = false;
                    }
                    //Active Game Player Is Playing Looping Until Player Either Quits Or Goes To Main Menu
                } while (currentGame != false);

                //Exit Program
            } while (gameState != false);
        }


        #endregion

        #region Methods
        //If Day Is Equal To One, Story Line Is Presented
        public static void CurrentPhase(int day)
        {
            if (day == 1)
            {
                Console.Clear();
                Console.WriteLine("Greetings traveler!");
                Console.WriteLine("");
                Thread.Sleep(4000);
                Console.WriteLine("You are traveling alone across the great world of [placeholder] in seek of new adventures... ");
                Thread.Sleep(4000);
                Console.WriteLine("But be cautious!...");
                Console.WriteLine("");
                Thread.Sleep(4000);
                Console.WriteLine("You will encounter many dangerous foes along your journey! Fight back and survive!  ");
                Thread.Sleep(4000);
                Console.WriteLine("Every day you survive you will grow a little bit stronger...");
                Thread.Sleep(4000);
                Console.WriteLine("As will the enemies!");
                Console.WriteLine("");
                Thread.Sleep(4000);
                Console.WriteLine("Be Safe...Your Journey Begins Now!");
                Thread.Sleep(4000);
                Console.Clear();
            }
            else if (day == 10)
            {
                Console.WriteLine("Phase 2 Begins...");
                Thread.Sleep(4000);
                Console.Clear();
            }

        }
        //Generates Min - Max Value Random Health Can Be For Enemy
        public static int NewEnemyHealth(int currentDay)
        {
            Random random = new Random();
            int enemyHealthMin = (currentDay + 25);
            int enemyHealthMax = (currentDay * 4) + 25;
            int enemyHealth = random.Next(enemyHealthMin, enemyHealthMax);
            return enemyHealth;
        }

        public static bool IsThereEncounter()
        {
            //95% Chance To Encounter Enemy. 5% Chance to not encounter enemy.
            Random random = new Random();
            int newEncounter = random.Next(1, 20);

            if (newEncounter == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Every Day User Is Presented With Options Before Facing Enemy
        public static bool DailyDecision()
        {
            Sound sound = new Sound();

            Console.WriteLine("[1]Continue down the road?");
            Console.WriteLine("[2]Main Menu");
            Console.WriteLine("[3]Quit");

            try
            {
                int userChoice = Convert.ToInt32(Console.ReadLine());
                sound.playSFX(5);
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("You continue down the road!");
                        Console.Clear();
                        return true;
                    case 2:
                        return false;
                    case 3:
                        Console.WriteLine("Exiting...");
                        System.Environment.Exit(0);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }
        //User Is Prompted To Choose Attack/Inventory/Main Menu/Quit
        public static int UserAttack()
        {
            Sound selectionSound = new Sound();

            Console.WriteLine("What will you do?");
            Console.WriteLine("");
            Console.WriteLine("[1]Magic Attack");
            Console.WriteLine("[2]Melee Attack");
            Console.WriteLine("[3]Bow Attack");
            Console.WriteLine("[4]Inventory");
            Console.WriteLine("[5]Main Menu");
            Console.WriteLine("[6]Exit Game");
            Console.WriteLine("");

            bool selectionMade = false;
            int userChoice;

            try
            {
                do
                {
                    //Options Are Returned To Main Game Loop Where Descisions Are Proccesed
                    userChoice = Convert.ToInt32(Console.ReadLine());
                    selectionSound.playSFX(5);

                    if (userChoice == 1)
                    {
                        return 1;
                    }
                    else if (userChoice == 2)
                    {
                        return 2;
                    }
                    else if (userChoice == 3)
                    {
                        return 3;
                    }
                    else if (userChoice == 4)
                    {
                        return 4;
                    }
                    else if (userChoice == 5)

                    {
                        return 5;
                    }
                    else if (userChoice == 6)
                    {
                        Console.WriteLine("Exiting...");
                        System.Environment.Exit(0);
                    }
                    else
                        Console.WriteLine("You chose nothing!...Better luck next turn!");
                    Thread.Sleep(1000);
                } while (selectionMade != true);
                return userChoice;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
            return 0;
        }

        //Takes In Players Attack Damage and Enemy Health Returns New Health. Vice Versa Enemy Damage Player Health
        public static int Combat(int damage, int health)
        {
            int newHealth = health - damage;
            if (newHealth < 0)
            {
                newHealth = 0;
            }
            return newHealth;
        }

        //Intro To Game
        public static void Intro()
        {
            Console.WriteLine("");
            Console.WriteLine("***************************");
            Thread.Sleep(500);
            Console.WriteLine("            A            ");
            Console.WriteLine("");
            Thread.Sleep(500);
            Console.WriteLine("         Work In            ");
            Console.WriteLine("");
            Thread.Sleep(500);
            Console.WriteLine("       Progress By            ");
            Console.WriteLine("");
            Thread.Sleep(500);
            Console.WriteLine("     Garrett Johnson            ");
            Console.WriteLine("");
            Thread.Sleep(500);
            Console.WriteLine("***************************");
            Thread.Sleep(2000);
            Console.Clear();
        }
        //Main Menu To Game
        public static void Menu()
        {
            Sound menuSound = new Sound();

            Console.Clear();
            Thread.Sleep(1000);
            ASCIIArt();
            Console.WriteLine("");
            Console.WriteLine("Survival RPG");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("[1]Start New Game");
            Console.WriteLine("[2]Credits");
            Console.WriteLine("[3]Quit Game");
            Console.WriteLine("");

            try
            {
                int userChoice = Convert.ToInt32(Console.ReadLine());
                menuSound.playSFX(5);

                switch (userChoice)
                {
                    case 1:
                        break;
                    case 2:
                        Credits();
                        userChoice = 0;
                        break;
                    case 3:
                        Console.WriteLine("Exiting...");
                        System.Environment.Exit(0);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //Credits To Game
        public static void Credits()
        {
            Console.Clear();
            Thread.Sleep(500);
            Console.WriteLine("***************************************");
            Console.WriteLine("");
            Console.WriteLine("   Programming       Garrett Johnson");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("   Music             Garrett Johnson");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("   SFX               Garrett Johnson");
            Thread.Sleep(1000);
            Console.WriteLine("                     Epic Stock Media");
            Thread.Sleep(2000);
            Console.WriteLine("");
            Console.WriteLine("   ASCII Art         Joan Stark [jgs]");
            Thread.Sleep(2000);
            Menu();
        }
        //ASCII Shield Art
        public static void ASCIIArt()
        {

            Console.WriteLine(@"   |\                     /)");
            Console.WriteLine(@" /\_\\__               (_//");
            Console.WriteLine(@"|   `>\-`     _._       //`)");
            Console.WriteLine(@" \ /` \\  _.-`:::`-._  //");
            Console.WriteLine(@"  `    \|`    :::    `|/");
            Console.WriteLine(@"        |     :::     |");
            Console.WriteLine(@"        |.....:::.....|");
            Console.WriteLine(@"        |:::::::::::::|");
            Console.WriteLine(@"        |     :::     |");
            Console.WriteLine(@"        \     :::     /");
            Console.WriteLine(@"         \    :::    /");
            Console.WriteLine(@"          `-. ::: .-'");
            Console.WriteLine(@"   jgs     //`:::`\\");
            Console.WriteLine(@"          //   '   \\");
            Console.WriteLine(@"         |/         \\");
        }
        #endregion
    }
}