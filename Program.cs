//        ####################     BODGE CENTRAL    ####################
#pragma warning disable CA2241 // Provide correct arguments to formatting methods
#pragma warning disable IDE0090 // New expression can be simplified
#pragma warning disable IDE0057 // Substring can be simplified

//        ####################    LIBRARY IMPORTS   ####################
using System;
using System.Collections.Generic;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace csharpcolossalcave
{
    class Program
    {
        // #################### VARIABLE DECLARATION ####################
            // Rooms
        public Room[] Rooms = new Room[5];
            // Items
        public List<Item> itemlist = new List<Item>();
        public List<Item> avaliableitems = new List<Item>();
        public List<CombinedItem> combineditemlist = new List<CombinedItem>();
            // Player
        public PlayableCharacter player;
        public List<Item> playerinv = new List<Item>();

        // #################### CLASS/STRUCT DECLARE ####################
            // Items
        public enum ItemType
        {
            Effectior = -1, //These items are like armour or the anti-radiation pendant
            Generic = 0, //These items are like pebbles
            Food = 1,
            Weapon = 2,
            Ship = 3 //For building the ship
        }
        public struct Item
        {
            public int code;
            public string name;
            public string text;
            public ItemType type;
            public int startlocation;
            public bool active;
            public double itemradiolevel;
        }
        public struct CombinedItem
        {
            public int code;
            public string name;
            public string text;
            public ItemType type;
            public int item1;
            public int item2;
        }
            // Rooms
        public struct Room
        {
            public int code;
            public string name;
            public string text;
            public MapRegion roomregion;
            public double radiolevel;
            public int exitNorth;
            public int exitSouth;
            public int exitWest;
            public int exitEast;
            public List<Item> items;
        }
        public struct Hotspot
        {
            int code;
            string name;
            string text;
            int location;
            string searchtext;

        }
        public enum MapRegion
        {
            LightSurface,
            DarkSurface,
            Underground,
            Space,
            DebugNone
        }
            // Player
        public struct PlayableCharacter
        {
            public Room currentroom;
            public double health;
            public double radiolevel;
            public double atk;
            public double def;
            public double speed;
        }
            // Misc
        public struct TextForParsing
        {
            public string original;
            public string processed;
        }

        // #################### INITIALISATION STUFF ####################
            // All
        void InitAll(bool roomsInit, bool itemsInit)
        {
            player.health = 20.00;

            if (roomsInit == true)
            {
                InitialiseRooms();
            }
            if (itemsInit == true)
            {
                InitialiseItems();
            }
        }
            // Rooms
        public void InitialiseRooms()
        {
            Rooms[0].code = 1;
            Rooms[0].name = "a Small Shack";
            Rooms[0].text = "The shack was deserted and full of homemade furniture packed with odds and ends.";
            Rooms[0].radiolevel = 0.0;
            Rooms[0].exitNorth = 2;
            Rooms[0].exitSouth = -1;
            Rooms[0].exitEast = -1;
            Rooms[0].exitWest = -1;
            Rooms[0].items = new List<Item>();

            Rooms[1].code = 2;
            Rooms[1].name = "a Beautiful Meadow";
            Rooms[1].text = "The shack was stood strong and tall between the flowers and the grass, despite it's old look it complements the surroundings well.";
            Rooms[1].radiolevel = 1.0;
            Rooms[1].exitNorth = 3;
            Rooms[1].exitSouth = 1;
            Rooms[1].exitEast = 4;
            Rooms[1].exitWest = 5;
            Rooms[1].items = new List<Item>();

            Rooms[2].code = 3;
            Rooms[2].name = "a Hill";
            Rooms[2].text = "The shack and meadow were visible from the top of the hill, now more beautiful than ever.";
            Rooms[2].radiolevel = 0.5;
            Rooms[2].exitNorth = -1;
            Rooms[2].exitSouth = 3;
            Rooms[2].exitEast = -1;
            Rooms[2].exitWest = -1;
            Rooms[2].items = new List<Item>();

            player.currentroom = Rooms[0];
        }
            // Items
        void InitialiseItems()
        {
            Item tempitem;
            CombinedItem tempcombineditem;

            tempitem.code = 1;
            tempitem.name = "Gold Coins";
            tempitem.text = "The coins were legal tender, detailed with text and images of the kingdom.";
            tempitem.type = ItemType.Generic;
            tempitem.startlocation = 1;
            tempitem.itemradiolevel = 0.5;
            tempitem.active = false;
            itemlist.Add(tempitem);
            avaliableitems.Add(tempitem);

            tempitem.code = 2;
            tempitem.name = "Small Coin Wallet";
            tempitem.text = "The handcrafted leather coin wallet was mildly aged and was perfect for throwing thiefs off the tracks if they wanted to steal something...";
            tempitem.type = ItemType.Generic;
            tempitem.startlocation = 1;
            tempitem.itemradiolevel = 0.7;
            tempitem.active = false;
            itemlist.Add(tempitem);
            avaliableitems.Add(tempitem);

            tempcombineditem.code = 3;
            tempcombineditem.name = "Bag of coins";
            tempcombineditem.text = "This will stop thieves from knowing if you have gold on you or not.";
            tempcombineditem.type = ItemType.Generic;
            tempcombineditem.item1 = 1;
            tempcombineditem.item2 = 2;
            combineditemlist.Add(tempcombineditem);

            foreach (Item i in itemlist)
            {
                foreach (Room r in Rooms)
                {
                    if (i.startlocation == r.code)
                    {
                        r.items.Add(i);
                    }
                }
            }
        }

        // ####################     TITLES STUFF     ####################
            // Splash
        static void Title()
        {
            Console.WriteLine("                .                                              ", 1);
            Console.WriteLine("        .                  .     ╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗  ╔═══╗╔═╗ ╔╗  ╔═══╗╔╗ ╔╗╔═══╗╔═══╗╔═══╗╔═══╗", 2);
            Console.WriteLine("  .         .                    ║╔══╝║╔═╗║║╔═╗║║╔═╗║║╔═╗║║╔═╗║╚╗╔╗║║╔══╝  ║╔═╗║║ ╚╗║║  ║╔══╝║║ ║║║╔═╗║║╔═╗║║╔═╗║║╔═╗║", 3);
            Console.WriteLine("                                 ║╚══╗║╚══╗║║ ╚╝║║ ║║║╚═╝║║║ ║║ ║║║║║╚══╗  ║║ ║║║╔╗╚╝║  ║╚══╗║║ ║║║╚═╝║║║ ║║║╚═╝║║║ ║║", 4);
            Console.WriteLine("                                 ║╔══╝╚══╗║║║ ╔╗║╚═╝║║╔══╝║╚═╝║ ║║║║║╔══╝  ║║ ║║║║╚╗ ║  ║╔══╝║║ ║║║╔╗╔╝║║ ║║║╔══╝║╚═╝║", 5);
            Console.WriteLine("         *              .        ║╚══╗║╚═╝║║╚═╝║║╔═╗║║║   ║╔═╗║╔╝╚╝║║╚══╗  ║╚═╝║║║ ║ ║  ║╚══╗║╚═╝║║║║╚╗║╚═╝║║║   ║╔═╗║", 6);
            Console.WriteLine("          \\    .                 ╚═══╝╚═══╝╚═══╝╚╝ ╚╝╚╝   ╚╝ ╚╝╚═══╝╚═══╝  ╚═══╝╚╝ ╚═╝  ╚═══╝╚═══╝╚╝╚═╝╚═══╝╚╝   ╚╝ ╚╝", 7);
            Console.WriteLine("           \\                .         ", 8);
            Console.WriteLine(" .          \\          .           ", 9);
            Console.WriteLine("      .                            ", 10);
            Console.WriteLine("      .                            .           .          ,                        *           .                        *           .            *                       .", 11);
            Console.WriteLine(".               .                                                .                     *                       .", 12);
            Console.WriteLine(".           ,             .                       .", 13);
            Console.WriteLine("                   .                              .        .                          .              .                 *           .", 14);
            Console.WriteLine(" .                        .                           .           .                       . ", 15);
            Console.WriteLine("    .                         .                     ,            ,                    .   *          .          .                          *                       .", 16);
            Console.WriteLine("        .              .                     .           ", 17);
            Console.WriteLine("      .                        .                             ,       .                *               .               .                       .         ", 18);
            Console.WriteLine(".      .                    .             .                                                          ,       ,", 19);
            Console.WriteLine("____^/\\___^--____/\\-##________________/\\/\\---/\\___________---_________/\\____________/\\____________/\\", 20);
            Console.WriteLine("   /\\^   ^  ^    ^                  ^^ ^  '\\ ^          ^       ---                                -\\  ", 21);
            Console.WriteLine("         --           -            --  -      -         ---  __       ^         ^                   -\\", 22);
            Console.WriteLine("--  __                      ___--  ^  ^                         --  __                             \\_    .        .              .  ", 23);
            Console.WriteLine("                                                                                     --                 -\\             ", 24);
            Console.WriteLine("");
            Thread.Sleep(2500);
            Console.Clear();
            TitleDecor("Escapade on Europa / By Dagan Harris.");
            Thread.Sleep(1500);
            Console.Clear();
            TitleDecor("        Now programmed in C#!        ");
            Thread.Sleep(1500);
            Console.Clear();
        }

        // ####################    GAME LOOP STUFF   ####################
            // Main & Loop
        static void Main()
        {
            Program Program = new Program();
            Program.InitAll(true, true);
            Program.MainGameLoop();
        }
        void MainGameLoop()
        {
            bool inMainLoop = true;
            Title();
            CurrentLocation(true);
            while (inMainLoop == true)
            {
                string input = Read();
                inMainLoop = Parser(input);
            }

        }
            // Parser
        bool Parser(string input)
        {
            bool keepPlaying = true;
            input = CapsFiveChars(input);
            Checkifalive();
            Radiation();
            switch (input)
            {
                case "N":
                case "NORTH":
                    Go(0);
                    break;
                case "S":
                case "SOUTH":
                    Go(1);
                    break;
                case "E":
                case "EAST":
                    Go(2);
                    break;
                case "W":
                case "WEST":
                    Go(3);
                    break;
                case "L":
                case "LOOK":
                    LookAround();
                    break;
                case "H":
                case "HUNT":
                case "SCAVE":
                    ItemSearch();
                    break;
                case "T":
                case "TAKE":
                    TakeItem();
                    break;
                case "I":
                case "INVEN":
                case "BAG":
                    ShowInv();
                    break;
                case "D":
                case "DROP":
                    DropItem();
                    break;
                case "INSP":
                case "INSPE":
                    InspectItem();
                    break;
                case "QUIT":
                    keepPlaying = false;
                    break;
                default:
                    Print("Unfortunately, that command is not understood.");
                    Print("Type a COMMAND to continue.");
                    Print("");
                    break;
            }
            return keepPlaying;
        }

        // ####################     PLAYER STUFF     ####################
            // Inventory
        void ShowInv()
        {
            if (playerinv.Count > 0)
            {
                Print("You look in your bag:");
                ItemListing(playerinv);
            }
            else
            {
                Print("There are no items in your bag.");
                Console.WriteLine();
            }
        }
            // Player In Map
        void CurrentLocation(bool linefeed)
        {
            Print("You are in " + player.currentroom.name + ".");
            if (linefeed)
            {
                Print("");
            }
        }

        void LookAround()
        {
            CurrentLocation(false);
            Print(player.currentroom.text);
            Print("");
        }
        // Health 
        void Radiation()
        {
            double countspermin = (player.currentroom.radiolevel + Averageinvradiation()) * 60;
            if (countspermin > 10)
            {
                player.health = -(countspermin / 16);
            }
        }

        void Checkifalive()
        {
            double countspermin = (player.currentroom.radiolevel + verageinvradiation()) * 60;
            if (player.health <= 0)
            {
                if (countspermin / 16 > 1)
                {
                    Die("Most Likely Radiation Poisoning");
                } else {
                    Die("");
                }
            }
        }

        static void Die(string cause)
        {
            Console.Clear();
            Thread.Sleep(400);
            TitleDecor("You Died!");
            Thread.Sleep(2000);
            Console.Clear();
            Thread.Sleep(400);

        if (cause.Length != 0) 
            {
                TitleDecor("Cause of death: " + cause + ".");
                Thread.Sleep(2000);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Environment.Exit(0);
        }
        double Averageinvradiation()
        {
            double averageinvradiation = 0;
            if (playerinv.Count >= 1) {
                foreach (Item i in playerinv)
                {
                    averageinvradiation =+ i.itemradiolevel;
                }

                averageinvradiation /= playerinv.Count;
            return averageinvradiation;
            } else {
                return 0;
            }
        }

        // ####################      ITEM STUFF      ####################
            // Listing
        void ItemListing(List<Item> toscreen)
        {
            foreach (Item i in toscreen)
            {
                Item temp = i;
                bool available = avaliableitems.Contains(temp);
                if (available == true)
                {
                    BulletList(temp.name);
                }
                else
                {

                }

            }
        }
            // Scavenge
        void ItemSearch()
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
                Print("You notice in your vicinity some items:");
                ItemListing(player.currentroom.items);
                Console.WriteLine();
            }
            else
            {
                Print("You notice no items nearby.");
                Console.WriteLine();
            }
        }
            // Take & Drop
        void TakeItem()
        {
            if (player.currentroom.items.Count > 0)
            {
                Print("What would you like to take?");
                ItemListing(player.currentroom.items);
                Console.WriteLine();
                TextForParsing chosenitem;
                chosenitem.original = Read();
                chosenitem.processed = CapsFiveChars(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < player.currentroom.items.Count; i++)
                {
                    if ((chosenitem.processed == CapsFiveChars(player.currentroom.items[i].name)) && (avaliableitems.Contains(player.currentroom.items[i])))
                    {
                        avaliableitems.Remove(player.currentroom.items[i]);
                        playerinv.Add(player.currentroom.items[i]);
                        itemfound = true;
                        Print("You have now obtained " + player.currentroom.items[i].name + ".");
                        Console.WriteLine();
                        player.currentroom.items.Remove(player.currentroom.items[i]);
                    }
                }
                if (!itemfound)
                {
                    Print("You couldn't see " + chosenitem.original + " nearby.");
                    Console.WriteLine();
                }
            }
            else
            {
                Print("You notice no items nearby.");
                Console.WriteLine();
            }
        }
        void DropItem()
        {
            if (playerinv.Count > 0)
            {
                Print("What would you like to drop?");
                ItemListing(playerinv);
                Console.WriteLine();
                TextForParsing chosenitem;
                chosenitem.original = Read();
                chosenitem.processed = CapsFiveChars(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < playerinv.Count; i++)
                {
                    if ((chosenitem.processed == CapsFiveChars(playerinv[i].name)) && (!avaliableitems.Contains(playerinv[i])))
                    {
                        avaliableitems.Add(playerinv[i]);
                        player.currentroom.items.Add(playerinv[i]);
                        itemfound = true;
                        Print("You have now dropped " + playerinv[i].name + ".");
                        Console.WriteLine();
                        playerinv.Remove(playerinv[i]);
                    }
                }
                if (!itemfound)
                {
                    Print("You couldn't see " + chosenitem.original + " in your inventory.");
                    Console.WriteLine();
                }
            }
            else
            {
                Print("Your bag is empty.");
                Console.WriteLine();
            }
        }
            // Inspect
        void InspectItem()
        {
            if (playerinv.Count > 0)
            {
                Print("What would you like to inspect?");
                ItemListing(playerinv);
                Console.WriteLine();
                TextForParsing chosenitem;
                chosenitem.original = Read();
                chosenitem.processed = CapsFiveChars(chosenitem.original);
                bool itemfound = false;
                for (int i = 0; i < playerinv.Count; i++)
                {
                    if ((chosenitem.processed == CapsFiveChars(playerinv[i].name)) && (!avaliableitems.Contains(playerinv[i])))
                    {
                        itemfound = true;
                        Print(playerinv[i].text);
                        Console.WriteLine();
                    }
                }
                if (!itemfound)
                {
                    Print("You couldn't see " + chosenitem.original + " in your inventory.");
                    Console.WriteLine();
                }
            }
            else
            {
                Print("Your bag is empty.");
                Console.WriteLine();
            }
        }

        // ####################    MOVEMENT STUFF    ####################
            // Moving
        void Go(int Option)
        {
            switch (Option)
            {
                case 0:
                    if (player.currentroom.exitNorth == -1)
                    {
                        MovementBlocked();
                    }
                    else
                    {
                        player.currentroom = Rooms[player.currentroom.exitNorth - 1];
                        LookAround();
                    }
                    break;
                case 1:
                    if (player.currentroom.exitSouth == -1)
                    {
                        MovementBlocked();
                    }
                    else
                    {
                        player.currentroom = Rooms[player.currentroom.exitSouth - 1];
                        LookAround();
                    }
                    break;
                case 2:
                    if (player.currentroom.exitEast == -1)
                    {
                        MovementBlocked();
                    }
                    else
                    {
                        player.currentroom = Rooms[player.currentroom.exitEast - 1];
                        LookAround();
                    }
                    break;
                case 3:
                    if (player.currentroom.exitWest == -1)
                    {
                        MovementBlocked();
                    }
                    else
                    {
                        player.currentroom = Rooms[player.currentroom.exitWest - 1];
                        LookAround();
                    }
                    break;
            }
        }
            //Error
        static void MovementBlocked()
        {
            Print("Unfortunately, that direction is blocked.");
            Print("Type a COMMAND to continue.");
            Print("");
        }

        // ####################     HOTSPOT STUFF    ####################

        // ####################     COMBAT STUFF     ####################

        // ####################      MISC STUFF      ####################
            // Title
        static void TitleDecor(string Text)
        {
            var length = Text.Length + 8;
            Console.SetCursorPosition((Console.WindowWidth / 2) - (length / 2), 2);
            for (int i = 0; i < length; i++)
            {
                Console.Write("=");

            }
            Console.SetCursorPosition((Console.WindowWidth / 2) - ((length - 8) / 2), 4);
            Console.Write(Text);
            Console.SetCursorPosition((Console.WindowWidth / 2) - (length / 2), 6);
            for (int i = 0; i < length; i++)
            {
                Console.Write("=");

            }
        }
            // Listing
        static void BulletList(string itemname)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  * ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(itemname);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
            // Output
        static void Print(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for(int i = 0; i < input.Length; i++)
            {
                Console.Write((char)input[i]);
                Thread.Sleep(10);
            }
            Thread.Sleep(100);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static string Read()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("   @>  ");
            Console.ForegroundColor = ConsoleColor.White;
            var Readfromconsole = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return Readfromconsole;
        }
            // Parsing
        static string CapsFiveChars(string input)
        {
            if (input.Length > 4)
            {
                input = input.Substring(0,5).ToUpper();
            }
            else
            {
                input = input.ToUpper();
            }
            return input;
        }
    }
}
