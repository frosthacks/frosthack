using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Global;

    public static int baseHealth = 100;
    public static int baseMoney = 200;

    public int health;
    public int money;

    void Start() {
        Global = this;
        health = UserManager.baseHealth;
        money = UserManager.baseMoney;
    }

    public void takeDamage(int damage) {
        UserManager.Global.health -= damage;
        if (UserManager.Global.health <= 0) {
            UserManager.Global.health = 0;
            handleDeath();
        }
    }

    public void gainMoney(int amount) {
        UserManager.Global.money += amount;
    }

    // returns if successful
    public bool spendMoney(int amount) {
        if (UserManager.Global.money - amount < 0) {
            return false;
        } 
        UserManager.Global.money -= amount;
        return true;
    }

    void handleDeath() { }

}
