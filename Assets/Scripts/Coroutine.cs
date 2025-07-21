using UnityEngine;
using System.Collections;

public class Coroutine : MonoBehaviour
{
    [SerializeField]
    int[] intsToPrint;

    [SerializeField]
    float pauseDuration;

    [SerializeField]
    bool continueCoroutine = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TaskOne());
    }

    IEnumerator TaskOne()
    {
        yield return TaskTwo(); // Wait for TaskTwo to complete before proceeding
        for (int i = 0; i < intsToPrint.Length; i++)
        {
            Debug.Log("Printing integer: " + intsToPrint[i]);
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator TaskTwo()
    {
        while (!continueCoroutine)
        {
            Debug.Log("Waiting for continueCoroutine to be true...");
            yield return null; // Wait for the next frame
        }
        Debug.Log("continueCoroutine is now true, proceeding with TaskTwo.");
    }
}
