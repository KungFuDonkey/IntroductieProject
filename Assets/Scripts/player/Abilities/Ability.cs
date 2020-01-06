using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected float damage;
    protected string type;
    protected string statusEffect = "none";
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (true)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
