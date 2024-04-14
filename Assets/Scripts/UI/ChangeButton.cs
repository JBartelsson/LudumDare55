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

    [SerializeField] private BodyPartSO currentPart;


    [SerializeField] 

    void changeButtonContent(BodyPartSO item)
    {
        currentPart = item;
        type.SetText(item.type.ToString());
        hp.SetText(item.stats.HP.ToString());
        attack.SetText(item.stats.Attack.ToString());
        block.SetText(item.stats.Block.ToString());
        crit.SetText(item.stats.Crit.ToString());
        dodge.SetText(item.stats.Dodge.ToString());

        int typeNum = (int)item.type;
        int rarity = (int)item.rarity;
        frame.GetComponent<Image>().color = typeColor1[typeNum];
        bottomRight.GetComponent<Image>().color = typeColor2[typeNum];
        bottomLeft.GetComponent<Image>().color = typeColor2[typeNum];
        topRight.GetComponent<Image>().color = typeColor2[typeNum];
        topLeft.GetComponent<Image>().color = typeColor2[typeNum];
        smallFrame.GetComponent<Image>().color = typeColor2[typeNum];

        backgroundButton.GetComponent<Image>().color = rarityColor[rarity];

    }

    void Start()
    {
        //fairy blue
        ColorUtility.TryParseHtmlString("#aae0f0", out typeColor1[0]);
        //underground orange
        ColorUtility.TryParseHtmlString("#FA9038", out typeColor1[1]);
        //food grün
        ColorUtility.TryParseHtmlString("#B0C5A4", out typeColor1[2]);

        //fairy pink
        ColorUtility.TryParseHtmlString("#f4c6f2", out typeColor2[0]);
        //underground rot
        ColorUtility.TryParseHtmlString("#F11732", out typeColor2[1]);
        //food gelb
        ColorUtility.TryParseHtmlString("#F1EF99", out typeColor2[2]);

        //rarity common grün
        ColorUtility.TryParseHtmlString("#00CC00", out rarityColor[0]);
        //rarity uncommon blau
        ColorUtility.TryParseHtmlString("#00CCFF", out rarityColor[1]);
        //rarity rare violet
        ColorUtility.TryParseHtmlString("9900CC", out rarityColor[2]);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
