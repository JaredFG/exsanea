using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MirrorController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private CinemachineVirtualCamera Origincam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entraste en mi");
            if (CameraSwitcher.ActiveCamera != cam) CameraSwitcher.SwitchCamera(cam);
        }
    }
}
