using UnityEngine;

public class DependenciesManager : MonoBehaviour
{
    private static DependenciesManager _instance = null;

    private void Awake()
    {
        // Saftey check
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set reference to this instance
        _instance = this;

        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        if (!ServiceLocator.Get<SessionData>().DependenciesLoaded)
        {
            ServiceLocator.Get<SessionData>().DependenciesLoaded = true;
            ServiceLocator.Register<DependenciesManager>(this);
            gameObject.transform.SetParent(GameLoader.SystemsParent);
        }
    }
}
