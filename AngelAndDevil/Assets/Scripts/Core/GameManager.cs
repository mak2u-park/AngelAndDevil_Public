using UnityEngine;

public class GameManager : Singleton<GameManager>
{   
    public int getHostageCount(HostageType type)
    {
        Hostage[] hostages = FindObjectsOfType<Hostage>();
        int count = 0;
        foreach(Hostage hostage in hostages)
        {
            if(hostage.Type == type)
            {
                count++;
            }
        }
        return count;
    }   
}
