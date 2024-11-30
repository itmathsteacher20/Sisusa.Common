namespace Sisusa.Common.ReturnTypes
{
    /// <summary>
    /// Represents a result type that can either signify success or a failure,
    /// encapsulating potential failure information.
    /// </summary>
    public class FailureOrNothing
    {
        private bool IsError { get; set; }

        private FailureInfo Error { get; set; }

        private FailureOrNothing()
        {
            IsError = false;
            Error = FailureInfo.WithMessage("An error occurred.");
        }

        private FailureOrNothing(FailureInfo failureInfo)
        {
            IsError = true;
            Error = failureInfo;
        }

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies success.
        /// </summary>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies success.</returns>
        public static FailureOrNothing Success() => new();

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure with the provided failure information.
        /// </summary>
        /// <param name="failureInfo">The failure information describing the error.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies a failure.</returns>
        public static FailureOrNothing Failure(FailureInfo failureInfo) => new(failureInfo);

        /// <summary>
        /// Creates a new <see cref="FailureOrNothing"/> instance that signifies a failure with the provided error message.
        /// </summary>
        /// <param name="message">The error message that describes the failure.</param>
        public static FailureOrNothing Failure(string message) => new(FailureInfo.WithMessage(message));

        /// <summary>
        /// Executes the provided action if the current instance does not signify an error,
        /// returning a new <see cref="FailureOrNothing"/> instance indicating success or failure.
        /// </summary>
        /// <param name="doAction">The action to be executed if there is no error.</param>
        /// <returns>A new <see cref="FailureOrNothing"/> instance that signifies success if the action executed without throwing an exception; otherwise, signifies a failure.</returns>
        public FailureOrNothing Then(Action doAction)
        {
            if (IsError)
            {
                return this;
            }

            try
            {
                doAction();
                return Success();
            }
            catch (Exception ex)
            {
                return Failure(FailureInfo.FromException(ex, "Action threw an exception"));
            }
        }

        /// <summary>
        /// Executes the provided functions based on the result state of the current instance.
        /// </summary>
        /// <typeparam name="TResult">The return type of the functions to be executed.</typeparam>
        /// <param name="success">The function to execute if the result signifies success.</param>
        /// <param name="failure">The function to execute if the result signifies failure, with the failure information provided.</param>
        /// <returns>The result of the executed function, either the success function or the failure function.</returns>
        public TResult Match<TResult>(Func<TResult> success, Func<FailureInfo, TResult> failure)
        {
            return IsError ? failure(Error) : success();
        }


        /// <summary>
        /// Executes a specified action if the current instance represents a failure,
        /// passing the failure information to the provided action.
        /// </summary>
        /// <param name="catchAction">The action to execute with the <see cref="FailureInfo"/> of the failure.</param>
        public void Catch(Action<FailureInfo> catchAction)
        {
            if (IsError)
            {
                catchAction(Error);
            }
        }


        /// <summary>
        /// Wraps the given exception into a generic wrapper exception type for consistent error handling.
        /// </summary>
        /// <param name="theException">The exception to be wrapped.</param>
        /// <returns>A new wrapper exception that contains the original exception.</returns>
        public static FailureOrNothing WrapException(Exception theException)
        {
            return WrapException(theException, theException.Message);
        }

        /// <summary>
        /// Wraps an exception in a <see cref="FailureOrNothing"/> instance, providing a failure result with the specified message.
        /// </summary>
        /// <param name="ex">The exception to be wrapped.</param>
        /// <param name="message">A message describing the context of the exception.</param>
        /// <returns>A <see cref="FailureOrNothing"/> instance encapsulating the exception and message as failure information.</returns>
        public static FailureOrNothing WrapException(Exception ex, string message)
        {
            return Failure(FailureInfo.FromException(ex, message));
        }

        /// <summary>
        /// Throws the underlying exception if the current instance represents an error.
        /// If no inner exception is present, throws a new exception with the error message.
        /// </summary>
        /// <exception cref="Exception">Thrown when the instance represents an error.</exception>
        public void ThrowAsException()
        {
            if (!IsError) return;

            if (Error.InnerException is null)
            {
                throw new Exception(Error.Message);
            }
            throw Error.InnerException;
        }

    }

}
