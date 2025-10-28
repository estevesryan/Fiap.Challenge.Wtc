using Fiap.Challenge.Wtc.Domain.Entities;

namespace Fiap.Challenge.Wtc.Tests.Unit.Domain;

public class BaseEntityTests
{
    private class TestEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        
        public TestEntity(string name) : base()
        {
            Name = name;
        }
    }

    [Fact]
    public void BaseEntity_Should_GenerateId_WhenCreated()
    {
        // Arrange & Act
        var entity = new TestEntity("Test");

        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public void BaseEntity_Should_SetCreatedAt_WhenCreated()
    {
        // Arrange & Act
        var entity = new TestEntity("Test");

        // Assert
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.True(entity.CreatedAt >= DateTime.UtcNow.AddSeconds(-1));
    }

    [Fact]
    public void UpdateTimestamp_Should_UpdateUpdatedAt()
    {
        // Arrange
        var entity = new TestEntity("Test");
        var originalUpdatedAt = entity.UpdatedAt;

        // Act
        Thread.Sleep(1); // Ensure time difference
        entity.UpdateTimestamp();

        // Assert
        Assert.True(entity.UpdatedAt > originalUpdatedAt);
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenEntitiesHaveSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity("Test1");
        var entity2 = new TestEntity("Test2");

        // Use reflection to set the same Id
        typeof(BaseEntity).GetProperty("Id")?.SetValue(entity1, id);
        typeof(BaseEntity).GetProperty("Id")?.SetValue(entity2, id);

        // Act & Assert
        Assert.Equal(entity1, entity2);
    }
}