using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleShip_ZugastiKhanKearney
{
    public abstract class Player
    {
        public Board B { get; set; }
        public bool[][] TriedToHit { get; set; } = new bool[10][];

        public Player(Board B)
        {
            this.B = B;
            for (int i = 0; i < 10; i++)
            {
                TriedToHit[i] = new bool[10];
            }
        }

        abstract public Point Play();
    }
}
