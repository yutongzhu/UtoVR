using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeadPortraitEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
       
        Transform parent = this.transform.parent;

        foreach (Button item in parent.GetComponentsInChildren <Button>())
        {
            item.transform.Find("Select").gameObject.SetActive(false );
            item.tag = "Untagged";
        }
        this.transform.Find("Select").gameObject.SetActive(true);
        this.transform.tag = "HeadSelected";
        Image image = this.transform.Find("Image").GetComponent<Image>();
        AccountSetMamager.instance.portraitSprite = image.sprite;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
       // this.transform.Find("Select").gameObject.SetActive(false);

    }

}
