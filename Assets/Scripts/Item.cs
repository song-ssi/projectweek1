using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    Card first = GameManager.Instance.firstCard;
    Card second = GameManager.Instance.secondCard;
    
    public Button rainbowBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.firstCard != null && GameManager.Instance.secondCard == null)
        {
            rainbowBtn.interactable = true;
        }
        else
        {
            rainbowBtn.interactable = false;
        }
        
    }
    public void Rainbow()
    { 
        if (second != null && first != null)
        {
            second.idx = GameManager.Instance.saveidx;
        }
//         public void UpdateCardAppearance()
//         {
//             front.sprite = Resources.Load<Sprite>("CardImages");
//             case 0:
//                 front = Resources.Load<Sprite>("CardImages/Card0");
//                 break;
//             case 1:
//                 frontSprite = Resources.Load<Sprite>("CardImages/Card1");
//                 break;
//             // 다른 cardID에 대한 처리를 추가합니다.
//         }

//         // 카드를 보여주기 위해 이미지 갱신
//         spriteRenderer.sprite = frontSprite;



//  ㅜ
//         }
       
//     }
        
//     public void CopyFirstCardToSecond()
//     {

//     }
    }
}