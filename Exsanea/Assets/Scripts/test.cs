using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Fui tocado control");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("fui tocado colission ");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("fui tocado trigger");
    }

}
