using Business.Extensions;
using Xunit;

namespace BuisnessTest.Extensions
{
    public class StringExtensionTest
    {

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("1234", "1234")]
        [InlineData("     1234", "1234")]
        [InlineData("1234     ", "1234")]
        [InlineData("     1234     ", "1234")]
        public void Should_ExtensionMethod_RemovesSpace(string value, string expectedValue)
        {
            //Act

            var actualValue = value.RemoveSpace();

            //Assert

            Assert.Equal(expectedValue, actualValue);
        }
    }
}
