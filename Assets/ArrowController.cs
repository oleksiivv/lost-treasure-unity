using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(clean),5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward/1.3f);
    }

    void clean(){
        Destroy(gameObject);
    }
}
