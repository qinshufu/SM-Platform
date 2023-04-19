using AutoMapper.Internal;

namespace SmPlatform.Api.Test;

/// <summary>
/// 验证 Autofac 的接口
/// </summary>
public class AutofacTest
{
    [Fact]
    public void IsGenericTypeTest()
    {
        Assert.True(typeof(List<int>).IsGenericType(typeof(List<>)));
    }

}
