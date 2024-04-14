using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Entity;

public class BodyPartManager : MonoBehaviour
{
   public static BodyPartManager Instance { get; private set; }
    public List<BodyPartSO> bodyParts;

    public float HPBalancingMultiplier = 1f;
    public float DMGBalancingMultiplier = 1f;
    public float BlockBalancingMultiplier = 1f;
    public float CritChanceBalancingMultiplier = 1f;
    public float DodgeBalancingMultiplier = 1f;


    public float CritChanceMultiplier = 2f;


    //Booster UI stuff
    [SerializeField] public int stage = 0;
    [SerializeField] public Entity player;
    public int[] chance = { 100, 90, 50 };

    public void DrawParts()
    {
        //
        List<BodyPartSO> shopLimbs = new List<BodyPartSO>();
        List<Entity.SpecificBodyPart> missingParts = new List<Entity.SpecificBodyPart>
            {Entity.SpecificBodyPart.LeftLeg, Entity.SpecificBodyPart.RightLeg, Entity.SpecificBodyPart.LeftArm, Entity.SpecificBodyPart.RightArm, Entity.SpecificBodyPart.Body, Entity.SpecificBodyPart.Head};

        // compute total weight, init some stuff

        int[] itemRar = { 0, 0, 0 };
        int total = 0;
        foreach (var weight in chance)
        {
            total += weight;
        }

        //check what slots are free
        foreach (var parts in player.bodyParts)
        {
            if (!parts.bodyPartSO.isDefault)
            {
                missingParts.Remove(parts.bodyPosition);
            }
        }


        // 
        for (int i = 0; i < 3; i++)
        {
            //calculate rarity
            float rand = Random.Range(0.0f, total);
            foreach (var weight in chance)
            {
                if (rand <= weight)
                {
                    break;
                }
                else
                {
                    rand -= weight;
                    itemRar[i]++;
                }
            }
        }
        // shuffle list

        var count = missingParts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = missingParts[i];
            missingParts[i] = missingParts[r];
            missingParts[r] = tmp;
        }

        int iterate = 0;
        if (missingParts.Count > 3)
        {
            iterate = 3;
        }
        else
        {
            iterate = missingParts.Count;
        }
        // add missing limb options to shop
        for (int i = 0; i < iterate; i++)
        {
            var part = missingParts[i];
            Debug.Log("ItemRarity:" + itemRar[i]);
            Debug.Log("BodyPart:" + part);
            //filter limb list
            List<BodyPartSO> elgibleLimbs = BodyPartManager.Instance.bodyParts.Where((bodyPart) =>
            {
                return (bodyPart.bodyPosition == part && (int)bodyPart.rarity == itemRar[i]);

            }).ToList();
            bool found = false;
            while (!found)
            {
                int limb = Random.Range(0, elgibleLimbs.Count - 1);
                Debug.Log("Limb:" + limb);
                PositionedBodyPart existingLimb = player.bodyParts.FirstOrDefault(i => i.bodyPartSO == elgibleLimbs[limb]);
                if (null == existingLimb)
                {
                    shopLimbs.Add(elgibleLimbs[limb]);
                    found = true;
                }
            }

        }
        // find leftover limbs
        int leftover = 3 - iterate;
        for (int i = 0; i < leftover; i++)
        {
            var part = missingParts[i];

            //filter limb list
            List<BodyPartSO> elgibleLimbs = BodyPartManager.Instance.bodyParts.Where((bodyPart) =>
            {
                return ((int)bodyPart.rarity == itemRar[i]);

            }).ToList();
            bool found = false;
            while (!found)
            {
                int limb = Random.Range(0, elgibleLimbs.Count - 1);
                PositionedBodyPart existingLimb = player.bodyParts.FirstOrDefault(i => i.bodyPartSO == elgibleLimbs[limb]);
                if (null == existingLimb && !shopLimbs.Contains(elgibleLimbs[limb]))
                {
                    shopLimbs.Add(elgibleLimbs[limb]);
                    found = true;
                }
            }

        }
        {

        }


        //calculate item


        foreach (var part in shopLimbs)
        {
            Debug.Log("Shopitem: " + part.name);
        }


    }


    public BodyPartSO DrawEnemyParts(SpecificBodyPart part)
    {
        //
        BodyPartSO returnLimbs;

        // compute total weight, init some stuff

        int itemRar = 0;
        int total = 0;
        foreach (var weight in chance)
        {
            total += weight;
        }



        //calculate rarity
        float rand = Random.Range(0.0f, total);
        foreach (var weight in chance)
        {
            if (rand <= weight)
            {
                break;
            }
            else
            {
                rand -= weight;
                itemRar++;
            }
        }





        //filter limb list
        List<BodyPartSO> elgibleLimbs = BodyPartManager.Instance.bodyParts.Where((bodyPart) =>
        {
            return (bodyPart.bodyPosition == part && (int)bodyPart.rarity == itemRar);

        }).ToList();


        int limb = Random.Range(0, elgibleLimbs.Count - 1);
        return elgibleLimbs[limb];



    }

   
    private void Awake()
    {
        Instance = this;
    }
}
