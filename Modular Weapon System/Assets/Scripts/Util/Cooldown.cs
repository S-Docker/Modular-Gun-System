public class Cooldown
{
    bool isCooldown = false; public bool IsCooldown => isCooldown;
    float currentCooldownTime;
    float maxCooldownTime;

    public void StartCooldownTimer(float maxCooldownTime){
        this.maxCooldownTime = maxCooldownTime;
        isCooldown = true;
    }

    public void IncrementCooldownTimer(float amount){
        if (isCooldown){
            currentCooldownTime += amount;
            
            if (currentCooldownTime >= maxCooldownTime){
                isCooldown = false;
                currentCooldownTime = 0;
            }
        }
    }

    public void StopCooldownTimer(){
        isCooldown = false;
        currentCooldownTime = 0;        
    }
}
