﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Movement{
public class Mover : MonoBehaviour, IAction, ISaveable
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 6f;
    NavMeshAgent navMeshAgent;
    Health health;

    private void Awake() {
        health = GetComponent<Health>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Start(){

    }
    void Update()
    {
        navMeshAgent.enabled = !health.IsDead();
        UpdateAnimator();
    }
   
   public void StartMoveAction(Vector3 destination, float speedFraction){
       GetComponent<ActionSchedule>().StartAction(this);
       MoveTo(destination, speedFraction);
   }

    public void MoveTo(Vector3 destination, float speedFraction){
        navMeshAgent.destination = destination;
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped = false;
    }

    public void Cancel(){
        navMeshAgent.isStopped = true;
    }

    private void UpdateAnimator(){
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed= localVelocity.z; 
        GetComponent<Animator>().SetFloat("foward(Speed)", speed);
    }

        public object CaptureState()
        {

            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            navMeshAgent.enabled = false;
            transform.position = position.ToVector();
            navMeshAgent.enabled = true;
            GetComponent<ActionSchedule>().CancelCurrentAction();
        
        }
    }
}
