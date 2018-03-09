using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarSystem : MonoBehaviour
{
    public Color starColour = Color.white;
    public Color lerpedColour = Color.white;
    public int maxStars = 600;
    public float starSize = 10.0f;
    public float starMaxDistance = 600.0f;
    public float starMinDistance = 300.0f;
    
    private ParticleSystem.Particle[] points;

    // Use this for initialization
    void Start ()
    {
        createStars();
    }

    // Update is called once per frame
    void Update ()
    {
        for (int i = 0; i < maxStars; i++)
        {
            lerpedColour = Color.Lerp(Color.white, starColour, Mathf.PingPong(Time.time, Random.Range(1.0f, 2.0f)));
            points[i].startColor = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, points[i].startColor.a);

            points[i].startSize = Random.Range(starSize - 1, starSize + 1);
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, starMinDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, starMaxDistance);
    }


    private void createStars()
    {
        points = new ParticleSystem.Particle[maxStars];

        for (int i = 0; i < maxStars; i++)
        {
            points[i].position = Random.onUnitSphere * Random.Range(starMinDistance, starMaxDistance) + transform.position;
            points[i].startColor = new Color(starColour.r, starColour.g, starColour.b, starColour.a);
            points[i].startSize = starSize;
        }
    }
}
