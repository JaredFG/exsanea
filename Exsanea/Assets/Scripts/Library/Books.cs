using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Books : MonoBehaviour
{
    [Tooltip("Text where the book content will be shown")] public Text contentText;
    [Tooltip("Reference to the BooksPanel GameObject")] public GameObject booksPanel;
    public static bool darkPlane;   // Player is on the dark plane?
    private bool canExplore;        // Player can look through books with the keys?
    private int xCoord;             // X coordinate of the current book selected
    private int yCoord;             // Y coordinate of the current book selected
    private GameObject selectedBook;

    void Start()
    {
        darkPlane = false;
        canExplore = false;
        xCoord = 0;
        yCoord = 0;
    }

    void Update()
    {
        if(canExplore)
        {
            if(Input.GetAxis("Horizontal") != 0)
            {
                xCoord += (Input.GetAxis("Horizontal") > 0 ? 1 : -1);
                GetBook();
            }

            if(Input.GetAxis("Vertical") != 0)
            {
                yCoord += (Input.GetAxis("Vertical") > 0 ? 1 : -1);
                GetBook();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
                ExitBookPanel();
            
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                SelectBook(GetBook());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            booksPanel.SetActive(true);
            canExplore = true;
        }
    }

    private BookItem GetBook()
    {
        // Returns the book from the booksPanel given x and y coordinates
        BookItem book = booksPanel.transform.GetChild(yCoord).GetChild(xCoord).GetComponent<BookItem>();
        if(selectedBook != null)
            selectedBook.GetComponent<Image>().color = Color.white;
            // selectedBook.GetComponent<Animator>().SetBool("Selected", false);
        
        selectedBook = book.gameObject;
        selectedBook.GetComponent<Image>().color = Color.blue;
        selectedBook.GetComponent<Animator>().SetBool("Selected", true);
        return book;
    }

    public void SelectBook(BookItem book)
    {
        if(book.isBad)
        {
            booksPanel.GetComponent<Animator>().SetTrigger("Jumpscare");
            darkPlane = true;
        }

        if(!book.wasRead)
        {
            book.wasRead = true;
            if(!string.IsNullOrEmpty(book.trueText))
                book.text = book.trueText;
        }
        
        contentText.text = book.text;
        
        if(book.isFinal)
            ExitBookPanel();
    }

    public void ExitBookPanel()
    {
        if(darkPlane)
        {
            // Change to dark plane
        }
        else
        {
            booksPanel.SetActive(false);
            canExplore = false;
        }
    }
}
