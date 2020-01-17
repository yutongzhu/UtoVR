using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneEnum
 {
    Main,
    MainVR

}
public class SceneStatus : MonoBehaviour {

    public static SceneStatus instance;
    public static SceneEnum sceneEnum;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

	void Start () {
        sceneEnum = SceneEnum.Main;

    }
	
	
}
