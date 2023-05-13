using csharpcolossal_cave;
using System;
using System.Collections.Generic;
using System.Data;

namespace csharpcolossalcave
{
    class Program
    {
       Map Map = new Map();
        public struct item
        {
            public int code;
            public string name;
            public string text;
            public int startlocation;
            public bool active;
        }
        public struct combineditem
        {
            public int code;
            public string name;
            public string text;
            public int item1;
            public int item2;
        }
        void centralt(string input, int ypos)
        {
            int xpos = (Console.WindowWidth / 2) - (input.Length / 2);
            Console.SetCursorPosition(xpos, ypos);
            Console.WriteLine(input);
        }
        void title()
        {
            centralt("                .                                              ", 1);
            centralt("        .                  .     ╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗  ╔═══╗╔═╗ ╔╗  ╔═══╗╔╗ ╔╗╔═══╗╔═══╗╔═══╗╔═══╗", 2);
            centralt("  .         .                    ║╔══╝║╔═╗║║╔═╗║║╔═╗║║╔═╗║║╔═╗║╚╗╔╗║║╔══╝  ║╔═╗║║ ╚╗║║  ║╔══╝║║ ║║║╔═╗║║╔═╗║║╔═╗║║╔═╗║", 3);
            centralt("                                 ║╚══╗║╚══╗║║ ╚╝║║ ║║║╚═╝║║║ ║║ ║║║║║╚══╗  ║║ ║║║╔╗╚╝║  ║╚══╗║║ ║║║╚═╝║║║ ║║║╚═╝║║║ ║║", 4);
            centralt("                                 ║╔══╝╚══╗║║║ ╔╗║╚═╝║║╔══╝║╚═╝║ ║║║║║╔══╝  ║║ ║║║║╚╗ ║  ║╔══╝║║ ║║║╔╗╔╝║║ ║║║╔══╝║╚═╝║", 5);
            centralt("         *              .        ║╚══╗║╚═╝║║╚═╝║║╔═╗║║║   ║╔═╗║╔╝╚╝║║╚══╗  ║╚═╝║║║ ║ ║  ║╚══╗║╚═╝║║║║╚╗║╚═╝║║║   ║╔═╗║", 6);
            centralt("          \\    .                 ╚═══╝╚═══╝╚═══╝╚╝ ╚╝╚╝   ╚╝ ╚╝╚═══╝╚═══╝  ╚═══╝╚╝ ╚═╝  ╚═══╝╚═══╝╚╝╚═╝╚═══╝╚╝   ╚╝ ╚╝", 7);
            centralt("           \\                .         ", 8);
            centralt(" .          \\          .           ", 9);
            centralt("", 10);
            centralt("", 11);
            centralt("", 12);
            centralt("", 13);
            centralt("", 14);
            centralt("", 15);
            Console.WriteLine("");
        }
        
        public struct textforprocessing
        {
            public string original;
            public string processed;
        }

        public struct playablecharacter
        {
            public Map.room currentroom;
            public double health;
            //public double atk;
            //public double def;
            //public double speed;
        }

        public List<item> itemlist = new List<item>();
        public List<item> avaliableitems = new List<item>();
        
        public playablecharacter player;
        public List<item> playerinv = new List<item>();
        public List<combineditem> combineditemlist = new List<combineditem>();
        void initAll(bool roomsInit, bool itemsInit)
        {
            player.health = 20.00;

            if (roomsInit == true)
            {
                Map.initialiseRooms();
            }
            if (itemsInit == true)
            {
                initialiseItems();
            }
        }
        void initialiseItems()
        {
            item tempitem;
            combineditem tempcombineditem;

            tempitem.code = 1;
            tempitem.name = "Gold Coins";
            tempitem.text = "The coins were legal tender, detailed with text and images of the kingdom.";
            tempitem.startlocation = 1;
            tempitem.active = false;
            itemlist.Add(tempitem);
            avaliableitems.Add(tempitem);

            tempitem.code = 2;
            tempitem.name = "Small Coin Wallet";
            tempitem.text = "The handcrafted leather coin wallet was mildly aged and was perfect for throwing thiefs off the tracks if they wanted to steal something...";
            tempitem.startlocation = 1;
            tempitem.active = false;
            itemlist.Add(tempitem);
            avaliableitems.Add(tempitem);

            tempcombineditem.code = 3;
            tempcombineditem.name = "Bag of coins";
            tempcombineditem.text = "This will stop thieves from knowing if you have gold on you or not.";
            tempcombineditem.item1 = 1;
            tempcombineditem.item2 = 2;
            combineditemlist.Add(tempcombineditem);

            foreach (item i in itemlist)
            {
                foreach (Map.room r in Map.Rooms)
                {
                    if (i.startlocation == r.code)
                    {
                        r.items.Add(i);
                    }
                }
            }
        }

        

        static void Main(string[] args)
        {
            Program Program = new Program();    
            Program.initAll(true, true);
            Program.MainGameLoop();
        }

        void MainGameLoop()
        {
            bool inMainLoop = true;
            title();
            lookAround();
            while (inMainLoop == true)
            {
                string input = read();
                inMainLoop = parser(input);
            }

        }
        void listing(string itemname)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  * ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(itemname);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        void itemlisting(List<item> toscreen)
        {
            foreach (item i in toscreen)
            {
                item temp = i;
                bool available = avaliableitems.Contains(temp);
                if (available == true)
                {
                    listing(temp.name);
                } else {

                }
                
            }
        }
        void scavenge()
        {
            int itemcount = 0;
            for (int i = 0; i < player.currentroom.items.Count; i++)
            {
                if (avaliableitems.Contains(player.currentroom.items[i]))
                {
                    itemcount++;
                }
             }
            if (itemcount > 0)
            {
                print("You notice in your vicinity some items:");
                itemlisting(player.currentroom.items);
                Console.WriteLine();
            } else {
                print("You notice no items nearby.");
                Console.WriteLine();
            }
        }
        void takeItem()
        {
            if (player.currentroom.items.Count > 0)
            {
                print("What would you like to take?");
                itemlisting(player.currentroom.items);
                Console.WriteLine();
                textforprocessing chosenitem;
                chosenitem.original = read();
                chosenitem.processed = capsfive(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < player.currentroom.items.Count; i++)
                {
                    if ((chosenitem.processed == capsfive(player.currentroom.items[i].name)) && (avaliableitems.Contains(player.currentroom.items[i])))
                    {
                        avaliableitems.Remove(player.currentroom.items[i]);
                        playerinv.Add(player.currentroom.items[i]);
                        itemfound = true;
                        print("You have now obtained " + player.currentroom.items[i].name + ".");
                        Console.WriteLine();
                        player.currentroom.items.Remove(player.currentroom.items[i]);
                    }
                }
                if (!itemfound)
                {
                    print("You couldn't see " + chosenitem.original + " nearby.");
                    Console.WriteLine();
                }
            } else {
                print("You notice no items nearby.");
                Console.WriteLine();
            }
        }
        void dropItem()
        {
            if (playerinv.Count > 0)
            {
                print("What would you like to drop?");
                itemlisting(playerinv);
                Console.WriteLine();
                textforprocessing chosenitem;
                chosenitem.original = read();
                chosenitem.processed = capsfive(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < playerinv.Count; i++)
                {
                    if ((chosenitem.processed == capsfive(playerinv[i].name)) && (!avaliableitems.Contains(playerinv[i])))
                    {
                        avaliableitems.Add(playerinv[i]);
                        player.currentroom.items.Add(playerinv[i]);
                        itemfound = true;
                        print("You have now dropped " + playerinv[i].name + ".");
                        Console.WriteLine();
                        playerinv.Remove(playerinv[i]);
                    }
                }
                if (!itemfound)
                {
                    print("You couldn't see " + chosenitem.original + " in your inventory.");
                    Console.WriteLine();
                }
            }
            else
            {
                print("Your bag is empty.");
                Console.WriteLine();
            }
        }
        void inspectItem()
        {
            if (playerinv.Count > 0)
            {
                print("What would you like to inspect?");
                itemlisting(playerinv);
                Console.WriteLine();
                textforprocessing chosenitem;
                chosenitem.original = read();
                chosenitem.processed = capsfive(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < playerinv.Count; i++)
                {
                    if ((chosenitem.processed == capsfive(playerinv[i].name)) && (!avaliableitems.Contains(playerinv[i])))
                    {
                        itemfound = true;
                        print(playerinv[i].text);
                        Console.WriteLine();
                    }
                }
                if (!itemfound)
                {
                    print("You couldn't see " + chosenitem.original + " in your inventory.");
                    Console.WriteLine();
                }
            }
            else
            {
                print("Your bag is empty.");
                Console.WriteLine();
            }
        }
        void showInv()
        {
            if (playerinv.Count > 0)
            {
                print("You look in your bag:");
                itemlisting(playerinv);
            } else {
                print("There are no items in your bag.");
                Console.WriteLine();
            }
        }
        bool parser(string input)
        {
            bool keepPlaying = true;
            input = capsfive(input);
            switch (input)
            {
                case "N": case "NORTH":
                    goNorth();
                    break;
                case "S": case "SOUTH":
                    goSouth();
                    break;
                case "E": case "EAST":
                    goEast();
                    break;
                case "W": case "WEST":
                    goWest();
                    break;
                case "L": case "LOOK":
                    lookAround();
                    break;
                case "H": case "HUNT": case "SCAVE":
                    scavenge();
                    break;
                case "T": case "TAKE":
                    takeItem();
                    break;
                case "I": case "INVEN": case "BAG":
                    showInv();
                    break;
                case "D": case "DROP":
                    dropItem();
                    break;
                case "INSP": case "INSPE":
                    inspectItem();
                    break;
                case "QUIT":
                    keepPlaying = false;
                    break;
                default:
                    print("Unfortunately, that command is not understood.");
                    print("Type a COMMAND to continue.");
                    print("");
                    break;
            }
            return keepPlaying;
        }
        void goNorth()
        {
            if (player.currentroom.exitNorth == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Map.Rooms[player.currentroom.exitNorth - 1];
                lookAround();
            }
        }
        void goSouth()
        {
            if (player.currentroom.exitSouth == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Map.Rooms[player.currentroom.exitSouth - 1];
                lookAround();
            }
        }
        void goEast()
        {
            if (player.currentroom.exitEast == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Map.Rooms[player.currentroom.exitEast - 1];
                lookAround();
            }
        }
        void goWest()
        {
            if (player.currentroom.exitWest == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Map.Rooms[player.currentroom.exitWest - 1];
                lookAround();
            }
        }
        void errorLocation()
        {
            print("Unfortunately, that direction is blocked.");
            print("Type a COMMAND to continue.");
            print("");
        }
        void currentLocation()
        {
            print("You are in " + player.currentroom.name + ".");
            print("");
        }

        void lookAround()
        {
            print("You are in " + player.currentroom.name + ".");
            print(player.currentroom.text);
            print("");
        }

        void print(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        string read()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("   @>  ");
            Console.ForegroundColor = ConsoleColor.White;
            return Console.ReadLine();
        }
        string capsfive(string input)
        {
            if (input.Length > 4)
            {
                input = input.Substring(0, 5).ToUpper();
            }
            else
            {
                input = input.ToUpper();
            }
            return input;
        }
    }
}
