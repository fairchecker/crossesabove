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
            var text = options[i].text;
            float position = -112.5f + i * 75;
            CreateChoiceButton(text, position);
        }
    }

    private GameObject CreateChoiceButton(string text, float position)
    {
        var button = new GameObject();
        button.transform.SetParent(transform);
        button.AddComponent<RectTransform>();
        button.transform.localPosition = new Vector3(800, position, 0);
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);
        button.AddComponent<Image>(); 
        button.AddComponent<Button>();
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>("");
        var textObject = new GameObject();
        textObject.transform.SetParent(button.transform);
        textObject.AddComponent<TextMeshProUGUI>();
        textObject.GetComponent<TextMeshProUGUI>().text = text;
        textObject.transform.localPosition = Vector3.zero;
        textObject.GetComponent<TextMeshProUGUI>().color = Color.black; 
        textObject.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        bt_obj.Add(button);
        return button;
    }
}
