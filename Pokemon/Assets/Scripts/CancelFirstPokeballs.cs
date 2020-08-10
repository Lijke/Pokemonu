using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelFirstPokeballs : MonoBehaviour
{
    public GameObject Ui;
    public GameObject player;
    public void Start()
    {
        player = GameObject.Find("Player");
    }
    public void CancelUi()
    {
        Ui.SetActive(false);
    }
}
