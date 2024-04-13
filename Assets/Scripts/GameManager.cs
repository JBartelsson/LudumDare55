using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public enum GameState
    {
        Fighting, Choosing, Pause, None
    }
    public GameState currentGameState = GameState.None;
    private Entity playerEntity;
    private Entity enemyEntity;
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
    }

    public void SwitchState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Fighting:
                StartCoroutine(StartFighting());
                break;
            case GameState.Choosing:
                break;
            case GameState.Pause:
                break;
            case GameState.None:
                break;
        }
    }


    public IEnumerator StartFighting()
    {
        enemyEntity.ResetStats();
        playerEntity.ResetStats();
        int round = 0;
        bool playerWin = false;
        while (continueFighting)
        {
            round++;
            Debug.Log($"=================Round: {round}==================");
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
        } else
        {
            Debug.Log("Enemy Won!");
        }
    }

    private bool CheckDodge(Entity entity)
    {
        return entity.IsDodging();
    }

    private void CalculateAttack(Entity attacker, Entity target)
    {

        int LeftOverDamageFromBlock = target.Block(attacker.GetAttackDamage());
        if (LeftOverDamageFromBlock > 0)
        {
            target.Hit(LeftOverDamageFromBlock);
        }

    }
}
