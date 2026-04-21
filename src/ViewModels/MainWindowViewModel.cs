using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Doer.Core;

namespace Doer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasError))]
  public partial string? ErrorMessage { get; set; } = null;
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

  [RelayCommand]
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
