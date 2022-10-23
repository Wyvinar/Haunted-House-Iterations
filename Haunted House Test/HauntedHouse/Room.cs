using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse
{
    class Room
    {
        private int roomNo;
        private string roomName;
        private string roomDescription;

        public Room(int roomNo, string roomName, string roomDescription)
        {
            this.roomNo = roomNo;
            this.roomName = roomName;
            this.roomDescription = roomDescription;
        }

        public int getRoomNo()
        {
            return roomNo;
        }

        public string getRoomName()
        {
            return roomName;
        }

        public string getRoomDescritpion()
        {
            return roomDescription;
        }
    }
}
