using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    TextMeshProUGUI textDiaplay;
    int index;
    float typingSpeed;
    bool allowToContinue = false, isTyping = false;

    public GameObject DialogObjectEn, DialogObjectTh;
    public string[] sentences;
    public float delayTypingSpeed;
    public float noDelayTyping;
    
    void Start()
    {
        if (FindObjectOfType<GameManager>().Language == "th")
        {
            DialogObjectEn.SetActive(false);
            DialogObjectTh.SetActive(true);
            textDiaplay = DialogObjectTh.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            DialogObjectTh.SetActive(false);
            DialogObjectEn.SetActive(true);
            textDiaplay = DialogObjectEn.GetComponent<TextMeshProUGUI>();
        }
        typingSpeed = delayTypingSpeed;
    }

    void Update()
    {
        if (textDiaplay.text == sentences[index])
        {
            isTyping = false;
            allowToContinue = true;
            typingSpeed = delayTypingSpeed;
        }
    }
    public void SetDialogLanguage()
    {
        if (FindObjectOfType<GameManager>().Language == "th")
        {
            DialogObjectEn.SetActive(false);
            DialogObjectTh.SetActive(true);
            textDiaplay = DialogObjectTh.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            DialogObjectTh.SetActive(false);
            DialogObjectEn.SetActive(true);
            textDiaplay = DialogObjectEn.GetComponent<TextMeshProUGUI>();
        }
    }
    IEnumerator Type()
    {
        isTyping = true;
        allowToContinue = false;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDiaplay.text += letter;
            if (typingSpeed != noDelayTyping)
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
    public void StartType()
    {
        index = 0;
        StartCoroutine(Type());
    }
    public void UpSpeedText()
    {
        typingSpeed = noDelayTyping;
    }
    public void NextSentence()
    {
        if (index < sentences.Length - 1 && allowToContinue)
        {
            index++;
            textDiaplay.text = "";
            StartCoroutine(Type());
        }
        else if (isTyping)
        {
            UpSpeedText();
        }
        else
        {
            textDiaplay.text = "";
            FindObjectOfType<GameManager>().CloseDialogCanvas();
        }
    }

}
