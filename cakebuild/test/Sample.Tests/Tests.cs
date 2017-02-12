using System;
using Xunit;

namespace Sample.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
			var str = new Lib().SayHello();
            Assert.True(str.Equals("Say Hello"));
        }
    }
}
