using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WebDataReader.Application.Interfaces;
using WebDataReader.Application.Sunat;
using WebDataReader.Client.Views.Sunat;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects;

namespace WebDataReader.Client
{
  public class MainWindowViewModel : BindableBase
  {
    public MainWindowViewModel()
    {
      App.Log.Trace("Starting");
      SunatWorkerViewModel = Bootstraper.Resolve<SunatWorkerViewModel>();
      SunatReportViewModel = Bootstraper.Resolve<SunatReportViewModel>();
    }

    private SunatWorkerViewModel sunatWorkerViewModel;
    public SunatWorkerViewModel SunatWorkerViewModel
    {
      get => sunatWorkerViewModel;
      set
      {
        if (sunatWorkerViewModel == value) return;
        sunatWorkerViewModel = value;
        RaisePropertyChanged(nameof(SunatWorkerViewModel));
      }
    }

    private SunatReportViewModel sunatReportViewModel;
    public SunatReportViewModel SunatReportViewModel
    {
      get => sunatReportViewModel;
      set
      {
        if (sunatReportViewModel == value) return;
        sunatReportViewModel = value;
        RaisePropertyChanged(nameof(SunatReportViewModel));
      }
    }

  }
}
