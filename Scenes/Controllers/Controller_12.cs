//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System.Collections.Generic;
using UnityEngine;

namespace EPPZ.Geometry.Scenes {

	using Lines;
	using Model;

	/// <summary>
	/// 10. Multiple polygon centroid
	/// </summary>
	public class Controller_12 : MonoBehaviour {

		public Source.Polygon polygonStart;
		public Source.Polygon[] polygonSubtractionSources;
		// List<Polygon> polygons = new List<Polygon> ();

		[SerializeField]
		PolygonLineRenderer resultLine;

		[SerializeField]
		Source.Mesh resultMesh;

		List<Source.Mesh> resultMeshes = new List<Source.Mesh> ();

		void Update () {
			// Collect polygons.
			List<Polygon> differencePolygons = new List<Polygon> ();
			differencePolygons.Add (polygonStart.polygon);
			foreach (Source.Polygon eachPolygonSource in polygonSubtractionSources) {

				List<Polygon> replacedDifferencePolys = new List<Polygon> ();
				for (int i = 0; i < differencePolygons.Count; i++) {
					differencePolygons[i].AddPolygon (eachPolygonSource.polygon);
					replacedDifferencePolys.AddRange (differencePolygons[0].DifferencePolygon ());
				}
				differencePolygons = replacedDifferencePolys;
			}

			for (int i = 0; i < resultMeshes.Count; i++) {
				GameObject.Destroy (resultMeshes[i].gameObject); // Destroy old.
			}
			resultMeshes.Clear();
			for (int i = 0; i < differencePolygons.Count; i++) {
				GameObject newGO = GameObject.Instantiate (resultMesh.gameObject);
				newGO.GetComponent<PolygonLineRenderer> ().polygon = differencePolygons[i];
				newGO.GetComponent<Source.Mesh> ().polygon = differencePolygons[i];
				newGO.SetActive (true);
				resultMeshes.Add(newGO.GetComponent<Source.Mesh> ());
			}

			resultMesh.gameObject.SetActive (false);
		}
	}
}