namespace WotPersonalDataCollector.SharedKernel.Exceptions
{
	/// <summary>
    /// Represents error that occurs during operation with local variables. 
    /// </summary>
    public class LocalVariableException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalVariableException"/> class.
        /// </summary>
        public LocalVariableException()
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalVariableException"/> class, with message.
		/// </summary>
		/// <param name="message">Error message.</param>
		public LocalVariableException(string message) : base(message)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalVariableException"/> class, with message and inner exception.
		/// </summary>
		/// <param name="message">Error message.</param>
		/// <param name="innerException">Inner exception.</param>
		public LocalVariableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
