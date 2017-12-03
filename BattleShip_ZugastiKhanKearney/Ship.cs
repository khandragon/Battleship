using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BattleShip_ZugastiKhanKearney
{
    abstract class Ship
    {
        public Board B { get; }
        public bool IsHorizontal { get; protected set; }
        public int Size { get; }
        public Ship(Board b, int size)
        {

            this.Size = size;
            this.B = b;
        }

        public bool IsOnGird()
        {
            Point currentPosition = Mouse.GetPosition(B.G);
            if (currentPosition.X >= 0 && currentPosition.X <= this.B.G.ActualWidth && currentPosition.Y >= 0 && currentPosition.Y <= this.B.G.ActualHeight)
            {
                return true;
            }
            return false;
        }
    }
}
