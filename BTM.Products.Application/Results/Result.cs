namespace BTM.Products.Application.Results
{

    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; } // List to hold multiple error messages
        public object? Data { get; set; }

        public static Result Success(object? data = null)
        {
            return new Result { IsSuccess = true, ErrorMessages = new List<string>(), Data = data };
        }

        public static Result Failure(List<string> errorMessages)
        {
            return new Result { IsSuccess = false, ErrorMessages = errorMessages };
        }

        public static Result Failure(string errorMessage)
        {
            return new Result { IsSuccess = false, ErrorMessages = new List<string> { errorMessage } };
        }
    }

    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
        public static Result<T> Success(T data)
        {
            return new Result<T> { IsSuccess = true, ErrorMessage = string.Empty, Data = data };
        }

        public static Result<List<T>> Success<T>(List<T> data)
        {
            return new Result<List<T>> { IsSuccess = true, ErrorMessage = string.Empty, Data = data };
        }
        public static Result<T> Failure(string message)
        {
            return new Result<T> { IsSuccess = false, ErrorMessage = message };
        }
    }
}
