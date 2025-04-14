using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx;
    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;
    public Animator anim;
    AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Images/rtan{idx}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);
        front.SetActive(true);
        back.SetActive(false);    
        //anim.SetTrigger("Open");

    
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            GameManager.Instance.Save();

        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.IsMatch();
        }
    
    }
    // public void SaveCardImage()
    // {
    //     GameManager.Instance.firstCard.idx = firstCardidx;
    //     firstCardidx = card.GetCard();  // 첫 번째 카드의 이미지를 저장

    // }
    public void DestroyCard()
    {
        Invoke(nameof(DestroyCardInvoke), 0.8f);
    }

    private void DestroyCardInvoke()
    {
        Destroy(this.gameObject);
    }
    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 0.8f);
    }
    private void CloseCardInvoke()
    {
        front.SetActive(false);
        back.SetActive(true);
        //anim.SetTrigger("Close");
    }
}
