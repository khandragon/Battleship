using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleShip_ZugastiKhanKearney
{
    abstract class Player
    {
        public Board B { get; set; }
        public Random RD { get; set; } = new Random();

        public Player(Board B)
        {
            this.B = B;
        }

        abstract public Point Play();
    }
}
