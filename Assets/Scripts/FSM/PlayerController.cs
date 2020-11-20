using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    private void Start() {
        // 初始化状态机，选择初始状态
        StateManager.GetInstance().AddState(new PlayerAttack());
        StateManager.GetInstance().AddState(new PlayerFocus());
    }
    
    private void Update() {
        StateManager.GetInstance().Excute<PlayerController>(this);
    }
}