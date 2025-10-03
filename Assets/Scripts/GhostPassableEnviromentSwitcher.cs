using System.Collections.Generic;
using UnityEngine;

public class GhostPassableEnviromentSwitcher : MonoBehaviour
{
    private readonly List<Collider2D> passColliders = new List<Collider2D>();
    private readonly List<Collider2D> nonPassColliders = new List<Collider2D>();

    private void Start()
    {
        CollectColliders();
    }

    private void CollectColliders()
    {
        passColliders.Clear();
        nonPassColliders.Clear();

        var set = new HashSet<Collider2D>();

        // GhostCanPass
        var passGhosts = FindObjectsByType<GhostCanPass>(FindObjectsSortMode.None);
        foreach (var g in passGhosts)
        {
            if (g == null) continue;
            foreach (var c in g.GetComponents<Collider2D>())
            {
                if (c != null && set.Add(c))
                    passColliders.Add(c);
            }
        }

        // GhostCanNotPass
        var nonPassGhosts = FindObjectsByType<GhostCanNotPass>(FindObjectsSortMode.None);
        foreach (var g in nonPassGhosts)
        {
            if (g == null) continue;
            foreach (var c in g.GetComponents<Collider2D>())
            {
                if (c != null && set.Add(c))
                    nonPassColliders.Add(c);
            }
        }
    }

    public void EnableGhostMode()
    {
        for (int i = 0; i < passColliders.Count; i++)
        {
            if (passColliders[i] != null) passColliders[i].isTrigger = true;
        }

        for (int i = 0; i < nonPassColliders.Count; i++)
        {
            if (nonPassColliders[i] != null) nonPassColliders[i].isTrigger = false;
        }
    }
    public void DisableGhostMode()
    {
        for (int i = 0; i < passColliders.Count; i++)
        {
            if (passColliders[i] != null) passColliders[i].isTrigger = false;
        }

        for (int i = 0; i < nonPassColliders.Count; i++)
        {
            if (nonPassColliders[i] != null) nonPassColliders[i].isTrigger = true;
        }
    }

    public void Refresh() => CollectColliders();
}