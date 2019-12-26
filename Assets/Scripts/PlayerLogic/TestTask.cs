using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTask
{
    /*

    void UpdateRacers(float deltaTimeS, List<Racer> racers)
    {
        List<Racer> racersNeedingRemoved;
        racersNeedingRemoved.Clear();
        // Updates the racers that are alive
        int racerIndex = 0;
        for (racerIndex = 1; racerIndex <= 1000; racerIndex++)
        {
            if (racerIndex <= racers.Count)
            {
                if (racers[racerIndex - 1].IsAlive())
                {
                    //Racer update takes milliseconds
                    racers[racerIndex - 1].Update(deltaTimeS * 1000.0f);
                }
            }
        }
        // Collides
        for (int racerIndex1 = 0; racerIndex1 < racers.Count; racerIndex1++)
        {
            for (int racerIndex2 = 0; racerIndex2 < racers.Count; racerIndex2++)
            {
                Racer racer1 = racers[racerIndex1];
                Racer racer2 = racers[racerIndex2];
                if (racerIndex1 != racerIndex2)
                {
                    if (racer1.IsCollidable() && racer2.IsCollidable() && racer1.CollidesWith(racer2))
                    {
                        OnRacerExplodes(racer1);
                        racersNeedingRemoved.Add(racer1);
                        racersNeedingRemoved.Add(racer2);

                    }
                }
            }
        }
        // Gets the racers that are still alive
        List<Racer> newRacerList = new List<Racer>();
        for (racerIndex = 0; racerIndex != racers.Count; racerIndex++)
        {
            // check if this racer must be removed
            if (racersNeedingRemoved.IndexOf(racers[racerIndex]) < 0)
            {
                newRacerList.Add(racers[racerIndex]);
            }
        }
        // Get rid of all the exploded racers
        for (racerIndex = 0; racerIndex != racersNeedingRemoved.Count; racerIndex++)
        {
            int foundRacerIndex = racers.IndexOf(racersNeedingRemoved[racerIndex]);
            if (foundRacerIndex >= 0) // Check we've not removed this already!
            {
                racersNeedingRemoved[racerIndex].Destroy();
                racers.Remove(racersNeedingRemoved[racerIndex]);
            }
        }
        // Builds the list of remaining racers
        racers.Clear();
        for (racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
        {
            racers.Add(newRacerList[racerIndex]);
        }
        for (racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
        {
            newRacerList.RemoveAt(0);

        }
    }


    /*
     * Прохо когда методы делают сразу много дел, это сложно читать и сложно изменять.
     * Я разбил этот большой метод на несколько маленьких, чтобы каждый занимался своим делом.
     * Во всем остальном просто улучшил читабельность.
     * Производительность улучшилась за счет оптимизации циклов или же удаления ненужных циклов.
     * 
     


    private void UpdateRacersNew(float deltaTimeS, List<Racer> racers)
    {
        List<Racer> racersNeedingRemoved;
        List<Racer> newRacerList;

        // Updates the racers that are alive
        foreach (var elem in racers)
        {
            if (elem.IsAlive())
            {
                elem.Update(deltaTimeS * 1000.0f);
            }
        }

        CheckCollisionBetweenRacers(racers, out racersNeedingRemoved);

        GetsAliveRacers(racers, racersNeedingRemoved, out newRacerList);

        DestroyExplodedRacers(racersNeedingRemoved);

        // "Builds the list of remaining racers" необходимый лист уже и есть newRacerList


    }

    private void CheckCollisionBetweenRacers(List<Racer> racers, out List<Racer> racersNeedingRemoved)
    {
        racersNeedingRemoved = new List<Racer>();
        //Не нужно проверять последний элемент, т.к. его не с чем будет сравнивать
        for (int racerIndex1 = 0; racerIndex1 < racers.Count-1; racerIndex1++)
        {
            //Не нужно проверять элементы ,которые уже были проверены, поэтому на каждой итерации двигаемся вперед
            for (int racerIndex2 = racerIndex1+1; racerIndex2 < racers.Count; racerIndex2++)
            {
                Racer racer1 = racers[racerIndex1];
                Racer racer2 = racers[racerIndex2];
                bool correctCollision = racer1.IsCollidable()
                                        &&
                                        racer2.IsCollidable()
                                        &&
                                        racer1.CollidesWith(racer2);
                if (correctCollision)
                {
                    OnRacerExplodes(racer1);
                    racersNeedingRemoved.Add(racer1);
                    racersNeedingRemoved.Add(racer2);
                }
            }
        }

    }

    private void GetsAliveRacers(List<Racer> racers, List<Racer> racersNeedingRemoved, out List<Racer> newRacerList)
    {
        newRacerList = new List<Racer>();
        if (racersNeedingRemoved.Count==0)
        {
            newRacerList = racers;
            return;
        }
                      
        foreach (var racer in racers)
        {
            if (!racersNeedingRemoved.Contains(racer))
            {
                newRacerList.Add(racer);
            }

        }
        
        
    }

    private void DestroyExplodedRacers(List<Racer> racersNeedingRemoved)
    {
        if (racersNeedingRemoved.Count == 0)       
            return;

        foreach (var racer in racersNeedingRemoved)
        {
            racersNeedingRemoved.Destroy();
        }

    }

    */
    }








public class Racer
{
    public bool IsAlive()
    {
        return false;
    }

    public bool IsCollidable()
    {
        return false;
    }

    public bool CollidesWith(Racer racer)
    {
        return false;
    }
    
}
