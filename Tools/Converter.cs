using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using PedraPaperTisores.Model;

namespace PedraPaperTisores.Tools
{
    public class EstatToConfiguracioVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility resultat;
            EstatPartida estat = (EstatPartida)value;
            resultat = estat == EstatPartida.Configuracio ? Visibility.Visible : Visibility.Collapsed;
            return resultat;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class OpcioToImatgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage resultat = new BitmapImage();
            Opcio opcio = (Opcio)value;
            resultat.BeginInit();
            switch (opcio)
            {
                case Opcio.Pedra:
                    resultat.UriSource = new Uri("/Images/stone.png", UriKind.Relative);
                    break;
                case Opcio.Paper:
                    resultat.UriSource = new Uri("/Images/paper.png", UriKind.Relative);
                    break;
                case Opcio.Tisores:
                    resultat.UriSource = new Uri("/Images/scissors.png", UriKind.Relative);
                    break;
                case Opcio.Llangardaix:
                    resultat.UriSource = new Uri("/Images/lizard.png", UriKind.Relative);
                    break;
                case Opcio.Spock:
                    resultat.UriSource = new Uri("/Images/spock.png", UriKind.Relative);
                    break;
                default:
                    break;
            }
            resultat.EndInit();
            return resultat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
