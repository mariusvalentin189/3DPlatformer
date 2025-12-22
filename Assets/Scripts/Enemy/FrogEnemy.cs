using UnityEngine;

public class FrogEnemy : Enemy
{
    bool isJumping;
    protected override void SetMovementSpeed()
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 directionToDestination = (enemy.destination - transform.position).normalized;
        float dotProduct = Vector3.Dot(forwardDirection, directionToDestination);

        if (isJumping)
        {
            if (dotProduct >= 0.9f)
                enemy.speed = currentSpeed;
            else enemy.speed = 0.2f;
        }
        else enemy.speed = 0f;

        if (directionToDestination != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToDestination), currentRotationSpeed * Time.deltaTime);
    }

    public void CanNotMove()
    {
        isJumping = false;
    }
    public void CanMove()
    {
        isJumping = true;
    }
}
