using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPlay : MonoBehaviour
{
    [SerializeField] private List<GameObject> toDisableList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameobject in toDisableList)
        {
            gameobject.SetActive(false);
        }
    }
}
