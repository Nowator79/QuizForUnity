using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartBtn : MonoBehaviour
{
    [SerializeField]
    private Buffer buffer;

    public void StartFunction()
    {
        buffer.InitLevel();
        gameObject.SetActive(false);
    }
}