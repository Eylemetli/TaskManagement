using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Services;
using Xunit;

public class TaskServiceTests
{
    // Her test için yeni, temiz bir InMemory context oluşturan yardımcı fonksiyon
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // her test için farklı DB
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Task()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);

        var task = new TaskItem
        {
            Title = "Görev 1",
            Description = "Açıklama 1",
            IsCompleted = false
        };

        // Act
        var added = await service.AddAsync(task);

        // Assert
        Assert.NotEqual(0, added.Id); // Id atanmış olmalı
        var fromDb = await context.Tasks.FindAsync(added.Id);
        Assert.NotNull(fromDb);
        Assert.Equal("Görev 1", fromDb!.Title);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Tasks()
    {
        using var context = CreateInMemoryContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "T1", Description = "D1" },
            new TaskItem { Title = "T2", Description = "D2" }
        );
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        // Act
        var list = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Task_When_Exists()
    {
        using var context = CreateInMemoryContext();
        var task = new TaskItem { Title = "T1", Description = "D1" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        // Act
        var result = await service.GetByIdAsync(task.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(task.Id, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Exists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_When_Task_Exists()
    {
        using var context = CreateInMemoryContext();
        var task = new TaskItem { Title = "Eski", Description = "Eski desc" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        // değişiklik
        task.Title = "Yeni";
        task.Description = "Yeni desc";

        var updated = await service.UpdateAsync(task);

        Assert.NotNull(updated);
        var fromDb = await context.Tasks.FindAsync(task.Id);
        Assert.Equal("Yeni", fromDb!.Title);
        Assert.Equal("Yeni desc", fromDb.Description);
    }

    [Fact]
    public async Task UpdateAsync_Should_Return_Null_When_Task_Not_Exists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);

        var nonExisting = new TaskItem
        {
            Id = 999,
            Title = "Yok",
            Description = "Yok"
        };

        var result = await service.UpdateAsync(nonExisting);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_When_Task_Exists()
    {
        using var context = CreateInMemoryContext();
        var task = new TaskItem { Title = "Silinecek", Description = "Desc" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TaskService(context);

        var result = await service.DeleteAsync(task.Id);

        Assert.True(result);
        var fromDb = await context.Tasks.FindAsync(task.Id);
        Assert.Null(fromDb);
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_False_When_Task_Not_Exists()
    {
        using var context = CreateInMemoryContext();
        var service = new TaskService(context);

        var result = await service.DeleteAsync(999);

        Assert.False(result);
    }
}

