using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static csharpcolossalcave.Program;

namespace csharpcolossal_cave
{
    class Map
    {
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
    }
}
