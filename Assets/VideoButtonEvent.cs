using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoButtonEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Transform Parent;
    public Sprite normalSprite;
    public Sprite highLightSprite;
    void Start()
    {

        Parent = this.transform.parent;
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (this.transform .name == "StandardBt")
        {
          
            //this .transform.GetComponent<Image>().sprite = ResourceContine.Instance.highLightStandard;
        }
        else if (this.transform.name == "HDButton")
        {
            this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.highLightHD;
        }
        else if (this.transform.name == "VeryHighBt")
        {
            this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.highLightlVeryHD;
           
        }
        else if (this.transform.name == "BluRayBt")
        {
            this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.hightLightBlueRay;
         
        }
        else if (this.transform.name == "_4KBt")
        {
            this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.hightLight_4K;
         
        }

    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {

        //if (this.transform.name == "StandardBt")
        //{

        //    this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.normalStandard;
        //}
        //else if (this.transform.name == "HDButton")
        //{
        //    this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.normalHD;
        //}
        //else if (this.transform.name == "VeryHighBt")
        //{
        //    this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.normalVeryHD ;

        //}
        //else if (this.transform.name == "BluRayBt")
        //{
        //    this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.normalBlueRay;

        //}
        //else if (this.transform.name == "_4KBt")
        //{
        //    this.transform.GetComponent<Image>().sprite = ResourceContine.Instance.normal_4k;

        //}

    }
}
