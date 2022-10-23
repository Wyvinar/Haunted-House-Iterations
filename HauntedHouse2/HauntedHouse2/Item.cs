using System;
using System.Collections.Generic;
using System.Text;

namespace HauntedHouse2
{
    class Item
    {
        protected int itemNum;
        protected int itemLoc;
        protected int itemVal;
        protected int itemDura = -1;
        protected string itemName;
        protected string itemDesc;

        public Item(int itemNum, string itemName, int itemLoc, string itemDesc, int itemVal)
        {
            this.itemNum = itemNum;
            this.itemName = itemName;
            this.itemLoc = itemLoc;
            this.itemDesc = itemDesc;
            this.itemVal = itemVal;
        }

        public Item(int itemNum, string itemName, int itemLoc)
        {
            this.itemNum = itemNum;
            this.itemName = itemName;
            this.itemLoc = itemLoc;
        }


        public int GetItemNum()
        {
            return itemNum;
        }

        public string GetItemName()
        {
            return itemName;
        }

        public int GetItemLoc()
        {
            return itemLoc;
        }

        public int GetItemVal()
        {
            return itemVal;
        }

        public string GetItemDesc()
        {
            return itemDesc;
        }

        public int GetItemDura()
        {
            return itemDura;
        }

        public void SetItemDura(int itemUses)
        {
            this.itemDura = itemUses;
        }

        public void SetItemLoc(int itemLoc)
        {
            this.itemLoc = itemLoc;
        }

        public override string ToString()
        {
            if (itemDura > 0)
                return String.Format("Name:\t\t{0}\nValue:\t\t{1}g\nUses:\t\t{3}\nDescription:\t{2}", itemName, itemVal, itemDesc, itemDura);
            else if (itemDura == -1)
                return String.Format("Name:\t\t{0}\nValue:\t\t{1}g\nDescription:\t{2}", itemName, itemVal, itemDesc);
            else
                return "";
        }
    }

    class Consumable : Item
    {
        public Consumable(int itemNum, string itemName, int itemLoc, string itemDesc, int itemVal, int itemDura):base(itemNum, itemName, itemLoc, itemDesc, itemVal)
        {
            this.itemDura = itemDura;
        }

        public Consumable(int itemNum, string itemName, int itemLoc) : base(itemNum, itemName, itemLoc)
        {

        }
    }

    class Key : Consumable
    {
        private int roomUnlock;
        public Key(int itemNum, string itemName, int itemLoc, string itemDesc, int itemVal, int itemDura, int roomUnlock) : base(itemNum, itemName, itemLoc, itemDesc, itemVal, itemDura)
        {
            this.roomUnlock = roomUnlock;
        }
    }

    class Torch : Consumable
    {
        public Torch(int itemNum, string itemName, int itemLoc, string itemDesc, int itemVal, int itemDura) : base(itemNum, itemName, itemLoc, itemDesc, itemVal, itemDura)
        {

        }
    }
}
