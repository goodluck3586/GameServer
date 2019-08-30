using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //public GameObject model;

    public void Move(Vector3 tPos)
    {
        if (this.routine != null)
            StopCoroutine(routine);
        this.routine = StartCoroutine(this.MoveImpl(tPos));
    }

    private Coroutine routine;

    private IEnumerator MoveImpl(Vector3 tPos)
    {
        this.transform.LookAt(tPos);

        while (true)
        {
            var dis = Vector3.Distance(tPos, this.transform.position);
            var dir = (tPos - this.transform.position).normalized;  // Vector3.normalized
            print($"dis: {dis}, dir: {dir}");
            if (dis <= 0.2f)
            {
                break;
            }
            this.transform.position += 2 * Time.deltaTime * dir;
            yield return null;
        }
        Debug.LogFormat("move complete!");
    }
}
