using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class test : MonoBehaviour
{
    public bool isTrigger = false;
    public GameObject actualPlane;
    public GameObject newPlane;

    public GameObject prefab;
    public Transform spawnPoint;
    public GameObject gameObjectPlayer;
    [SerializeField] private CinemachineVirtualCamera Nextcam;

    private void Start()
    {
        gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().isInteracting == true)
            {
                changeView();
            }
        }
    }
    public void changeView()
    {
        //playeble director
        if (CameraSwitcher.ActiveCamera != Nextcam) CameraSwitcher.SwitchCamera(Nextcam);
        Destroy(gameObjectPlayer);
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        actualPlane.SetActive(false);
        newPlane.SetActive(true);


    }
}
