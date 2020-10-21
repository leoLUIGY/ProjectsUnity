﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;

namespace RPG.Combat{
public class Fighter : MonoBehaviour, IAction, ISaveable
{

    [SerializeField] float timeBetweenAttacks = 1f;

    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] Weapon defaultWeapon= null;
    Weapon currentWeapon = null;
    Health target;
    float timeSinceLastAttack = Mathf.Infinity;

    private void Start(){
        if(currentWeapon == null){
            EquipWeapon(defaultWeapon);
        }
    }

 
    private void Update(){

        timeSinceLastAttack += Time.deltaTime;
        if(target == null) return;
        if(target.IsDead())return;
        if(!GetIsInRange()){
            GetComponent<Mover>().MoveTo(target.transform.position, 1f);
        }
        else{
              GetComponent<Mover>().Cancel();
              AttackBehaviour();
        }
    }

    public void EquipWeapon(Weapon weapon){
        currentWeapon = weapon;
        Animator animator = GetComponent<Animator>();
        weapon.Spawn(rightHandTransform,leftHandTransform, animator); 
    }


    private void AttackBehaviour(){
        transform.LookAt(target.transform);
        if(timeSinceLastAttack > timeBetweenAttacks){
            TriggerAttack(); 
            timeSinceLastAttack = 0;
        }
    }

    private void TriggerAttack(){
        GetComponent<Animator>().ResetTrigger("stopAttack");
        GetComponent<Animator>().SetTrigger("attack");
    }
    void Hit(){
        if(target == null) return;
        if(currentWeapon.HasProjectile())
        {
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
        } else
        {
            target.TakeDamage(currentWeapon.GetDamage());
        }
        
    }

    void Shoot(){
        Hit();
    }

    private bool GetIsInRange(){
        return Vector3.Distance(transform.position, target.transform.position)<currentWeapon.GetRange();
    }

    public bool CanAttack(GameObject combatTarget){
        if(combatTarget == null){return false;}
        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }
    public void Attack(GameObject combatTarget){
        GetComponent<ActionSchedule>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }
    public void Cancel(){
        StopAttack();
        target = null;
        GetComponent<Mover>().Cancel();
    }

    private void StopAttack(){
        GetComponent<Animator>().SetTrigger("attack");
        GetComponent<Animator>().ResetTrigger("stopAttack");
    }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon= Resources.Load<Weapon>(weaponName);

            EquipWeapon(weapon);
        }
    }
}