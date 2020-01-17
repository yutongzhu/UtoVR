using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

   // public MediaPlayerCtrl mediaPlayerCtrl;
	void Start () {
		
	}
    bool playEnable=true;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playEnable)
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.HLLIvePauseSpi;
         
        }
        else
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.HLLivePlaySpi;
         
        }      

    }

    public void OnPointerExit(PointerEventData eventData)
    {
    

        if (playEnable)
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.normalLivePauseSpi;
          
        }
        else
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.normralLivePlaySpi;
       
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (playEnable)
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.normralLivePlaySpi;
          //  mediaPlayerCtrl.Pause();

            playEnable = false;
        }
        else
        {
            this.GetComponent<Image>().sprite = ResourceContine.Instance.normalLivePauseSpi;
           // mediaPlayerCtrl.Play ();
            playEnable = true;
        }
    }
}
