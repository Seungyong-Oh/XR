using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private InputActionReference triggerAction;
    [SerializeField]
    private InputActionReference gripAction;

    // Animator変数にコンポーネントを設定
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // 有効になったときに各アクションのフェーズに実行する関数を設定する
    private void OnEnable()
    {
        triggerAction.action.performed += TriggerPressed;
        gripAction.action.performed += GripPressed;

    }

    //無効になったときに各アクションの処理を無効化する
    private void OnDisable()
    {
        triggerAction.action.performed -= TriggerPressed;
        gripAction.action.performed -= GripPressed;
    }

    // 人差し指ボタン入力時
    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        _anim.SetFloat("Pinch", obj.ReadValue<float>());
    }

    // 中指ボタン入力時
    private void GripPressed(InputAction.CallbackContext obj)
    {
        _anim.SetFloat("Flex", obj.ReadValue<float>());
    }
}
