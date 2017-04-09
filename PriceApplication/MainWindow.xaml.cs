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

namespace PriceApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int aantal100Consumpties;
        private int aantal150Consumpties;
        private int aantal200Consumpties;
        private int aantal250Consumpties;

        private double totalePrijs;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Initialization();
        }

        private void btnIncreasePrice150_Click(object sender, RoutedEventArgs e)
        {
            aantal150Consumpties++;
            txtPrice150.Text = aantal150Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnDecreasePrice150_Click(object sender, RoutedEventArgs e)
        {
            if( aantal150Consumpties > 0) { aantal150Consumpties--; }
            txtPrice150.Text = aantal150Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnIncreasePrice200_Click(object sender, RoutedEventArgs e)
        {
            aantal200Consumpties++;
            txtPrice200.Text = aantal200Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnDecreasePrice200_Click(object sender, RoutedEventArgs e)
        {
            if (aantal200Consumpties > 0) { aantal200Consumpties--; } 
            txtPrice200.Text = aantal200Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnIncreasePrice250_Click(object sender, RoutedEventArgs e)
        {
            aantal250Consumpties++; 
            txtPrice250.Text = aantal250Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnDecreasePrice250_Click(object sender, RoutedEventArgs e)
        {
            if (aantal250Consumpties > 0) { aantal250Consumpties--; }
            txtPrice250.Text = aantal250Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnIncreasePrice100_Click(object sender, RoutedEventArgs e)
        {
            aantal100Consumpties++; 
            txtPrice100.Text = aantal100Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnDecreasePrice100_Click(object sender, RoutedEventArgs e)
        {
            if (aantal100Consumpties > 0) { aantal100Consumpties--; }
            txtPrice100.Text = aantal100Consumpties.ToString();
            CalculateTotalPrice();
        }

        private void btnAfrekenen_Click(object sender, RoutedEventArgs e)
        {
            PaymentDone();
        }

        #region Helper Methods

        public void CalculateTotalPrice()
        {
            totalePrijs = (aantal100Consumpties * 1) + (aantal150Consumpties * 1.5) + (aantal200Consumpties * 2) + (aantal250Consumpties * 2.5);
            lblTotalePrijsAmount.Content = string.Format("€ {0}", totalePrijs.ToString());
        }

        private void Initialization()
        {
            txtPrice100.Text = aantal100Consumpties.ToString();
            txtPrice150.Text = aantal150Consumpties.ToString();
            txtPrice200.Text = aantal200Consumpties.ToString();
            txtPrice250.Text = aantal250Consumpties.ToString();
        }

        public void Reset()
        {
            aantal100Consumpties = 0;
            aantal150Consumpties = 0;
            aantal200Consumpties = 0;
            aantal250Consumpties = 0;
            totalePrijs = 0;
            lblTotalePrijsAmount.Content = string.Empty;
        }

        public void PaymentDone()
        {
            Reset();
            Initialization();
        }

        #endregion
    }
}
