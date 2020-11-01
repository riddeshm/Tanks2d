using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject turrent, projectileSpawn;

    float speed = 5f;
    float rotationSpeed = 20f;
    float turrentRotationLimit = 45f;
    float turrentRotLimitQtn;


    private void Awake()
    {
        turrentRotLimitQtn = Quaternion.Euler(0f, 0f, turrentRotationLimit + 0.1f).z;
    }

    public void MoveTank(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void MoveTurrent(Vector3 direction)
    {
        float rotZ = turrent.transform.rotation.z;
        if (rotZ < turrentRotLimitQtn && rotZ > -turrentRotLimitQtn)
        {
            turrent.transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (rotZ > 0)
                turrent.transform.eulerAngles = new Vector3(0f, 0f, turrentRotationLimit);
            else
                turrent.transform.eulerAngles = new Vector3(0f, 0f, -turrentRotationLimit);
        }
    }

    public Vector3 FireDirection()
    {
        return projectileSpawn.transform.position - turrent.transform.position;
    }
}
