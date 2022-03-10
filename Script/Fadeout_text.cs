using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout_text : MonoBehaviour
{
    public Text endment;
    // Start is called before the first frame update
    void Start()
    {
        endment = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainFade()
    {
        StartCoroutine(FadeCoroutine());
    }

    public IEnumerator FadeCoroutine()
    {
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {

            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            endment.color = new Color(255, 255, 255, fadeCount);

        }
    }
}
