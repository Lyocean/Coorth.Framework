namespace Coorth.Errors; 

public interface IError {
    
}

public record ErrorText(string Text) : IError {
    public string Text { get; } = Text;
}