using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public class Habb : MonoBehaviour {

	const int SCount = 36;
	List<double[]> weight;
	List<double> w0;
	List<string> alphabet;
	List<double[]> excersices;

	public void Init (PatternSet ps)
	{
		// print ("started init");
		alphabet = new List<string>();
		weight = new List<double[]>();
		excersices = new List<double[]>();
		w0 = new List<double>();
		// print ("wtf");
		foreach (var v in ps.patterns) {
			// print (v.name);
			weight.Add (new double[SCount]);
			alphabet.Add(v.name);
			excersices.Add(v.BipolarVector());
			w0.Add(0f);
			// print (v.name);
			TrainWeight(excersices.Last(), excersices.Count - 1);
			// print (v.name);
			Teach();
		}
	}

	private void Teach() {
		bool success = true;
		int numb = 0;
		int i = 0;

		for (; i < excersices.Count && success; i++)
		{
			double[] y = new double[excersices.Count];
			for (int j = 0; j < excersices.Count; j++)
			{
				y[j] = i == j ? 1 : -1;
			}
			if (!IsTeached(excersices[i],y))
			{
				numb = i;
				success = false;
			}
		}
		int iter = 0;
		i = 0;
		while (!success && iter < 100000) {

			TrainWeight(excersices[numb], numb);

			success = true;

			for (; i < excersices.Count && success; i++) {
				double[] y = new double[excersices.Count];
				for (int j = 0; j < excersices.Count; j++) {
					y[j] = i == j ? 1 : -1;
				}
				if (!IsTeached(excersices[i], y))  {
					numb = i;
					success = false;
				}
			}
			iter++;
		}
		print (iter);
		if (iter == 100000) Error.TrainErrorMessage (alphabet[i]);
	}

	private void TrainWeight(double[] x,int numb)
	{
		double[] y = new double[excersices.Count];
		for (int i = 0; i < excersices.Count; i++) {
			y[i] = i == numb ? 1 : -1;
		}

		for (int i = 0; i < weight.Count; i++) {
			w0[i] += 1 * y[i];

			for (int j = 0; j < SCount; j++) {
				weight[i][j] += x[j] * y[i];
			}
		}
	}

	private bool IsTeached(double[] x, double[] t)
	{
		double[] y = new double[t.Length];
		double S = 0;

		for (int i = 0; i < excersices.Count; i++) {
			S = w0[i];
			for (int j = 0; j < SCount; j++) {
				S += weight[i][j] * x[j];
			}

			y[i] = S > 0 ? 1 : -1;

			if (y[i] != t[i])
				return false;
		}
		return true;
	}

	public string Process(double[] x) //Распознавание изображения
	{
		double[] Neurons = new double[excersices.Count];
		int answer=-1;

		for(int i=0; i<excersices.Count; i++) {
			Neurons[i] = w0[i];
			for (int j = 0; j < x.Length; j++) {
				Neurons[i] += weight[i][j] * x[j];
			}
			Neurons[i] = (Neurons[i] > 0) ? 1 : -1;
		}

		for (int i = 0; i < Neurons.Length; i++)
			if (Neurons[i] > 0) {
				if (answer < 0)
					answer = i;
				else return " ";
			}

		return answer < 0 ? " " : alphabet[answer];
	}
}
