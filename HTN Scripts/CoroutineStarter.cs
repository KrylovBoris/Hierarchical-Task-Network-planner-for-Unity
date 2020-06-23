using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
    public void StartRunningCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
