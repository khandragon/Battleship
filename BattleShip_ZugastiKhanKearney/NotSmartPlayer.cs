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
        RadarBoard rb;
        ComputerShip[] allShips;
        

        public NotSmartPlayer(PlayingBoard pb, RadarBoard rb) : base(pb)
        {
            this.rb = rb;
            allShips = new ComputerShip[5];
            allShips[0] = new ComputerShip(rb, 2);
            allShips[1] = new ComputerShip(rb, 3);
            allShips[2] = new ComputerShip(rb, 3);
            allShips[3] = new ComputerShip(rb, 4);
            allShips[4] = new ComputerShip(rb, 5);
        }

        
        public override Point Play()
        {
            Point p = new Point(-1,-1);
            do
            {
                p = new Point(rb.RD.Next(10), rb.RD.Next(10));
            } while (TriedToHit[(int)p.X][(int)p.Y]);

            TriedToHit[(int)p.X][(int)p.Y] = true;

            Rectangle rect = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(255, (byte)(!B.IsAHit(p) ? 50 : 255), 50, 50))
            };
            B.G.Children.Add(rect);
            Grid.SetRow(rect, (int)p.X);
            Grid.SetColumn(rect, (int)p.Y);
            Grid.SetZIndex(rect, 10);

            return p;
        }

        public void PlaceShips()
        {
            for (int i = 0; i < allShips.Length; i++)
            {
                if (!allShips[i].PlaceInRandomPosition())
                {
                    rb.Reset();
                    i = -1; //-1 because i++ gets called right after
                }
            }
        }

        public bool IsShowing { get; private set; }

        public void Show()
        {
            for(int i = 0; i < allShips.Length; i++)
            {
                allShips[i].Show();
            }
            IsShowing = true;
        }

        public void Hide()
        {
            for(int i = 0; i < allShips.Length; i++)
            {
                allShips[i].Hide();
            }
            IsShowing = false;
        }
    }
}
