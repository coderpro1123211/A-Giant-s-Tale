using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeShower : MonoBehaviour {

    public string[] lines;
    public Sprite[] expressions;
    public bool endGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.transform.root.gameObject.CompareTag("Player")) return;
        DialougeManager.ShowDialougeBox(lines, expressions, () => { Destroy(gameObject); if (endGame) { FindObjectOfType<EndGameScreen>().Show(); } });
    }
}
