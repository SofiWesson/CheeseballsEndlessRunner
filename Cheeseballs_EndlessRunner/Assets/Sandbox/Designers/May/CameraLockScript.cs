using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockScript : MonoBehaviour
{
    public GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerCamera.transform.position = new Vector3(0, playerCamera.transform.position.y, playerCamera.transform.position.z);
    }
}
