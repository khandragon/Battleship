using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BattleShip_ZugastiKhanKearney
{
    class PlayingBoard : Board
    {
        public PlayingBoard(BattleShipUI bsu) : base(bsu)
        {

        }

        public override void ClickLocation(object sender, EventArgs r)
        {
            LastClickLocation = this.GetLocationOfClick();
        }
        

        public bool IsPositionValid(Ship s, Point p)
        {
            if (p.X > 10 - s.Size && !s.IsHorizontal)
            {
                return false;
            }

            if (p.Y > 10 - s.Size && s.IsHorizontal)
            {
                return false;
            }

            if (s.IsHorizontal)
            {
                for (int i = 0; i < s.Size; i++)
                {
                    if (IsOccupied[(int)p.X ][(int)p.Y + i] > 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < s.Size; i++)
                {
                    if (IsOccupied[(int)p.X + i][(int)p.Y ] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void Load()
        {
            Border b = new Border();
            b.BorderBrush = Brushes.Black;
            b.Background = Brushes.Blue;
            b.BorderThickness = new Thickness(2);
            Bsu.MainGrid.Children.Add(b);
            Grid.SetRow(b, 1);
            Grid.SetColumn(b, 1);
            Grid.SetRowSpan(b, 3);
            Bsu.MainGrid.Children.Add(this.G);
            Grid.SetRow(this.G, 1);
            Grid.SetColumn(this.G, 1);
            Grid.SetRowSpan(this.G, 3);
        }
    }
}
