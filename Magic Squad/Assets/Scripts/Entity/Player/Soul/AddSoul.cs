using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSoul : MonoBehaviour
{
    private SoulManager _soulManager;
    // Start is called before the first frame update
    void Start()
    {
        // This is how you get the script so you can reference items in the soul manager
        _soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _soulManager.AddSoul(10);
            Destroy(gameObject);
        }
    }
}
