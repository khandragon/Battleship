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
    public class HumanPlayer : Player
    {
        Point previousFireLocation = new Point(-1,-1);
        bool hasSelectedFireLocation;
        Point FireLocation { get; set; }
        public HumanPlayer(RadarBoard rb) : base(rb)
        {

        }

        public void selectFireLocation(Point p)
        {
            if (!TriedToHit[(int)p.X][(int)p.Y])
            {
                previousFireLocation = FireLocation;
                FireLocation = p;
                if(previousFireLocation.X >= 0)
                {
                    foreach (UIElement r in B.G.Children)
                    {
                        
                        if (r is Rectangle && Grid.GetRow(r) == previousFireLocation.X && Grid.GetColumn(r) == previousFireLocation.Y)
                        {
                            B.G.Children.Remove(r);
                            break;
                        }
                    }
                }
                Rectangle rect = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(255, 50, 255, 50))
                };
                B.G.Children.Add(rect);
                Grid.SetRow(rect, (int)p.X);
                Grid.SetColumn(rect, (int)p.Y);
                Grid.SetZIndex(rect, 10);
                hasSelectedFireLocation = true;
                B.Bsu.FireBtn.IsEnabled = true;
            }
        }

        public override Point Play()
        {
            B.Bsu.FireBtn.IsEnabled = false;

            if (B.IsAHit(FireLocation))
            {
                Rectangle rect = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(255, 255, 50, 50))
                };
                B.G.Children.Add(rect);
                Grid.SetRow(rect, (int)FireLocation.X);
                Grid.SetColumn(rect, (int)FireLocation.Y);
                Grid.SetZIndex(rect, 11);
            }
            else
            {
                Rectangle rect = new Rectangle
                {
                    Fill = Brushes.AliceBlue
                };
                B.G.Children.Add(rect);
                Grid.SetRow(rect, (int)FireLocation.X);
                Grid.SetColumn(rect, (int)FireLocation.Y);
                Grid.SetZIndex(rect, 11);
            }
            TriedToHit[(int)FireLocation.X][(int)FireLocation.Y] = true;
            hasSelectedFireLocation = false;
            return FireLocation;
        }
    }
}
