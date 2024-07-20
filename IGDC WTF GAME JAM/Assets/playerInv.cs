using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class playerInv : MonoBehaviour
{
    [Header("general")]
    public List<itemType> invlist;
    public int selecteditem;
    public float playerReach;
    [SerializeField] GameObject throwItemGO;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("item gameobjects")]
    [SerializeField] GameObject Sworditem;
    [SerializeField] GameObject Axeitem;

    [SerializeField] Camera cam;

    [Space(20)]
    [Header("item prefabs")]
    [SerializeField] GameObject sword_prefab;
    [SerializeField] GameObject axe_prefab;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };

    // Start is called before the first frame update
    void Start()
    {
        selecteditem = 0;
        itemSetActive.Add(itemType.Sword, Sworditem);
        itemSetActive.Add(itemType.Axe, Axeitem);

        itemInstantiate.Add(itemType.Sword, sword_prefab);
        itemInstantiate.Add(itemType.Axe, axe_prefab);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, playerReach) && Input.GetKey(pickItemKey))
        {
            Ipickable item = hitInfo.collider.GetComponent<Ipickable>();
            if (item != null) 
            {
                invlist.Add(hitInfo.collider.GetComponent<itemPickable>().itemScriptable.item_type);
                item.PickItem(); //just destroy from item pick script
            }
        }
        if (Input.GetKeyDown(throwItemKey) && invlist.Count > 1)
        {
            Instantiate(itemInstantiate[invlist[selecteditem]], position: throwItemGO.transform.position, new Quaternion());
            if (selecteditem != 0)
            {
                selecteditem -= 1;
            }

        }
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && invlist.Count > 0)
        {
            selecteditem = 0;
            NewItemSelected();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && invlist.Count > 0)
        {
            selecteditem = 1;
            NewItemSelected();
        }
    }
    public void NewItemSelected()
    {
        Axeitem.SetActive(false);
        Sworditem.SetActive(false);

        GameObject selectedItemObject = itemSetActive[invlist[selecteditem]];
        selectedItemObject.SetActive(true);
    }
}
public interface Ipickable
{
    void PickItem();
}