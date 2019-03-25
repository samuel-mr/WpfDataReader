using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using NCrontab.Advanced;
using Prism.Commands;
using Prism.Mvvm;

namespace WebDataReader.Client.Views.Sunat
{
  public class SunatWorkerViewModel : BindableBase
  {
    DispatcherTimer timer;

    public SunatWorkerViewModel()
    {
      Formato = @"0 */1 *  * *"; // ejecutará cada hora
      EjecutarCommand = new DelegateCommand(async () => await Work());

      timer = new DispatcherTimer();
      timer.Tick += (sender2, e2) =>
      {
        timer.Stop();
        PreWork();
      };
      PreWork();
    }

    public DelegateCommand EjecutarCommand { get; set; }

    void PreWork()
    {
      var ejecucion = ObtenerTiempoRestante();
      timer.Interval = ejecucion.TiempoRestante;
      timer.Start();
      ProximaEjecucion = $"Proxima ejecución: {ejecucion.ProximaEjecucion}";
    }

    async Task Work()
    {
      var sw = new Stopwatch();
      sw.Start();
      try
      {
        IsRunning = true;
        await Task.Delay(2000);
        App.Log.Trace($"Programación [{0}] ejecutada");
      }
      catch (Exception ex)
      {
        // Messenger.Default.Send(new FooterErrorMessage($"Error al ejecutar tarea Id [{Id}], Clase=[{this.Path}]", ex));
      }
      finally
      {
        IsRunning = false;
        if (sw.IsRunning)
          sw.Stop();
        var elapsedSeconds = (decimal)sw.ElapsedTicks / (decimal)System.Diagnostics.Stopwatch.Frequency;
        UltimoLogEjecucion = $"Ultimo ejecución:{DateTime.Now} | Duración: {decimal.Round(elapsedSeconds, 1)} segundo(s)";
      }
    }

    class TiempoItem
    {
      public TimeSpan TiempoRestante { get; set; }
      public DateTime ProximaEjecucion { get; set; }
    }

    TiempoItem ObtenerTiempoRestante()
    {
      var schedule = CrontabSchedule.Parse(Formato);
      var nextTime = schedule.GetNextOccurrence(DateTime.Now);

      TimeSpan tiempoRestante = nextTime - DateTime.Now;
      return new TiempoItem()
      {
        ProximaEjecucion = nextTime,
        TiempoRestante = tiempoRestante
      };
    }

    #region Properties
    private ProcedimientoState estado;
    public ProcedimientoState Estado
    {
      get => estado;
      set
      {
        if (estado == value) return;
        estado = value;
        RaisePropertyChanged(nameof(Estado));
      }
    }
    private string proximaEjecucion;
    public string ProximaEjecucion
    {
      get => proximaEjecucion;
      set
      {
        if (proximaEjecucion == value) return;
        proximaEjecucion = value;
        RaisePropertyChanged(nameof(ProximaEjecucion));
      }
    }
    private string formato;
    public string Formato
    {
      get => formato;
      set
      {
        if (formato == value) return;
        formato = value;
        RaisePropertyChanged(nameof(Formato));
      }
    }
    private bool isRunning;
    public bool IsRunning
    {
      get => isRunning;
      set
      {
        if (isRunning == value) return;
        isRunning = value;
        RaisePropertyChanged(nameof(IsRunning));
      }
    }
    private string ultimoLogEjecucion;
    public string UltimoLogEjecucion
    {
      get => ultimoLogEjecucion;
      set
      {
        if (ultimoLogEjecucion == value) return;
        ultimoLogEjecucion = value;
        RaisePropertyChanged(nameof(UltimoLogEjecucion));
      }
    }
    #endregion
    
    #region Metodos
    public void Iniciar()
    {
      if (Estado != ProcedimientoState.Iniciado)
      {
        PreWork();
        Estado = ProcedimientoState.Iniciado;
        App.Log.Trace($"Procedimiento iniciado");
      }
    }
    public void Detener()
    {
      if (Estado != ProcedimientoState.Detenido)
      {
        timer?.Stop();
        ProximaEjecucion = "";
        Estado = ProcedimientoState.Detenido;
        App.Log.Trace($"Procedimiento detenido");
      }
    }
    #endregion
  }
}