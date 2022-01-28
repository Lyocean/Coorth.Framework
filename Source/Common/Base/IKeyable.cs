namespace Coorth {
    public interface IKeyable<out T> {
        T Key { get; }
    }
}