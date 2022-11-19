using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public bool isTrigger = false;
    public GameObject actualPlane;
    public GameObject newPlane;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().isInteracting == true)
            {
                StartCoroutine(interaction());
            }
        }
    }
    IEnumerator interaction()
    {
        newPlane.SetActive(true);
        yield return new WaitForSeconds(2);
        actualPlane.SetActive(false);
    }

}
