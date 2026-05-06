using System.Collections;
using UnityEngine;

public class SkipTutorialButton : MonoBehaviour
{

    public float skipShowTime = 10f;

    void Start()
    {
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(skipShowTime);
        gameObject.SetActive(false);
    }

}