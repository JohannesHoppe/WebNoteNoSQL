namespace WordCountMapper
{
    public interface IConsole
    {
        void WriteLine(string value);
        void WriteLine(string value, params object[] arg);
        string ReadLine();
    }
}
