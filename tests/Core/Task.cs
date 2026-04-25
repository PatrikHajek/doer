using Doer.Core;

namespace Doer.Tests.Core;

public class TaskTests
{
  [Fact]
  public void Constructor_CreatesTask_From_BareInput()
  {
    var task = new Doer.Core.Task() { Name = "do stuff" };

    Assert.Equivalent(task, new Doer.Core.Task("do stuff"), true);
  }

  [Fact]
  public void Constructor_CreatesTask_From_ComplexInput()
  {
    var task = new Doer.Core.Task() { Name = "do stuff" };
    task.Assignees.Add(new Assignee("me"));
    task.Labels.Add(new Label("app"));
    Assert.Equivalent(task, new Doer.Core.Task("    do      stuff    @me    #app        "), true);
  }

  [Fact]
  public void Constructor_CreatesTask_From_JumbledComplexInput()
  {
    var task = new Doer.Core.Task() { Name = "do stuff" };
    task.Assignees.Add(new Assignee("me"));
    task.Labels.Add(new Label("app"));
    Assert.Equivalent(task, new Doer.Core.Task("  @me  do     #app  stuff    #app     @me   "), true);
  }

  [Fact]
  public void Constructor_CreatesTask_From_InputContainingDiacritics()
  {
    var task = new Doer.Core.Task() { Name = "Udělat další úkol" };
    task.Assignees.Add(new Assignee("kámoš"));
    task.Labels.Add(new Label("červená"));
    task.Labels.Add(new Label("modrá"));
    Assert.Equivalent(task, new Doer.Core.Task("Udělat další úkol @kámoš #červená #modrá"), true);
  }

  [Fact]
  public void Constructor_CreatesTask_With_MultipleAttributes()
  {
    var task = new Doer.Core.Task() { Name = "do stuff" };
    task.Assignees.Add(new Assignee("me"));
    task.Assignees.Add(new Assignee("him"));
    task.Labels.Add(new Label("app"));
    task.Labels.Add(new Label("backend"));
    task.Labels.Add(new Label("important"));
    Assert.Equivalent(task, new Doer.Core.Task("do stuff @me #app #backend @him #important"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddLabel_When_PrecededWithNonWhitespaceChars()
  {
    var task = new Doer.Core.Task() { Name = "super secret code is 154#6681, don't forget!" };
    Assert.Equivalent(task, new Doer.Core.Task("super secret code is 154#6681, don't forget!"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddLabel_When_TheCharIsIsolated()
  {
    var task = new Doer.Core.Task() { Name = "super secret number is # don't forget!" };
    Assert.Equivalent(task, new Doer.Core.Task("super secret number is # don't forget!"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddLabel_When_EncasedInTheChars()
  {
    var task = new Doer.Core.Task() { Name = "super secret code is #hey# don't forget!" };
    Assert.Equivalent(task, new Doer.Core.Task("super secret code is #hey# don't forget!"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddLabel_When_TheCharIsAfter()
  {
    var task = new Doer.Core.Task() { Name = "super secret code is hey# don't forget!" };
    Assert.Equivalent(task, new Doer.Core.Task("super secret code is hey# don't forget!"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddAssignee_When_PrecededWithNonWhitespaceChars()
  {
    var task = new Doer.Core.Task() { Name = "write john-doe@gmail.com about cats" };
    Assert.Equivalent(task, new Doer.Core.Task("write john-doe@gmail.com about cats"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddAssignee_When_TheCharIsIsolated()
  {
    var task = new Doer.Core.Task() { Name = "write @ about cats" };
    Assert.Equivalent(task, new Doer.Core.Task("write @ about cats"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddAssignee_When_EncasedInTheChars()
  {
    var task = new Doer.Core.Task() { Name = "write @someone@ about cats" };
    Assert.Equivalent(task, new Doer.Core.Task("write @someone@ about cats"), true);
  }

  [Fact]
  public void Constructor_DoesNotAddAssignee_When_TheCharIsAfter()
  {
    var task = new Doer.Core.Task() { Name = "write someone@ about cats" };
    Assert.Equivalent(task, new Doer.Core.Task("write someone@ about cats"), true);
  }

  [Fact]
  public void Constructor_Fails_When_InputIsEmpty()
  {
    Assert.Throws<ArgumentException>(() => new Doer.Core.Task(""));
  }
}
