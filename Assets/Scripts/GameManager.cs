using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        Fighting, Choosing, Pause, None
    }
    public GameState currentGameState = GameState.None;
    public Entity playerEntity;
    public Entity enemyEntity;
    [Header("Fighting Options")]
    [SerializeField] private bool continueFighting = false;
    [SerializeField] private float fightAttackDelay = 1f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        enemyEntity = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Entity>();

        SwitchState(currentGameState);
        Debug.Log($"Fairytale: {GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale)}");
        Debug.Log($"Underground: {GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground)}");
        Debug.Log($"Food: {GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food)}");
    }

    public void SwitchState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Fighting:
                StartCoroutine(StartFighting());
                break;
            case GameState.Choosing:
                SwitchState(GameState.Fighting);
                break;
            case GameState.Pause:
                break;
            case GameState.None:
                break;
        }
    }

    public int GetAmountOfItemsOfPlayer(BodyPartSO.Type type)
    {
        return playerEntity.GetAmountOfItemsOfType(type);
    }


    public IEnumerator StartFighting()
    {
        enemyEntity.ResetStats();
        playerEntity.ResetStats();
        int round = 0;
        bool playerWin = false;
        AudioManager.Instance.PlayFightMusic();
        while (continueFighting)
        {
            round++;
            Debug.Log($"=================Round: {round}==================");
            playerEntity.AttackAnimation();
            yield return new WaitForSeconds(.33f);
            if (!CheckDodge(enemyEntity))
            {
                
                CalculateAttack(playerEntity, enemyEntity);
            }
            if (enemyEntity.EntityFightingStats.HP <= 0)
            {
                playerWin = true;
                break;
            }
            yield return new WaitForSeconds(fightAttackDelay);
            enemyEntity.AttackAnimation();
            yield return new WaitForSeconds(.33f);
            if (!CheckDodge(playerEntity))
            {
                

                CalculateAttack(enemyEntity, playerEntity);
            }
            if (playerEntity.EntityFightingStats.HP <= 0)
            {
                playerWin = false;
                break;
            }
            yield return new WaitForSeconds(fightAttackDelay);
        }
        if (playerWin)
        {
            Debug.Log("Player won!");
            HandleWin();
        } else
        {
            TriggerGameOver();

            Debug.Log("Enemy Won!");
        }
    }

    private bool CheckDodge(Entity entity)
    {
        return entity.IsDodging();
    }

    public void TriggerGameOver()
    {

    }

    public void HandleWin()
    {
        CreateNewEnemy();
        SwitchState(GameState.Choosing);
    }

    public void CreateNewEnemy()
    {
        List<Entity.SpecificBodyPart> list = new List<Entity.SpecificBodyPart>() { Entity.SpecificBodyPart.LeftLeg, Entity.SpecificBodyPart.RightLeg, Entity.SpecificBodyPart.LeftArm, Entity.SpecificBodyPart.RightArm, Entity.SpecificBodyPart.Body, Entity.SpecificBodyPart.Head };
        int nonDefaultItemsOfPlayer = playerEntity.bodyParts.Where((x) => { return !x.bodyPartSO.isDefault; }).Count();
        for (int i = 0; i < nonDefaultItemsOfPlayer; i++)
        {
            BodyPartSO enemyPart = BodyPartManager.Instance.DrawEnemyParts(list[i]);

        }
        Debug.Log($"Non default items: {nonDefaultItemsOfPlayer}");
    }

    public void SwitchBodyPart(BodyPartSO bodyPartSO)
    {
        playerEntity.SwitchBodyPart(bodyPartSO.bodyPosition, bodyPartSO);
    }

    private void CalculateAttack(Entity attacker, Entity target)
    {
        bool crit = attacker.GetAttackDamage(out int damage);
        int LeftOverDamageFromBlock = target.Block(damage);
        if (LeftOverDamageFromBlock > 0)
        {
            target.Hit(LeftOverDamageFromBlock, crit);
        }

    }
}
