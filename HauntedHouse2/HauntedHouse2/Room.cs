using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse2
{
    class Room
    {
        private int roomNum;
        private string roomName;
        private string roomDesc;
        private bool locked;
        private bool lit;
        private int[] accessibles;

        public Room(int roomNum, string roomName, bool locked, bool lit, int[] accessibles, string roomDesc)
        {
            this.roomNum = roomNum;
            this.roomName = roomName;
            this.locked = locked;
            this.lit = lit;
            this.accessibles = accessibles;
            this.roomDesc = roomDesc;
        }


        public int GetRoomNum()
        {
            return roomNum;
        }

        public string GetRoomName()
        {
            return roomName;
        }

        public string GetRoomDesc()
        {
            return roomDesc;
        }

        public bool GetLocked()
        {
            return locked;
        }

        public bool GetLit()
        {
            return lit;
        }

        public int[] GetAccessibles()
        {
            return accessibles;
        }



        public void SetLocked(bool locked)
        {
            this.locked = locked;
        }

        public void SetLit(bool lit)
        {
            this.lit = lit;
        }
    }
}
