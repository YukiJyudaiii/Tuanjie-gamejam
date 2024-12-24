using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAchive : MonoBehaviour
{
    // Start is called before the first frame aupdate
    [SerializeField] public HorizontalCardHolder enemyHolder;
    [SerializeField] public HorizontalCardHolder playerHolder;
    List<GameObject> enemyList;
    [SerializeField] EnemyBase enemy;
    [SerializeField] int enemyIndex;
    void Start()
    {   
        enemyList = new List<GameObject>();
        EnemyInit();
        AddListeners(enemy);
    }
    void AddListeners(EnemyBase enemy){// addlisteners to enemy right now
        TurnManager.Instance.EnemyTurn_Start.AddListener(enemy.OnTurnStart);
        TurnManager.Instance.EnemyTurn_Draw.AddListener(enemy.OnTurnDraw);
        TurnManager.Instance.EnemyTurn_Suspend.AddListener(enemy.OnTurnSuspend);
        TurnManager.Instance.EnemyTurn_End.AddListener(enemy.OnTurnEnd);
        TurnManager.Instance.PlayerTurn_Start.AddListener(enemy.OnPlayerStart);
        TurnManager.Instance.PlayerTurn_Draw.AddListener(enemy.OnPlayerDraw);
        TurnManager.Instance.PlayerTurn_Suspend.AddListener(enemy.OnPlayerSuspend);
        TurnManager.Instance.PlayerTurn_End.AddListener(enemy.OnPlayerEnd);
    }
    void RemoveListeners(EnemyBase enemy){// remove listeners to enemy right now
        TurnManager.Instance.EnemyTurn_Start.RemoveListener(enemy.OnTurnStart);
        TurnManager.Instance.EnemyTurn_Draw.RemoveListener(enemy.OnTurnDraw);
        TurnManager.Instance.EnemyTurn_Suspend.RemoveListener(enemy.OnTurnSuspend);
        TurnManager.Instance.EnemyTurn_End.RemoveListener(enemy.OnTurnEnd);
        TurnManager.Instance.PlayerTurn_Start.RemoveListener(enemy.OnPlayerStart);
        TurnManager.Instance.PlayerTurn_Draw.RemoveListener(enemy.OnPlayerDraw);
        TurnManager.Instance.PlayerTurn_Suspend.RemoveListener(enemy.OnPlayerSuspend);
        TurnManager.Instance.PlayerTurn_End.RemoveListener(enemy.OnPlayerEnd);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextEnemy(){
        RemoveListeners(enemy);
        enemyIndex++;
        if(enemyIndex >= enemyList.Count){
            enemyIndex = 0;
        }
        SelectEnemy(enemyIndex);
        AddListeners(enemy);
    }
    public void SelectEnemy(int IndexOfEnemyToSelect){
        enemy.gameObject.SetActive(false);
        enemy = enemyList[IndexOfEnemyToSelect].GetComponent<EnemyBase>();
        enemy.gameObject.SetActive(true);
        GamePointBoard.Instance.enemyMaxHealth = enemy.health;
        GamePointBoard.Instance.enemyCurrentHealth = GamePointBoard.Instance.enemyMaxHealth;
    }
    public void EnemyInit(){
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            enemyList.Add(child.gameObject);
            // 在这里可以对获取到的子游戏对象进行各种操作
            if(i != enemyIndex)
            {
                child.gameObject.SetActive(false);
            }else{
                enemy = child.gameObject.GetComponent<EnemyBase>();
            }   
        }
    }
}
