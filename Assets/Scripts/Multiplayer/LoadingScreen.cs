using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static void loading()
    {
        float progress = Mathf.Clamp(UIManager.instance.loadTime / UIManager.instance.LOADTIME * 290, 0, 290);
        GameManager.instance.loadingBar.rectTransform.sizeDelta = new Vector2(progress, 20);
        GameManager.instance.loadingBar.rectTransform.localPosition = new Vector2(progress / 2 -145, 0);
    }
}
