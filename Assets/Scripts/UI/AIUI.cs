using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIUI : MonoBehaviour
{
    [SerializeField] private GameObject aiText;

    private Animation anim;
    [Header("Show")]
    [SerializeField] AnimationClip showAnim;
    [Header("Hide")]
    [SerializeField] AnimationClip hideAnim;

    internal static bool canTalk;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        anim.clip = hideAnim;
        anim.Play();
        aiText.GetComponent<Text>().text = "";
        canTalk = true;
    }

    public void HidePanel()
    {
        anim.clip = hideAnim;
        anim.Play();
    }

    public void ShowPanel()
    {
        anim.clip = showAnim;
        anim.Play();
    }

    public void ShowText(string text)
    {
        if (canTalk)
        {
            StartCoroutine(Conversation(text));
        }
    }

    public void ShowText(Queue<string> texts)
    {
        if (canTalk)
        {
            StartCoroutine(Conversation(texts));
        }
    }

    IEnumerator Conversation(string text)
    {
        canTalk = false;
        ShowPanel();
        aiText.GetComponent<Text>().text = "";
        string _text = text;

        foreach (char item in _text)
        {
            aiText.GetComponent<Text>().text += item;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(7f);
        HidePanel();
        aiText.GetComponent<Text>().text = "";
        canTalk = true;
        StopCoroutine("Conversation");

    }

    IEnumerator Conversation(Queue<string> texts)
    {
        canTalk = false;
        aiText.GetComponent<Text>().text = "";
        ShowPanel();

        while (texts.Count > 0)
        {
            
            string _text = texts.Dequeue();
            
            foreach (char item in _text)
            {
                aiText.GetComponent<Text>().text += item;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(4f);
            aiText.GetComponent<Text>().text = "";
        }

        HidePanel();
        canTalk = true;
        StopCoroutine("Conversation");
    }
}
