using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMace : MonoBehaviour
{
    // gm stands for Game Manager
    private GameManager gm;

    public Transform up;
    public Transform down;

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
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        upPos = up.position;
        downPos = down.position;
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
            transform.position = Vector2.Lerp(upPos, downPos, interpolate);
            yield return null;  
        }
        transform.position = downPos;
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
            transform.position = Vector2.Lerp(downPos, upPos, interpolate);
            yield return null;
        }
        transform.position = upPos;
        StartCoroutine(WaitBeforeDrop(0));
    }

    IEnumerator WaitBeforeDrop(float extra)
    {
        yield return new WaitForSeconds(riseToDropWaitDuration + extra);
        StartCoroutine(Drop());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.Lose();
    }
}
