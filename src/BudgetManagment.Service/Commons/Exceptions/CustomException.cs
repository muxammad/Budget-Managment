namespace BudgetManagment.Service.Commons.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }
        public CustomException(int code, string message) : base(message)
        {
        }
    }
}
