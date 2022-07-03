using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI  dialogueText;
    
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();

        playerHUD.countdownBar.maxValue = playerHUD.countdownBaseMax;
        playerHUD.countdownBar.value = playerHUD.countdownReset;
        
        enemyHUD.countdownBar.maxValue = enemyHUD.countdownBaseMax;
        enemyHUD.countdownBar.value = enemyHUD.countdownReset;
    }

    void Update()
    {        
        if (state != BattleState.WON && state != BattleState.LOST)
        {    
            playerHUD.countdownBar.value += Time.deltaTime * playerHUD.countdownSpeed;
            enemyHUD.countdownBar.value += Time.deltaTime * enemyHUD.countdownSpeed;
            if (playerHUD.countdownBar.value == playerHUD.countdownBaseMax)
            {
                PlayerTurn();
            }
            if (enemyHUD.countdownBar.value == enemyHUD.countdownBaseMax)
            {
                EnemyTurn();
            }
        }    
    }
    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " appeared!";

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);
    }

    void PlayerTurn()
    {
        dialogueText.text = "What will you do?";
        state = BattleState.PLAYERTURN;
    }

    void EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " is attacking!" + " for " + enemyUnit.damage + " damage!";

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHealth(playerUnit.currentHP);
        
        enemyHUD.countdownBar.value = enemyHUD.countdownReset;

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.START;
            PlayerTurn();
        }
    }

    void PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHealth(enemyUnit.currentHP);
        dialogueText.text = "You attacked " + enemyUnit.unitName + " for " + playerUnit.damage + " damage!";
                
        playerHUD.countdownBar.value = playerHUD.countdownReset;

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.START;
        }
    }

    void PlayerHeal()
    {
        dialogueText.text = "You healed yourself for " + playerUnit.damage + " damage!";

        playerUnit.Heal(playerUnit.damage);
        playerHUD.SetHealth(playerUnit.currentHP);

        playerHUD.countdownBar.value = playerHUD.countdownReset;

        state = BattleState.START;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        PlayerAttack();
    }

    public void OnHeal()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        PlayerHeal();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle.";
        }
    }

}
