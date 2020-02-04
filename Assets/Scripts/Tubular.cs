
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;



abstract class Curve
{

    // Get point at relative position in curve according to arc length
    // - u [0 .. 1]

    public abstract Vector3 getPoint(float u);

    public Vector3 getPointAt(float u)
    {
        float t = this.getUtoTmapping(u);
        return this.getPoint(t);
    }

    // Get sequence of points using getPoint( t )

    List<Vector3> getPoints(float divisions = 0)
    {
        if (divisions == 0) divisions = 5;
        List<Vector3> pts = new List<Vector3>();
        for (float d = 0; d <= divisions; d++)
        {
            pts.Add(this.getPoint(d / divisions));
        }
        return pts;
    }

    // Get sequence of points using getPointAt( u )

    List<Vector3> getSpacedPoints(float divisions = 0)
    {
        if (divisions == 0) divisions = 5;
        List<Vector3> pts = new List<Vector3>();
        for (float d = 0; d <= divisions; d++)
        {
            pts.Add(this.getPointAt(d / divisions));
        }
        return pts;
    }

    // Get total curve arc length

    float getLength()
    {
        List<float> lengths = this.getLengths();
        return lengths[lengths.Count - 1];
    }

    // Get list of cumulative segment lengths
    int __arcLengthDivisions = 0; // anselm TODO examine xxx
    List<float> cacheArcLengths = null;
    bool needsUpdate = true;

    List<float> getLengths(int divisions = 0)
    {
        if (divisions == 0) divisions = this.__arcLengthDivisions > 0 ? this.__arcLengthDivisions : 200;
        if (this.cacheArcLengths != null
            && (this.cacheArcLengths.Count == divisions + 1)
            && !this.needsUpdate)
        {
            return this.cacheArcLengths;
        }

        this.needsUpdate = false;

        List<float> cache = new List<float>();
        Vector3 current, last = this.getPoint(0);
        float sum = 0;

        cache.Add(0);

        for (float p = 1; p <= divisions; p++)
        {
            current = this.getPoint(p / divisions);
            sum += Vector3.Distance(current, last);
            cache.Add(sum);
            last = current;
        }

        this.cacheArcLengths = cache;

        return cache; // { sums: cache, sum:sum }; Sum is in the last element.	
    }

    void updateArcLengths()
    {
        this.needsUpdate = true;
        this.getLengths();
    }

    // Given u ( 0 .. 1 ), get a t to find p. This gives you points which are equi distance

    float getUtoTmapping(float u, float distance = 0)
    {

        List<float> arcLengths = this.getLengths();
        int i = 0, il = arcLengths.Count;
        float targetArcLength; // The targeted u distance value to get

        if (distance != 0)
        {
            targetArcLength = distance;
        }
        else
        {
            targetArcLength = u * arcLengths[il - 1];
        }

        //var time = Date.now();

        // binary search for the index with largest value smaller than target u distance

        int low = 0, high = il - 1;

        while (low <= high)
        {
            i = (int)Mathf.Floor(low + (high - low) / 2); // less likely to overflow, though probably not issue here, JS doesn't really have integers, all numbers are floats
            float comparison = arcLengths[i] - targetArcLength;
            if (comparison < 0)
            {
                low = i + 1;
            }
            else if (comparison > 0)
            {
                high = i - 1;
            }
            else
            {
                high = i;
                break;
            }
        }

        i = high;

        //console.log('b' , i, low, high, Date.now()- time);

        if (arcLengths[i] == targetArcLength)
        {
            float t = ((float)i) / (((float)il) - 1.0f);
            return t;
        }

        // we could get finer grain at lengths, or use simple interpolatation between two points

        float lengthBefore = arcLengths[i];
        float lengthAfter = arcLengths[i + 1];

        float segmentLength = lengthAfter - lengthBefore;

        // determine where we are between the 'before' and 'after' points

        float segmentFraction = (targetArcLength - lengthBefore) / segmentLength;

        // add that fractional amount to t

        float t2 = (((float)i) + ((float)segmentFraction)) / (((float)il) - 1.0f);

        return t2;

    }

    // Returns a unit vector tangent at t
    // In case any sub curve does not implement its tangent derivation,
    // 2 points a small delta apart will be used to find its gradient
    // which seems to give a reasonable approximation

    Vector3 getTangent(float t)
    {

        float delta = 0.0001f;
        float t1 = t - delta;
        float t2 = t + delta;

        // Capping in case of danger

        if (t1 < 0) t1 = 0;
        if (t2 > 1) t2 = 1;

        Vector3 pt1 = this.getPoint(t1);
        Vector3 pt2 = this.getPoint(t2);

        Vector3 vec = pt2 - pt1; // anselm
        vec.Normalize();
        return vec;
    }

    public Vector3 getTangentAt(float u)
    {
        float t = this.getUtoTmapping(u);
        return this.getTangent(t);
    }
    /*
	float tangentQuadraticBezier(float t,float p0,float p1,float p2 ) {
		return 2 * ( 1 - t ) * ( p1 - p0 ) + 2 * t * ( p2 - p1 );
	}
		
	// Puay Bing, thanks for helping with this derivative!
		
	float tangentCubicBezier(float t, float p0, float p1, float p2, float p3 ) {
		return - 3 * p0 * (1 - t) * (1 - t)  +
			3 * p1 * (1 - t) * (1 - t) - 6 * t * p1 * (1 - t) +
				6 * t *  p2 * (1 - t) - 3 * t * t * p2 +
				3 * t * t * p3;
	}
	
	float tangentSpline(float t,float p0,float p1,float p2,float p3 ) {
		// To check if my formulas are correct
		float h00 = 6 * t * t - 6 * t; 	// derived from 2t^3 − 3t^2 + 1
		float h10 = 3 * t * t - 4 * t + 1; // t^3 − 2t^2 + t
		float h01 = - 6 * t * t + 6 * t; 	// − 2t3 + 3t2
		float h11 = 3 * t * t - 2 * t;	// t3 − t2
		return h00 + h10 + h01 + h11;	
	}
	*/
    // Catmull-Rom

    public float interpolate(float p0, float p1, float p2, float p3, float t)
    {
        float v0 = (p2 - p0) * 0.5f;
        float v1 = (p3 - p1) * 0.5f;
        float t2 = t * t;
        float t3 = t * t2;
        return (2 * p1 - 2 * p2 + v0 + v1) * t3 + (-3 * p1 + 3 * p2 - 2 * v0 - v1) * t2 + v0 * t + p1;
    }
}

class SplineCurve3 : Curve
{
    List<Vector3> points = null;
    public SplineCurve3(List<Vector3> points = null)
    {
        this.points = points != null ? points : new List<Vector3>();
    }

    public override Vector3 getPoint(float t)
    {
        int length = points.Count;
        float point = (length - 1) * t;
        int intPoint = (int)Mathf.Floor(point);
        float weight = point - intPoint;

        Vector3 point0 = points[intPoint == 0 ? intPoint : intPoint - 1];
        Vector3 point1 = points[intPoint];
        Vector3 point2 = points[intPoint > length - 2 ? length - 1 : intPoint + 1];
        Vector3 point3 = points[intPoint > length - 3 ? length - 1 : intPoint + 2];

        return new Vector3(
            this.interpolate(point0.x, point1.x, point2.x, point3.x, weight),
            this.interpolate(point0.y, point1.y, point2.y, point3.y, weight),
            this.interpolate(point0.z, point1.z, point2.z, point3.z, weight));

    }

}

public class Tubular : MonoBehaviour
{

    public static Tubular instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }





    public Material mainMaterial;
    public Material shadowMaterial;


    public enum STYLE
    {
        NONE,
        RIBBON,
        SWATCH,
        TUBE,
        //GLOW,
        //SPARKLE,
        //BLOCK,
        //PUTTY,
        //MAGNETIC
    };

    public STYLE style = STYLE.NONE;

    GameObject main;
    GameObject bottom;
    GameObject shadow;
    Mesh mainMesh;
    Mesh bottomMesh;
    Mesh shadowMesh;
    int totalCount = 0;

    void Start()
    {

        //focus = Instantiate(prefabSwatch) as GameObject;
        //focus.transform.parent = this.transform;
        //Swatch3d art = focus.GetComponent<Swatch3d>() as Swatch3d;
        //  Setup(Color.green, STYLE.TUBE);

        

    }

    void Update()
    {

    }

    public void Setup(Color _color, STYLE _style = STYLE.NONE,List<Vector3> trajectory = null, string wellname = null, Material _material = null, Material _shadowMaterial = null )
    {

        List<Vector3> points = new List<Vector3>();
        points = new List<Vector3>(trajectory);

        style = _style;

        // Set materials supplied else use default else crash
        if (_material != null) mainMaterial = _material;
        if (_shadowMaterial != null) shadowMaterial = _material;

        // Always clone the main material so we can modify it without affecting other swatches
        mainMaterial = Object.Instantiate(mainMaterial) as Material;
        mainMaterial.color = _color;

        // Basic mesh
        if (true)
        {
            main = gameObject;
            main.name = wellname;
            totalCount++;
            //gameObject.GetComponent<Renderer>().material = mainMaterial;
            GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            mainMesh = meshFilter.mesh = new Mesh();
        }

        

       // points.Add(new Vector3(-1000, 0, 0));
       // for (int i = 0; i < 10; i++)
      //  {
          //  points.Add(new Vector3(Random.Range(-1000, 1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)));


       // }
       // points.Add(new Vector3(1000, 0, 0));

        var path = new SplineCurve3(points);

        TubeGeometry(path, 500, 100, 6);

    }

    Matrix4x4 makeRotationAxis(Vector3 axis, float angle)
    { 

        Matrix4x4 mat = new Matrix4x4();

        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);
        float t = 1 - c;
        float x = axis.x, y = axis.y, z = axis.z;
        float tx = t * x, ty = t * y;

        mat.SetRow(0, new Vector3(tx * x + c, tx * y - s * z, tx * z + s * y));
        mat.SetRow(1, new Vector3(tx * y + s * z, ty * y + c, ty * z - s * x));
        mat.SetRow(2, new Vector3(tx * z - s * y, ty * z + s * x, t * z * z + c));
        mat.SetRow(3, new Vector4(0, 0, 0, 1));

        return mat;
    }

    // For computing of Frenet frames, exposing the tangents, normals and binormals the spline
    void FrenetFrames(Curve path, float segments, Vector3[] tangents, Vector3[] normals, Vector3[] binormals)
    {

        Vector3 normal = new Vector3();

        Vector3 vec;

        float numpoints = segments + 1;
        float epsilon = 0.0001f;
        float smallest;

        float tx, ty, tz;
        int i;
        float u;

        // compute the tangent vectors for each segment on the path


        for (i = 0; i < numpoints; i++)
        {
            u = ((float)i) / (((float)numpoints) - 1.0f);
            tangents[i] = path.getTangentAt(u);
            tangents[i].Normalize();
        }

        {
            // select an initial normal vector perpenicular to the first tangent vector,
            // and in the direction of the smallest tangent xyz component

            normals[0] = new Vector3();
            binormals[0] = new Vector3();
            smallest = float.MaxValue; //Number.MAX_VALUE;
            tx = Mathf.Abs(tangents[0].x);
            ty = Mathf.Abs(tangents[0].y);
            tz = Mathf.Abs(tangents[0].z);

            if (tx <= smallest)
            {
                smallest = tx;
                normal.Set(1, 0, 0);
            }

            if (ty <= smallest)
            {
                smallest = ty;
                normal.Set(0, 1, 0);
            }

            if (tz <= smallest)
            {
                normal.Set(0, 0, 1);
            }

            vec = Vector3.Cross(tangents[0], normal).normalized;
            normals[0] = Vector3.Cross(tangents[0], vec);
            binormals[0] = Vector3.Cross(tangents[0], normals[0]);
        }

        // compute the slowly-varying normal and binormal vectors for each segment on the path

        for (i = 1; i < numpoints; i++)
        {
            normals[i] = normals[i - 1];
            binormals[i] = binormals[i - 1];
            vec = Vector3.Cross(tangents[i - 1], tangents[i]);
            if (vec.magnitude > epsilon)
            {
                vec.Normalize();
                float dot = Vector3.Dot(tangents[i - 1], tangents[i]);
                if (dot < -1) dot = -1;
                if (dot > 1) dot = 1;
                float theta = Mathf.Acos(dot);
                // anselm normals[ i ].applyMatrix4( makeRotationAxis( vec, theta ) );
                normals[i] = makeRotationAxis(vec, theta).MultiplyVector(normals[i]);

            }
            binormals[i] = Vector3.Cross(tangents[i], normals[i]);
        }

    }

   

    void TubeGeometry(Curve path, int segments = 0, float radius = 0, int radialSegments = 0)
    {

        if (segments == 0) segments = 64;
        if (radius == 0) radius = 1;
        if (radialSegments == 0) radialSegments = 8;

        int numpoints = segments + 1;

        int i, j;

        Vector3[] tangents = new Vector3[numpoints];
        Vector3[] normals = new Vector3[numpoints];
        Vector3[] binormals = new Vector3[numpoints];

        FrenetFrames(path, segments, tangents, normals, binormals);

              Vector2[] uv = new Vector2[numpoints * radialSegments];
        for (i = 0; i < numpoints; i++)
        {
            float p2 = (float)i / (float)(numpoints - 1);
            for (j = 0; j < radialSegments; j++)
            {
                float p1 = (float)j / (float)(((float)radialSegments) - 1);
                uv[i * radialSegments + j] = new Vector2(p1, p2);
               // Debug.Log("Made UVS " + uv[i * radialSegments + j]);
            }
        }

        // construct the grid
        Vector3[] v = new Vector3[numpoints * radialSegments];
        for (i = 0; i < numpoints; i++)
        {
            Vector3 pos = path.getPointAt(((float)i) / (numpoints - 1));
            //Vector3 tangent = tangents[ i ];
            Vector3 normal = normals[i];
            Vector3 binormal = binormals[i];
            for (j = 0; j < radialSegments; j++)
            {
                float vi = ((float)j) / ((float)radialSegments) * 2.0f * Mathf.PI;
                float cx = -radius * Mathf.Cos(vi); // Hack: Negating it so it faces outside.
                float cy = radius * Mathf.Sin(vi);
                v[i * radialSegments + j] = new Vector3(
                    pos.x + cx * normal.x + cy * binormal.x,
                    pos.y + cx * normal.y + cy * binormal.y,
                    pos.z + cx * normal.z + cy * binormal.z
                    );
                //Debug.Log("Made Vertex " + v[i * radialSegments + j] + " " + radius + " " + vi + " " + cx + " " + cy + " " + normal + " " + binormal);
            }
        }

        // construct the mesh
        int[] tri = new int[segments * radialSegments * 2 * 3];
        for (i = 0; i < segments; i++)
        {
            for (j = 0; j < radialSegments; j++)
            {
                int ip = i + 1;
                int jp = (j + 1) % radialSegments;
                int a = i * radialSegments + j;     // *** NOT NECESSARILY PLANAR ! ***
                int b = ip * radialSegments + j;
                int c = ip * radialSegments + jp;
                int d = i * radialSegments + jp;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 0] = a;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 1] = b;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 2] = d;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 3] = b;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 4] = c;
                tri[i * radialSegments * 2 * 3 + j * 2 * 3 + 5] = d;
                //Vector2 uva = new Vector2( i / segments, j / radialSegments );
                //Vector2 uvb = new Vector2( ( i + 1 ) / segments, j / radialSegments );
                //Vector2 uvc = new Vector2( ( i + 1 ) / segments, ( j + 1 ) / radialSegments );
                //Vector2 uvd = new Vector2( i / segments, ( j + 1 ) / radialSegments );
                //this.faceVertexUvs[ 0 ].push( [ uva, uvb, uvd ] );
                //this.faceVertexUvs[ 0 ].push( [ uvb.clone(), uvc, uvd.clone() ] );
               // Debug.Log("Tri " + tri[i * radialSegments + j * 2 * 3]);
            }
        }

        if (true)
        {
            // Promote geometry to unity - for ribbons, swatches and tubes
            mainMesh.Clear();

            mainMesh.vertices = v;
            mainMesh.uv = uv;
            mainMesh.triangles = tri;

            //mainMesh.vertices = new Vector3[] { new Vector3(0,1,0), new Vector3(1,1,0), new Vector3(1,1,1) };
            //mainMesh.triangles = new int[3] { 1,0,2 };
            //mainMesh.uv = new Vector2[3] { new Vector2(0,0), new Vector2(0,1), new Vector2(1,1) };

            mainMesh.RecalculateNormals();
            mainMesh.RecalculateBounds();
        }

        //this.computeFaceNormals();
        //this.computeVertexNormals();
        // todo
    }


    float getSquareSegmentDistance(Vector3 p, Vector3 p1, Vector3 p2)
    {
        float x = p1.x, y = p1.y, z = p1.z, dx = p2.x - x, dy = p2.y - y, dz = p2.z - z;
        if (dx != 0 || dy != 0 || dz != 0)
        {
            float t = ((p.x - x) * dx + (p.y - y) * dy + (p.z - z) * dz) / (dx * dx + dy * dy + dz * dz);
            if (t > 1)
            {
                x = p2.x;
                y = p2.y;
                z = p2.z;
            }
            else if (t > 0)
            {
                x += dx * t;
                y += dy * t;
                z += dz * t;
            }
        }
        dx = p.x - x;
        dy = p.y - y;
        dz = p.z - z;
        return dx * dx + dy * dy + dz * dz;
    }

    List<Vector3> simplifyRadialDistance(List<Vector3> points, float sqTolerance)
    {
        Vector3 p1 = points[0];
        List<Vector3> newPoints = new List<Vector3>();
        newPoints.Add(p1);
        Vector3 p2 = p1;
        for (int i = 1, len = points.Count; i < len; i++)
        {
            p2 = points[i];
            float dx = p1.x - p2.x, dy = p1.y - p2.y, dz = p1.z - p2.z;
            if (dx * dx + dy * dy + dz * dz > sqTolerance)
            {
                newPoints.Add(p2);
                p1 = p2;
            }
        }
        if (p2 != p1)
        {
            newPoints.Add(p2); // might as well keep where the player is currently focused
        }
        return newPoints;
    }

    /*
        const float sqrToleranceMin = 0.1f;
        const float sqrToleranceQuick = 3 * 3;	
        public bool simplifyDouglasPeucker(float Tolerance = sqrToleranceMin) {

            if (points.Count < 4) return false;

            List<int> pointIndexsToKeep = new List<int>();

            int firstPoint = 0;
            int lastPoint = points.Count - 1;

            pointIndexsToKeep.Add(firstPoint);
            pointIndexsToKeep.Add(lastPoint);

            while (points[firstPoint].Equals(points[lastPoint])) {
                lastPoint--;
            }

            DouglasPeuckerReduction(firstPoint, lastPoint, Tolerance, ref pointIndexsToKeep);

            if(pointIndexsToKeep.Count == points.Count) return false;

            //Debug.Log ("Number of points coming into the system was " + points.Count );
            pointIndexsToKeep.Sort();		
            List<Vector3> points2 = new List<Vector3>();
            List<Vector3> rights2 = new List<Vector3>();
            List<Vector3> forwards2 = new List<Vector3>();
            List<float> velocities2 = new List<float>();			
            foreach (int i in pointIndexsToKeep) {
                points2.Add(points[i]);
                rights2.Add(rights[i]);
                forwards2.Add(forwards[i]);
                velocities2.Add(velocities[i]);
            }
            points = points2;
            rights = rights2;
            forwards = forwards2;
            velocities = velocities2;		
            //Debug.Log ("Number of points after system was " + points.Count );

            return true;		
        }

        private void DouglasPeuckerReduction(int firstPoint, int lastPoint, float tolerance, ref List<int> pointIndexsToKeep) {
            float maxDistance = 0;
            int indexFarthest = 0;

            // find the biggest bump
            for (int index = firstPoint; index < lastPoint; index++) {
                float distance = getSquareSegmentDistance(points[index], points[firstPoint], points[lastPoint]);
                //float distance = PerpendicularDistance(points[firstPoint], points[lastPoint], points[index]);
                if (distance > maxDistance) {
                    maxDistance = distance;
                    indexFarthest = index;
                }
            }

            // keep it	
            if (maxDistance > tolerance && indexFarthest != 0) {
                pointIndexsToKeep.Add(indexFarthest);
                DouglasPeuckerReduction(firstPoint, indexFarthest, tolerance, ref pointIndexsToKeep);
                DouglasPeuckerReduction(indexFarthest, lastPoint, tolerance, ref pointIndexsToKeep);
            }
        }
    */
}