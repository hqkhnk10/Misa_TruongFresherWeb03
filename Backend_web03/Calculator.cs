namespace FresherWeb03
{
    public class Calculator
    {
        public long Add(int a, int b)
        {
            return a + (long)b;
        }
        public long Sub(int a, int b)
        {
            return a - (long)b;
        }
        public long Mul(int a, int b)
        {
            return a * (long)b;
        }
        public double Div(int a, int b)
        {
            if (b == 0)
            {
                throw new Exception("0");
            }
            return (double)a / b;
        }
    }
}
