using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleShip_ZugastiKhanKearney
{
    interface IDragable
    {
        void Drop(object sender, MouseButtonEventArgs e);

        void Turn(object sender, MouseButtonEventArgs e);

        void Move(object sender, MouseEventArgs e);

        void Grab(object sender, MouseButtonEventArgs e);
    }
}
