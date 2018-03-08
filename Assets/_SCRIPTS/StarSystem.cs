using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarSystem : MonoBehaviour
{
    public Color starColour;
    public Color lerpedColour = Color.white;
    public int starMax = 600;
    public float starSize = 10f;
    public float starDistance = 60f;
    
    private ParticleSystem.Particle[] points;

    

    // Use this for initialization
    void Start ()
    {
        createStars();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, starDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, starDistance/2);
    }

    // Update is called once per frame
    void Update ()
    {

        for (int i = 0; i < starMax; i++)
        {
            lerpedColour = Color.Lerp(Color.white, starColour, Mathf.PingPong(Time.time, Random.Range(1.0f, 2.0f)));
            points[i].startColor = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, points[i].startColor.a);
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private void createStars()
    {
        points = new ParticleSystem.Particle[starMax];

        for (int i = 0; i < starMax; i++)
        {
            points[i].position = Random.onUnitSphere * Random.Range(starDistance / 2, starDistance) + transform.position;
            points[i].startColor = new Color(starColour.r, starColour.g, starColour.b, starColour.a);
            points[i].startSize = starSize;
        }
    }
}
