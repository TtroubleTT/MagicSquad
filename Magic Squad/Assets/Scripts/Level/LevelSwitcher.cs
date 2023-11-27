using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSwitcher : MonoBehaviour
{

    [SerializeField] public string levelName;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider PlayerObject)
    {
        //if (other.CompareTag("PlayerObject")) {
            //SceneManager.LoadScene(Scene 2);
        //}
    }

    // Update is called once per frame
}
