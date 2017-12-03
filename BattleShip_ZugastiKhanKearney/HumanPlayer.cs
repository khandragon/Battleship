using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BattleShip_ZugastiKhanKearney
{
    class HumanPlayer : Player
    {
        public HumanPlayer(RadarBoard rb) : base(rb)
        {

        }
        public override Point Play()
        {
            if (!B.IsAHit(B.LastClickLocation))
            {
                Rectangle rect = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50))
                };
                B.G.Children.Add(rect);
                Grid.SetRow(rect, (int)B.LastClickLocation.X);
                Grid.SetColumn(rect, (int)B.LastClickLocation.Y);
            }
            return B.LastClickLocation;
        }
    }
}
