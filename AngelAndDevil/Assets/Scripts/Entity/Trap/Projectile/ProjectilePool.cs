// Trap.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectilePool : Singleton<ProjectilePool>
{
    [SerializeField] private ProjectileContoroller[] _projectilePrefabs;
    [SerializeField] private int _poolSize = 10;

    private Dictionary<int, List<ProjectileContoroller>> _projectilePool;

    private void Awake()
    {
        base.Awake();

        _projectilePool = new Dictionary<int, List<ProjectileContoroller>>();    
        for(int i = 0; i < _projectilePrefabs.Length; i++)
        {
            _projectilePool[i] = new List<ProjectileContoroller>();
            for(int j = 0; j < _poolSize; j++)
            {
                Debug.Log("ProjectilePool: " + i);
                GameObject projectile = Instantiate(_projectilePrefabs[i].gameObject, transform);
                projectile.SetActive(false);
                _projectilePool[i].Add(projectile.GetComponent<ProjectileContoroller>());
            }
        }
    }

    public ProjectileContoroller GetProjectile(int index, Vector3 position, Quaternion rotation)
    {
        foreach(var projectile in _projectilePool[index])
        {
            if(!projectile.gameObject.activeSelf)
            {
                projectile.transform.position = position;
                projectile.transform.rotation = rotation;
                projectile.gameObject.SetActive(true);
                return projectile;
            }

        }
        Debug.Log("ProjectilePool: " + index + " is full");
        // 없으면 하나씩 추가
        GameObject newProjectile = Instantiate(_projectilePrefabs[index].gameObject, position, rotation);
        _projectilePool[index].Add(newProjectile.GetComponent<ProjectileContoroller>());
        newProjectile.SetActive(true);
        return newProjectile.GetComponent<ProjectileContoroller>();
    }

    public void ReturnProjectile(ProjectileContoroller projectile)
    {   
        projectile.gameObject.SetActive(false);
    }
}