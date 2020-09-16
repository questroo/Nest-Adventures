using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRedirect : MonoBehaviour
{
    [SerializeField] private float delay = 2.0f;
    private void Awake()
    {
        StartCoroutine(RedirectAfterDelay());
    }

    private IEnumerator RedirectAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        yield return SceneManager.LoadSceneAsync(ServiceLocator.Get<SessionData>().SceneRedirectIndex);
    }
}
