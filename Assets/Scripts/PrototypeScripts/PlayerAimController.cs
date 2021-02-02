using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public PlayerInput input;
    
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        input.OnZoom += SetCam;
    }

    private void SetCam()
    {
        mainCamera.SetActive(false);
        aimCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (input.OnZoom == 1f && !aimCamera.activeInHierarchy)
        //{
        //    mainCamera.SetActive(false);
        //    aimCamera.SetActive(true);

        //    //Allow time for the camera to blend before enabling the UI
        //    StartCoroutine(ShowReticle());
        //}
        //else if (input.aimValue != 1f && !mainCamera.activeInHierarchy)
        //{
        //    mainCamera.SetActive(true);
        //    aimCamera.SetActive(false);
        //    aimReticle.SetActive(false);
        //}

    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }
}
