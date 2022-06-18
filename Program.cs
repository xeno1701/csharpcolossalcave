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
        void title()
        {
            Console.WriteLine("   _____      _                     _    _____                   _____  _  _   ");
            Console.WriteLine("  / ____|    | |                   | |  / ____|                 / ____|| || |_ ");
            Console.WriteLine(" | |     ___ | | ___  ___ ___  __ _| | | |     __ ___   _____  | |   |_  __  _|");
            Console.WriteLine(" | |    / _ \\| |/ _ \\/ __/ __|/ _` | | | |    / _` \\ \\ / / _ \\ | |    _| || |_ ");
            Console.WriteLine(" | |___| (_) | | (_) \\__ \\__ \\ (_| | | | |___| (_| |\\ V /  __/ | |___|_  __  _|");
            Console.WriteLine("  \\_____\\___/|_|\\___/|___/___/\\__,_|_|  \\_____\\__,_| \\_/ \\___|  \\_____||_||_| ");
            Console.WriteLine("_______________________________________________________________________________");
            Console.WriteLine("                              By DaganHX // Xeno1701");
            Console.WriteLine("_______________________________________________________________________________");
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

        public struct playablecharacter
        {
            public room currentroom;
            public List<item> inv = new List<item>();
            public double health;
            public double atk;
            public double def;
            public double speed;
        }

        public List<item> itemlist = new List<item>();
        public room[] Rooms = new room[5];
        public playablecharacter player;

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

            tempitem.code = 1;
            tempitem.name = "Gold Coins";
            tempitem.text = "The coins were legal tender, detailed with text and images of the kingdom";
            tempitem.startlocation = 1;
            tempitem.active = false;
            itemlist.Add(tempitem);

            tempitem.code = 1;
            tempitem.name = "Small Coin Wallet";
            tempitem.text = "The handcrafted leather coin wallet was mildly aged and was perfect for throwing thiefs off the tracks if they wanted to steal something...";
            tempitem.startlocation = 1;
            tempitem.active = false;
            itemlist.Add(tempitem);

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
        bool parser(string input)
        {
            bool keepPlaying = true;
            input = capsfive(input);
            switch (input)
            {
                case "N":
                    goNorth();
                    break;
                case "S":
                    goSouth();
                    break;
                case "E":
                    goEast();
                    break;
                case "W":
                    goWest();
                    break;
                case "L":
                    lookAround();
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
            }
            else
            {
                player.currentroom = Rooms[player.currentroom.exitNorth - 1];
                lookAround();
            }
        }
        void goSouth()
        {
            if (player.currentroom.exitSouth == 99)
            {
                errorLocation();
            }
            else
            {
                player.currentroom = Rooms[player.currentroom.exitSouth - 1];
                lookAround();
            }
        }
        void goEast()
        {
            if (player.currentroom.exitEast == 99)
            {
                errorLocation();
            }
            else
            {
                player.currentroom = Rooms[player.currentroom.exitEast - 1];
                lookAround();
            }
        }
        void goWest()
        {
            if (player.currentroom.exitWest == 99)
            {
                errorLocation();
            }
            else
            {
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
