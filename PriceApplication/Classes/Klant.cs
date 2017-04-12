using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceApplication.Classes
{
    public class Klant : INotifyPropertyChanged
    {
        #region Variables

        private string _name;

        #endregion

        #region Properties

        public int KlantID { get; set; }
        public string KlantNaam
        {
            get { return _name; }
            set
            {
                _name = value;
                onPropertyChanged(this, "KlantNaam");
            }
        }
        public int Aantal100Consumpties { get; set; }
        public int Aantal150Consumpties { get; set; }
        public int Aantal200Consumpties { get; set; }
        public int Aantal250Consumpties { get; set; }
        public double Totaleprijs { get; set; }
        public double TotaleprijsDividedByTwo { get; set; }
        public double TotaleprijsDividedByThree { get; set; }
        public double TotaleprijsDividedByFour { get; set; }
        public double PrijsVanPersoonDieWegIs { get; set; }
        public bool isPloeg { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
