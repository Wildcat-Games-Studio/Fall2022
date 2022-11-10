using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public  Cards[] deck = new Cards[25];
    private Cards currentCard;

    public GameObject handPanel;
    private Cards[] hand = new Cards[5];
    private int cardI;

     // Start is called before the first frame update
    void Start()
    {
        shuffle(deck, deck.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = deck[cardI];
                cardI++;
                break;
            }
        }

        if (cardI >= deck.Length)
        {
            shuffle(deck, deck.Length);
        }
    }

    private void DrawHand()
    {
       for(int i = 0; i < hand.Length; i ++)
        {
            hand[i] = deck[cardI];
            cardI++;
        }
    }

    public void shuffle(Cards[] card, int n)
    {
        int rand;

        cardI = 0;

        for (int i = 0; i < n; i++)
        {
            rand = Random.Range(i, n);

            //swapping the elements
            Cards temp = card[rand];
            card[rand] = card[i];
            card[i] = temp;

        }
    }
}
