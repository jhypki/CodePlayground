using System.Runtime.Serialization;

namespace CodePlayground.Tests.Core.Helpers;
using CodePlayground.Core.Helpers;

public class EnumHelperTests
{
    private enum TestEnum
    {
        [EnumMember(Value = "Value 1")]
        Value1,
        [EnumMember(Value = "Value 2")]
        Value2
    }

    [Fact]
    public void GetEnumStringValue_WhenCalled_ReturnsEnumStringValue()
    {
        // Arrange
        var enumValue = TestEnum.Value1;

        // Act
        var result = EnumHelper.GetEnumStringValue(enumValue);

        // Assert
        Assert.Equal("Value 1", result);
    }
}