using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private CharacterController cc;
    public float speed = 4;
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
    {
        this.animator.SetBool("Walk", true);
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
}
