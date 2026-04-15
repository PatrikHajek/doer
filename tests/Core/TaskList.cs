using Doer.Core;

namespace Doer.Tests.Core;

public class TaskListTests
{
  readonly TaskList List = new();

  [Fact]
  public void Add_AddsTask_From_BareInput()
  {
    Assert.Empty(List.Tasks);

    List.Add("do stuff");

    Assert.Single(List.Tasks);
    Assert.Equivalent(new Doer.Core.Task() { Name = "do stuff" }, List.Tasks[0], true);
  }

  [Fact]
  public void Add_AddsTask_From_ComplexInput()
  {
    Assert.Empty(List.Tasks);

    List.Add("    do      stuff    @me    #app        ");

    Assert.Single(List.Tasks);

    var task = new Doer.Core.Task() { Name = "do stuff" };
    task.Assignees.Add(new Assignee("me"));
    task.Labels.Add(new Label("app"));
    Assert.Equivalent(task, List.Tasks[0], true);
  }

  [Fact]
  public void Add_AddsTask_From_JumbledComplexInput()
  {
    Assert.Empty(List.Tasks);

    List.Add("  @me  do     #app  stuff    #app     @me   ");

    Assert.Single(List.Tasks);

    var task = new Doer.Core.Task() { Name = "do stuff" };
    task.Assignees.Add(new Assignee("me"));
    task.Labels.Add(new Label("app"));
    Assert.Equivalent(task, List.Tasks[0], true);
  }

  [Fact]
  public void Add_AddsTask_From_InputContainingDiacritics()
  {
    Assert.Empty(List.Tasks);

    List.Add("Udělat další úkol @kámoš #červená #modrá");

    Assert.Single(List.Tasks);

    var task = new Doer.Core.Task() { Name = "Udělat další úkol" };
    task.Assignees.Add(new Assignee("kámoš"));
    task.Labels.Add(new Label("červená"));
    task.Labels.Add(new Label("modrá"));
    Assert.Equivalent(task, List.Tasks[0], true);
  }

  [Fact]
  public void Add_Fails_When_InputIsEmpty()
  {
    Assert.Empty(List.Tasks);

    Assert.Throws<ArgumentException>(() => List.Add(""));
  }
}
