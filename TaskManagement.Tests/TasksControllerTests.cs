using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Controllers;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Services;
using Xunit;

public class TasksControllerTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Add_Should_Return_Ok_And_Save_Task()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);
        var controller = new TasksController(service);

        var dto = new TaskCreateDto
        {
            Title = "Test Görev",
            Description = "Açıklama",
            IsCompleted = false
        };

        // Act
        var result = await controller.Add(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTask = Assert.IsType<TaskItem>(okResult.Value);

        Assert.Equal("Test Görev", returnedTask.Title);
        Assert.NotEqual(0, returnedTask.Id);

        var fromDb = await context.Tasks.FindAsync(returnedTask.Id);
        Assert.NotNull(fromDb);
    }

    [Fact]
    public async Task GetAll_Should_Return_Ok_With_List()
    {
        using var context = CreateInMemoryContext();

        context.Tasks.AddRange(
            new TaskItem { Title = "T1", Description = "D1" },
            new TaskItem { Title = "T2", Description = "D2" }
        );
        await context.SaveChangesAsync();

        var service = new TaskService(context);
        var controller = new TasksController(service);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<List<TaskItem>>(ok.Value);
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public async Task GetById_Should_Return_Ok_When_Exists()
    {
        using var context = CreateInMemoryContext();

        var task = new TaskItem { Title = "Test", Description = "Açıklama" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);
        var controller = new TasksController(service);

        var result = await controller.GetById(task.Id);

        var ok = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<TaskItem>(ok.Value);

        Assert.Equal(task.Id, returned.Id);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_NotExists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);
        var controller = new TasksController(service);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_Should_Update_When_Exists()
    {
        using var context = CreateInMemoryContext();

        var task = new TaskItem { Title = "Old", Description = "Old desc" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);
        var controller = new TasksController(service);

        var dto = new TaskUpdateDto
        {
            Id = task.Id,
            Title = "New",
            Description = "New desc",
            IsCompleted = true
        };

        var result = await controller.Update(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var updated = Assert.IsType<TaskItem>(ok.Value);

        Assert.Equal("New", updated.Title);
        Assert.Equal(true, updated.IsCompleted);
    }

    [Fact]
    public async Task Update_Should_Return_NotFound_When_NotExists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);
        var controller = new TasksController(service);

        var dto = new TaskUpdateDto
        {
            Id = 999,
            Title = "X",
            Description = "Y"
        };

        var result = await controller.Update(dto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_Should_Return_Ok_When_Deleted()
    {
        using var context = CreateInMemoryContext();

        var task = new TaskItem { Title = "Sil", Description = "Desc" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);
        var controller = new TasksController(service);

        var result = await controller.Delete(task.Id);

        var ok = Assert.IsType<OkObjectResult>(result);
        var fromDb = await context.Tasks.FindAsync(task.Id);

        Assert.Null(fromDb);
    }

    [Fact]
    public async Task Delete_Should_Return_NotFound_When_NotExists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);
        var controller = new TasksController(service);

        var result = await controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}

