using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
public class Health : MonoBehaviour, ISaveable{
    [SerializeField] float RegenerationPercentage = 70;
    float healthPoints = -1f;
    bool isDead = false;

    private void Start(){
        
        if(healthPoints < 0){
        healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }
    }

    void OnEnable()
    {
        GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
    }

    void OnDisable()
    {
        GetComponent<BaseStats>().onLevelUp-= RegenerateHealth;
    }

    private void RegenerateHealth()
    {
        float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (RegenerationPercentage / 100);
        healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
    }
     
        public bool IsDead(){
        return isDead;
    }

      
        public void TakeDamage(GameObject instigator, float damage){
        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if(healthPoints <= 0){
            Die();
            AwardExperience(instigator);
        }
    }

    public float GetHealthPoints()
    {
        return healthPoints;
    }

    public float GetMaxHealthPoints()
    {
        return GetComponent<BaseStats>().GetStat(Stat.Health);
    }
    private void AwardExperience(GameObject instigator){
        Experience experience= instigator.GetComponent<Experience>();
        if(experience == null) return;
        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
    }

    public float GetPercentage(){
        return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
    }
    private void Die(){
        if(isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<ActionSchedule>().CancelCurrentAction();
    }
    public object CaptureState()
    {
        return healthPoints;
    }

    public void RestoreState(object state)
    {
        healthPoints = (float) state;

        if(healthPoints <= 0){
            Die();
        }
    }


}
}