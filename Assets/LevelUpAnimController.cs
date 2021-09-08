using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpAnimController : MonoBehaviour
{
    public GameObject FloatinLevelUpText, Particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartLevelUpAnim()
    {
        FloatinLevelUpText.SetActive(true);
        Particles.SetActive(true);
        Invoke("DisabledText", 3.2f);
    }

    void DisabledText()
    {
        FloatinLevelUpText.SetActive(false);
    }
}
