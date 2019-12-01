using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

   void Attack(bool isAttacking, GameObject enemy);

   void Death();

   void Move();

   void Damage();


}
