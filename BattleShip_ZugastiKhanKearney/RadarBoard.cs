using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleShip_ZugastiKhanKearney
{
    class RadarBoard : Board
    {
        public RadarBoard(BattleShipUI bsu) : base(bsu)
        {

        }

        public override void ClickLocation (object sender, EventArgs r)
        {

        }

        public override bool IsAHit(Point p)
        {
            return false;
        }
        public override void Load()
        {
            Border b = new Border();
            b.BorderBrush = Brushes.Black;
            b.Background = Brushes.Blue;
            b.BorderThickness = new Thickness(2);
            Bsu.MainGrid.Children.Add(b);
            Grid.SetRow(b, 1);
            Grid.SetColumn(b, 3);
            Grid.SetRowSpan(b, 3);
            Bsu.MainGrid.Children.Add(this.G);
            Grid.SetRow(this.G, 1);
            Grid.SetColumn(this.G, 3);
            Grid.SetRowSpan(this.G, 3);
        }
    }
}
