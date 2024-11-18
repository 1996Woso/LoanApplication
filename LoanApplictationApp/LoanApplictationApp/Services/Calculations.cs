namespace LoanApplictationApp.Services
{
    public class Calculations
    {
        public async Task<decimal> PresentValue(int n,decimal r, decimal x)
        {
            n = n* 12;
           decimal  i = (r / 100) / 12;
            decimal exp = (decimal)Math.Pow((double)(1 + i), -n);//(1+i)^(-n)
            return Math.Round((x * (1 - exp)) / i, 2);//P = x(1-(1+i)^-n)/i
        }
        public async Task<decimal> MonthlyPayment(int n, decimal r, decimal P)
        {
            n = n* 12;
            decimal i = (r / 100) / 12;
            decimal exp = (decimal)Math.Pow((double)(1 + i), -n);//(1+i)^(-n)
            return Math.Round((P * i) / (1 - exp), 2);//x = P*i/((1-(1+i)^-n))
        }
        public async Task<int>  Period(decimal x, decimal r, decimal P)
        {
            decimal i = (r / 100) / 12;
            decimal val1 = (decimal)Math.Log(1 - (double)((P * i) / x));// log(1-(P*i)/x)
            decimal val2 = (decimal)Math.Log((double)(1 + i));// log(1+i)
            decimal n = Math.Round((decimal)((-val1 / val2) / 12), 0);//n = -log(1-(p*i)/x)/log(1+i)
            return (int)n;
        }
    }
}
