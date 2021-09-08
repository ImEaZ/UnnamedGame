using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class stopTransparent : MonoBehaviour
{
    public GameObject transparenterObj;
    public CapsuleCollider2D capCollider;
    public void stopTransparentFunction()
    {
        var temp = transparenterObj.GetComponent<transparenter>();
        temp.ReleaseGameObject();
        temp.turnOffCollider();
        //var gobj = gameObject.GetComponents<CapsuleCollider2D>().ToList();
        //if (gobj.Count > 0)
        //{
        //    gobj.Where(o => o.direction == CapsuleDirection2D.Vertical).FirstOrDefault().enabled = true;
        //}
    }
}
