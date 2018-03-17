namespace Api
{
    public static class Log
    {
        public static void Trace(string msg)
        {
            System.Diagnostics.Trace.WriteLine($"Trace: {msg}");
            //System.Diagnostics.Debug.WriteLine($"Debug: {msg}");
        }
    }
}