namespace ExpenseManagement.BusinessLogicLayer.Common.Reponse
{
    public class ApiReponse<T>
    {
        private bool isSuccessed;
        private string message;
        private int statusCode;
        private T responseObj;

        public bool IsSuccessed { get => isSuccessed; set => isSuccessed = value; }
        public string Message { get => message; set => message = value; }
        public int StatusCode { get => statusCode; set => statusCode = value; }
        public T ResponseObj { get => responseObj; set => responseObj = value; }

        public ApiReponse() { }

        public ApiReponse(bool isSuccessed, string message, int statusCode)
        {
            this.isSuccessed = isSuccessed;
            this.message = message;
            this.statusCode = statusCode;
        }

        public ApiReponse(bool isSuccessed, string message, int statusCode, T responseObj) : this(isSuccessed, message, statusCode)
        {
            this.responseObj = responseObj;
        }

        public ApiReponse(bool isSuccessed, string message, int statusCode, IEnumerable<T> responseObj) : this(isSuccessed, message, statusCode)
        {
            this.responseObj = (T?)responseObj;
        }
    }
}
