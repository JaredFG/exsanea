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
    private bool canMove;
    private GameObject selectedBook;

    void Start()
    {
        canMove = true;
        darkPlane = false;
        canExplore = false;
        xCoord = 0;
        yCoord = 0;
    }

    void Update()
    {
        if(canExplore && canMove)
        {
            if(Input.GetAxis("Horizontal") != 0)
            {
                xCoord += (Input.GetAxis("Horizontal") > 0 ? 1 : -1);
                xCoord = Mathf.Clamp(xCoord, 0, booksPanel.transform.GetChild(0).childCount - 1);
                StartCoroutine(ResetInput());
                GetBook();
            }

            if(Input.GetAxis("Vertical") != 0)
            {
                yCoord += (Input.GetAxis("Vertical") < 0 ? 1 : -1);
                yCoord = Mathf.Clamp(yCoord, 0, booksPanel.transform.childCount - 1);
                StartCoroutine(ResetInput());
                GetBook();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
                ExitBookPanel();
            
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                SelectBook(GetBook());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            booksPanel.SetActive(true);
            canExplore = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            contentText.text = "";
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
        // selectedBook.GetComponent<Animator>().SetBool("Selected", true);
        return book;
    }

    public void SelectBook(BookItem book)
    {
        if(book.isBad)
        {
            // booksPanel.GetComponent<Animator>().SetTrigger("Jumpscare");
            print("jumpscare");
            darkPlane = true;
        }

        contentText.text = book.text;
        if(!book.wasRead)
        {
            book.wasRead = true;
            if(!string.IsNullOrEmpty(book.trueText))
                book.text = book.trueText;
        }        
        
        if(book.isFinal)
            StartCoroutine(Final());
    }

    public void ExitBookPanel()
    {
        if(darkPlane)
        {
            // Change to dark plane
        }
        booksPanel.SetActive(false);
        canExplore = false;
        xCoord = 0;
        yCoord = 0;
    }

    private IEnumerator ResetInput()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

    private IEnumerator Final()
    {
        yield return new WaitForSeconds(0.6f);
        ExitBookPanel();
    }
}
