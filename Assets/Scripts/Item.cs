// public class Card : MonoBehaviour
// {
//     public int cardNum;
//     public GameObject front;
//     public GameObject back;

//     // public void OpenCard()
//     // {
//     //     front.SetActive(true);
//     //     back.SetActive(false);
//     // }

//     // public void CloseCard()
//     // {
//     //     front.SetActive(false);
//     //     back.SetActive(true);
//     // }

//     // void OnMouseDown()
//     // {
//     //     if (!front.activeSelf) // 뒷면일 때만 클릭 허용
//     //     {
//     //         GameManager.instance.CardClicked(this);
//     //     }
//     // }


//     using UnityEngine;

// public class Card : MonoBehaviour
// {
//     public int cardNum;

//     // public GameObject front; // 앞면 이미지
//     // public GameObject back;  // 뒷면 이미지

//     // void Start()
//     // {
//     //     GameManager.instance.RegisterCard(this);
//     //     CloseCard(); // 시작 시 닫힌 상태로
//     // }

//     // public void OpenCard()
//     // {
//     //     front.SetActive(true);
//     //     back.SetActive(false);
//     // }

//     // public void CloseCard()
//     // {
//     //     front.SetActive(false);
//     //     back.SetActive(true);
//     // }

//     // public bool IsOpen()
//     // {
//     //     return front.activeSelf;
//     // }

//     // void OnMouseDown()
//     // {
//     //     if (!IsOpen())
//     //     {
//     //         GameManager.instance.CardClicked(this);
//     //     }
//     }



// // public void CardClicked(Card clickedCard)
// // {
// //     if (hintMode)
// //     {
// //         hintMode = false;

// //         clickedCard.OpenCard();

// //         foreach (Card card in allCards)
// //         {
// //             if (card != clickedCard && card.cardNum == clickedCard.cardNum)
// //             {
// //                 card.OpenCard(); // 짝 카드 자동 공개
// //                 break;
// //             }
// // //         }
// //     }
// //     else
// //     {
// //         clickedCard.OpenCard();
// //         // 일반 매칭 게임 로직 진행 (나중에 추가)
// //     }
// // }

// // }
