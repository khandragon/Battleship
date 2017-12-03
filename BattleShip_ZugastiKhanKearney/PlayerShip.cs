using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BattleShip_ZugastiKhanKearney
{
    class PlayerShip : Ship, IDragable
    {
        Canvas canvas;
        BattleShipUI bsu;
        public Grid ImageToShow { get; }
        public bool IsFinal { get; set; }
        double originalPositionX;
        double originalPositionY;

        public PlayerShip(int size, PlayingBoard b, Grid imageToShow, double originalPositionX, double originalPositionY, Canvas canvas, BattleShipUI bsu) : base(b, size)
        {
            this.ImageToShow = imageToShow;
            this.originalPositionX = originalPositionX;
            this.originalPositionY = originalPositionY;
            this.canvas = canvas;
            this.bsu = bsu;
            
            imageToShow.AddHandler(Grid.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Grab));
            imageToShow.AddHandler(Grid.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Drop));
            imageToShow.AddHandler(Grid.MouseRightButtonDownEvent, new MouseButtonEventHandler(Turn));
            imageToShow.AddHandler(Grid.MouseMoveEvent, new MouseEventHandler(Move));
        }

        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;

        Point PointOfGrab;

        public Point GetLocationOfClick()
        {
            Point p = Mouse.GetPosition(ImageToShow);

            int row = 0;
            int col = 0;
            double totalHeight = 0.0;
            double totalWidth = 0.0;

            foreach (RowDefinition rd in ((Grid)ImageToShow).RowDefinitions)
            {

                totalHeight += rd.ActualHeight;
                if (totalHeight >= p.Y)
                {
                    break;
                }
                row++;
            }

            foreach (ColumnDefinition cd in ((Grid)ImageToShow).ColumnDefinitions)
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

        public void Drop(object sender, MouseButtonEventArgs e)
        {
            if (!IsFinal)
            {
                Mouse.Capture(null);
                captured = false;
                Point p = ((PlayingBoard)B).GetLocationOfClick();
                if (!this.IsOnGird() || !((PlayingBoard)B).IsPositionValid(this, p))
                {
                    if (wasOnGridBefore && !this.IsOnGird())
                    {
                        bsu.TotalShipsPlaced--;
                        bsu.FinalizePositionBtn.IsEnabled = false;
                    }

                    Canvas.SetLeft(ImageToShow, originalPositionX);
                    Canvas.SetTop(ImageToShow, originalPositionY);
                    if (((Grid)ImageToShow).ActualWidth > ((Grid)ImageToShow).ActualHeight)
                    {
                        flipGrid();
                    }
                }
                else
                {
                    Grid GridToPosition = (Grid)ImageToShow;
                    GridToPosition.Height = (GridToPosition.RowDefinitions.Count == 0 ? 1 : GridToPosition.RowDefinitions.Count) * (B.G.ActualWidth / 10);
                    GridToPosition.Width = (GridToPosition.ColumnDefinitions.Count == 0 ? 1 : GridToPosition.ColumnDefinitions.Count) * (B.G.ActualWidth / 10);

                    double xDisplacement = PointOfGrab.X;
                    double yDisplacement = PointOfGrab.Y;

                    double x = bsu.MainGrid.ColumnDefinitions[0].ActualWidth + (B.G.ActualWidth / 10) * ((p.Y));
                    double y = bsu.MainGrid.RowDefinitions[0].ActualHeight + (B.G.ActualHeight / 10) * ((p.X));
                    Canvas.SetLeft(ImageToShow, x);
                    Canvas.SetTop(ImageToShow, y);

                    if (IsHorizontal)
                    {
                        for (int i = (p.Y > 0 ? -1 : 0); i < Size + (p.Y <= 9 - Size ? 1 : 0); i++)
                        {
                            for (int j = (p.X > 0 ? -1 : 0); j < (p.X <= 8 ? 2 : 1); j++)
                            {
                                B.IsOccupied[(int)p.X + j][(int)p.Y + i]++;
                            }
                        }

                        for (int i = 0; i < Size; i++)
                        {
                            B.AllLocations[(int)p.X][(int)p.Y + i] = Location.WillHit;
                        }
                    }
                    else
                    {
                        for (int i = (p.X > 0 ? -1 : 0); i < Size + (p.X <= 9 - Size ? 1 : 0); i++)
                        {
                            for (int j = (p.Y > 0 ? -1 : 0); j < (p.Y <= 8 ? 2 : 1); j++)
                            {
                                B.IsOccupied[(int)p.X + i][(int)p.Y + j]++;
                            }
                        }
                        for (int i = 0; i < Size; i++)
                        {
                            B.AllLocations[(int)p.X + i][(int)p.Y] = Location.WillHit;
                        }
                    }
                    bsu.TotalShipsPlaced++;
                    if (bsu.TotalShipsPlaced == 5)
                    {
                        bsu.FinalizePositionBtn.IsEnabled = true;
                    }
                }
            }
        }

        private void flipGrid()
        {
            Grid GridToTurn = (Grid)(this.ImageToShow);

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

        public void Turn(object sender, MouseButtonEventArgs e)
        {
            flipGrid();
        }

        public void Move(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                x_shape += x - x_canvas;
                Canvas.SetLeft(ImageToShow, x_shape + PointOfGrab.Y * (B.G.ActualWidth / 10));
                x_canvas = x;
                y_shape += y - y_canvas;
                Canvas.SetTop(ImageToShow, y_shape + PointOfGrab.X * (B.G.ActualWidth / 10));
                y_canvas = y;
            }
        }

        bool wasOnGridBefore = false;

        public void Grab(object sender, MouseButtonEventArgs e)
        {
            if (!IsFinal)
            {
                if (this.IsOnGird())
                {
                    wasOnGridBefore = true;
                }
                else
                {
                    wasOnGridBefore = false;
                }
                PointOfGrab = GetLocationOfClick();
                Mouse.Capture(ImageToShow);
                captured = true;
                x_shape = Canvas.GetLeft(ImageToShow);
                x_canvas = e.GetPosition(canvas).X;
                y_shape = Canvas.GetTop(ImageToShow);
                y_canvas = e.GetPosition(canvas).Y;

                Point PointOnBoard = ((PlayingBoard)B).GetLocationOfClick();

                Point p = (Point)(PointOnBoard - PointOfGrab);

                if (x_shape != originalPositionX && y_shape != originalPositionY)
                {
                    if (IsHorizontal)
                    {
                        for (int i = (p.Y > 0 ? -1 : 0); i < Size + (p.Y <= 9 - Size ? 1 : 0); i++)
                        {
                            for (int j = (p.X > 0 ? -1 : 0); j < (p.X <= 8 ? 2 : 1); j++)
                            {
                                B.IsOccupied[(int)p.X + j][(int)p.Y + i]--;
                            }
                        }

                        for (int i = 0; i < Size; i++)
                        {
                            B.AllLocations[(int)p.X][(int)p.Y + i] = Location.WillMiss;
                        }
                    }
                    else
                    {
                        for (int i = (p.X > 0 ? -1 : 0); i < Size + (p.X <= 9 - Size ? 1 : 0); i++)
                        {
                            for (int j = (p.Y > 0 ? -1 : 0); j < (p.Y <= 8 ? 2 : 1); j++)
                            {
                                B.IsOccupied[(int)p.X + i][(int)p.Y + j]--;
                            }
                        }
                        for (int i = 0; i < Size; i++)
                        {
                            B.AllLocations[(int)p.X + i][(int)p.Y] = Location.WillMiss;
                        }
                    }
                }
            }
        }
    }
}
