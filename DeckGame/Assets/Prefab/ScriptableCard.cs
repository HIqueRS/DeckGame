using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card",menuName = "Card")]
public class ScriptableCard : ScriptableObject
{
    public string Name;
    public int Energy;
    public string Type;//pode ser um enum viu
    public string Descryption;
    public Sprite Image;

    public int Damage;
    public int Shield;
    public int Poison;
    public int Strength; 
}
