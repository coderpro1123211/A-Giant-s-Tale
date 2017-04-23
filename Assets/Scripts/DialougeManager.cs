using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {

    public static DialougeManager Instance { get; private set; }
    GameObject dialougeBox;

    string[] lines;
    Sprite[] expressions;
    bool disp;
    int c;
    public event System.Action onFinished;

    Text t;
    Image i;
    public Player p;
    //int TMP;

	void Awake ()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        dialougeBox = FindObjectOfType<DialougeBox>().gameObject;
        dialougeBox.SetActive(false);
        p = FindObjectOfType<Player>();

        t = dialougeBox.GetComponentInChildren<Text>();
        //i = dialougeBox.GetComponentInChildren<Image>();
	}

    public static void ShowDialougeBox(string message, Sprite expression)
    {
        Instance.m_ShowDialougeBox(message, expression, null);
    }

    public static void ShowDialougeBox(string[] lines, Sprite[] expressions)
    {
        Instance.m_ShowDialougeBox(lines, expressions, null);
    }

    public static void ShowDialougeBox(string message, Sprite expression, System.Action onFinish)
    {
        Instance.m_ShowDialougeBox(message, null, onFinish);
    }

    public static void ShowDialougeBox(string[] lines, Sprite[] expressions, System.Action onFinish)
    {
        Instance.m_ShowDialougeBox(lines, null, onFinish);
    }

    private void Update()
    {
        if (!disp) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            c++;
        }

        if (c >= lines.Length)
        {
            disp = false;
            dialougeBox.SetActive(false);
            if (p != null)
            p.disableMovement = false;
            lines = null;
            expressions = null;
            System.Action temp = onFinished;
            onFinished = null;
            if (temp != null)
            {
                temp();
            }
            return;
        }

        t.text = lines[c];
        //i.sprite = expressions[c];
    }

    void m_ShowDialougeBox(string message, Sprite expression, System.Action onFinish)
    {
        c = 0;
        lines = new string[] { message };
        expressions = new Sprite[] { expression ?? null };
        disp = true;
        onFinished = onFinish;
        if (p != null)
        p.disableMovement = true;
        dialougeBox.SetActive(true);
    }

    void m_ShowDialougeBox(string[] lines, Sprite[] expressions, System.Action onFinish)
    {
        c = 0;
        this.lines = lines;
        this.expressions = expressions;
        disp = true;
        onFinished = onFinish;
        dialougeBox.SetActive(true);
        if (p != null)
        p.disableMovement = true;
    }
}
