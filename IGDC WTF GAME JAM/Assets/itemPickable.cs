using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickable : MonoBehaviour
{
    public itemSo itemScriptable;

    public void pickItem()
    {
        Destroy(gameObject);
    }
}
