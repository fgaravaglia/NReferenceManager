using System.Diagnostics.CodeAnalysis;
namespace NReferenceManager.Cli
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class LogHelper
    {
        public static void LogError(Exception ex, string message)
        {
            ArgumentNullException.ThrowIfNull(ex);
            ArgumentNullException.ThrowIfNullOrEmpty(message);

            Console.WriteLine("********************************************************");
            Console.WriteLine(message);
            Console.WriteLine(" Message: " + ex.Message);
            Console.WriteLine(" Stack Trace:");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("********************************************************");
        }
    }
}