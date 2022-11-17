using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Card File",menuName ="Card Files Archive")]
public class Cards : ScriptableObject
{
    public string cardName;
    public Sprite[] sprite_frames;
    [Range(0.1f, 100.0f)]
    public float frame_rate;
    public int cost;
    public int damage;
    public int attackSpeed;
    public int radius;

}
