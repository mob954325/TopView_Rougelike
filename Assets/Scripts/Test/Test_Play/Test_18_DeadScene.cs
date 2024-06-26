#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_18_DeadScene : TestBase
{
    Player player;


/*             (int)(coinAmount +
                    BombCount* 20 +
                    KeyCount* 100 +
                    additionalAttackPower* 200 +
                    additionalDefencePower* 200 +
                    addtionalSpeed* 100);*/
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player = FindFirstObjectByType<Player>();
        player.CoinAmount = 1000;
        player.BombCount = 5;
        player.KeyCount = 1;
        player.IncreaseAttackPower(5);
        player.IncreaseDefencePower(5);
        player.InCreaseSpeed(10);

        // 예상 점수 값 = 4200;
        player.CurrentHealth = 0;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        GameManager.Instance.EndGame(10000, true);
    }
}
#endif