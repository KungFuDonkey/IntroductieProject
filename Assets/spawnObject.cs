using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject CandyItem;
    public GameObject CandyItemTarget;
    public GameObject CandyItem2;
    public GameObject CandyItemTarget2;
    public GameObject LumBerryItem;
    public GameObject LumBerryItemTarget;
    public GameObject OranBerryItem;
    public GameObject OranBerryItemTarget;
    public GameObject PechaBerryItem;
    public GameObject PechaBerryItemTarget;
    public GameObject PokeballItem;
    public GameObject PokeballItemTarget;
    public GameObject BandanaItem;
    public GameObject BandanaItemTarget;
    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
            SpawnItem();
    }

    public void SpawnItem()
    {
        Vector3 pos = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300)) ;
        GameObject clone;
        clone = Instantiate(CandyItem, pos, Quaternion.identity);
        CandyItemTarget = clone;

        RaycastHit hit;

        if (Physics.Raycast(CandyItemTarget.transform.position, - Vector3.up, out hit))
        {
            CandyItemTarget.transform.position = new Vector3(CandyItemTarget.transform.position.x, hit.point.y + 5, CandyItemTarget.transform.position.z);
        }

        Vector3 pos2 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone2;
        clone2 = Instantiate(CandyItem2, pos2, Quaternion.identity);
        CandyItemTarget2 = clone2;

        RaycastHit hit2;

        if (Physics.Raycast(CandyItemTarget2.transform.position, -Vector3.up, out hit2))
        {
            CandyItemTarget2.transform.position = new Vector3(CandyItemTarget2.transform.position.x, hit2.point.y + 5, CandyItemTarget2.transform.position.z);
        }

        Vector3 pos3 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone3;
        clone3 = Instantiate(LumBerryItem, pos3, Quaternion.identity);
        LumBerryItemTarget = clone3;

        RaycastHit hit3;

        if (Physics.Raycast(LumBerryItemTarget.transform.position, -Vector3.up, out hit3))
        {
            LumBerryItemTarget.transform.position = new Vector3(LumBerryItemTarget.transform.position.x, hit3.point.y + 5, LumBerryItemTarget.transform.position.z);
        }

        Vector3 pos4 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone4;
        clone4 = Instantiate(PokeballItem, pos4, Quaternion.identity);
        PokeballItemTarget = clone4;

        RaycastHit hit4;

        if (Physics.Raycast(PokeballItemTarget.transform.position, -Vector3.up, out hit4))
        {
            PokeballItemTarget.transform.position = new Vector3(PokeballItemTarget.transform.position.x, hit4.point.y + 5, PokeballItemTarget.transform.position.z);
        }

        Vector3 pos5 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone5;
        clone5 = Instantiate(BandanaItem, pos5, Quaternion.identity);
        BandanaItemTarget = clone5;

        RaycastHit hit5;

        if (Physics.Raycast(BandanaItemTarget.transform.position, -Vector3.up, out hit5))
        {
            BandanaItemTarget.transform.position = new Vector3(BandanaItemTarget.transform.position.x, hit5.point.y + 5, BandanaItemTarget.transform.position.z);
        }

        Vector3 pos6 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone6;
        clone6 = Instantiate(OranBerryItem, pos6, Quaternion.identity);
        OranBerryItemTarget = clone6;

        RaycastHit hit6;

        if (Physics.Raycast(OranBerryItemTarget.transform.position, -Vector3.up, out hit6))
        {
            OranBerryItemTarget.transform.position = new Vector3(OranBerryItemTarget.transform.position.x, hit6.point.y + 5, OranBerryItemTarget.transform.position.z);
        }

        Vector3 pos7 = new Vector3(this.transform.position.x + Random.Range(-300, 300), 1, this.transform.position.z + Random.Range(-300, 300));
        GameObject clone7;
        clone7 = Instantiate(PechaBerryItem, pos7, Quaternion.identity);
        PechaBerryItemTarget = clone7;

        RaycastHit hit7;

        if (Physics.Raycast(PechaBerryItemTarget.transform.position, -Vector3.up, out hit7))
        {
            PechaBerryItemTarget.transform.position = new Vector3(PechaBerryItemTarget.transform.position.x, hit7.point.y + 5, PechaBerryItemTarget.transform.position.z);
        }
    }
    

 
}
