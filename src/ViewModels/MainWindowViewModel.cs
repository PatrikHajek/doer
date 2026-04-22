using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Doer.Core;

namespace Doer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  private Source? Source;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasError))]
  public partial string? ErrorMessage { get; set; } = null;
  public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(TaskAddCommand))]
  public partial string TaskNameAdd { get; set; } = string.Empty;
  private bool TaskAddEnabled => !string.IsNullOrWhiteSpace(TaskNameAdd);

  public ObservableCollection<Task> Tasks => Source!.TaskList.Tasks;

  public MainWindowViewModel()
  {
    try
    {
      Source = Source.Read();
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
    }
  }

  [RelayCommand]
  private void FileNew()
  {
    try
    {
      Source = Source.Init();
      ErrorMessage = null;
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
    }
  }

  [RelayCommand(CanExecute = nameof(TaskAddEnabled))]
  private void TaskAdd()
  {
    Source!.TaskList.Add(TaskNameAdd);
    TaskNameAdd = string.Empty;
    Source!.Save();
  }

  [RelayCommand]
  private void TaskRemove(Task task)
  {
    Tasks.Remove(task);
    Source!.Save();
  }
}
