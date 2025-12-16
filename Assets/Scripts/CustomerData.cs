using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tatooo/Customer")]
public class CustomerData : ScriptableObject
{
    public string customerName;
    public Sprite portrait;
    public float difficultyMultiplier = 1f;
    public float tipMultiplier = 1f;
}
