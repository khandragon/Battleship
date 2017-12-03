using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip_ZugastiKhanKearney
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BattleShipUI : Window
    {
        public HumanPlayer P { get; }
        NotSmartPlayer dp;
        PlayingBoard pb;
        RadarBoard rb;
        PlayerShip[] allShips = new PlayerShip[5];
        public int TotalShipsPlaced { get; set; }
        double[] originalShipPositionX = new double[5];
        double[] originalShipPositionY = new double[5];
        public BattleShipUI()
        {
            InitializeComponent();
            pb = new PlayingBoard(this);
            pb.Load();
            rb = new RadarBoard(this);
            rb.Load();
            P = new HumanPlayer(rb);
            dp = new NotSmartPlayer(pb, rb);
            dp.PlaceShips();

            Grid[] tempShips = { ShipSize30, ShipSize31, ShipSize2, ShipSize4, ShipSize5 };
            for(int i = 0; i < tempShips.Length; i++)
            {
                originalShipPositionX[i] = Canvas.GetLeft(tempShips[i]);
                originalShipPositionY[i] = Canvas.GetTop(tempShips[i]);
                allShips[i] = new PlayerShip(tempShips[i].RowDefinitions.Count, pb, tempShips[i], originalShipPositionX[i], originalShipPositionY[i], LayoutRoot, this);
            }
        }

        private void FireBtn_Click(object sender, RoutedEventArgs e)
        {
            P.Play();
            dp.Play();
        }

        private void ShowEnemyPositionsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!dp.IsShowing)
            {
                dp.Show();
            }
            else
            {
                dp.Hide();
            }
        }

        private void FinalizePositionBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayerShip s in allShips)
            {
                s.IsFinal = true;
            }
            ShowEnemyPositionsBtn.IsEnabled = true;
            FinalizePositionBtn.IsEnabled = false;
        }
    }
}
