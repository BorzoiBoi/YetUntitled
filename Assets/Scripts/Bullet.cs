using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDirection;
    private float existingTime;

    public void Setup(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
    }

    // Update is called once per frame
    private void Update()
    {
        //direction * speed * deltaTime
        transform.position += shootDirection * 50f * Time.deltaTime;
        existingTime += Time.deltaTime;
        if(existingTime > 5f)
        {
            Destroy(gameObject);
        }
    }
}
