

using FresherWeb03;

namespace FresherWeb03Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        /// <summary>
        /// Test Add
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="result"></param>
        [TestCase(1,2,3)]
        [TestCase(-1, 2, 1)]
        [TestCase(int.MaxValue, 2, (long)int.MaxValue +2)]
        [TestCase(int.MinValue, int.MaxValue, (long)int.MinValue + int.MaxValue)]
        public void Test_Add(int a, int b, long xResult)
        {
            //act
            Calculator cal = new Calculator();
            var aResult = cal.Add(a,b);
            //assert
            Assert.That(aResult, Is.EqualTo(xResult));
        }

        [TestCase(1, 2, -1)]
        [TestCase(int.MinValue, 2, (long)int.MinValue - 2)]
        [TestCase(int.MinValue, int.MaxValue, (long)int.MinValue - int.MaxValue)]
        public void Test_Sub(int a, int b, long xResult)
        {
            //act
            Calculator cal = new Calculator();
            var aResult = cal.Sub(a, b);
            //assert
            Assert.That(aResult, Is.EqualTo(xResult));
        }

        [TestCase(1, 2, (long)1 * 2)]
        [TestCase(0, 2, 0)]
        [TestCase(int.MaxValue, 2, (long)int.MaxValue * 2)]
        [TestCase(int.MinValue, int.MaxValue, (long)int.MaxValue * int.MinValue)]
        public void Test_Mul(int a, int b, long xResult)
        {
            //act
            Calculator cal = new Calculator();
            var aResult = cal.Mul(a, b);
            //assert
            Assert.That(aResult, Is.EqualTo(xResult));
        }

        [TestCase(1, 2, (double)1/2)]
        [TestCase(10, 2, (double)10/2)]
        public void Test_Div(int a, int b, double xResult)
        {
            //act
            Calculator cal = new Calculator();
            var aResult = cal.Div(a, b);
            //assert
            Assert.That(aResult, Is.EqualTo(xResult));
        }


        [TestCase(1, 0, "Divide 0")]
        public void Divide_Zero(int a, int b, string mess)
        {
            //act && assert
            var cal = new Calculator();
            var handle = () => cal.Div(a, b);

            Assert.Throws<Exception>(() => handle());
        }
    }
}
