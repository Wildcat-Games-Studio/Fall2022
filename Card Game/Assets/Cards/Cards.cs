using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Card File",menuName ="Card Files Archive")]
public class Cards : ScriptableObject
{
    public string cardName;
    public int cost;
    public int damage;
    public int attackSpeed;
    public int radius;

}
