using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptBorder : MonoBehaviour
{
    public int Id;
    private void OnMouseDown()
    {
        Buffer buffer = GameObject.FindGameObjectWithTag("Buffer").GetComponent<Buffer>();
        buffer.Request(Id, this.transform);
    }
    private bool IsDown = false;
    public void ClickFalse()
    {
        if (!IsDown)
        {
            Transform ChildTransform = transform.Find("ViewImg");
            ChildTransform.DOShakePosition(1, 2, fadeOut: false);

            Invoke("SetReload", 1);
            IsDown = true;
        }

    }
    private void SetReload()
    {
        IsDown = false;
    }
}

