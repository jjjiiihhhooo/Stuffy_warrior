using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class NodeTypes : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NodeType type;
    public Paths[] paths;
    public string name;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        if (GameManager.Instance.Skill.type == type)
        {
            this.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
