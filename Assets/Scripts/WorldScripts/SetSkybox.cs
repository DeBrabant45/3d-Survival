using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkybox : MonoBehaviour
{
    [SerializeField] private TimeCycle _timeCycle;
    [SerializeField] private Material _day;
    [SerializeField] private Material _night;

    [SerializeField] private GameObject sunDirection;
    [SerializeField] private GameObject moonDirection;

    private Material skyboxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial = new Material(_night);
    }

    // Update is called once per frame
    void Update()
    {
        skyboxMaterial.Lerp(_day, _night, _timeCycle.TimeOfDay);
        RenderSettings.skybox = skyboxMaterial;
        //DynamicGI.UpdateEnvironment();
        if (sunDirection != null)
        {
            Shader.SetGlobalVector("GlobalSunDirection", -sunDirection.transform.forward);
        }
        else
        {
            Shader.SetGlobalVector("GlobalSunDirection", Vector3.zero);
        }

        if (moonDirection != null)
        {
            Shader.SetGlobalVector("GlobalMoonDirection", -moonDirection.transform.forward);
        }
        else
        {
            Shader.SetGlobalVector("GlobalMoonDirection", Vector3.zero);
        }
    }
}
