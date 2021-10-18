using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairs : MonoBehaviour
{
    [SerializeField] public Transform target1;
    [SerializeField] public Transform camera1;
    [SerializeField] public Transform target2;
    [SerializeField] public Transform camera2;
    [SerializeField] public Collider2D notTrigger;

    private bool isTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Not anymore");
        isTrigger = false;
    }
    void Update()
    {
        if (isTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                target1.transform.position = new Vector3(target1.position.x+5, transform.position.y+5, target1.position.z);
                camera1.transform.position = new Vector3(camera1.position.x+5, transform.position.y+5, camera1.position.z);
                isTrigger = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                target2.transform.position = new Vector3(target2.position.x+5, transform.position.y+5, target2.position.z);
                camera2.transform.position = new Vector3(camera2.position.x+5, transform.position.y+5, camera2.position.z);
                isTrigger = false;
            }
        }
    }
}
