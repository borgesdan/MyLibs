namespace MyAspNetApiLib.Exceptions
{
    /// <summary>
    /// Representa uma exceção com um status code específico.
    /// </summary>
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCodeException(int code, string message) : base(message) 
        {
            StatusCode = code;
        }

        public HttpStatusCodeException(int code, string message, Exception? innerException) : base(message, innerException) 
        {
            StatusCode = code;
        }

        public int StatusCode { get; protected set; }
    }
}
