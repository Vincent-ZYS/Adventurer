using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
public class PlayerMove : MonoBehaviour {

    private CharacterController cc;
    public float speed = 4;
    public ETCJoystick playerJoy;
    [HideInInspector]
    public Animator animator;
    void Awake()
    {
        cc = this.GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();
    }
  
    public void Move(Vector2 dir)
    {
        if (dir.x != 0 || dir.y != 0)
        {
            cc.SimpleMove(speed * new Vector3(dir.x, 0, dir.y));
            transform.forward = new Vector3(dir.x, 0, dir.y);
        }
        this.animator.SetBool("Walk", true);
    }
    public void OnMoveDown()
    {if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRange")|| this.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackA")|| this.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackB") )     {
            this.animator.SetBool("Walk", false);
            playerJoy.isTurnAndMove = false;
        }
        else
        {
            this.animator.SetBool("Walk", true);
            playerJoy.isTurnAndMove = true;
        }
        
    }
    public void OnMoveUp()
    {
        this.animator.SetBool("Walk", false);

    }
    public void OnJumpClick()
    {
        this.animator.SetTrigger("Jump");
    }
    public void OnJumpOver()
    {
        this.animator.SetBool("Walk", false);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag==Tag.enviroment)
        {
            SkillShortCut.instance.targetPosition =transform.position + transform.forward *8;
          

        }
    }
}
