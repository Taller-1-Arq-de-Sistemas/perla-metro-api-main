namespace PerlaMetroApiMain.Exceptions;

/// <summary>
/// Exception thrown when a downstream service returns an error payload.
/// </summary>
public class DownstreamServiceException : Exception
{
    /// <summary>
    /// Initializes an instance that carries the downstream response details.
    /// </summary>
    /// <param name="statusCode">HTTP status returned by the downstream service.</param>
    /// <param name="responseBody">Raw body returned by the downstream service.</param>
    /// <param name="contentType">Content type provided by the downstream service.</param>
    /// <param name="innerException">Optional inner exception.</param>
    public DownstreamServiceException(int statusCode, string responseBody, string? contentType = "application/json", Exception? innerException = null)
        : base($"Downstream service returned status code {statusCode}.", innerException)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
        ContentType = string.IsNullOrWhiteSpace(contentType) ? "application/json" : contentType;
    }

    /// <summary>
    /// Gets the HTTP status code received from the downstream service.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets the raw response body returned by the downstream service.
    /// </summary>
    public string ResponseBody { get; }

    /// <summary>
    /// Gets the content type provided by the downstream service.
    /// </summary>
    public string ContentType { get; }
}
