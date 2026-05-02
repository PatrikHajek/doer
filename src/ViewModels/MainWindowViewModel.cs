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

  public ObservableCollection<Task> Tasks { get; set; } = [];

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(TaskAddCommand))]
  public partial string TaskNameAdd { get; set; } = string.Empty;
  private bool TaskAddEnabled => !string.IsNullOrWhiteSpace(TaskNameAdd);

  [ObservableProperty]
  public partial string SearchQuery { get; set; } = string.Empty;

  public MainWindowViewModel()
  {
    try
    {
      Source = Source.Read();
      Source.TaskList.Tasks.CollectionChanged += (s, e) => UpdateTasks();
      UpdateTasks();
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
    Source!.TaskList.Tasks.Remove(task);
    Source!.Save();
  }

  partial void OnSearchQueryChanged(string value)
  {
    UpdateTasks();
  }

  private void UpdateTasks()
  {
    Tasks.Clear();
    if (!string.IsNullOrEmpty(SearchQuery))
    {
      foreach (var task in Source!.TaskList.Search(SearchQuery))
      {
        Tasks.Add(task);
      }
    }
    else
    {
      foreach (var task in Source!.TaskList.Tasks)
      {
        Tasks.Add(task);
      }
    }
  }
}
