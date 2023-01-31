using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMace : MonoBehaviour
{
    public Transform up;
    public Transform down;

    Vector2 upPos;
    Vector2 downPos;

    float dummyDuration = 0;
    float fallDuration = 1;
    float waitDuration = 1;
    float riseDuration = 1;
    // Start is called before the first frame update
    void Start()
    {
        upPos = up.position;
        downPos = down.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
            StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        float interpolate;
        for (float currentTime = 0; currentTime < fallDuration; currentTime += Time.deltaTime)
        {
            interpolate = currentTime / fallDuration;
            interpolate = interpolate * interpolate * interpolate;
            transform.position = Vector2.Lerp(upPos, downPos, interpolate);
            yield return null;
        }
        transform.position = downPos;
        yield return new WaitForSeconds(waitDuration);
        for (float currentTime = 0; currentTime < riseDuration; currentTime += Time.deltaTime)
        {
            interpolate = currentTime / fallDuration;
            transform.position = Vector2.Lerp(downPos, upPos, interpolate);
            yield return null;
        }
        transform.position = upPos;
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
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitDuration);
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
    }
}
