namespace Coorth.Framework;

public readonly record struct ActorOptions(string? Name, int Throughput, int MailboxSize) {
    
    public readonly string? Name = Name;
    
    public readonly int Throughput = Throughput;
    
    public readonly int MailboxSize = MailboxSize;
}