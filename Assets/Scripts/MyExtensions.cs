using System.Threading.Tasks;
using UnityEngine;

public static class MyExtensions
{
    public static async void Recicle(this TrailRenderer trail)
    {
        float defaultTime = trail.time;

        trail.time = -.1f;

        await Task.Delay(16);

        if (trail != null && Application.isPlaying)
            trail.time = defaultTime;
    }
}
