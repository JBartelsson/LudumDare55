using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeButton : MonoBehaviour
{
    
    public Color[] rarityColor = {new Color(), new Color(), new Color()};
    public Color[] typeColor1 = { new Color(), new Color(), new Color() };
    public Color[] typeColor2 = { new Color(), new Color(), new Color() };
    // Start is called before the first frame update
    [SerializeField] private Image frame;
    [SerializeField] private Image bottomRight;
    [SerializeField] private Image bottomLeft;
    [SerializeField] private Image topRight;
    [SerializeField] private Image topLeft;
    [SerializeField] private Image smallFrame;

    [SerializeField] private Button backgroundButton;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text type;
    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text block;
    [SerializeField] private TMP_Text crit;
    [SerializeField] private TMP_Text dodge;


    [SerializeField] 

    void changeButtonContent(BodyPartSO item)
    {
        type.SetText(item.type.ToString());
        hp.SetText(item.stats.HP.ToString());
        attack.SetText(item.stats.Attack.ToString());
        block.SetText(item.stats.Block.ToString());
        crit.SetText(item.stats.Crit.ToString());
        dodge.SetText(item.stats.Dodge.ToString());



    }

    void Start()
    {
        //ColorUtility.TryParseHtmlString(htmlValue, out newCol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
