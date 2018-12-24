using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habb : ScriptableObject {

	string[] alphabet;
	List<double[]> weight;

	public string Process(PatternSet ps, double[] S) {
        
		weight = ps.Weight();
		alphabet = ps.GetNames();

		double[] A;
		double[] Z;
		double[] Y;

		Z = Z_process(S);
		A = A_process(Z);
		Y = Y_process(A);

		for(int i=0;i<alphabet.Length; i++)
			if (Y[i] == 1)
				return alphabet[i];

		return null;
	}

	private double[] Z_process( double[] S) {
		double[] Z = new double[alphabet.Length];
		for (int i = 0; i < alphabet.Length; i++) {
			Z[i] = S.Length / 2.0;
			for (int j = 0; j < S.Length; j++)
				Z[i] += S[j] * weight[i][j];
		}
		return Z;
	}

	private double[] A_process( double[] Z) {
		double[] A = new double[alphabet.Length];

		int count=0;
		double eps = 0.001;
		double e = 1.0 / alphabet.Length;

		for (int i = 0; i < A.Length; i++)
			A[i] = Z[i] / 10;

		do {
			double[] A_New = new double[alphabet.Length];
			count = 0;

			for (int i = 0; i < A.Length; i++) {
				double new_weight = A[i];

				for (int j = 0; j < A.Length; j++)
					if (i != j)
						new_weight -= e * A[j];
				if (new_weight < 0)
					new_weight = 0;

				A_New[i] = new_weight;
				if (Mathf.Abs((float)(A_New[i] - A[i])) > eps)
					count++;
			}

			A = A_New;
			A_New = null;

		} while (count > 0);
		return A;
	}

	private double[] Y_process(double[] A) {
		double[] Y = new double[alphabet.Length];
		for(int i = 0; i < alphabet.Length; i++)
			Y[i] = A[i] > 0 ? 1 : 0;
		return Y;
	}
}
