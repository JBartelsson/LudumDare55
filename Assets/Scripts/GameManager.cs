using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
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
    [SerializeField] private BoosterUI shopWindow;
    private int round = 1;
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
                OpenShop();
                break;
            case GameState.Pause:
                break;
            case GameState.None:
                break;
        }
        currentGameState = newState;
    }

    public int GetAmountOfItemsOfPlayer(BodyPartSO.Type type)
    {
        return playerEntity.GetAmountOfItemsOfType(type);
    }


    public IEnumerator StartFighting()
    {
        enemyEntity.CalculateStats();
        playerEntity.CalculateStats();
        UpdateStats();

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
            UpdateStats();
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
            UpdateStats();
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

    void UpdateStats()
    {
        playerEntity.UpdateStats();
        enemyEntity.UpdateStats();
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
        round++;
        SwitchState(GameState.Choosing);
        CreateNewEnemy();
    }

    public void CreateNewEnemy()
    {
        List<Entity.SpecificBodyPart> list = new List<Entity.SpecificBodyPart>() { Entity.SpecificBodyPart.LeftLeg, Entity.SpecificBodyPart.RightLeg, Entity.SpecificBodyPart.LeftArm, Entity.SpecificBodyPart.RightArm, Entity.SpecificBodyPart.Body, Entity.SpecificBodyPart.Head };
        int nonDefaultItemsOfPlayer = playerEntity.bodyParts.Where((x) => { return !x.bodyPartSO.isDefault; }).Count();
        if (nonDefaultItemsOfPlayer == 0) nonDefaultItemsOfPlayer = 1;
        enemyEntity.ResetEnemy();
        for (int i = 0; i < nonDefaultItemsOfPlayer; i++)
        {
            BodyPartSO enemyPart = BodyPartManager.Instance.DrawEnemyParts(list[i]);
            Debug.Log($"Enemy got equipped with {enemyPart.name}");
            enemyEntity.SwitchBodyPart(enemyPart);


        }
        Debug.Log($"Non default items: {nonDefaultItemsOfPlayer}");
    }

    private void Update()
    {
        
    }

    public void SwitchBodyPart(BodyPartSO bodyPartSO)
    {
        playerEntity.SwitchBodyPart(bodyPartSO);
        shopWindow.CloseShop();
        SwitchState(GameState.Fighting);
    }

    public void SkipChoice()
    {
        SwitchState(GameState.Fighting);
        shopWindow.CloseShop();
    }

    public void OpenShop()
    {
        List<BodyPartSO> shopItems = BodyPartManager.Instance.DrawParts();
        shopWindow.DrawShop(shopItems);
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
