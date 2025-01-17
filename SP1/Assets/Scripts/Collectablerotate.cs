using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablerotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;

    
    void Update()
    {
      transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
