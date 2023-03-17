using System.Collections;
using UnityEngine;

public class LineMotion : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    private IEnumerator Disable()
    {
        GameplayManager.Instance.LineIsMoving = false;
        yield return null;
        gameObject.SetActive(false);
    }



}//class
