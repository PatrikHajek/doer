using Avalonia.Controls;
using Doer.Core;
using Doer.ViewModels;

namespace Doer.Views;

public partial class MainWindow : Window
{
  private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;
  private Source Source => ViewModel.Source;

  public MainWindow()
  {
    InitializeComponent();
  }

  private void TaskAdd(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
  {
    if (TaskName.Text is not null)
    {
      Source.TaskList.Add(TaskName.Text);
    }
  }
}
