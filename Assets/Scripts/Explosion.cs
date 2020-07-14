using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start() // Start is called before the first frame update
    {
        Destroy(this.gameObject, 3.0f);
    }
}
