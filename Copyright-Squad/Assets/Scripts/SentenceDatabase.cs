using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SentenceDatabase", menuName = "GameSentence Database")]
public class SentenceDatabase : ScriptableObject
{
    public List<string> sentences;
}
