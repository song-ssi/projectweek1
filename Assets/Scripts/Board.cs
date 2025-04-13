
using Unity.Collections;
using UnityEngine;
using System.Linq;
using UnityEditor.PackageManager;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    int level = GameManager.Instance.level;

    // Start is called before the first frame update
    void Start()
    {
        CreateCard();
    }

    

    // Update is called once per frame

    private void CreateCard()
    {   
        int[] arr = {0, 0 , 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        arr = arr.OrderBy(x => Random.Range(0.0f, 7.0f)).ToArray();

        for (int i=0; i<16; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform);
            card.name = $"Card_{i:00}";
            float x = (i % 4) * 1.1f - 1.65f;
            float y = (i / 4) * 1.1f - 2.44f;

            card.transform.position = new Vector2(x, y);
            card.GetComponent<Card>().Setting(arr[i]);
            


        }
        GameManager.Instance.cardCount = arr.Length;
    }

}
