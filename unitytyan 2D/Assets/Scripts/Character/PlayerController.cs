using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : BaseCharacterController
{
  [SerializeField]
  string horizontalInputName = "Horizontal";
  [SerializeField]
  string jumpButtonName = "Jump";
  bool jump = false;
  bool damage = false;
  float inputH = 0;
  Animator animator;
  SpriteRenderer spriteRenderer;
  Rigidbody2D rigidBody2D;
  protected override void Start()
  {
    base.Start();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    rigidBody2D = GetComponent<Rigidbody2D>();
  }
  protected override void Update()
  {
    GetInput();
  }
  protected override void FixedUpdate()
  {
    Move();
  }
  void GetInput()
  {
    if (!isActive)
    {
      return;
    }
    inputH = Input.GetAxisRaw(horizontalInputName);
    jump = Input.GetButtonDown(jumpButtonName);
  }
  protected override void Move()
  {
    if (!isActive)
    {
      return;
    }
    //接地判定
    GroundCheck();
    //移動速度の計算処理
    if (inputH != 0)
    {
      direction = Mathf.Sign(inputH);
      speed = defaultSpeed * direction;
      //入力がマイナスならスプライトの向きを反転させる
      spriteRenderer.flipX = direction < 0 ? true : false;
    }
    else
    {
      speed = 0;
    }
    //ジャンプの速度計算
    if (jump && isGrounded)
    {
      rigidBody2D.velocity = Vector3.up * jumpPower;
    }
    //アニメーションを更新
    UpdateAnimation();
    //実際の移動処理
    rigidBody2D.velocity = new Vector2(speed, rigidBody2D.velocity.y);
  }
    
  protected override void Damage()
  {
    
  }
  protected override void Dead()
  {
    isActive = false;
  }
    protected override void UpdateAnimation()
    {
      /*animator.SetFloat("Speed", Mathf.Abs(speed));
      animator.SetBool("Jump", !isGrounded);*/
    }
}