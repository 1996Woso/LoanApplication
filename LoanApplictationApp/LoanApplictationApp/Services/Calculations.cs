using Microsoft.AspNetCore.Components;

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
        public async Task<int> Period(decimal x, decimal r, decimal P)
        {
            decimal i = (r / 100) / 12;
            decimal val1 = (decimal)Math.Log(1 - (double)((P * i) / x));// log(1-(P*i)/x)
            decimal val2 = (decimal)Math.Log((double)(1 + i));// log(1+i)
            decimal n = Math.Round((decimal)((-val1 / val2) / 12), 0);//n = -log(1-(p*i)/x)/log(1+i)
            return (int)n;
        }

        public async void OnInputDecimal<T>(ChangeEventArgs e, Action<decimal?> updateProperty,T model) where T: class
        {
            if(e != null && updateProperty != null)
            {
                if (string.IsNullOrEmpty(e.Value?.ToString()))
                {
                    updateProperty(null);
                }
                else if(decimal.TryParse(e.Value.ToString(), out decimal result))
                {
                    updateProperty(result);
                }
            }
            Task task = CalculateAffordable(model);
            
            Task task1 = CalculateInterest(model);
        }

        public void OnInputInteger<T>(ChangeEventArgs e, Action<int> updateProperty,T model) where T: class
        {
            if(e != null && updateProperty != null)
            {
                if (string.IsNullOrEmpty(e.Value?.ToString()))
                {
                    updateProperty(0);
                }
                else if(int.TryParse(e.Value.ToString(), out int result))
                {
                    updateProperty(result);
                }
            }
            Task task = CalculateAffordable(model);
            Task task1 = CalculateInterest(model);
        }
        public async Task CalculateAffordable<T>(T model) where T: class
        {
            if (model is Models.Affordability affordabilityModel)
            {
                if (affordabilityModel.GrossIncome.HasValue && affordabilityModel.n.HasValue && affordabilityModel.i.HasValue)
                {
                    affordabilityModel.MaxAffordInstall = (decimal)0.3 * affordabilityModel.GrossIncome.Value;
                    affordabilityModel.MaxAffordLoan = await PresentValue(affordabilityModel.n.Value, affordabilityModel.i.Value, affordabilityModel.MaxAffordInstall.Value);
                }
                else
                {
                    affordabilityModel.MaxAffordLoan = null;
                    affordabilityModel.MaxAffordInstall = null;
                }
            }
        }
  //      public async Task CalculateRepayable<T>(T model) where T: class
  //      {
		//	if (model is Models.Repayable repayable)
		//	{
		//		if (repayable.x.HasValue && repayable.n.HasValue && repayable.i.HasValue)
		//		{
		//			repayable.P = await PresentValue(repayable.n.Value, repayable.i.Value, repayable.x.Value);
		//		}
		//		else if (repayable.x.HasValue && repayable.i.HasValue && repayable.P.HasValue && repayable.D.HasValue)
		//		{
		//			repayable.n = await Period(repayable.x.Value, repayable.i.Value, repayable.P.Value - repayable.D.Value);
		//		}
		//		else if (repayable.P.HasValue && repayable.n.HasValue && repayable.i.HasValue && repayable.D.HasValue)
		//		{
		//			repayable.x = await MonthlyPayment(repayable.n.Value, repayable.i.Value, repayable.P.Value - repayable.D.Value);
		//		}
		//	}
		//}
        public async Task CalculateInterest<T>(T model) where T: class
        {
            if(model is Models.Repayable repayable)
            {
                if (repayable.x.HasValue && repayable.n.HasValue && repayable.P.HasValue && repayable.i.HasValue)
				{
					repayable.T = Math.Round((decimal)(repayable.x.Value * repayable.n.Value * 12), 2);
					repayable.I = Math.Round((decimal)(repayable.T.Value - repayable.P.Value), 2);
				}
                else
                {
                    repayable.T = null;
                    repayable.I = null;
                }
			}
            else if(model is Models.Affordability affordable)
            {
				if (affordable.x.HasValue && affordable.n.HasValue && affordable.P.HasValue && affordable.i.HasValue)
				{
					affordable.T = Math.Round((decimal)(affordable.x.Value * affordable.n.Value * 12), 2);
					affordable.I = Math.Round((decimal)(affordable.T.Value - affordable.P.Value), 2);
				}
			}
		}
    }
}
