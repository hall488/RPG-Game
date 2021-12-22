using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_test_game.Game
{
    public class Card
    {
        public string name { get; set; }
        public int level { get; set; }
        public int dmg { get; set; }
        public int range { get; set; }
        public int targets { get; set; }
        public int xp { get; set; }
        public int xppt { get; set; }
        public bool discard { get; set; }
        public int prio { get; set; }
        public int move { get; set; }


    }
}
