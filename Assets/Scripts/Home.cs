using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public Text timeText;
    private float totalTime = 10;
	// Use this for initialization
	void Start () {
        StartCoroutine(CountDown());
	}

    private IEnumerator StartLoading()
    {
        int displayProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync("Main");
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }
        while (displayProgress < 101f)
        {
            displayProgress = displayProgress + 60;
        }
        op.allowSceneActivation = true;
        Debug.Log("load main");
    }

    private IEnumerator CountDown()
    {
        while (totalTime > 0)
        {
            timeText.text = totalTime.ToString();
            yield return new WaitForSeconds(1);
            totalTime--;
            //visitBackTime.text = string.Format("{0:D2}分钟{1:D2}秒", (int)totalTime / 60, (int)totalTime % 60);
        }

        Debug.Log("load main");
        SceneManager.LoadSceneAsync("Main");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
