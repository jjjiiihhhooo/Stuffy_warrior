using System.Collections;
using UnityEngine;

[System.Serializable]
public class Paths
{
    public int x;
    public int y;
}

public class NodeManager : MonoBehaviour
{
    #region Nodess
    [System.Serializable]
    public class Nodes
    {
        public Node[] node = new Node[6];
    }

    [System.Serializable]
    public class Transforms
    {
        public Transform[] t = new Transform[6];
    }

    #endregion

    [SerializeField] private Nodes[] nodes;
    [SerializeField] private Transforms[] transforms;
    [SerializeField] private Node playerNode;
    [SerializeField] private Node targetNode;
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject iron;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy_obj;

    public Node PlayerNode { get => playerNode; set => playerNode = value; }
    public Nodes[] Nodess { get => nodes; set => nodes = value; }

    public void Init()
    {
        Setting();
        // MapSetting();
    }

    private void Setting()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].node.Length; j++)
            {
                nodes[i].node[j] = transforms[i].t[j].GetComponentInChildren<Node>();
                nodes[i].node[j].NodePos = new Vector2(i, j);
                nodes[i].node[j].transform.GetComponentInParent<NodeTypes>().type = nodes[i].node[j].type;
            }
        }

    }

    private void MapSetting()
    {

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                NodeType n = transforms[i].t[j].GetComponent<NodeTypes>().type;
                string s = transforms[i].t[j].GetComponent<NodeTypes>().name;

                GameObject obj = null;
                GameObject temp = null;
                if (n == NodeType.Normal)
                {
                    obj = Instantiate(normal, transforms[i].t[j]);
                }
                else if (n == NodeType.Start)
                {
                    obj = Instantiate(start, transforms[i].t[j]);
                    PlayerNode = obj.GetComponent<Node>();
                }
                else if (n == NodeType.End)
                {
                    obj = Instantiate(end, transforms[i].t[j]);
                }
                else if (n == NodeType.Wall)
                {
                    if (s == "Iron")
                    {
                        obj = Instantiate(iron, transforms[i].t[j]);
                    }
                    else
                    {
                        obj = Instantiate(wall, transforms[i].t[j]);
                    }

                }
                else if (n == NodeType.Trigger)
                {
                    if (s == "Spike")
                    {
                        obj = Instantiate(spike, transforms[i].t[j]);
                    }

                }
                else if (n == NodeType.Enemy)
                {
                    obj = Instantiate(enemy, transforms[i].t[j]);
                    temp = Instantiate(enemy_obj, transforms[i].t[j]);
                    temp.GetComponent<EnemyTrigger>().paths = transforms[i].t[j].GetComponent<NodeTypes>().paths;
                    temp.GetComponent<EnemyTrigger>().curPos = new Vector2(i, j);
                }
                //else if(n == NodeType.Item)
                //{
                //    obj = Instantiate(enemy, transforms[i].t[j]);
                //    temp = Instantiate(enemy_obj, transforms[i].t[j]);
                //}


                obj.transform.position = obj.transform.parent.position;

                if (temp != null)
                    temp.transform.position = temp.transform.parent.position;

                Node node = obj.GetComponent<Node>();
                //node.type = n;
                nodes[i].node[j] = node;
                node.NodePos = new Vector2(i, j);
            }
        }

        Vector3 pos = playerNode.transform.position;
        pos.y = 0;
        GameManager.Instance.CreatePlayer(pos);
    }

    public void ChangeDir()
    {
        Dir t = Player.Instance.GetPlayerDir();
        int x = (int)Player.Instance.playerPos.x;
        int y = (int)Player.Instance.playerPos.y;
        int max_x = nodes.Length;
        int max_y = nodes[0].node.Length;
        switch (t)
        {
            case Dir.Left:
                Player.Instance.SetPlayerDir(Dir.Back);
                if (x + 1 < max_x)
                {
                    if (nodes[x + 1].node[y].type == NodeType.Wall)
                    {
                        if (nodes[x + 1].node[y].isfake)
                        {
                            Player.Instance.isfake = true;
                            Player.Instance.fakeWall = nodes[x + 1].node[y].gameObject;
                            break;
                        }
                        ChangeDir();
                    }
                }
                else ChangeDir();
                break;
            case Dir.Right:
                Player.Instance.SetPlayerDir(Dir.Front);
                if (x - 1 > -1)
                {
                    if (nodes[x - 1].node[y].type == NodeType.Wall)
                    {
                        if (nodes[x - 1].node[y].isfake)
                        {
                            Player.Instance.isfake = true;
                            Player.Instance.fakeWall = nodes[x - 1].node[y].gameObject;
                            break;
                        }
                        ChangeDir();
                    }
                }
                else ChangeDir();
                break;
            case Dir.Front:
                Player.Instance.SetPlayerDir(Dir.Left);
                if (y - 1 > -1)
                {
                    if (nodes[x].node[y - 1].type == NodeType.Wall)
                    {
                        if (nodes[x].node[y - 1].isfake)
                        {
                            Player.Instance.isfake = true;
                            Player.Instance.fakeWall = nodes[x].node[y - 1].gameObject;
                            break;
                        }
                        ChangeDir();
                    }
                }
                else ChangeDir();
                break;
            case Dir.Back:
                Player.Instance.SetPlayerDir(Dir.Right);
                if (y + 1 < max_y)
                {
                    if (nodes[x].node[y + 1].type == NodeType.Wall)
                    {
                        if (nodes[x].node[y + 1].isfake)
                        {
                            Player.Instance.isfake = true;
                            Player.Instance.fakeWall = nodes[x].node[y + 1].gameObject;
                            break;
                        }
                        ChangeDir();
                    }
                }
                else ChangeDir();
                break;
        }

    } //방향만 정한다.

    public Vector3 TargetPos()
    {
        Vector2 pos = SearchPos();
        int x = (int)pos.x;
        int y = (int)pos.y;

        targetNode = nodes[x].node[y];

        Vector3 target = new Vector3(nodes[x].node[y].transform.position.x, 0, nodes[x].node[y].transform.position.z);

        playerNode = targetNode;
        Player.Instance.playerPos = pos;
        return target;
    }

    public Vector2 SearchPos()
    {
        int x = (int)Player.Instance.playerPos.x;
        int y = (int)Player.Instance.playerPos.y;
        int max_y = nodes[0].node.Length;
        int max_x = nodes.Length;
        Dir dir = Player.Instance.GetPlayerDir();
        bool b = false;

        if (dir == Dir.Left)
        {
            for (y = (int)Player.Instance.playerPos.y; y > -1; y--)
            {
                if (nodes[x].node[y].type == NodeType.Item)
                {
                    Player.Instance.isDestroyMeat = true;
                }

                if (nodes[x].node[y].type == NodeType.Wall)
                {
                    y += 1;
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.End)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Trigger)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Enemy)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].meatArea)
                {
                    MeatTrigger m = FindObjectOfType<MeatTrigger>();

                    Vector2 pos = m.pos;
                    if (pos.x == playerNode.NodePos.x)
                        continue;

                    b = true;
                    Player.Instance.isMeat = true;
                    Player.Instance.mm.SetActive(true);
                    m.DeSetting();
                    break;
                }
            }
            if (b == false)
                y = 0;
        }
        else if (dir == Dir.Right)
        {
            for (y = (int)Player.Instance.playerPos.y; y < max_y; y++)
            {
                if (nodes[x].node[y].type == NodeType.Item)
                {
                    Player.Instance.isDestroyMeat = true;
                }
                if (nodes[x].node[y].type == NodeType.Wall)
                {
                    y -= 1;
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.End)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Trigger)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Enemy)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].meatArea)
                {
                    MeatTrigger m = FindObjectOfType<MeatTrigger>();

                    Vector2 pos = m.pos;
                    if (pos.x == playerNode.NodePos.x)
                        continue;

                    b = true;
                    Player.Instance.isMeat = true;
                    Player.Instance.mm.SetActive(true);
                    m.DeSetting();
                    break;
                }
            }
            if (b == false)
                y = max_y - 1;
        }
        else if (dir == Dir.Front)
        {
            for (x = (int)Player.Instance.playerPos.x; x > -1; x--)
            {
                if (nodes[x].node[y].type == NodeType.Item)
                {
                    Player.Instance.isDestroyMeat = true;
                }
                if (nodes[x].node[y].type == NodeType.Wall)
                {
                    x += 1;
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.End)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Trigger)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Enemy)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].meatArea)
                {
                    MeatTrigger m = FindObjectOfType<MeatTrigger>();

                    Vector2 pos = m.pos;
                    if (pos.y == playerNode.NodePos.y)
                        continue;

                    b = true;
                    Player.Instance.isMeat = true;
                    Player.Instance.mm.SetActive(true);
                    m.DeSetting();
                    break;
                }
            }
            if (b == false)
                x = 0;

        }
        else if (dir == Dir.Back)
        {
            for (x = (int)Player.Instance.playerPos.x; x < max_x; x++)
            {
                if (nodes[x].node[y].type == NodeType.Item)
                {
                    Player.Instance.isDestroyMeat = true;
                }
                if (nodes[x].node[y].type == NodeType.Wall)
                {
                    x -= 1;
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.End)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Trigger)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].type == NodeType.Enemy)
                {
                    b = true;
                    break;
                }
                else if (nodes[x].node[y].meatArea)
                {
                    MeatTrigger m = FindObjectOfType<MeatTrigger>();

                    Vector2 pos = m.pos;
                    if (pos.y == playerNode.NodePos.y)
                        continue;

                    b = true;
                    Player.Instance.isMeat = true;
                    Player.Instance.mm.SetActive(true);
                    m.DeSetting();
                    break;
                }
            }
            if (b == false)
                x = max_x - 1;
        }

        return new Vector2(x, y);
    }

    public bool CurNodeCheck()
    {
        if (playerNode.type == NodeType.Trigger || playerNode.type == NodeType.Enemy || playerNode.type == NodeType.Item)
        {
            return true;
        }
        else return false;
    }

    public bool ClearNodeCheck()
    {
        if (playerNode.type == NodeType.End)
        {
            return true;
        }
        else return false;
    }

    public void NodeAction()
    {
        StopCoroutine(ActionCor());
        StartCoroutine(ActionCor());
    }

    private IEnumerator ActionCor()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].node.Length; j++)
            {
                yield return new WaitForEndOfFrame();
                nodes[i].node[j].Action();
            }
        }
        GameManager.Instance.TurnNodeReady();
    }

}
