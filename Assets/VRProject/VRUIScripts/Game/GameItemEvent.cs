using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameItemEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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


}
public virtual void OnPointerExit(PointerEventData eventData)
{ }
}
