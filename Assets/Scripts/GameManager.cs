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
    public RectTransform timeBar;
    public GameObject endText;
    public Card firstCard;
    public Card secondCard;
    public int cardCount;

    float time = 20.0f;
    float rightnow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        passedTime = 20.0f;


    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        passedTime -= Time.deltaTime;
        timeText.text = passedTime.ToString("N2");
        // rightnow = float.Parse(timeText.text);
        // Debug.Log(rightnow);
        timeBar.localScale = new Vector3(-(float.Parse(timeText.text) / time), 1.0f,1.0f);

        if(passedTime < 0.0f)
        {
            GameOver();
        }
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
                GameOver();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = secondCard = null;

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
}
