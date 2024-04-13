using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Entity;

public class BoosterUI : MonoBehaviour
{
    [SerializeField] public int stage = 0;
    [SerializeField] public Entity player;
    public int[] chance = {100,0,0};

    public void DrawParts()
    {
        //
        List<BodyPartSO> shopLimbs = new List<BodyPartSO>();
        List<Entity.SpecificBodyPart> missingParts = new List<Entity.SpecificBodyPart>
            {Entity.SpecificBodyPart.LeftLeg, Entity.SpecificBodyPart.RightLeg, Entity.SpecificBodyPart.LeftArm, Entity.SpecificBodyPart.RightArm, Entity.SpecificBodyPart.Body, Entity.SpecificBodyPart.Head};
        
        // compute total weight, init some stuff

        int[] itemRar = { 0, 0, 0 };
        int total= 0;
        foreach (var weight in chance)
        {
            total += weight;
        }

        //check what slots are free
        foreach (var parts in player.bodyParts)
        {
            if(!parts.bodyPartSO.isDefault)
            {
                missingParts.Remove(parts.bodyPosition);
            }
        }


        // 
        for(int i = 0; i < 3; i++)
        {
            //calculate rarity
            float rand = Random.Range(0.0f, total);
            foreach (var weight in chance)
            {
                if(rand <= weight)
                {
                    break;
                } else
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
        if(missingParts.Count > 3)
        {
            iterate = 3;
        } else
        {
            iterate = missingParts.Count;
        }
        // add missing limb options to shop
        for (int i = 0; i < iterate; i++)
        {
            var part = missingParts[i];

            //filter limb list
            List<BodyPartSO> elgibleLimbs = BodyPartManager.Instance.bodyParts.Where((bodyPart) =>
            {
            return (bodyPart.bodyPosition == part && (int)bodyPart.rarity == itemRar[i]);

            }).ToList();
            bool found = false;
            while(!found) {
                int limb = Random.Range(0, elgibleLimbs.Count-1);
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
                if (null == existingLimb)
                {
                    shopLimbs.Add(elgibleLimbs[limb]);
                    found = true;
                }
            }

        }
        {

        }


            //calculate item





    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
