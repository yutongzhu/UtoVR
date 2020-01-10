using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackImageBehavior : MonoBehaviour
{
     Transform recommendButton;
      Transform vrButton;
     Transform giantScreenButton;
     Transform travelButton;
    public Transform backImage;
     Transform button;
    void Start()
    {
        this.transform.position  = recommendButton.position ;
        recommendButton = UIManager.instance.GetGameObject("RecommendButton").transform;
        vrButton = UIManager.instance.GetGameObject("VRButton").transform;
        giantScreenButton = UIManager.instance.GetGameObject("GiantscreenButton").transform;
        travelButton = UIManager.instance.GetGameObject("TravelButton").transform;
        recommendButton. GetComponent<Button>().onClick.AddListener(RecommendOnclick);
        vrButton.GetComponent<Button>().onClick.AddListener(VROnclick);
        giantScreenButton.GetComponent<Button>().onClick.AddListener(giantScreenOnclick);
        travelButton.GetComponent<Button>().onClick.AddListener(TravelOnclick);
    }
    public void SetBackImagePo()
    {
       
        backImage.position = button.position;
        //UIManager.instance.GetGameObject("").GetComponent<Button>().onClick.AddListener() ;
    }
    private void giantScreenOnclick()
    {

        backImage.position = giantScreenButton.position;
    }
    private void VROnclick()
    {
        backImage.position = vrButton.position;
    }
    private void TravelOnclick()
    {
        backImage.position = travelButton.position;
    }
    private void RecommendOnclick()
    {
        backImage.position = recommendButton.position;
    }


}
