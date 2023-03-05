using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMaceController : MonoBehaviour
{
    [SerializeField] private Transform mace;
    [SerializeField] private float up;
    [SerializeField] private float down;
    private Vector2 upPos;
    private Vector2 downPos;

    public float initialWaitTime = 0;
    public float fallDuration = 1;
    public float dropToRiseWaitDuration = 1;
    public float riseDuration = 1;
    public float riseToDropWaitDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        upPos += new Vector2(transform.position.x, transform.position.y + up);
        downPos += new Vector2(transform.position.x, transform.position.y + down);
        StartCoroutine(WaitBeforeDrop(initialWaitTime));
    }

    IEnumerator Drop()
    {
        // Interpolation!!
        float interpolate;
        for (float currentTime = 0; currentTime < fallDuration; currentTime += Time.deltaTime)
        {
            interpolate = currentTime / fallDuration;
            interpolate = interpolate * interpolate * interpolate;
            mace.position = Vector2.Lerp(upPos, downPos, interpolate);
            yield return null;  
        }
        mace.position = downPos;
        StartCoroutine(WaitBeforeRise());
    }

    IEnumerator WaitBeforeRise()
    {
        yield return new WaitForSeconds(dropToRiseWaitDuration);
        StartCoroutine(Rise());
    }

    IEnumerator Rise()
    {
        // Interpolation!!
        float interpolate;
        for (float currentTime = 0; currentTime < riseDuration; currentTime += Time.deltaTime)
        {
            interpolate = currentTime / fallDuration;
            mace.position = Vector2.Lerp(downPos, upPos, interpolate);
            yield return null;
        }
        mace.position = upPos;
        StartCoroutine(WaitBeforeDrop(0));
    }

    IEnumerator WaitBeforeDrop(float extra)
    {
        yield return new WaitForSeconds(riseToDropWaitDuration + extra);
        StartCoroutine(Drop());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + up, transform.position.z), Vector3.one);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + down, transform.position.z), Vector3.one);
    }
}
