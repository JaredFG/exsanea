using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Text narratorText;
    public bool canLoop;
    [TextArea] public string[] texts;
    private int text_index;

    void Start()
    {
        text_index = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !string.IsNullOrEmpty(narratorText.text))
        {
            text_index++;
            if(text_index >= texts.Length)
                text_index = (canLoop ? 0 : texts.Length - 1);

            narratorText.text = texts[text_index] + "\n►";
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            narratorText.text = texts[text_index] + "\n►";
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            narratorText.text = "";
    }
}
