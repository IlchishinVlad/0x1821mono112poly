namespace MonopolyNetworking
{
    public abstract class AbstractSocket
    {
        public string IP { get; set; }
        public int Port { get; set; }

        public abstract void Run();
        public abstract void Close();
    }
}
