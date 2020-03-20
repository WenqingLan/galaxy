using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace chardata{
    public class UseMyScriptableObject : MonoBehaviour
    {
    	private Transform tx;
    	public GenerateStars myScriptableObject;
        private ParticleSystem.Particle[] particles;
        private StarData[] stars;

        // Start is called before the first frame update
        void Start()
        {
            stars = myScriptableObject.stars;
            if (stars.Length == null && myScriptableObject == null)
            {
                Debug.Log("here");
                stars = CreateStarData();
            }
            else if (stars.Length == null && myScriptableObject != null)
            {
                stars = myScriptableObject.stars;

            }

            tx = transform;
        }

        void CreateStars() {
    		particles = new ParticleSystem.Particle[stars.Length];
    		
    	
            for (int i = 0; i < particles.Length; i++) {
            	particles[i].position = new Vector3(stars[i].position.x, stars[i].position.y, stars[i].position.z);
                particles[i].position = particles[i].position.normalized * 900;
           		particles[i].remainingLifetime = Mathf.Infinity;
                particles[i].size = (1 - stars[i].mag * 0.4f) * 15.0f;
                if (i % 5 == 0) {
                    particles[i].color = new Color (Random.Range (0.67f, 1.0f), Random.Range (0.67f, 1.0f), Random.Range (0.67f, 1.0f), Random.Range (0.67f, 1.0f));

                } else {
                    particles[i].color = stars[i].color;
                }
            }
            
        }
        public static StarData[] CreateStarData()
        {

            const string filename = "Assets/data/hygxyz.csv";
            const int limit = 7;
            string[] starLines = File.ReadAllLines(filename);
            List<StarData> starList = new List<StarData>();
            for (int i = 2; i < starLines.Length; i++)
            {
                string[] splits = starLines[i].Split(',');
                float mag = float.Parse(splits[13]);
                if (mag < limit)
                {
                    StarData sd = new StarData();
                    sd.mag = mag;
                    Vector3 v = new Vector3(float.Parse(splits[17]) * 2,
                                            float.Parse(splits[18]) * 2,
                                            float.Parse(splits[19]) * 2);
                    sd.position = v;
                    float ci = 2800;
                    float.TryParse(splits[16], out ci);
                    sd.color = GetColorFromColorIndex(ci);
                    sd.color.a = GetScaleFromMagnitude(mag);
                    starList.Add(sd);
                }
            }
            return starList.ToArray();
        }
        public static float GetScaleFromMagnitude(float magnitude)
        {
            float size = 1 - (magnitude) * 0.2f;

            return Mathf.Clamp(size, 0.1f, 10);
        }

        public static Color GetColorFromColorIndex(float B_V)
        {
            return GetColorFromTemperature(GetTemperatureFromColorIndex(B_V));
        }

        public static float GetTemperatureFromColorIndex(float B_V)
        {
            // From https://en.wikipedia.org/wiki/Color_index#cite_note-PyAstronomy-6
            return 4600 * (1 / ((0.92f * B_V) + 1.7f) + 1 / ((0.92f * B_V) + 0.62f));
        }

        public static Color GetColorFromTemperature(float temp)
        {
            // from http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/

            temp = temp / 100;

            // RED

            float r, g, b;

            if (temp <= 66)
            {
                r = 255;
            }
            else
            {
                r = temp - 60;
                r = 329.698727446f * (Mathf.Pow(r, -0.1332047592f));

                r = Mathf.Clamp(r, 0, 255);
            }

            // GREEN

            if (temp <= 66)
            {
                g = temp;
                g = 99.4708025861f * Mathf.Log(g) - 161.1195681661f;

                g = Mathf.Clamp(g, 0, 255);
            }
            else
            {
                g = temp - 60;
                g = 288.1221695283f * Mathf.Pow(g, -0.0755148492f);

                g = Mathf.Clamp(g, 0, 255);
            }

            // BLUE

            if (temp >= 66)
            {
                b = 255;
            }
            else
            {
                if (temp <= 19)
                {
                    b = 0;
                }
                else
                {
                    b = temp - 10;
                    b = 138.5177312231f * Mathf.Log(b) - 305.0447927307f;

                    b = Mathf.Clamp(b, 0, 255);
                }
            }

            return new Color(r / 255, g / 255, b / 255);
        }


        // Update is called once per frame
        void Update()
        {
            if (stars == null && myScriptableObject == null)
            {
                Debug.Log("here");
                stars = CreateStarData();
            } else if (stars == null && myScriptableObject != null)
            {
                stars = myScriptableObject.stars;

            }else if(stars.Length == 0)
            {
                stars = CreateStarData();
            }

            if ( particles == null ) CreateStars();
        	// test
            for (int i = (int)Mathf.Round(Random.Range (0.0f, 100.0f)); i < particles.Length; i += 1000) {
                particles[i].color = new Color (particles[i].color.r, particles[i].color.g, particles[i].color.b, Random.Range (0.0f, 1.0f));
            }
            GetComponent<ParticleSystem>().SetParticles(particles, particles.Length);
        }
    }
}
