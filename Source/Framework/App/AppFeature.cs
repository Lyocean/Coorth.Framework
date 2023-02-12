using System.Threading.Tasks;


namespace Coorth.Framework; 

public interface IAppFeature : IFeature<AppBase> {

    void OnSetup(AppBase app);
    
    Task LoadAsync(AppBase app);
}

public abstract class AppFeature : IAppFeature {

    public abstract void Install(AppBase app);

    public abstract void OnSetup(AppBase app) ;

    public abstract Task LoadAsync(AppBase app);

}
