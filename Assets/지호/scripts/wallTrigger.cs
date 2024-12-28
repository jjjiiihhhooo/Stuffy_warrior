using UnityEngine;

public class wallTrigger : MonoBehaviour
{
    public FakeWall fakewall;
    public GameObject[] fake;
    public GameObject dest;

    public void Change()
    {
        transform.GetComponent<Node>().type = NodeType.Normal;
        Destroy(dest);
        fake[0].SetActive(true);
        fake[1].SetActive(true);
        fakewall.isCheck = false;
    }
}
