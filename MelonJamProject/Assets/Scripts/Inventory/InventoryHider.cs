using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryHider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator anim;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).speed == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Show"))
            anim.SetTrigger("Show");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hide"))
            anim.SetTrigger("Hide");
    }
}
