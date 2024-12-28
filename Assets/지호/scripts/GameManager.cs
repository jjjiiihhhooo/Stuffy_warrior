using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private string mainSceneName;
    [SerializeField] private GameObject player_obj;
    [SerializeField] private UIManager ui;
    [SerializeField] private CameraManager cam;
    [SerializeField] private InputSystem input;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private TurnManager turn;
    [SerializeField] private SkillManager skill;

    public GameObject spaceObj;
    public UIManager UI { get => ui; }
    public CameraManager Cam { get => cam; }
    public InputSystem Input { get => input; }
    public NodeManager NodeManager { get => nodeManager; }
    public TurnManager Turn { get => turn; }
    public SkillManager Skill { get => skill; }


    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        ui.Init();
        cam.Init();
        input.Init();
        nodeManager.Init();
        turn.Init();
        skill.Init();
        StartCoroutine(TurnDelay());
    }

    private IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(1f);
        TurnReady();
    }

    public void CreatePlayer(Vector3 pos)
    {
        Debug.Log("pos = " + pos);
        GameObject obj = Instantiate(player_obj);
        obj.transform.position = pos;
        Player.Instance.playerPos = new Vector2(nodeManager.PlayerNode.NodePos.x, nodeManager.PlayerNode.NodePos.y);
    }

    public void GameStart()
    {
        LoadingSceneManager.LoadScene(mainSceneName);
    }

    public void TurnStart()
    {
        turn.turnReady = false;
        Player.Instance.arrow.SetActive(false);
        spaceObj.SetActive(false);
        cam.SetPrioiry(1);
        StopCoroutine(MoveDelay());
        StartCoroutine(MoveDelay());
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Player.Instance.PlayerMove();
    }

    public void TurnEnd()
    {
        cam.SetPrioiry(0);

        nodeManager.NodeAction();   
    }

    public void TurnNodeReady()
    {
        StopCoroutine(TurnDelay());
        StartCoroutine(TurnDelay());
    }

    public void PlayerEnd()
    {
        if (nodeManager.CurNodeCheck())
        {
            nodeManager.PlayerNode.Damage();
        }
        else if (nodeManager.ClearNodeCheck())
        {
            StageClear();
        }
        else
        {
            nodeManager.ChangeDir();
        }
    }

    public void TurnReady()
    {
        turn.AddCount();
        ui.TurnReady();
        skill.CheckUse();
        spaceObj.SetActive(true);
        Player.Instance.arrow.SetActive(true);
    }


    public void StageClear()
    {
        ui.ClearUI();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SceneChange(string name)
    {
        LoadingSceneManager.LoadScene(name);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        ui.OverUI();
    }
    
    public void Exit()
    {
        Application.Quit();
    }

}
