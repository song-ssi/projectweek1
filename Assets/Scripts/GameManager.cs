using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI failCountText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    public Image itemModeBtnImage;
    public Slider brightnessSlider; // 버튼의 밝기 조절용
    public Button itemModeBtn;
    public Button retryBtn;
    public Button homeBtn;
    public Button stopBtn;
    public Button continueBtn;
    public RectTransform timeBar;
    public GameObject endText;
    private List<Card> allCards = new List<Card>{};
    public Card firstCard;
    public Card secondCard;
    public int cardCount;
    float passedTime;
    public int level = 1;
    public int itemcount = 10;
    float time; // 제한시간
    int failCount; // 실패 가능 횟수
    



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
        
        GameManager.Instance.OffItemBtn();
        LoadLevel();
        LoadItemCount();
        SaveItemCount();
        levelText.text = $"Lv.{level}";
        Debug.Log(passedTime);


        
        if(PlayerPrefs.HasKey("passedtime"))
        {
            LoadTime();
        }
        else
        {
            if(level == 1)
            {
                passedTime = 60.0f;
                time = 60.0f;
                failCount = 30;
            }
            else if(level == 2)
            {
                passedTime = 60.0f;
                time = 60.0f;
                failCount = 20;
            }
            else if(level == 3)
            {
                passedTime = 40.0f;
                time = 40.0f;
                failCount = 20;
            }
            else if(level == 4)
            {
                passedTime = 40.0f;
                time = 40.0f;
                failCount = 10;
            }
            else if(level == 5)
            {
                passedTime = 30.0f;
                time = 30.0f;
                failCount = 5;
            }
        }
        

        
        

    }

    void Update()
    {
        passedTime -= Time.deltaTime;
        timeText.text = passedTime.ToString("N2");
        timeBar.localScale = new Vector3(-(float.Parse(timeText.text) / time), 1.0f, 1.0f);
        failCountText.text = $"실패 가능 횟수 : {failCount}";
        itemCountText.text = $"X {itemcount}";
        
        if(passedTime < 0.0f)
        {
            GameOver();
        }

        if(passedTime < 6.0f)

        if(passedTime < 6.0f)
        {
            Image img = timeBar.GetComponent<Image>();
            img.color = Color.red;
            

        }
    }

    // 아이템 버튼 온/오프
    public void OnItemBtn()
    {
        itemModeBtn.interactable = true;
        float brightness = 2.0f;
        Color baseColor = Color.white * brightness;
        baseColor.a = itemModeBtnImage.color.a;
        itemModeBtnImage.color = baseColor;
    }
    public void OffItemBtn()
    {
        itemModeBtn.interactable = false;
        float brightness = 0.5f;
        Color baseColor = Color.white * brightness;
        baseColor.a = itemModeBtnImage.color.a;
        itemModeBtnImage.color = baseColor;
    }
    
    // 아이템 모드
    public void OnItemMode()
    {
        Debug.Log("아이템모드 시작");
        itemModeBtn.interactable = false; //중복으로 누르지 못하게 버튼 비활성화
        allCards = FindObjectsOfType<Card>().ToList();

        foreach (Card card in allCards)
        {
            if (card != firstCard && card.idx == firstCard.idx)
            {
                card.OpenCard();
                itemcount --;
                SaveItemCount();
                Debug.Log("아이템모드 종료");
                break;
            }
        }
    }
    
    // 카드 매칭 시스템
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
            
            if(failCount == 0)
            {
                GameOver();
                retryBtn.gameObject.SetActive(true);
                homeBtn.gameObject.SetActive(true);    

            }
        }
        firstCard = secondCard = null;
    }

    // 게임 오버
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        endText.SetActive(true);
    }

    // 처음으로 버튼
    public void HomeBtn()
    {
        level = 1;
        SaveLevel();
        itemcount = 10;
        SaveItemCount();
        SceneManager.LoadScene("MainScene");
    }

    // 다시하기 버튼
    public void RetryBtn()
    {
        LoadItemCount();
        SceneManager.LoadScene("MainScene");
    }

    // 이어하기 버튼
    public void ContinueBtn()
    {
        LoadTime();
        Debug.Log($"시간 저장 : {passedTime}");
        SceneManager.LoadScene("MainScene");
    }

    // 스탑 버튼
    public void StopBtn()
    {   
        PlayerPrefs.DeleteKey("passedtime");
        SaveTime();
        Time.timeScale = 0.0f;
        retryBtn.gameObject.SetActive(true);
        homeBtn.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(true);
    }

    // 레벨 저장, 불러오기
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("currentLevel", level);
    }
    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt("currentLevel", 1);
        level = PlayerPrefs.GetInt("currentLevel", 1);
    }

    // 아이템 사용횟수 저장, 불러오기
    void SaveItemCount()
    {
        PlayerPrefs.SetInt("itemcount", itemcount);
    }
    void LoadItemCount()
    {
        itemcount = PlayerPrefs.GetInt("itemcount", 10);
    }

    // 시간 저장, 불러오기
    void SaveTime()
    {
        PlayerPrefs.SetFloat("passedTime", passedTime);
    }
    void LoadTime()
    {
        passedTime = PlayerPrefs.GetFloat("passedTime", 1.0f);
    }


} 




