using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {

    public RectTransform[] levelTransforms;
    public bool[] levelUnlocked;
    public RectTransform player;
    public float animSpeed;

    public Sprite notUnlockedSprite;
    public Sprite currentSprite;
    public Sprite unlockedSprite;

    MainMenuController c;
    Image[] levelImages;
    float t;
    int currentLevel;
    bool walking;
    Direction walkDir;
    Animator pAnim;

	// Use this for initialization
	void Start () {
        c = GetComponentInParent<MainMenuController>();
        pAnim = player.GetComponentInChildren<Animator>();
        levelUnlocked = new bool[levelTransforms.Length];
        levelImages = new Image[levelTransforms.Length];
        levelUnlocked[0] = true;
        levelImages[0] = levelTransforms[0].GetComponent<Image>();
        levelImages[0].sprite = currentSprite;
        for (int i = 1; i < levelTransforms.Length; i++)
        {
            levelImages[i] = levelTransforms[i].GetComponent<Image>();
            if (!PlayerPrefs.HasKey("l" + i)) PlayerPrefs.SetInt("l"+i, 0);
            levelUnlocked[i] = PlayerPrefs.GetInt("l" + (i)) == 1;
            //levelUnlocked[i] = true;
            if (!levelUnlocked[i])
            {
                levelImages[i].sprite = notUnlockedSprite;
            }
            else
            {
                levelImages[i].sprite = unlockedSprite;
            }
        }
        PlayerPrefs.Save();
	}
	
	// Update is called once per frame
	void Update () {
        if (!c.showingLevelSelectScreen) return;

        float input = Input.GetAxisRaw("Horizontal");

        if (input >= 0.25f && !walking && currentLevel < levelTransforms.Length-1 && levelUnlocked[currentLevel + 1])
        {
            walking = true;
            walkDir = Direction.Right;
            player.localScale = new Vector3(1, 1, 1);
        }
        else if (input <= -0.25f && !walking && currentLevel > 0 && levelUnlocked[currentLevel -1])
        {
            walking = true;
            walkDir = Direction.Left;
            player.localScale = new Vector3(-1, 1, 1);
        }

        if (walking)
        {
            t += Time.deltaTime * animSpeed;
            switch (walkDir)
            {
                case Direction.Left:
                    player.position = Vector2.Lerp(levelTransforms[currentLevel].position, levelTransforms[currentLevel-1].position, t);
                    break;
                case Direction.Right:
                    player.position = Vector2.Lerp(levelTransforms[currentLevel].position, levelTransforms[currentLevel + 1].position, t);
                    break;
                default:
                    break;
            }
        }
        if (t >= 1)
        {
            t = 0;
            walking = false;
            levelImages[currentLevel].sprite = unlockedSprite;
            currentLevel += walkDir == Direction.Left ? -1 : 1;
            levelImages[currentLevel].sprite = currentSprite;
        }

        pAnim.SetBool("walking", walking);
	}

    public void PlayCurrentLevel()
    {
        if (walking) return;
        Debug.Log("LvlToLoad:" + (currentLevel + 1));
        SceneManager.LoadScene(currentLevel + 1);
    }
}

enum Direction
{
    Left, Right
}