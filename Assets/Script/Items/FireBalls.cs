using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FireBalls
{
    public static void ShootFireBall(List<GameObject> projectiles, float direction, Vector3 attackPoint, LayerMask whatIsPlayer, LayerMask gameObjectLayer)
    {
        AudioManager.instance.PlaySound(AudioManager.instance.FireBallClip);
        int idx = FindIt(projectiles);
        projectiles[idx].transform.position = attackPoint;
        projectiles[idx].GetComponent<Projectile>().SetDirection(direction);
        projectiles[idx].GetComponent<Projectile>().SetLayers(whatIsPlayer, gameObjectLayer);
    }
    private static int FindIt(List<GameObject> projectiles)
    {
        for (int i = 0; i < projectiles.Count; i++)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }
}
