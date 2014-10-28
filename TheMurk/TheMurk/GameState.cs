using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace TheMurk
{
    class GameState
    {
        public static readonly int EAST = 1;
        public static readonly int WEST = 2;
        public static readonly int NORTH = 3;
        public static readonly int SOUTH = 4;

        private bool isEast, isWest, isNorth, isSouth;
        private int gameTime;
        private int losingTime;

        public SoundBank bank;
        public Cue[] cues = new Cue[3];


        public GameState(SoundBank sBank)
        {
            isEast = false;
            isWest = false;
            isNorth = false;
            isSouth = false;
            this.bank = sBank;

            cues[0] = null;
            cues[1] = null;
            cues[2] = null;
        }

        public bool isCuePlaying(int i)
        {
            if (cues[i] == null)
            {
                return false;
            }
            return true;
        }

        public void playCue(String str, int i)
        {
            if (cues[i] == null)
            {
                cues[i] = bank.GetCue(str);
                cues[i].Play();
            }
        }

        public void stopCue(int i)
        {
            if (cues[i] != null)
            {
                cues[i].Stop(AudioStopOptions.Immediate);
                cues[i] = null;
            }
        }

        public bool getState(int direction)
        {
            if(direction == EAST){
                return isEast;
            }
            if (direction == WEST)
            {
                return isWest;
            }
            if (direction == NORTH)
            {
                return isNorth;
            }
            if (direction == SOUTH)
            {
                return isSouth;
            }
            return false;
        }

        public void setState(int direction, bool set)
        {
            if (direction == EAST)
            {
                isEast = set;
            }
            if (direction == WEST)
            {
                isWest = set;
            }
            if (direction == NORTH)
            {
                isNorth = set;
            }
            if (direction == SOUTH)
            {
                isSouth = set;
            }
        }

        public void setGameTime(int gameTime)
        {
            this.gameTime = gameTime;
        }

        public int getGameTime()
        {
            return gameTime;
        }

        public void setLosingTime(int losingTime)
        {
            this.losingTime = losingTime;
        }

        public int getLosingTime()
        {
            return losingTime;
        }

        public void playCue(String cueName)
        {
            bank.PlayCue(cueName);
        }
    }
}
