namespace Coorth.Framework; 

public interface IFeature<in TContext> {
    void Install(TContext context);
}