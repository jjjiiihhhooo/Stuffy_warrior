using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{
    Left, Right, Front, Back
}

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private Transform actionCam;
    [SerializeField] private Transform moveCam;

    public Animator anim;
    public GameObject arrow;
    public GameObject mm;
    public Vector2 playerPos;
    public Vector3 target;

    public Dir dir;

    public bool isMeat;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        dir = Dir.Front;
        GameManager.Instance.Cam.Cams[1].Follow = actionCam;
        GameManager.Instance.Cam.Cams[1].LookAt = actionCam;
        GameManager.Instance.Cam.Cams[2].Follow = moveCam;
        GameManager.Instance.Cam.Cams[2].LookAt = moveCam;

    }

    public Dir GetPlayerDir()
    {
        return dir;
    }

    public void SetPlayerDir(Dir t)
    {
        dir = t;
        Rotate();
    }

    public void Rotate()
    {
        StopCoroutine(RotCor());
        StartCoroutine(RotCor());
    }

    private IEnumerator RotCor()
    {
        Vector3 m = new Vector3(0, 1, 0);
        float t = 90;

        while (t > 0)
        {
            yield return new WaitForSeconds(0.01f);
            transform.eulerAngles -= m;
            t--;
        }

        Player.Instance.mm.SetActive(false);
        GameManager.Instance.TurnEnd();
    }

    public void ReRotate()
    {
        StopCoroutine(ReRotCor());
        StartCoroutine(ReRotCor());
    }

    private IEnumerator ReRotCor()
    {
        Vector3 m = new Vector3(0, 1, 0);
        float t = 0;

        while (t < 90)
        {
            yield return new WaitForSeconds(0.01f);
            transform.eulerAngles += m;
            t++;
        }

        Player.Instance.mm.SetActive(false);
        GameManager.Instance.TurnEnd();
    }


    public void PlayerMove()
    {
        target = GameManager.Instance.NodeManager.TargetPos();

        anim.Play("Ready");
    }

    public void Move()
    {
        StopCoroutine(MoveCor(target));
        StartCoroutine(MoveCor(target));
    }

    private IEnumerator MoveCor(Vector3 t)
    {
        Vector3 target = new Vector3(t.x, transform.position.y, t.z);

        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.8f);
            yield return new WaitForFixedUpdate();
        }

        transform.position = target;

        if (GameManager.Instance.NodeManager.PlayerNode.type == NodeType.Enemy || 
            GameManager.Instance.NodeManager.PlayerNode.type == NodeType.Trigger)
        {
            anim.Play("Dead");
        }
        else if (isMeat)
        {
            anim.Play("Meat");
            isMeat = false;
        }
        else
        {
            anim.Play("Collision");
        }

        //GameManager.Instance.PlayerEnd();
    }
}
