using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    public GameObject LaserExit;
   
    protected Vector3 pos;
    protected Vector3 dir;

    public GameObject laserObj;

    protected LineRenderer referenceRenderer;
    protected LineRenderer laserRenderer;
    
    public List<Vector3> laserIndices = new List<Vector3>();
    public ShootLaserType shootLaserType;


    // Handle Layers
    protected LayerMask layerMask;        //given by the ShootLaser (layers the laser can collide with)


    // Laser Parameters
    protected int maxLenght = 100;
    public float power = 0;
    public bool passedPinhole;


    public LaserBeam(GameObject laserExit,  
                                LaserBeam referenceBeam = null, 
                                LineRenderer referenceLineRenderer = null, 
                                ShootLaserType shootLaserType = ShootLaserType.LaserSource , 
                                string[] layerMask = null,
                                float power = 200) 
    {



        this.LaserExit = laserExit;
        // Create Laser Beam object
        this.laserObj = new GameObject("Laser Beam");
        this.laserRenderer = this.laserObj.AddComponent<LineRenderer>();
        this.shootLaserType = shootLaserType;




        if (layerMask == null)
        {
            this.layerMask = LayerMask.GetMask("Default");
        }

        this.layerMask = LayerMask.GetMask(layerMask);


        this.power = power;


        HandleShootLaserType(shootLaserType , referenceBeam , referenceLineRenderer);

    }


    public void HandleShootLaserType(ShootLaserType shootLaserType, LaserBeam referenceBeam , LineRenderer referenceLineRenderer)
    {
        switch (shootLaserType)
        {
            case ShootLaserType.LaserSource:
                {
                    LaserTypeLaserSource(referenceBeam , referenceLineRenderer);
                    break;
                }
            case ShootLaserType.Object:
                {
                    LaserTypeObject(referenceBeam, referenceLineRenderer);
                    break;
                }
            case ShootLaserType.Expander:
                {
                    LaserTypeExpander(referenceBeam , referenceLineRenderer);
                    maxLenght = 2;
                    break;
                }
            default: break;
        }

    }


    public void LaserTypeLaserSource(LaserBeam referenceBeam, LineRenderer referenceLineRenderer)
    {
        this.referenceRenderer = referenceLineRenderer;     // reference renderer becomes the renderer of the shoot laser object that creates the laser
    }


    public void LaserTypeObject(LaserBeam referenceBeam, LineRenderer referenceLineRenderer)
    {
        CopyReferenceBeam(referenceBeam);   // Copy line renderer from reference beam (ignore if no reference beam)
    }

    public void LaserTypeExpander(LaserBeam referenceBeam, LineRenderer referenceLineRenderer)
    {
        CopyReferenceBeam(referenceBeam);   // Copy line renderer from reference beam (ignore if no reference beam
    }


    public void Update(LaserBeam referenceLaser)
    {
        if (referenceLaser != null)
        {
            this.referenceRenderer = referenceLaser.laserRenderer;
            this.layerMask = referenceLaser.layerMask;

            this.power = referenceLaser.power;
        }

        UpdateReferenceBeam();
        Clear();
        CastRay(LaserExit.transform.position, LaserExit.transform.up);

    }

    public void Clear()
    {
        laserIndices.Clear();
        laserRenderer.positionCount = 0;
    }


    public void UpdateReferenceBeam()
    {
        this.laserRenderer.startWidth = referenceRenderer.startWidth;
        this.laserRenderer.endWidth = referenceRenderer.endWidth;
        this.laserRenderer.widthCurve = referenceRenderer.widthCurve;

        this.laserRenderer.startColor = referenceRenderer.startColor;
        this.laserRenderer.endColor = referenceRenderer.endColor;

        this.laserRenderer.numCapVertices = referenceRenderer.numCapVertices;
        this.laserRenderer.numCornerVertices = referenceRenderer.numCornerVertices;

        this.laserRenderer.material = referenceRenderer.material;

        this.laserRenderer.material.EnableKeyword("_EMISSION");
        this.laserRenderer.material.SetColor("_EmissionColor", this.laserRenderer.startColor);

    }




    // After calculating a hit update the laser indices and set the points in the line renderer
    private void UpdateLaser()
    {
        int count = 0;
        laserRenderer.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            if (count < maxLenght)
            {
                laserRenderer.SetPosition(count, idx);
                count++;
            }
            else
                break;
        }


        laserRenderer.positionCount = count;




        // EXPANDER
        if (ShootLaserType.Expander == shootLaserType)
        {               
            float distance = Vector3.Distance(laserIndices[0], laserIndices[1]);
            laserRenderer.endWidth = distance / 5;
        }


    }



    private void CastRay(Vector3 pos, Vector3 dir)
    {
        //TODO  MAKE IT FOR EXPANDER
        if (laserIndices.Count < maxLenght)           // check if number of positions is lower than allowed number of positions
        {
            laserIndices.Add(pos);

            Ray ray = new Ray(pos, dir);
            RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 300, 1)) // Get laser hit
            if (Physics.Raycast(ray, out hit, 300, layerMask)) // Get laser hit
            {
                CheckHit(hit, dir);
            }
            else
            {
                laserIndices.Add(ray.GetPoint(300)); // If no target hit, add point at the end of the laser
                UpdateLaser();
            }

            UpdateLaser();
        }
    }


    private void CheckHit(RaycastHit hitInfo, Vector3 direction)
    {
        BaseObject obj = hitInfo.collider.transform.GetComponent<BaseObject>(); // Check if it hit a material that it can interact with

        if (obj != null)
        {
            if (obj.reflective == ReflectiveType.Reflective) // Check material types
            {
                Vector3 pos = hitInfo.point;
                Vector3 dir = Vector3.Reflect(direction, hitInfo.normal); // Calculate reflection angle

                CastRay(pos, dir);
            }
            else if (obj.reflective == ReflectiveType.Refractive)
            {
                Vector3 pos = hitInfo.point;
                laserIndices.Add(pos);

                Vector3 newPos1 = new Vector3(
                    Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.0001f + pos.x,
                    Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.0001f + pos.y,
                    Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.0001f + pos.z); // Calculate new point close to hitpoint

                // Get values for refraction
                float material_1 = WorldInfo.Instance.refraction_index;
                float material_2 = obj.refractionIndex;

                Vector3 norm = hitInfo.normal;
                Vector3 incident = direction;
                Vector3 refractedVector = Refract(material_1, material_2, norm, incident); // Calculate refracted vector

                // Create new ray because the ray only collides with collider once (and wouldn't refract when leaving the object)
                Ray ray1 = new Ray(newPos1, (refractedVector == Vector3.zero) ? direction : refractedVector); // If no refraction occurs, continue with original direction
                CastRay(newPos1, refractedVector); // Cast new ray with refracted direction
            }
            else if (obj.reflective == ReflectiveType.nonReflective)
            {
                obj.HandleTouchLaser(this);
                laserIndices.Add(hitInfo.point);
                UpdateLaser();
            }

        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    private Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        float dot = Vector3.Dot(-norm, incident);

        // Check if n2 < n1, and if so, swap the norm and incident direction vectors
        if (n2 < n1)
        {
            norm = -norm;
            float temp = n1;
            n1 = n2;
            n2 = temp;
        }

        float refractiveIndexRatio = n1 / n2;
        float discriminant = 1.0f - refractiveIndexRatio * refractiveIndexRatio * (1 - dot * dot);

        if (discriminant > 0)
        {
            Vector3 refractedDirection = refractiveIndexRatio * incident + (refractiveIndexRatio * dot - Mathf.Sqrt(discriminant)) * norm;

            return refractedDirection.normalized; // Normalize the refracted direction
        }
        else
        {
            // Return the incident direction as the refracted direction when total internal reflection occurs
            return incident.normalized;
        }
    }



    // copy Line Renderer proprieties
    private void CopyReferenceBeam(LaserBeam referenceLaser)
    {
        if (this.shootLaserType == ShootLaserType.Expander)                 // TODO
        {
            // Get Values from own line Renderer
            this.laserRenderer.widthCurve = referenceLaser.laserRenderer.widthCurve;


            // Get Values from incoming laser
            this.laserRenderer.startColor = referenceLaser.laserRenderer.startColor;
            this.laserRenderer.endColor = referenceLaser.laserRenderer.endColor;

            this.laserRenderer.numCapVertices = referenceLaser.laserRenderer.numCapVertices;
            this.laserRenderer.numCornerVertices = referenceLaser.laserRenderer.numCornerVertices;

            this.laserRenderer.material = referenceLaser.laserRenderer.material;

            this.laserRenderer.material.EnableKeyword("_EMISSION");
            this.laserRenderer.material.SetColor("_EmissionColor", this.laserRenderer.startColor);


            this.laserRenderer.SetPositions(new Vector3[0]);


        }

        else
        {
            // Get Values from incoming laser
            this.laserRenderer.startWidth = referenceLaser.laserRenderer.endWidth;              // startWidth becomes the same as the end width as the beam that touched it
            this.laserRenderer.endWidth = referenceLaser.laserRenderer.endWidth;
            this.laserRenderer.widthCurve = referenceLaser.laserRenderer.widthCurve;

            this.laserRenderer.startColor = referenceLaser.laserRenderer.startColor;
            this.laserRenderer.endColor = referenceLaser.laserRenderer.endColor;

            this.laserRenderer.numCapVertices = referenceLaser.laserRenderer.numCapVertices;
            this.laserRenderer.numCornerVertices = referenceLaser.laserRenderer.numCornerVertices;

            this.laserRenderer.material = referenceLaser.laserRenderer.material;

            this.laserRenderer.material.EnableKeyword("_EMISSION");
            this.laserRenderer.material.SetColor("_EmissionColor", this.laserRenderer.startColor);


            this.laserRenderer.SetPositions(new Vector3[0]);        // clear indices of line renderer

            this.maxLenght = referenceLaser.maxLenght - referenceLaser.laserIndices.Count;
        }


    }




    public float CalculateLaserDistance()
    {
        float pathDistance = 0;
        for (int i = 0; i < laserIndices.Count - 1; i++)
        {
            pathDistance += Vector3.Distance(laserIndices[i], laserIndices[i + 1]);
        }


        LaserBeam previousLaser = LaserExit.GetComponent<ShootLaser>().referenceLaser;              // check if has a previous laser path
        if (LaserExit.GetComponent<ShootLaser>().referenceLaser != null)                            // if has a previous laser calculate the distance of previous laser and add to distance (recursive funtion)
        {
            pathDistance += 0.1f + LaserExit.GetComponent<ShootLaser>().referenceLaser.CalculateLaserDistance();        //WARNING We add 0.1 to account the distance of the laser entry -> laser exit (which is not account for in the distance travelled sum)
        }

        return pathDistance;
    }

}