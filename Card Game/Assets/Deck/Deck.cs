using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private int deckLimit = 25;
    public  Cards[] deck = new Cards[25];
    private Cards currentCard;

    public GameObject handPanel;
    private Cards[] hand = new Cards[5];
    private int prev;
     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        int random = Random.Range(0, deck.Length);


    }

    private void DrawHand()
    {
       for(int i = 0; i < hand.Length; i ++)
        {
            int random = Random.Range(0, deck.Length);

            while(prev == random)
            {
                random = Random.Range(0, deck.Length);
            }
            prev = random;
            hand[i] = deck[random];
        }
    }
}
