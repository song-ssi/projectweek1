using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    public int idx;
    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;
<<<<<<< HEAD
<<<<<<< HEAD

    void Start()
    {

    }

=======
>>>>>>> parent of d4ad9f1 (코드수정_승현)
=======
>>>>>>> parent of d4ad9f1 (코드수정_승현)
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Images/rtan{idx}");
    }
    

    public void OpenCard()
    {
        front.SetActive(true);
<<<<<<< HEAD
        back.SetActive(false);
  
=======
        back.SetActive(false);    
<<<<<<< HEAD
>>>>>>> parent of d4ad9f1 (코드수정_승현)
=======
>>>>>>> parent of d4ad9f1 (코드수정_승현)

    
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            
            if (GameManager.Instance.itemcount > 0)
            {
                GameManager.Instance.OnItemBtn();
            }
            else
            {
                return; // itemcount=0 이라면 itemModeBtn을 누를 수 없도록 false상태로 유지.
            }


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
        GameManager.Instance.OffItemBtn();
    }

    private void DestroyCardInvoke()
    {
        Destroy(this.gameObject);
        
    }
    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 0.8f);
        GameManager.Instance.OffItemBtn();

    }
    private void CloseCardInvoke()
    {
        front.SetActive(false);
        back.SetActive(true);
    }
}
