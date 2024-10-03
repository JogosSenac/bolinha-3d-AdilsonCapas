using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboBrilhante : MonoBehaviour
{
    [SerializeField] private float rX;
    [SerializeField] private float rY;
    [SerializeField] private float rZ;
    [SerializeField] private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (rX * velocity * Time.deltaTime,
            rY * velocity * Time.deltaTime,
            rZ * velocity * Time.deltaTime);
    }

}