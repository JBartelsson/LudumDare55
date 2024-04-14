using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ChangeButton : MonoBehaviour
{
    
    public Color[] rarityColor = {new Color(), new Color(), new Color()};
    public Color[] rarityColor1 = { new Color(), new Color(), new Color() };
    public Color[] rarityColor2 = { new Color(), new Color(), new Color() };
    public Color[] typeColor1 = { new Color(), new Color(), new Color() };
    public Color[] typeColor2 = { new Color(), new Color(), new Color() };
    // Start is called before the first frame update
    [SerializeField] private Image frame;
    [SerializeField] private Image bottomRight;
    [SerializeField] private Image bottomLeft;
    [SerializeField] private Image topRight;
    [SerializeField] private Image topLeft;
    [SerializeField] private Image smallFrame;

    [SerializeField] private Image partSprite;

    [SerializeField] private Button backgroundButton;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text type;
    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text block;
    [SerializeField] private TMP_Text crit;
    [SerializeField] private TMP_Text dodge;

    [SerializeField] private BodyPartSO currentPart;
    [SerializeField] private ChangeButton currentButton;


    [SerializeField] 

    public void changeButtonContent(BodyPartSO item)
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
        Debug.Log("rarity:" + rarity);
        frame.GetComponent<Image>().color = typeColor1[typeNum];
        bottomRight.GetComponent<Image>().color = typeColor2[typeNum];
        bottomLeft.GetComponent<Image>().color = typeColor2[typeNum];
        topRight.GetComponent<Image>().color = typeColor2[typeNum];
        topLeft.GetComponent<Image>().color = typeColor2[typeNum];
        smallFrame.GetComponent<Image>().color = typeColor2[typeNum];

        partSprite.GetComponent<Image>().sprite = item.sprite;

        
        


        

        var colors = backgroundButton.GetComponent<Button>().colors;
        colors.normalColor = rarityColor[rarity];
        colors.highlightedColor = rarityColor1[rarity];
        colors.pressedColor = rarityColor2[rarity];


        backgroundButton.GetComponent<Button>().colors = colors;


    }

    public void ChangeCurrent()
    {
        BodyPartSO playerCurrentPart = GameManager.Instance.playerEntity.bodyParts.Where((x) => { return x.bodyPosition == currentPart.bodyPosition; }).First().bodyPartSO;
        currentButton.changeButtonContent(playerCurrentPart);
    }

    public void onClick()
    {
        GameManager.Instance.SwitchBodyPart(currentPart);

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
        ColorUtility.TryParseHtmlString("#B600E2", out rarityColor[2]);


        rarityColor1[0] = (rarityColor[0] * 1.5f);
        rarityColor1[0].a = 1;
        rarityColor1[1] = (rarityColor[1] * 1.5f);
        rarityColor1[1].a = 1;
        rarityColor1[2] = (rarityColor[2] * 1.5f);
        rarityColor1[2].a = 1;

        rarityColor2[0] = (rarityColor[0] * 0.5f);
        rarityColor2[0].a = 1;
        rarityColor2[1] = (rarityColor[1] * 0.5f);
        rarityColor2[1].a = 1;
        rarityColor2[2] = (rarityColor[2] * 0.5f);
        rarityColor2[2].a = 1;



        //rarity hovered
        //rarity common grün
        //ColorUtility.TryParseHtmlString("#00CC00", out rarityColor1[0]);
        //rarity uncommon blau
        //ColorUtility.TryParseHtmlString("#00CCFF", out rarityColor1[1]);
        //rarity rare violet
        //ColorUtility.TryParseHtmlString("#B600E2", out rarityColor1[2]);

        //rarity clicked
        //rarity common grün
        // ColorUtility.TryParseHtmlString("#00CC00", out rarityColor2[0]);
        //rarity uncommon blau
        //ColorUtility.TryParseHtmlString("#00CCFF", out rarityColor2[1]);
        //rarity rare violet
        //ColorUtility.TryParseHtmlString("#B600E2", out rarityColor2[2]);

        if (!currentPart.isDefault)
        {
            changeButtonContent(currentPart);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
