using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float passedTime;
    [SerializeField] private TextMeshProUGUI levelText;
    public RectTransform timeBar;
    public GameObject endText;
    public Card firstCard;
    public Card secondCard;
    public int saveidx;
    public int cardCount;
    public int firstCardidx;
    private int failCount = 10;
    public int level = 1;
    float time = 30.0f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        passedTime = 30.0f;
        level = 1;
        // LoadLevel() 지우지마세요;
        // levelText.text = $"Lv.{level}" 지우지마세요;
        

    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        passedTime -= Time.deltaTime;
        timeText.text = passedTime.ToString("N2");
        timeBar.localScale = new Vector3(-(float.Parse(timeText.text) / time), 1.0f,1.0f);
        
        if(passedTime < 0.0f)
        {
            GameOver();
        }

        if(passedTime < 6.0f)
        {
            Image img = timeBar.GetComponent<Image>();
            img.color = Color.red;
            

        }
    }

    public void Save()
    {
        saveidx = firstCard.idx;
        Debug.Log($"인덱스가 저장되었습니다.{saveidx}");

        
    }
    public void IsMatch()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if(cardCount == 0)
            {
                level++;
                SaveLevel();
                GameOver();
                
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            failCount --;
            Debug.Log($"{failCount}남았습니다");
            Debug.Log($"[체크] level: {level}");


            if(level == 3)
            {
                MinusTime();
            }

            if(level == 1)
            {   
                if(failCount <= 0)
                {
                    GameOver();
                }
            }
            




        }
        firstCard = secondCard = null;
    }

    public void MinusTime()
    {
        if(firstCard.idx != secondCard.idx)
        time -= 1.0f;
    }    

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        endText.SetActive(true);       
    }
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("currentLevel", level);
        PlayerPrefs.Save();
    }

    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt("currentLevel", 1);
    }
}


