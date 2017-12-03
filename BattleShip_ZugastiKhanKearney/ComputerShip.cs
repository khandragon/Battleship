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
    class ComputerShip : Ship
    {
        Grid actualShip = new Grid();
        public ComputerShip(RadarBoard b, int size) : base(b, size)
        {
            for(int i = 0; i < size; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(10, GridUnitType.Star);
                actualShip.RowDefinitions.Add(rd);
            }
            for(int i = 0; i < size; i++)
            {
                Rectangle rect = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(0, 50, 50, 50))
                };
                actualShip.Children.Add(rect);
                Grid.SetRow(rect, i);
            }
            actualShip.Height = 50 * size;
            actualShip.Width = 50;
        }
        public void Show()
        {
            foreach (Rectangle r in actualShip.Children)
            {
                r.Fill = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
            }
        }
    
        public void Hide()
        {
            foreach (Rectangle r in actualShip.Children)
            {
                r.Fill = new SolidColorBrush(Color.FromArgb(0, 50, 50, 50));
            }
        }

        public bool PlaceInRandomPosition()
        {
            bool turnedVertical = B.RD.Next(100) > 50 ? true : false;
            B.G.Children.Add(this.actualShip);
            Point whereToBePlaced = new Point(-1, -1);
            if(turnedVertical)
            {
                flipGrid();
            }
            if (IsHorizontal)
            {
                int trialCounter = 0;
                do
                {
                    whereToBePlaced.X = B.RD.Next(0, 10 - Size);
                    whereToBePlaced.Y = B.RD.Next(0, 10);
                    trialCounter++;
                    if (trialCounter > 100)
                    {
                        return false;
                    }
                } while (!((RadarBoard)B).IsPositionValid(this, whereToBePlaced));                
            }
            else
            {
                int trialCounter = 0;
                do
                {
                    whereToBePlaced.X = B.RD.Next(0, 10);
                    whereToBePlaced.Y = B.RD.Next(0, 10 - Size);
                    trialCounter++;
                    if(trialCounter > 100)
                    {
                        return false;
                    }
                } while (!((RadarBoard)B).IsPositionValid(this, whereToBePlaced));
            }

            if (IsHorizontal)
            {
                for (int i = (whereToBePlaced.Y > 0 ? -1 : 0); i < Size + (whereToBePlaced.Y <= 9 - Size ? 1 : 0); i++)
                {
                    for (int j = (whereToBePlaced.X > 0 ? -1 : 0); j < (whereToBePlaced.X <= 8 ? 2 : 1); j++)
                    {
                        B.IsOccupied[(int)whereToBePlaced.X + j][(int)whereToBePlaced.Y + i]++;
                    }
                }
                for(int i = 0; i < Size; i++)
                {
                    B.AllLocations[(int)whereToBePlaced.X][(int)whereToBePlaced.Y + i] = Location.WillHit;
                }
            }
            else
            {
                for (int i = (whereToBePlaced.X > 0 ? -1 : 0); i < Size + (whereToBePlaced.X <= 9 - Size ? 1 : 0); i++)
                {
                    for (int j = (whereToBePlaced.Y > 0 ? -1 : 0); j < (whereToBePlaced.Y <= 8 ? 2 : 1); j++)
                    {
                        B.IsOccupied[(int)whereToBePlaced.X + i][(int)whereToBePlaced.Y + j]++;
                    }
                }
                for (int i = 0; i < Size; i++)
                {
                    B.AllLocations[(int)whereToBePlaced.X + i][(int)whereToBePlaced.Y] = Location.WillHit;
                }
            }

            Grid.SetRow(actualShip, (int)whereToBePlaced.X);
            Grid.SetColumn(actualShip, (int)whereToBePlaced.Y);
            Grid.SetColumnSpan(actualShip, IsHorizontal ? Size : 1);
            Grid.SetRowSpan(actualShip, IsHorizontal ? 1 : Size);
            
            Grid.SetZIndex(actualShip, 10);

            return true;
        }

        private void flipGrid()
        {
            Grid GridToTurn = this.actualShip;

            RowDefinitionCollection rdc = GridToTurn.RowDefinitions;
            ColumnDefinitionCollection cdc = GridToTurn.ColumnDefinitions;

            if (!IsHorizontal)
            {
                double temp = GridToTurn.Width;
                GridToTurn.Width = GridToTurn.Height;
                GridToTurn.Height = temp;

                foreach (RowDefinition rd in rdc)
                {
                    ColumnDefinition cd = new ColumnDefinition();
                    cd.Width = new GridLength(10, GridUnitType.Star);
                    cdc.Add(cd);
                }

                for (int i = 0; i < GridToTurn.Children.Count; i++)
                {
                    Grid.SetColumn(GridToTurn.Children[i], i);
                    Grid.SetRow(GridToTurn.Children[i], 0);
                }

                while (rdc.Count > 0)
                {
                    rdc.RemoveAt(rdc.Count - 1);
                }
            }
            else
            {
                double temp = GridToTurn.Height;
                GridToTurn.Height = GridToTurn.Width;
                GridToTurn.Width = temp;

                foreach (ColumnDefinition cd in cdc)
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(10, GridUnitType.Star);
                    rdc.Add(rd);
                }

                for (int i = 0; i < GridToTurn.Children.Count; i++)
                {
                    Grid.SetRow(GridToTurn.Children[i], i);
                    Grid.SetColumn(GridToTurn.Children[i], 0);
                }

                while (cdc.Count > 0)
                {
                    cdc.RemoveAt(cdc.Count - 1);
                }
            }

            IsHorizontal = !IsHorizontal;
        }
    }
}
