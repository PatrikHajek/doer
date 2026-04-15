using System;
using System.Collections.ObjectModel;
using Doer.Core;

namespace Doer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  private string? _errorMessage;
  public string? ErrorMessage
  {
    get => _errorMessage;
    private set
    {
      if (_errorMessage != value)
      {
        _errorMessage = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(HasError));
      }
    }
  }

  public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

  private Source? _source;
  public Source Source => _source!;
  public ObservableCollection<Task> Tasks => _source!.TaskList.Tasks;

  public MainWindowViewModel()
  {
    try
    {
      _source = Source.Read();
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
    }
  }

  public void FileNew()
  {
    try
    {
      _source = Source.Init();
      ErrorMessage = null;
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
    }
  }
}
