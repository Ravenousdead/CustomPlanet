using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (PlanetBase))]
public class CustomPlanet : Editor {

	PlanetBase pb;

	void OnEnable () {
		pb = (PlanetBase)target;
	}

	public override void OnInspectorGUI ()
	{
		DrawSpaceInfo ();
		DrawLandInfo ();
		DrawLifeInfo ();
	}

	void DrawSpaceInfo () {
		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.LabelField ("Space Info", EditorStyles.boldLabel);
		pb.mainColor = EditorGUILayout.ColorField (new GUIContent("Main Color: ", "Main color of the planet."), pb.mainColor);
		pb.planetSize = EditorGUILayout.IntSlider (new GUIContent("Planet Size: ", "Radius of the planet in miles."), pb.planetSize, 500, 100000);
		pb.moonAmount = EditorGUILayout.IntSlider (new GUIContent("Number of Moons: ", "Number of moons orbiting the planet. Zero for no planets."), pb.moonAmount, 0, 200);
		pb.orbitTime = EditorGUILayout.Slider (new GUIContent("Orbit Time: ", "Number of days it takes to make up one year."), pb.orbitTime, 0f, 100000f);
		pb.revolutionTime = EditorGUILayout.Slider (new GUIContent("Revolution Time: ", "Number of hours in a day."), pb.revolutionTime, 0f, 5000f);
		EditorGUILayout.EndVertical ();
	}


	void DrawLandInfo () {
		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.LabelField ("Landscapes Info", EditorStyles.boldLabel);

		pb.hasWater = EditorGUILayout.Toggle(new GUIContent("Has Water: ", ""), pb.hasWater);
		pb.radiationAmount = EditorGUILayout.Slider (new GUIContent("Radiation: ", "Radiation measured in rads."), pb.radiationAmount, 0f, 100f);
		
		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.LabelField ("Elevations");
		EditorGUILayout.MinMaxSlider (ref pb.lowElevation, ref pb.highElevation, -50f, 50f);
		pb.lowElevation = EditorGUILayout.FloatField (new GUIContent("Lowest: ", "Lowest elevation in miles."), pb.lowElevation);
		pb.highElevation = EditorGUILayout.FloatField (new GUIContent("Highest: ", "Highest elevation in miles."), pb.highElevation);
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.LabelField ("Temperature");
		EditorGUILayout.MinMaxSlider (ref pb.lowTemp, ref pb.highTemp, -200f, 1000f);
		pb.lowTemp = EditorGUILayout.FloatField (new GUIContent("Lowest: ", "Lowest temperature measured in celsius."), pb.lowTemp);
		pb.highTemp = EditorGUILayout.FloatField (new GUIContent("Highest: ", "Highest temperature measured in celsius."), pb.highTemp);
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ("box");
		serializedObject.Update();
		SerializedProperty myProp = serializedObject.FindProperty("mainElements");
		EditorGUILayout.LabelField (new GUIContent("Elements", "Array of strings, elements hosted on the planet."));
		EditorGUILayout.PropertyField (myProp, true);
		serializedObject.ApplyModifiedProperties ();
		EditorGUILayout.EndVertical ();

		EditorGUILayout.EndVertical ();
	}

	void DrawLifeInfo () {
		EditorGUILayout.BeginVertical ("box");
		EditorGUILayout.LabelField ("Life Info", EditorStyles.boldLabel);

		EditorGUILayout.BeginVertical ("box");
		pb.isHabitable = EditorGUILayout.Toggle (new GUIContent("Is Habitable: ", "Supports life."), pb.isHabitable);
		GUI.enabled = pb.isHabitable;
		if (!pb.isHabitable)
			pb.flora = false;
		pb.flora = EditorGUILayout.Toggle (new GUIContent("Flora: ", "Has planet life. Must be habitable."), pb.flora);
		GUI.enabled = pb.flora;
		if (!pb.flora)
			pb.fauna = false;
		pb.fauna = EditorGUILayout.Toggle (new GUIContent("Fauna: ", "Has animal life. Must have flora."), pb.fauna);
		GUI.enabled = true;
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ("box");
		pb.intelligentCreatures = EditorGUILayout.Toggle (new GUIContent("Intelligent Creatures: ", "Has intelligent life. Native or otherwise."), pb.intelligentCreatures);
		GUI.enabled = pb.intelligentCreatures;
		if (!pb.intelligentCreatures)
			pb.icPopulation = 0;
		pb.icPopulation = EditorGUILayout.IntField (new GUIContent("Intelligent Population: ", "Must have intelligent life."), pb.icPopulation); 				//if it has intel creatures, how many;
		GUI.enabled = true;

		EditorGUILayout.EndVertical ();
		EditorGUILayout.EndVertical ();
	}

}
