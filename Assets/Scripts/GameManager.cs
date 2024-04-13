using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private bool stopFighting = false;

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
                StartFighting();
                break;
            case GameState.Choosing:
                break;
            case GameState.Pause:
                break;
            case GameState.None:
                break;
        }
    }


    public void StartFighting()
    {
        while (enemyEntity.entityStats.HP > 0 || playerEntity.entityStats.HP > 0 || !stopFighting)
        {

        }
    }
}
