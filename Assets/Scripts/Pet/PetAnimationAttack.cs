using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimationAttack : MonoBehaviour {
    public Animator animator;
    public static PetAnimationAttack instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }
  public void AttackClaw()
    {
        this.animator.SetTrigger("AttackClaws");
        this.animator.SetBool("Walk", false);
    }
}
