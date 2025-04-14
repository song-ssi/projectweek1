using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI failCountText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private float passedTime;
    public RectTransform timeBar;
    public GameObject endText;
    public Card firstCard;
    public Card secondCard;
    public int saveidx;
    public int cardCount;
    public int firstCardidx;
    private int failCount;
    private float remainingRatio;
    public int level = 1;
    public int itemcount = 10;
    float time;
    AudioSource audioSource;  // 오디오 소스 추가
    public AudioClip matchSound; // 추가
    public AudioClip failSound;


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
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기 코드 추가

        LoadLevel(); // 레벨 불러오기
        ApplyLevelSettings(); // 불러온 레벨에 맞춰서 난이도 파라미터 세팅하기
        // 기존에 Start문에 있는 코드는 에러가 자주나서 ApplyLevelSettings 함수에 넣어서 관리하도록 변경

        levelText.text = $"Lv.{level}";
        itemCountText.text = $"X {itemcount}";  

    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        passedTime -= Time.deltaTime;
        timeText.text = passedTime.ToString("N2");
        timeBar.localScale = new Vector3(-(float.Parse(timeText.text) / time), 1.0f,1.0f);
        failCountText.text = $"실패 가능 횟수 : {failCount}";
        remainingRatio = (passedTime / time);

        if(passedTime < 0.0f)
        {
            GameOver();
        }
        else if(remainingRatio < 0.3f) // 비율 기준 계산이 아니라 초 기준 계산이어서 코드 수정함 
        {
            Image img = timeBar.GetComponent<Image>();
            img.color = Color.red;
        }

        else if(remainingRatio < 0.5f) 
        {
            Image img = timeBar.GetComponent<Image>();
            img.color = Color.yellow;
        }


        if(passedTime < 0.0f)
        {
            GameOver();
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
            audioSource.PlayOneShot(matchSound); // 카드 매칭 사운드 한 번만 출력
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
            audioSource.PlayOneShot(failSound);
            firstCard.CloseCard();
            secondCard.CloseCard();
            failCount --;
            Debug.Log($"{failCount}남았습니다");
            Debug.Log($"[체크] level: {level}");
            
            if(failCount == 0)
            {
                GameOver();
            }

        
        }

        firstCard = secondCard = null;
    }

    private void ApplyLevelSettings() // 새로 추가한 코드 여기서 난이도 관리
    {
        switch (level)
        {
            case 1:
                 passedTime = 60.0f;
                 time = 60.0f;
                 failCount = 30;
                 break;

            case 2:
                passedTime = 60.0f;
                time = 60.0f;
                failCount = 20;
                break;

            case 3:
                passedTime = 40.0f;
                time = 40.0f;
                failCount = 20;
                break;

            case 4:
                passedTime = 40.0f;
                time = 40.0f;
                failCount = 10;
                break;

            case 5:
                passedTime = 30.0f;
                time = 30.0f;
                failCount = 5;
                break;

            default:
                Debug.LogWarning("정의되지 않은 레벨입니다.");
                break;
        }
    }


    // public void MinusTime()
    // {
    //     if(firstCard.idx != secondCard.idx)
    //     time -= 1.0f;
    // }    

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


