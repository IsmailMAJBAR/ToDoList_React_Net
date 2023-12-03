namespace todo_list_server.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using todo_list_server.Controllers;
using todo_list_server.Models;
using Xunit;

public class TodoItemsControllerTests
{
    [Fact]
    public async Task GetTodoItems_ReturnsAllTodoItems()
    {
        // Arrange: Set up an in-memory database
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_GetTodoItems")
            .Options;

        // Insert seed data into the in-memory database
        using (var context = new TodoContext(options))
        {
            context.TodoItems.Add(new TodoItem { Name = "Task 1", IsComplete = false , Description=""});
            context.TodoItems.Add(new TodoItem { Name = "Task 2", IsComplete = true , Description=""});
            context.SaveChanges();
        }

        // Act: Execute the GetTodoItems method
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            var result = await controller.GetTodoItems();

            // Assert: Verify that the result is as expected
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
            var returnValue = Assert.IsType<List<TodoItem>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Contains(returnValue, item => item.Name == "Task 1" && item.IsComplete == false && item.Description == "");
            Assert.Contains(returnValue, item => item.Name == "Task 2" && item.IsComplete == true && item.Description == "");
        }
    }

    [Fact]
    public async Task GetTodoItem_ReturnsTodoItem_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_GetTodoItem_Exists")
            .Options;

        using (var context = new TodoContext(options))
        {
            context.TodoItems.Add(new TodoItem { Id = 1, Name = "Task 1", IsComplete = false });
            context.SaveChanges();
        }

        // Act
        ActionResult<TodoItem> result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.GetTodoItem(1);
        }

        // Assert
        var actionResult = Assert.IsType<ActionResult<TodoItem>>(result);
        var returnValue = Assert.IsType<TodoItem>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal("Task 1", returnValue.Name);
        Assert.False(returnValue.IsComplete);
    }

    [Fact]
    public async Task GetTodoItem_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_GetTodoItem_NotExists")
            .Options;

        // No items added to context

        // Act
        ActionResult<TodoItem> result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.GetTodoItem(1); // ID not present in the database
        }

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostTodoItem_ReturnsCreatedAtAction_WithTodoItem()
    {
        // Arrange
        var mocDb = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb")
            .Options;

        using var context = new TodoContext(mocDb);
        var controller = new TodoItemsController(context);
        var newItem = new TodoItem { Name = "Test Task", IsComplete = false };

        // Act
        var result = await controller.PostTodoItem(newItem);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<TodoItem>(createdAtActionResult.Value);
        Assert.Equal(newItem.Name, returnValue.Name);
        Assert.False(returnValue.IsComplete);
    }

    [Fact]
    public async Task PutTodoItem_UpdatesItem_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_PutTodoItem_Exists")
            .Options;

        using (var context = new TodoContext(options))
        {
            context.TodoItems.Add(new TodoItem { Id = 1, Name = "Original Task", IsComplete = false });
            context.SaveChanges();
        }

        var updatedItem = new TodoItem { Id = 1, Name = "Updated Task", IsComplete = true };

        // Act
        IActionResult result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.PutTodoItem(1, updatedItem);
        }

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Additional assert to check if the item was updated in the database
        using (var context = new TodoContext(options))
        {
            var item = await context.TodoItems.FindAsync(1);
            Assert.Equal("Updated Task", item.Name);
            Assert.True(item.IsComplete);
        }
    }

    [Fact]
    public async Task PutTodoItem_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_PutTodoItem_NotExists")
            .Options;

        var itemToUpdate = new TodoItem { Id = 1, Name = "Updated Task", IsComplete = true };

        // Act
        IActionResult result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.PutTodoItem(1, itemToUpdate); // ID not present in the database
        }

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteTodoItem_DeletesItem_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_DeleteTodoItem_Exists")
            .Options;

        using (var context = new TodoContext(options))
        {
            context.TodoItems.Add(new TodoItem { Id = 1, Name = "Task to delete", IsComplete = false });
            context.SaveChanges();
        }

        // Act
        IActionResult result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.DeleteTodoItem(1);
        }

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Additional assert to check if the item was actually removed from the database
        using (var context = new TodoContext(options))
        {
            var item = await context.TodoItems.FindAsync(1);
            Assert.Null(item);
        }
    }

    [Fact]
    public async Task DeleteTodoItem_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTestDb_DeleteTodoItem_NotExists")
            .Options;

        // Act
        IActionResult result;
        using (var context = new TodoContext(options))
        {
            var controller = new TodoItemsController(context);
            result = await controller.DeleteTodoItem(1); // ID not present in the database
        }

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

}
