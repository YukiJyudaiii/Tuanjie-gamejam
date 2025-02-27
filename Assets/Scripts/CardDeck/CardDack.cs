using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDeck;
public class CardDack : MonoBehaviour
{
    
    //抽牌堆
    [SerializeField] public List<CardString> cardsPoint=new List<CardString>();
     //弃牌堆
     [SerializeField] public List<CardString> discardDeck=new List<CardString>();
    // Start is called before the first frame update
    void Start()
    {
        InitializeCardPoints();
    }
    
    
    //牌堆初始化
    void InitializeCardPoints()
    {
        List<String> point=new List<String>{"A","2","3","4","5","6","7","8","9","10","J","Q","K"};
        foreach (var suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (var str in point)
            {
                cardsPoint.Add(new CardString(str, (CardSuit)suit));
            }
        }
        //洗牌
        ShuffleCards(cardsPoint);
    }
    
    public void ShuffleCards(List<CardString> cardsPoint)
    {
        for (int i = cardsPoint.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            // Swap cardsPoint[i] with the element at randomIndex
            CardString temp = cardsPoint[i];
            cardsPoint[i] = cardsPoint[randomIndex];
            cardsPoint[randomIndex] = temp;
        }
    }
    
        public CardString DrawCard()
    {
        if (cardsPoint.Count == 0)
        {
            Debug.LogWarning("No cards left in the deck.");
            return new CardString("0", CardSuit.黑桃);
        }

        // 随机抽取一张牌
        int randomIndex = 0;
        CardString drawnCard = cardsPoint[randomIndex];

        // 从列表中移除这张牌
        cardsPoint.RemoveAt(randomIndex);

        return drawnCard;
    }
        
        //将弃牌堆的卡放入到抽牌堆
        public void RecoverDiscard()
    {
        cardsPoint.AddRange(discardDeck);
        discardDeck.Clear();
    }
}



