using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulObject : MonoBehaviour
{
    private SoulManager _soulManager;

    [SerializeField] 
    private float soulAmount;
    
    // Start is called before the first frame update
    private void Start()
    {
        // This is how you get the script so you can reference items in the soul manager
        _soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool addedSoul = _soulManager.AddSoul(soulAmount);
            
            if (addedSoul)
                Destroy(gameObject);
        }
    }
    
    public void InitializeSoulAmount(float amount)
    {
        soulAmount = amount;
    }
}
