using PriceApplication.Classes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PriceApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Klant> klanten;
        private Klant geselecteerdeKlant;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events

        private void Window_Initialized(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            EnableOrDisableButtons(klanten.Count > 0);
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                Klant klant = new Klant()
                {
                    KlantNaam = (bool)chkIsPloeg.IsChecked ? $"{txtCustomerName.Text} ({txtAantalAanwezig.Text} personen aanwezig) [{txtSpelersNamen.Text}]" : $"{txtCustomerName.Text}",
                    KlantID = klanten.Count + 1,
                    isPloeg = (bool)chkIsPloeg.IsChecked
                };
                klanten.Add(klant);
                geselecteerdeKlant = klanten.OrderBy(x => x.KlantID).Last();
                DisplayPrices();
                listCustomers.ItemsSource = klanten;
                txtCustomerName.Text = string.Empty;
            }
            EnableOrDisableButtons(klanten.Count > 0);
        }

        private void btnIncreasePrice150_Click(object sender, RoutedEventArgs e)
        {
            geselecteerdeKlant.Aantal150Consumpties++;
            txtPrice150.Text = geselecteerdeKlant.Aantal150Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnDecreasePrice150_Click(object sender, RoutedEventArgs e)
        {
            if (geselecteerdeKlant.Aantal150Consumpties > 0)
            {
                geselecteerdeKlant.Aantal150Consumpties--;
            }
            txtPrice150.Text = geselecteerdeKlant.Aantal150Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnIncreasePrice200_Click(object sender, RoutedEventArgs e)
        {
            geselecteerdeKlant.Aantal200Consumpties++;
            txtPrice200.Text = geselecteerdeKlant.Aantal200Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnDecreasePrice200_Click(object sender, RoutedEventArgs e)
        {
            if (geselecteerdeKlant.Aantal200Consumpties > 0)
            {
                geselecteerdeKlant.Aantal200Consumpties--;
            }
            txtPrice200.Text = geselecteerdeKlant.Aantal200Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnIncreasePrice250_Click(object sender, RoutedEventArgs e)
        {
            geselecteerdeKlant.Aantal250Consumpties++;
            txtPrice250.Text = geselecteerdeKlant.Aantal250Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnDecreasePrice250_Click(object sender, RoutedEventArgs e)
        {
            if (geselecteerdeKlant.Aantal250Consumpties > 0)
            {
                geselecteerdeKlant.Aantal250Consumpties--;
            }
            txtPrice250.Text = geselecteerdeKlant.Aantal250Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnIncreasePrice100_Click(object sender, RoutedEventArgs e)
        {
            geselecteerdeKlant.Aantal100Consumpties++;
            txtPrice100.Text = geselecteerdeKlant.Aantal100Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnDecreasePrice100_Click(object sender, RoutedEventArgs e)
        {
            if (geselecteerdeKlant.Aantal100Consumpties > 0)
            {
                geselecteerdeKlant.Aantal100Consumpties--;
            }
            txtPrice100.Text = geselecteerdeKlant.Aantal100Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        private void btnAfrekenen_Click(object sender, RoutedEventArgs e)
        {
            klanten.Remove(geselecteerdeKlant);
            RecalculateKlantIDs();
            if (klanten.Count > 0)
            {
                geselecteerdeKlant = klanten.OrderBy(x => x.KlantID).First();
                DisplayPrices();
            }
            else
            {
                Reset();
            }
            listCustomers.ItemsSource = klanten;
            EnableOrDisableButtons(klanten.Count > 0);
        }

        private void listCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listCustomers.SelectedIndex != -1)
            {
                geselecteerdeKlant = klanten.Where(x => x.KlantID == listCustomers.SelectedIndex + 1).First();
                DisplayPrices();
            }
        }

        private void btnEnkelvoudigeAfrekeningTweePersonen_Click(object sender, RoutedEventArgs e)
        {
            EnkelvoudigeAfrekening(2);
            listCustomers.ItemsSource = klanten;
            lblCurrentCustomerName.Content = geselecteerdeKlant.KlantNaam;
        }

        private void btnEnkelvoudigeAfrekeningDriePersonen_Click(object sender, RoutedEventArgs e)
        {
            EnkelvoudigeAfrekening(3);
            listCustomers.ItemsSource = klanten;
            lblCurrentCustomerName.Content = geselecteerdeKlant.KlantNaam;
        }

        private void btnEnkelvoudigeAfrekeningVierPersonen_Click(object sender, RoutedEventArgs e)
        {
            EnkelvoudigeAfrekening(4);
            lblCurrentCustomerName.Content = geselecteerdeKlant.KlantNaam;
            listCustomers.ItemsSource = klanten;
        }

        private void chkIsPloeg_Checked(object sender, RoutedEventArgs e)
        {
            txtAantalAanwezig.IsEnabled = true;
            txtSpelersNamen.IsEnabled = true;
        }

        private void chkIsPloeg_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAantalAanwezig.Text = string.Empty;
            txtAantalAanwezig.IsEnabled = false;
            txtSpelersNamen.Text = string.Empty;
            txtSpelersNamen.IsEnabled = false;
        }

        private void btnKorting_Click(object sender, RoutedEventArgs e)
        {
            ExecuteKorting();
        }

        private void btnWijzigSpelersNamen_Click(object sender, RoutedEventArgs e)
        {
            WijzigSpelersNamen();
        }

        #endregion Events

        #region Helper Methods

        private void WijzigSpelersNamen()
        {
            if (geselecteerdeKlant.isPloeg && !string.IsNullOrEmpty(txtGewijzigdeSpelersNamen.Text))
            {
                int startIndexStringToRemove = geselecteerdeKlant.KlantNaam.IndexOf("[");
                int endIndexStringToRemove = geselecteerdeKlant.KlantNaam.IndexOf("]");
                geselecteerdeKlant.KlantNaam = geselecteerdeKlant.KlantNaam.Remove(startIndexStringToRemove, (endIndexStringToRemove - startIndexStringToRemove) + 1);
                geselecteerdeKlant.KlantNaam = $"{geselecteerdeKlant.KlantNaam}[{txtGewijzigdeSpelersNamen.Text}]";
                lblCurrentCustomerName.Content = geselecteerdeKlant.KlantNaam;
                listCustomers.ItemsSource = klanten;
            }
            txtGewijzigdeSpelersNamen.Text = string.Empty;
        }

        private void ExecuteKorting()
        {
            if (!string.IsNullOrEmpty(txtKortingAmount.Text))
            {
                double korting = Convert.ToDouble(txtKortingAmount.Text);
                CalculateTotalPrice(korting);
                txtKortingAmount.Text = string.Empty;
            }

        }

        private void EnkelvoudigeAfrekening(int aantalPersonenAanwezig)
        {
            if (aantalPersonenAanwezig == 2)
            {
                geselecteerdeKlant.Totaleprijs = geselecteerdeKlant.Totaleprijs - geselecteerdeKlant.TotaleprijsDividedByTwo;
                geselecteerdeKlant.PrijsVanPersoonDieWegIs += geselecteerdeKlant.TotaleprijsDividedByTwo;
                geselecteerdeKlant.KlantNaam = geselecteerdeKlant.KlantNaam.Contains("aanwezig") ? geselecteerdeKlant.KlantNaam.Replace('2','1') : geselecteerdeKlant.KlantNaam;
            }
            else if (aantalPersonenAanwezig == 3)
            {
                geselecteerdeKlant.Totaleprijs = geselecteerdeKlant.Totaleprijs - geselecteerdeKlant.TotaleprijsDividedByThree;
                geselecteerdeKlant.PrijsVanPersoonDieWegIs += geselecteerdeKlant.TotaleprijsDividedByThree;
                geselecteerdeKlant.KlantNaam = geselecteerdeKlant.KlantNaam.Contains("aanwezig") ? geselecteerdeKlant.KlantNaam.Replace('3', '2') : geselecteerdeKlant.KlantNaam;
            }
            else
            {
                geselecteerdeKlant.Totaleprijs = geselecteerdeKlant.Totaleprijs - geselecteerdeKlant.TotaleprijsDividedByFour;
                geselecteerdeKlant.PrijsVanPersoonDieWegIs += geselecteerdeKlant.TotaleprijsDividedByFour;
                geselecteerdeKlant.KlantNaam = geselecteerdeKlant.KlantNaam.Contains("aanwezig") ? geselecteerdeKlant.KlantNaam.Replace('4', '3') : geselecteerdeKlant.KlantNaam;
            }
            CalculateCustomPrices();
        }

        private void EnableOrDisableButtons(bool isEnabled)
        {
            btnAfrekenen.IsEnabled = isEnabled;
            btnDecreasePrice100.IsEnabled = isEnabled;
            btnDecreasePrice150.IsEnabled = isEnabled;
            btnDecreasePrice200.IsEnabled = isEnabled;
            btnDecreasePrice250.IsEnabled = isEnabled;
            btnIncreasePrice100.IsEnabled = isEnabled;
            btnIncreasePrice150.IsEnabled = isEnabled;
            btnIncreasePrice200.IsEnabled = isEnabled;
            btnIncreasePrice250.IsEnabled = isEnabled;
            btnEnkelvoudigeAfrekeningTweePersonen.IsEnabled = isEnabled;
            btnEnkelvoudigeAfrekeningDriePersonen.IsEnabled = isEnabled;
            btnEnkelvoudigeAfrekeningVierPersonen.IsEnabled = isEnabled;
            btnKorting.IsEnabled = isEnabled;
            btnWijzigSpelersNamen.IsEnabled = isEnabled;
        }

        private void Reset()
        {
            txtPrice100.Text = string.Empty;
            txtPrice150.Text = string.Empty;
            txtPrice200.Text = string.Empty;
            txtPrice250.Text = string.Empty;
            lblTotalePrijsAmount.Content = string.Empty;
            lblTotalePrijsAmountDividedByTwo.Content = string.Empty;
            lblTotalePrijsAmountDividedByThree.Content = string.Empty;
            lblTotalePrijsAmountDividedyFour.Content = string.Empty;
            lblCurrentCustomerName.Content = string.Empty;
        }

        private void RecalculateKlantIDs()
        {
            for (int i = 0; i < klanten.Count; i++)
            {
                klanten[i].KlantID = i + 1;
            }
        }

        private void CalculateTotalPrice(double korting)
        {
            geselecteerdeKlant.PrijsVanPersoonDieWegIs += korting;
            double totalePrijs = (geselecteerdeKlant.Aantal100Consumpties * 1) + (geselecteerdeKlant.Aantal150Consumpties * 1.5)
                                             + (geselecteerdeKlant.Aantal200Consumpties * 2) + (geselecteerdeKlant.Aantal250Consumpties * 2.5) - geselecteerdeKlant.PrijsVanPersoonDieWegIs;
            if (totalePrijs < 0)
            {
                geselecteerdeKlant.PrijsVanPersoonDieWegIs -= korting;
                geselecteerdeKlant.Totaleprijs = totalePrijs + korting;
            }
            else
            {
                geselecteerdeKlant.Totaleprijs = totalePrijs;
            }

            CalculateCustomPrices();
        }

        private void CalculateCustomPrices()
        {
            geselecteerdeKlant.TotaleprijsDividedByTwo = geselecteerdeKlant.Totaleprijs / 2;
            geselecteerdeKlant.TotaleprijsDividedByThree = geselecteerdeKlant.Totaleprijs / 3;
            geselecteerdeKlant.TotaleprijsDividedByFour = geselecteerdeKlant.Totaleprijs / 4;
            lblTotalePrijsAmount.Content = string.Format("€ {0}", Math.Round(geselecteerdeKlant.Totaleprijs,2));
            lblTotalePrijsAmountDividedByTwo.Content = string.Format("€ {0}", Math.Round(geselecteerdeKlant.TotaleprijsDividedByTwo,2));
            lblTotalePrijsAmountDividedByThree.Content = string.Format("€ {0}", Math.Round(geselecteerdeKlant.TotaleprijsDividedByThree,2));
            lblTotalePrijsAmountDividedyFour.Content = string.Format("€ {0}", Math.Round(geselecteerdeKlant.TotaleprijsDividedByFour,2));
        }

        private void Initialization()
        {
            klanten = new ObservableCollection<Klant>();
            listCustomers.ItemsSource = klanten;
        }

        private void DisplayPrices()
        {
            lblCurrentCustomerName.Content = geselecteerdeKlant.KlantNaam;
            txtPrice100.Text = geselecteerdeKlant.Aantal150Consumpties.ToString();
            txtPrice150.Text = geselecteerdeKlant.Aantal150Consumpties.ToString();
            txtPrice200.Text = geselecteerdeKlant.Aantal200Consumpties.ToString();
            txtPrice250.Text = geselecteerdeKlant.Aantal250Consumpties.ToString();
            CalculateTotalPrice(0);
        }

        #endregion Helper Methods
    }
}