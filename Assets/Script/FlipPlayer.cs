using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    public void Filp(bool isFilp)
    {
        transform.localScale = new Vector3(isFilp ? 1 : -1, 1, 1);
    }

}
