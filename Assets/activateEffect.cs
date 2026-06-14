using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum effect {effect1, effect2, effect3, effect4, effect5, effect6, effect7, effect8, effect9, effect10, effect11};
public enum pannel {pannel1, pannel2, pannel3, pannel4, pannel5, pannel6, pannel7, pannel8, pannel9, pannel10, pannel11};
public class activateEffect : MonoBehaviour
{
    public effect effectToActivate;
    public pannel pannelToActivate;
    public GameObject effectManager;
    public GameObject pannelManager;

    // Start is called before the first frame update
    void DeactivateAll()
    {
        for(int i = 0; i < effectManager.transform.childCount; i++)
        {
            effectManager.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0; i < pannelManager.transform.childCount; i++)
        {
            pannelManager.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    void Start()
    {
        DeactivateAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DeactivateAll();
            other.gameObject.GetComponent<PlayerController>().activeEffect = effectToActivate;
            other.gameObject.GetComponent<PlayerController>().activePannel = pannelToActivate;
        }
    }
}
