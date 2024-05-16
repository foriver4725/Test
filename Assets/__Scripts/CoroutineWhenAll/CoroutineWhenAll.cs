using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoroutineWhenAll
{
    public class CoroutineWhenAll : MonoBehaviour
    {
#if true
        List<Coroutine> tasks = new List<Coroutine>();

        void Start()
        {
            tasks.Add(StartCoroutine(WaitSeconds(2, (seconds) => Debug.Log($"Waited for {seconds} sec."))));
            tasks.Add(StartCoroutine(WaitSeconds(4, (seconds) => Debug.Log($"Waited for {seconds} sec."))));
            tasks.Add(StartCoroutine(WaitSeconds(6, (seconds) => Debug.Log($"Waited for {seconds} sec."))));
        }

        void Update()
        {
            Debug.Log(tasks.Count);
        }

        IEnumerator WaitSeconds(float seconds, System.Action<float> callback)
        {
            yield return new WaitForSeconds(seconds);

            callback(seconds);
        }
#else
        List<Coroutine> tasks = new List<Coroutine>();

        void Start()
        {
            tasks.Add(StartCoroutine(Wait(2)));
            tasks.Add(StartCoroutine(Wait(4)));
            tasks.Add(StartCoroutine(Wait(6)));
        }

        void Update()
        {
            Debug.Log(tasks.Count);
        }

        IEnumerator Wait(float seconds)
        {
            Coroutine coroutine = StartCoroutine(WaitSeconds(seconds));

            yield return coroutine;

            Debug.Log($"Waited for {seconds} sec.");
        }

        IEnumerator WaitSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
#endif
    }
}