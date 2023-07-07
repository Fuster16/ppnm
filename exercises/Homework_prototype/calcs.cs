using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class calcs{

	public static matrix RRSM(int seed = 10){ // Random Real Symmetric Matrix

		matrix A = new matrix(5,5); // Matrix A of reasonable size
		Random a = new Random(seed);  // random integer with seed

		for(int i=0;i<A.size1 - 1;i++){ // Initializing A to be a (real) symmetric matrix with random indices
			for(int j = i+1 ; j<A.size1; j++){
				A[i,j] = a.Next(100);
				A[j,i] = A[i,j]; // Should work?
			}
		}

		for(int i= 0; i < A.size1 ; i++){
			A[i,i] = a.Next(100);
		}

		return A;
} // RandomM

	public static string MTS(matrix A, string s =""){ // Matrix to string

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