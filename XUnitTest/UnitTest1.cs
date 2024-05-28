namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrane
            int x = 2; int y = 3; int z;

            // Act
            z = x + y;

            // Assert
            Assert.Equal(5 , z);
        }
    }
}