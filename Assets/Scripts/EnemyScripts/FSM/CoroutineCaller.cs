using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a workaround for using CoRoutines within scriptable objects.
public class CoroutineCaller : MonoBehaviour, IGameModule
{
    [HideInInspector]public CoroutineCaller instance;

    public IEnumerator LoadModule()
    {
        Debug.Log("Loading Coroutine Caller");
        instance = this;

        ServiceLocator.Register<CoroutineCaller>(this);
        yield return null;
    }
}