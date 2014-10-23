using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheMurk
{
    class GameState
    {
        public static readonly int EAST = 1;
        public static readonly int WEST = 2;
        public static readonly int NORTH = 3;
        public static readonly int SOUTH = 4;

        private bool isEast, isWest, isNorth, isSouth;
        private bool collision;

        private bool collisionLeftRight;
        private bool collisionTopBottom;


        public GameState()
        {
            isEast = false;
            isWest = false;
            isNorth = false;
            isSouth = false;
            collision = false;
            collisionLeftRight = false;
            collisionTopBottom = false;
        }

        public void collisionOccured(Sprite player, Sprite sprite)
        {

            if (player.position.X + player.spriteSheet.currentSegment.frameSize.X == sprite.position.X + 1 || player.position.X == sprite.position.X + sprite.spriteSheet.currentSegment.frameSize.X - 1)
            {
                collisionLeftRight = true;
            }
            if (player.position.Y + player.spriteSheet.currentSegment.frameSize.Y == sprite.position.Y + 1 || player.position.Y == sprite.position.Y + sprite.spriteSheet.currentSegment.frameSize.Y - 1)
            {
                collisionTopBottom = true;
            }
        }

        public bool getCollisionTopBottom()
        {
            return collisionTopBottom;
        }

        public bool getCollisionLeftRight()
        {
            return collisionLeftRight;
        }

        public void setCollisionLeftRight(bool set)
        {
            collisionLeftRight = set;
        }

        public void setCollisionTopBottom(bool set)
        {
            collisionTopBottom = set;
        }

        public bool isCollision()
        {
            return collision;
        }

        public void setCollision(bool collision)
        {
            this.collision = collision;
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
    }
}
