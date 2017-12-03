using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BattleShip_ZugastiKhanKearney
{
    public abstract class Board
    {
        public Grid G { get; }
        public BattleShipUI Bsu { get; }
        public Point LastClickLocation { get; protected set; }
        public int[][] IsOccupied { get; set; }
        public Location[][] AllLocations { get; set; }
        public Random RD { get; set; } = new Random();


        public Board(BattleShipUI bsu)
        {
            IsOccupied = new int[10][];
            AllLocations = new Location[10][];
            for (int i = 0; i < IsOccupied.Length; i++)
            {
                AllLocations[i] = new Location[10];
                for(int j = 0; j < AllLocations[i].Length; j++)
                {
                    AllLocations[i][j] = Location.WillMiss;
                }
                IsOccupied[i] = new int[10];
            }
            G = new Grid();
            FillGrid();
            this.Bsu = bsu;
        }
        public Point GetLocationOfClick()
        {
            Point p = Mouse.GetPosition(G);

            int row = 0;
            int col = 0;
            double totalHeight = 0.0;
            double totalWidth = 0.0;

            foreach (RowDefinition rd in G.RowDefinitions)
            {
                totalHeight += rd.ActualHeight;
                if (totalHeight >= p.Y)
                {
                    break;
                }
                row++;
            }

            foreach (ColumnDefinition cd in G.ColumnDefinitions)
            {
                totalWidth += cd.ActualWidth;
                if (totalWidth >= p.X)
                {
                    break;
                }
                col++;
            }

            return new Point(row, col);
        }

        public bool IsAHit(Point p)
        {
            if (AllLocations[(int)p.X][(int)p.Y] == Location.WillHit)
            {
                return true;
            }
            return false;
        }

        private void FillGrid()
        {
            this.G.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            this.G.AddHandler(Grid.PreviewMouseLeftButtonDownEvent, new RoutedEventHandler(ClickLocation));

            this.G.ShowGridLines = true;
            
            for (int col = 0; col < 10; col++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(10, GridUnitType.Star);
                G.ColumnDefinitions.Add(cd);
            }

            
            for (int row = 0; row < 10; row++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(10, GridUnitType.Star);
                G.RowDefinitions.Add(rd);
            }
        }

        abstract public void Load();

        abstract public void ClickLocation(object sender, EventArgs r);
    }
}