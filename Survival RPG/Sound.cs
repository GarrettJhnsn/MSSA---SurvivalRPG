using System;
using System.Media;

namespace Survival_RPG
{
    class Sound
    {

        //Plays Music Without Checking If Music Is Already Playing
        public void playMusic(int x)
        {
            SoundPlayer soundPlayer = new SoundPlayer();

            try
            {
                if (x == 0)
                {

                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_background1_master.wav";
                    soundPlayer.PlayLooping();

                }
                else if (x == 1)
                {
                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_combat1_master.wav";
                    soundPlayer.PlayLooping();

                }
                else if (x == 2)
                {
                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_theme1_master.wav";
                    soundPlayer.PlayLooping();
                }
            }
            catch (Exception)
            {
                Console.Clear();
            }
        }
        //Plays Music While Checking If Music Is Already Playing.
        //If Music Is Playing Do Not Play Music
        public void playMusic(int x, bool isPlaying)
        {
            SoundPlayer soundPlayer = new SoundPlayer();

            try
            {
                if (x == 0 && isPlaying != true)
                {

                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_background1_master.wav";
                    soundPlayer.PlayLooping();

                }
                else if (x == 1 && isPlaying != true)
                {
                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_combat1_master.wav";
                    soundPlayer.PlayLooping();

                }
                else if (x == 2 && isPlaying != true)
                {
                    soundPlayer.SoundLocation = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\Music\main_theme1_master.wav";
                    soundPlayer.PlayLooping();
                }
            }
            catch (Exception)
            {
                Console.Clear();
            }
        }

        //Playing SFX When Triggered
        public void playSFX(int x)
        {
            WMPLib.WindowsMediaPlayer sfx = new WMPLib.WindowsMediaPlayer();
            try
            {
                if (x == 1)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\magic_attack2_master.wav";
                    sfx.controls.play();
                }
                else if (x == 2)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\melee_attack1_master.wav";
                    sfx.controls.play();
                }
                else if (x == 3)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\victorious1.wav";
                    sfx.controls.play();
                }
                else if (x == 4)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\defeated1.wav";
                    sfx.controls.play();
                }
                else if (x == 5)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\selection_sfx1_master.wav";
                    sfx.controls.play();
                }
                else if (x == 6)
                {
                    sfx.URL = @"C:\Users\garre\Desktop\MSSA\Survival RPG\Survival RPG\sfx\bow_attack1_master.wav";
                    sfx.controls.play();
                }
            }
            catch (Exception)
            {
                Console.Clear();
            }
        }
    }
}
