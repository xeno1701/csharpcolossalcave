using System;
using System.Collections.Generic;

namespace csharpcolossalcave
{
    internal class Program
    {
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
            centralt("   _____      _                     _    _____                   _____  _  _   ", 1);
            centralt("  / ____|    | |                   | |  / ____|                 / ____|| || |_ ", 2);
            centralt(" | |     ___ | | ___  ___ ___  __ _| | | |     __ ___   _____  | |   |_  __  _|", 3);
            centralt(" | |    / _ \\| |/ _ \\/ __/ __|/ _` | | | |    / _` \\ \\ / / _ \\ | |    _| || |_ ", 4);
            centralt(" | |___| (_) | | (_) \\__ \\__ \\ (_| | | | |___| (_| |\\ V /  __/ | |___|_  __  _|", 5);
            centralt("  \\_____\\___/|_|\\___/|___/___/\\__,_|_|  \\_____\\__,_| \\_/ \\___|  \\_____||_||_| ", 6);
            centralt("================================================================================", 7);
            centralt("By DaganHX // Xeno1701", 8);
            centralt("================================================================================", 9);
            Console.WriteLine("");
        }
        public struct room
        {
            public int code;
            public string name;
            public string text;
            public int exitNorth;
            public int exitSouth;
            public int exitWest;
            public int exitEast;
            public List<item> items;
        }
        public struct textforprocessing
        {
            public string original;
            public string processed;
        }

        public struct playablecharacter
        {
            public room currentroom;
            public double health;
            //public double atk;
            //public double def;
            //public double speed;
        }

        public List<item> itemlist = new List<item>();
        public List<item> avaliableitems = new List<item>();
        public room[] Rooms = new room[5];
        public playablecharacter player;
        public List<item> playerinv = new List<item>();
        public List<combineditem> combineditemlist = new List<combineditem>();
        void initAll(bool roomsInit, bool itemsInit)
        {
            player.health = 20.00;

            if (roomsInit == true)
            {
                initialiseRooms();
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
                foreach (room r in Rooms)
                {
                    if (i.startlocation == r.code)
                    {
                        r.items.Add(i);
                    }
                }
            }
        }

        void initialiseRooms()
        {
            Rooms[0].code = 1;
            Rooms[0].name = "a Small Shack";
            Rooms[0].text = "The shack was deserted and full of homemade furniture packed with odds and ends.";
            Rooms[0].exitNorth = 2;
            Rooms[0].exitSouth = 99;
            Rooms[0].exitEast = 99;
            Rooms[0].exitWest = 99;
            Rooms[0].items = new List<item>();

            Rooms[1].code = 2;
            Rooms[1].name = "a Beautiful Meadow";
            Rooms[1].text = "The shack was stood strong and tall between the flowers and the grass, despite it's old look it complements the surroundings well.";
            Rooms[1].exitNorth = 3;
            Rooms[1].exitSouth = 1;
            Rooms[1].exitEast = 4;
            Rooms[1].exitWest = 5;
            Rooms[1].items = new List<item>();

            Rooms[2].code = 3;
            Rooms[2].name = "a Hill";
            Rooms[2].text = "The shack and meadow were visible from the top of the hill, now more beautiful than ever.";
            Rooms[2].exitNorth = 99;
            Rooms[2].exitSouth = 3;
            Rooms[2].exitEast = 99;
            Rooms[2].exitWest = 99;
            Rooms[2].items = new List<item>();

            player.currentroom = Rooms[0];
        }

        static void Main(string[] args)
        {
            var prog = new Program();
            prog.initAll(true, true);
            prog.MainGameLoop();
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
                if (available = true)
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
                player.currentroom = Rooms[player.currentroom.exitNorth - 1];
                lookAround();
            }
        }
        void goSouth()
        {
            if (player.currentroom.exitSouth == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Rooms[player.currentroom.exitSouth - 1];
                lookAround();
            }
        }
        void goEast()
        {
            if (player.currentroom.exitEast == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Rooms[player.currentroom.exitEast - 1];
                lookAround();
            }
        }
        void goWest()
        {
            if (player.currentroom.exitWest == 99)
            {
                errorLocation();
            } else {
                player.currentroom = Rooms[player.currentroom.exitWest - 1];
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
