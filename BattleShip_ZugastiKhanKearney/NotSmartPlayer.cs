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
    class NotSmartPlayer : Player
    {
        public NotSmartPlayer(PlayingBoard pb) : base(pb)
        {
            
        }
        public override Point Play()
        {
            Point p = new Point(-1,-1);
            do
            {
                p = new Point(RD.Next(10), RD.Next(10));
            } while (B.IsAHit(p));


            Rectangle rect = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50))
            };
            B.G.Children.Add(rect);
            Grid.SetRow(rect, (int)p.X);
            Grid.SetColumn(rect, (int)p.Y);

            return p;
        }
    }
}
