using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        test.Jump = true;
    }
}
