using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoss
{
    // Обыкновенный босс
    void OnSpawnSimpleBoss();

    // Босс обыкновенный, но мы жертвуем свою кровь и получаем больше награды
     void OnSpawnBloodBoss();

    // Ослабленный босс
     void OnSpawnLightBoss();

 
}
