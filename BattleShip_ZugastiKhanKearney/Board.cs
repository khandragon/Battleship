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
    abstract class Board
    {
        public Grid G { get; }
        public BattleShipUI Bsu { get; }
        public Point LastClickLocation { get; protected set; }
        public int[][] IsOccupied { get; set; }
        

        public Board(BattleShipUI bsu)
        {
            IsOccupied = new int[10][];
            for (int i = 0; i < IsOccupied.Length; i++)
            {
                IsOccupied[i] = new int[10];
            }
            G = new Grid();
            FillGrid();
            this.Bsu = bsu;
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

        abstract public bool IsAHit(Point p);
    }
}