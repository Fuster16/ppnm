using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class calcs{

	public static matrix RRM(int n,int m, int seed = 10){ // Random Real Matrix

		matrix A = new matrix(n,m); // Matrix A of size (n,m)
		Random a = new Random(seed);  // random integer with seed

		for(int i=0;i<n;i++){ // Initializing A to be a (real) matrix with random indices
			for(int j = 0 ; j<m; j++){
				A[i,j] = a.Next(100); // Generates new random integer in the interval [0,99] with given seed
			}
		}

		return A;
} //RRM

	public static vector RRV(int n, int seed = 10){ // Random Real Vector

		vector v = new vector(n); // Vector v of size n
		Random b = new Random(seed);  // random integer with seed

		for(int i=0;i<n;i++){ // Initializing v to be a (real) vector with random indices
			v[i] = b.Next(100); // Generates new random integer in the interval [0,99] with given seed
		}

		return v;
} //RRV

	public static string MTS(matrix A, string s =""){ // Matrix to string. Crude, but gets the job done

		for(int i=0;i<A.size1;i++){
			s += "\n(";
			for(int j=0;j<A.size2;j++){
				s += $" {A[i,j]}  ";
			}
			s += ")\n";
		}

		return s;
} // MTS

	public static string VTS(vector v, string s =""){ // Vector to string

		for(int i=0;i<v.size;i++){

			s += $"\n({v[i]})";

		}

		return s;
} // VTS

	public static vector cmdread(string[] args){
	
	vector v = new vector(2);
	int i = 0;

	foreach(string arg in args){ // goes through each string in args
		string[] words = arg.Split(':'); // for each string element in args make a new string[] array and fill with elements split at ':' of given arg.
		v[i] = double.Parse(words[1]);
		i+=1;
	}
	return v;

} //cmdread

} //calcs