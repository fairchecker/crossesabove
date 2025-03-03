using Ink.Parsed;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    List<GameObject> bt_obj = new List<GameObject>();
    private TextScript textScript;
    private Dialog dialog = new Dialog();

    private void Awake()
    {
        textScript = GetComponent<TextScript>();
    }

    private void Start()
    {
        StartDialog();
    }

    public void StartDialog(string dialog_file = "test")
    {
        dialog.LoadStory("Dialogs/" + dialog_file);
        string text = dialog.ContinueStory();
        switch (text)
        {
            case "CHOISE":
                Choise();
                break;
            default:
                textScript.SayText(text);
                break;
        }
    }
    public void NextText()
    {
        string text = dialog.ContinueStory();
        switch (text)
        {
            case "CHOISE":
                Choise();
                break;
            default:
                textScript.SayText(text);
                break;
        }
    }

    private void Choise()
    {
        List<Ink.Runtime.Choice> options = dialog.GetChoices();
        options.Reverse();
        for (int i = 0; i < options.Count; i++)
        {
            GameObject tmp = new GameObject();
            tmp.transform.SetParent(transform);
            tmp.AddComponent<RectTransform>();
            tmp.transform.localPosition = new Vector3(800, -112.5f + i * 75, 0);
            tmp.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);
            tmp.AddComponent<Image>(); 
            tmp.AddComponent<Button>();
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("unity_builtin_extra");
            GameObject _text = new GameObject();
            _text.transform.SetParent(tmp.transform);
            _text.AddComponent<TextMeshProUGUI>();
            _text.GetComponent<TextMeshProUGUI>().text = options[i].text;
            _text.transform.localPosition = Vector3.zero;
            _text.GetComponent<TextMeshProUGUI>().color = Color.black; 
            _text.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
            bt_obj.Add(tmp);
        }
    }
}
