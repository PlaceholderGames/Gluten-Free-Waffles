using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    public Color starColour;
    public int starMax = 600;
    public float starSize = 0.35f;
    public float starDistance = 60f;
    public float starClipDistance = 15f;

    private Transform starSystem;
    private Transform playerPos;
    private ParticleSystem.Particle[] points;
    private float starDistanceSqr;
    private float starClipDistanceSqr;

    private void createStars()
    {
        points = new ParticleSystem.Particle[starMax];

        for (int i = 0; i < starMax; i++) {
            var pos = Random.insideUnitSphere;
            pos = new Vector3(pos.x, Random.Range(0.1f, 0.2f), pos.z);
            //print(pos);

            points[i].position = pos * starDistance + starSystem.position;


            points[i].startColor = new Color(starColour.r, starColour.g, starColour.b, starColour.a);
            points[i].startSize = starSize;
        }
    }

    // Use this for initialization
    void Start ()
    {
        starSystem = GetComponent<Transform>();
        playerPos = GameObject.Find("Character").transform;

        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (points == null)
            createStars();

        for (int i = 0; i < starMax; i++) {
            if ((points[i].position - starSystem.position).sqrMagnitude > starDistanceSqr) {
                points[i].position = Random.insideUnitSphere.normalized * starDistance + starSystem.position;
            }

            if ((points[i].position - starSystem.position).sqrMagnitude <= starClipDistanceSqr) {
                float percentage = (points[i].position - starSystem.position).sqrMagnitude / starClipDistanceSqr;
                points[i].startColor = new Color(starColour.r, starColour.g, starColour.b, 1);
                points[i].startSize = percentage * starSize;
            }


            float distToPlayer = Vector3.Distance(playerPos.position, points[i].position);
            if (distToPlayer > 80) {
                points[i].startColor = new Color(points[i].GetCurrentColor., points[i].color.g, points[i].color.b, points[i].GetCurrentColor.a);
            }
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }
}
