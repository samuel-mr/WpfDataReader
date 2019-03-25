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

namespace WebDataReader.Client.Views.Sunat
{
  /// <summary>
  /// Lógica de interacción para SunatReportView.xaml
  /// </summary>
  public partial class SunatReportView : UserControl
  {
    public SunatReportView()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var context = DataContext as SunatReportViewModel;
      if (context == null) return;
      WebBrowser.Navigate(context.SunatUrl);
    }

    private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
    {
      var context = DataContext as SunatReportViewModel;
      if (context == null) return;
      if (e.Key == Key.Enter)
      {
        WebBrowser.Navigate(context.SunatUrl);
      }
    }
  }
}
