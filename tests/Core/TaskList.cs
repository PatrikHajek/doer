using Doer.Core;

namespace Doer.Tests.Core;

public class TaskListTests
{
  readonly TaskList List = new();

  [Fact]
  public void Add_AddsTask()
  {
    List.Add("hello @me #app #backend");

    Assert.Single(List.Tasks);

    var task = new Doer.Core.Task("hello @me #app #backend");
    Assert.Equivalent(task, List.Tasks[0], true);
  }
}
