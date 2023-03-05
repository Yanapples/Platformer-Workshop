using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource die;
    [SerializeField] private AudioSource collect;

    public void Jump()
    {
        jump.Play();
    }

    public void Die()
    {
        die.Play();
    }

    public void Collect()
    {
        collect.Play();
    }
}
