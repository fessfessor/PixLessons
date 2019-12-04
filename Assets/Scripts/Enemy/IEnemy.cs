using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

   void Attack(bool isAttacking, GameObject enemy, GameObject sender = null);

   void Death();

   void Move();

   void Damage();


}
