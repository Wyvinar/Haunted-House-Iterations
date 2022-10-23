using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse
{
    class Item
    {
        private int itemNo;
        private string itemName;
        private int itemLoc;
        private int itemValue = 50;
        private string itemDesc;

        public Item(int itemNo, string itemName, int itemLoc, string itemDesc, int itemValue)
        {
            this.itemNo = itemNo;
            this.itemName = itemName;
            this.itemLoc = itemLoc;
            this.itemDesc = itemDesc;
            this.itemValue = itemValue;
        }

        public Item(int itemNo, string itemName, int itemLoc, string itemDesc)
        {
            this.itemNo = itemNo;
            this.itemName = itemName;
            this.itemLoc = itemLoc;
            this.itemDesc = itemDesc;
        }

        public string GetName()
        {
            return itemName;
        }

        public int GetLocation()
        {
            return itemLoc;
        }

        public int GetValue()
        {
            return itemValue;
        }

        public string GetDescription()
        {
            return itemDesc;
        }

        public void SetLocation(int itemLoc)
        {
            this.itemLoc = itemLoc;
        }
    }
}
