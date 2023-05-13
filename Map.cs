using csharpcolossalcave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static csharpcolossalcave.Program;

namespace csharpcolossal_cave
{
    class Map
    {
        Program fileprogram = new Program();
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
        public room[] Rooms = new room[5];

        public void initialiseRooms()
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

            fileprogram.player.currentroom = Rooms[0];
        }
    }
}
