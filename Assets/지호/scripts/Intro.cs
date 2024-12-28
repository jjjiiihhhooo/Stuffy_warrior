using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
public class Intro : MonoBehaviour
{
    public DOTweenAnimation dot;
    public TextMeshProUGUI text;
    public GameObject space;
    public GameObject logue;
    public GameObject king;
    private bool isDot;

    public string[] script;
    public int index;

    public bool next;
    public bool title;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDot)
        {
            isDot = true;
            dot.transform.GetChild(0).gameObject.SetActive(false);
            dot.DOPlayById("Fade");
            space.SetActive(false);
            ChatStart();
        }

        if (Input.GetKeyDown(KeyCode.Space) && next)
        {
            space.SetActive(false);
            next = false;
            index++;
            ChatStart();
        }

        if (Input.GetKeyDown(KeyCode.Space) && title)
        {
            LoadingSceneManager.LoadScene("Title");
        }

    }

    public void ChatStart()
    {
        if (index < 4)
        {
            logue.SetActive(true);
            StartCoroutine(Cor());
        }
        else
        {
            title = true;
        }
    }

    private IEnumerator Cor()
    {
        if (index % 2 != 0) king.SetActive(true);
        else king.SetActive(false);
        string s = "";
        for (int i = 0; i < script[index].Length; i++)
        {
            s += script[index][i];
            yield return new WaitForSeconds(0.02f);

            text.text = s;
        }
        next = true;
        space.SetActive(true);

    }
}
