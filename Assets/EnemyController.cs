using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float boundary = 2f;
    public float movementSpeed = 2f;
    float x = 1f;
    // Start is called before the first frame update
    void Start()
    {
        x *= movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(x * Time.deltaTime, 0f, 0f));
        if (Mathf.Abs(transform.position.x) > boundary)
            x *= -1f;
    }
}
