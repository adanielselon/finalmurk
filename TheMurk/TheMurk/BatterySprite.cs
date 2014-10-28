using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TheMurk
{
    class BatterySprite : MapBoundSprite
    {
        public bool isUsed;
        public Cue sound;
        public SoundBank bank;

        public BatterySprite(Texture2D image, Vector2 position, GameState state, SoundBank bank)
            : base(new SpriteSheet(image, new Point(0, 0), 0.05f), position,
           new CollisionOffset(0, 0, 0, 0), SpriteManager.speed, state)
        {
            this.sound = null;
            this.bank = bank;
            Point frameSize = new Point(238, 283);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 50);
            spriteSheet.setCurrentSegment(0);
            currentFrame = spriteSheet.currentSegment.startFrame;

            isUsed = false;
        }

        public bool isCuePlaying()
        {
            if (sound == null)
            {
                return false;
            }
            return true;
        }

        public void playCue()
        {
            if (sound == null)
            {
                sound = bank.GetCue("battery_nearby");
                sound.Play();
            }
        }

        public void stopCue()
        {
            if (sound != null)
            {
                sound.Stop(AudioStopOptions.Immediate);
                sound = null;
            }
        }
    }
}