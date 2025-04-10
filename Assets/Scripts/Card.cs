using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx;
    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Images/rtan{idx}");
    }

    public void OpenCard()
    {
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.IsMatch();
        }
    }
    
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
    }
}
