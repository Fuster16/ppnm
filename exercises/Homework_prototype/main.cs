using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(string[] args){ // Main function takes
						// rmax and rd values from command line

	/////// A.1 - A.2

	int seed = 10;
	Random rnd = new Random(seed);	

	matrix A = calcs.RRSM(seed);
	string A_string = calcs.MTS(A, "A = ");

	int p = rnd.Next(A.size1);
	int q = rnd.Next(A.size2);
	
	(int n, int m) t1 = (p+1, q+1);

	double apq = A[p,q], app = A[p,p], aqq = A[q,q];
	double theta = 0.5*Atan2(2*apq,aqq-app);

	jacobi.timesJ(A, p, q, theta);
	jacobi.Jtimes(A, p, q, theta);

	string A_string2 = calcs.MTS(A, "A = ");

	/////// A.1 - A.2

	/////// A.3

	matrix M = calcs.RRSM(seed);
	string M_string = calcs.MTS(M, "M = ");

	(vector w, matrix V) t2 = jacobi.cyclic(M);

	string V_string = calcs.MTS(t2.V, "V = ");
	string w_string = calcs.VTS(t2.w, "w = ");
	
	matrix D = (((t2.V).T)*M)*t2.V;
	matrix M1 = ((t2.V)*D)*(t2.V).T;
	matrix Id = (t2.V)*((t2.V).T);
	matrix IdT = ((t2.V).T)*(t2.V);

	string D_string = calcs.MTS(D, "D = ");
	string M1_string = calcs.MTS(M1, "M1 = ");
	string Id_string = calcs.MTS(Id, "Id = ");
	string IdT_string = calcs.MTS(IdT, "Id^T = ");
	
	/////// A.3

	/////// B.1

	vector R = calcs.cmdread(args);
	//(double rmax, double dr) t3 = (R[0],R[1]);
	
	(vector e, matrix f) t3 = Hatom.Rdiff(R[0],R[1]);
	
	string E0 = $"E0 = {(t3.e)[0]}";

	/////// B.1

	string text = File.ReadAllText(@"Homework2.txt");
	WriteLine(text, A_string, t1, theta, A_string2, M_string, V_string, w_string, 
			D_string, M1_string, Id_string, IdT_string,E0);
	}
}
