using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SentenceGame : MonoBehaviour
{
    public TMP_Text sentenceText;
    public Button[] optionButtons;
    public SentenceDatabase sentenceDatabase;

    private List<string> sentences;
    private string currentSentence;
    private string missingWord;
    private int correctButtonIndex;

    void Start()
    {
        sentences = sentenceDatabase.sentences;

        SetNextSentence();
    }

    void SetNextSentence()
    {
        int randomIndex = Random.Range(0, sentences.Count);
        currentSentence = sentences[randomIndex];

        string[] words = currentSentence.Split(' ');
        int missingWordIndex = Random.Range(0, words.Length);

        missingWord = words[missingWordIndex];
        words[missingWordIndex] = "_________";

        sentenceText.text = string.Join(" ", words);

        correctButtonIndex = Random.Range(0, optionButtons.Length);
        optionButtons[correctButtonIndex].GetComponentInChildren<TMP_Text>().text = missingWord;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i != correctButtonIndex)
            {
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = GetRandomWrongWord();
            }

            optionButtons[i].onClick.RemoveAllListeners();
            int buttonIndex = i; // To capture the correct button index in the lambda expression
            optionButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex));
        }
    }

    void CheckAnswer(int buttonIndex)
    {
        if (buttonIndex == correctButtonIndex)
        {
            Debug.Log("Doðru cevap!");
            SetNextSentence();
        }
        else
        {
            Debug.Log("Yanlýþ cevap!");
            StartCoroutine(ContinueGame());
        }
    }

    IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(1f);
        SetNextSentence();
    }

    string GetRandomWrongWord()
    {
        List<string> wrongWords = new List<string>();

        foreach (string sentence in sentences)
        {
            string[] words = sentence.Split(' ');

            foreach (string word in words)
            {
                if (word != missingWord && !wrongWords.Contains(word))
                {
                    wrongWords.Add(word);
                }
            }
        }

        int randomIndex = Random.Range(0, wrongWords.Count);
        return wrongWords[randomIndex];
    }
}
