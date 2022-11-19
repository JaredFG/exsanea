using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItem : MonoBehaviour
{
    [TextArea] public string text;
    [TextArea] public string trueText;
    public bool isBad = true;
    public bool isFinal = false;
    [System.NonSerialized] public bool wasRead;
    // [System.NonSerialized] public Vector2 coords;
    void Start()
    {
        wasRead = false;
        // coords = new Vector2(transform.GetSiblingIndex(), transform.parent.GetSiblingIndex());
    }

    // public void Select()
    // {
    //     FindObjectOfType<Books>().SelectBook(this);
    // }
}
