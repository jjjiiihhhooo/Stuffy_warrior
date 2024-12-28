using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTypes : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public NodeType type;
    public Paths[] paths;
    public string name;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.Turn.turnReady) return;
        if (!GameManager.Instance.Skill.all) return;

        if (GameManager.Instance.Skill.type == NodeType.Enemy)
        {
            if (name == "Enemy")
            {
                GameManager.Instance.Skill.UseSkill(transform.GetComponentInChildren<Node>());
            }
        }
        else if (GameManager.Instance.Skill.type == NodeType.Normal)
        {
            if (name != "Iron" && name != "Spike" && name != "f")
            {
                GameManager.Instance.Skill.UseSkill(transform.GetComponentInChildren<Node>());
            }
        }
        else if (GameManager.Instance.Skill.type == NodeType.Trigger)
        {
            if (name == "Iron" || name == "Spike")
            {
                GameManager.Instance.Skill.UseSkill(transform.GetComponentInChildren<Node>());
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.Instance.Turn.turnReady) return;
        if (!GameManager.Instance.Skill.all) return;

        if (GameManager.Instance.Skill.type == NodeType.Enemy)
        {
            if (name == "Enemy")
            {
                if (GameManager.Instance.outline == null)
                {
                    GameManager.Instance.outline = Instantiate(GameManager.Instance.outPrefab);
                }
                GameManager.Instance.mat.color = Color.green;
                GameManager.Instance.outline.transform.position = this.gameObject.transform.position + Vector3.up;
            }
        }
        else if (GameManager.Instance.Skill.type == NodeType.Normal)
        {
            if (name != "Iron" && name != "Spike" && name != "f" && type == NodeType.Normal)
            {
                if(GameManager.Instance.outline == null)
                {
                    GameManager.Instance.outline = Instantiate(GameManager.Instance.outPrefab);
                }
                GameManager.Instance.mat.color = Color.red;
                GameManager.Instance.outline.transform.position = this.gameObject.transform.position + Vector3.up;
                
               //this.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
        else if (GameManager.Instance.Skill.type == NodeType.Trigger)
        {
            if (name == "Iron" || name == "Spike")
            {
                if (GameManager.Instance.outline == null)
                {
                    GameManager.Instance.outline = Instantiate(GameManager.Instance.outPrefab);
                }
                GameManager.Instance.mat.color = Color.blue;
                GameManager.Instance.outline.transform.position = this.gameObject.transform.position + Vector3.up;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!GameManager.Instance.Skill.all) return;
        this.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
