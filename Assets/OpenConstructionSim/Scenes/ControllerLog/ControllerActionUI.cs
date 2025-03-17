using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerActionUI : MonoBehaviour
{
    public Text actionText; // UIテキスト用の変数（インスペクターで設定）

    private bool isRightCrawlerReversed = false; // 右クローラーの回転反転フラグ
    private bool isLeftCrawlerReversed = false;  // 左クローラーの回転反転フラグ

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // スティックの入力を取得
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();

        // トリガー（R2, L2）の値を取得
        float rightTrigger = gamepad.rightTrigger.ReadValue();
        float leftTrigger = gamepad.leftTrigger.ReadValue();

        // ボタンの入力を確認
        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            isRightCrawlerReversed = !isRightCrawlerReversed; // 右クローラーの回転方向を反転
        }

        if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            isLeftCrawlerReversed = !isLeftCrawlerReversed; // 左クローラーの回転方向を反転
        }

        // 入力に応じたアクションの文字列を生成
        string actionMessage = "";

        // 左スティックのX軸
        if (leftStick.x > 0)
        {
            actionMessage += "右旋回\n";
        }
        else if (leftStick.x < 0)
        {
            actionMessage += "左旋回\n";
        }

        // 左スティックのY軸
        if (leftStick.y > 0)
        {
            actionMessage += "ブーム開\n";
        }
        else if (leftStick.y < 0)
        {
            actionMessage += "ブーム閉\n";
        }

        // 右スティックのX軸
        if (rightStick.x > 0)
        {
            actionMessage += "バケット開\n";
        }
        else if (rightStick.x < 0)
        {
            actionMessage += "バケット閉\n";
        }

        // 右スティックのY軸
        if (rightStick.y > 0)
        {
            actionMessage += "ブーム開\n";
        }
        else if (rightStick.y < 0)
        {
            actionMessage += "ブーム閉\n";
        }

        // クローラーの動作
        if (rightTrigger > 0 && leftTrigger > 0) // R2とL2が同時に押されている
        {
            if (!isRightCrawlerReversed && !isLeftCrawlerReversed)
            {
                actionMessage += "前進\n";
            }
            else if (isRightCrawlerReversed && !isLeftCrawlerReversed)
            {
                actionMessage += "右回転\n";
            }
            else if (!isRightCrawlerReversed && isLeftCrawlerReversed)
            {
                actionMessage += "左回転\n";
            }
            else if (isRightCrawlerReversed && isLeftCrawlerReversed)
            {
                actionMessage += "後退\n";
            }
        }

        // アクションメッセージをUIに表示
        actionText.text = actionMessage;
    }
}
