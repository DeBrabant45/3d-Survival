using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public float dmg = 10;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            var target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                //target.TakeDamage(dmg);
            }
        }

    }
}
