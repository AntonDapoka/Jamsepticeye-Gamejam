using System.Collections.Generic;
using UnityEngine;

public class GhostPassableEnviromentSwitcher : MonoBehaviour
{
    private List<Collider2D> colliders2D = new List<Collider2D>();

    private void Start()
    {
        CollectColliders();
    }

    private void CollectColliders()
    {
        colliders2D.Clear();

        var ghosts = FindObjectsByType<GhostCanPass>(FindObjectsSortMode.None);

        var set = new HashSet<Collider2D>();

        foreach (var g in ghosts)
        {
            if (g == null) continue;
            foreach (var c in g.GetComponents<Collider2D>())
            {
                if (c != null && set.Add(c))
                    colliders2D.Add(c);
            }
        }
    }

    public void EnableTriggers()
    {
        for (int i = 0; i < colliders2D.Count; i++)
        {
            if (colliders2D[i] != null) colliders2D[i].isTrigger = true;
        }
    }
    public void DisableTriggers()
    {
        for (int i = 0; i < colliders2D.Count; i++)
        {
            if (colliders2D[i] != null) colliders2D[i].isTrigger = false;
        }
    }
    public void Refresh() => CollectColliders();
}