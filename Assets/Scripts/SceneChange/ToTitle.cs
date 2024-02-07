using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(JudgeHold());
        }
    }

    // 1•b’·‰Ÿ‚µ‚Åƒ^ƒCƒgƒ‹‚É–ß‚é
    IEnumerator JudgeHold()
    {
        float time = 0f;

        while (time < 1f)
        {
            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                yield break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Title");
    }
}
