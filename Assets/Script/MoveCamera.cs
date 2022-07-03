using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    public GameObject cam;
    public GameObject target0;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cam.transform.DOMove(new Vector3(target0.transform.position.x, target0.transform.position.y, target0.transform.position.z) , 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cam.transform.DOMove(new Vector3(target1.transform.position.x, target1.transform.position.y, target1.transform.position.z) , 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam.transform.DOMove(new Vector3(target2.transform.position.x, target2.transform.position.y, target2.transform.position.z) , 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cam.transform.DOMove(new Vector3(target3.transform.position.x, target3.transform.position.y, target3.transform.position.z) , 1);
        }
    }
}
