using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;
using static Entity;
using UnityEngine.SceneManagement;

public class BoosterUI : MonoBehaviour
{

    [SerializeField] private ChangeButton Upgrade_button1;
    [SerializeField] private ChangeButton Upgrade_button2;
    [SerializeField] private ChangeButton Upgrade_button3;
    [SerializeField] private ChangeButton Upgrade_button4;
    [SerializeField] private GameObject shopMenu;

    public void DrawShop(List<BodyPartSO> shopItems)
    {

        Upgrade_button1.changeButtonContent(shopItems[0]);
        Upgrade_button2.changeButtonContent(shopItems[1]);
        Upgrade_button3.changeButtonContent(shopItems[2]);
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shopMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
