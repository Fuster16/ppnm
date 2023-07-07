using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(){

	/////// A

	matrix A = calcs.RRM(7,5);
	string A_string = calcs.MTS(A, "A = ");

	(matrix Q, matrix R) t1 = QRGS.decomp(A);

	string Q_string = calcs.MTS(t1.Q, "Q = ");
	string R_string = calcs.MTS(t1.R, "R = ");
	
	string Id_string = calcs.MTS(((t1.Q).T)*(t1.Q), "Q^T*Q = ");
	string QR_string = calcs.MTS((t1.Q)*(t1.R), "Q*R = ");

	Func<double,double>[] fs = { t => 1.0, t => t };

	// Fetching ThX data from Makefile
	char[] split_delimiters = {' ','\t','\n'};
	var split_options = StringSplitOptions.RemoveEmptyEntries;

	vector x = new vector(9);
	vector y = new vector(9);
	vector dy = new vector(9);
	
	int s = 0;
	for( string line = ReadLine(); line != null; line = ReadLine() ){

		var numbers = line.Split(split_delimiters,split_options);
		x[s] = double.Parse(numbers[0]);
		y[s] = double.Parse(numbers[1]);
		dy[s] = double.Parse(numbers[2]);
		s += 1;
       	}

	// Prparing for linear regression.
	vector z = y.map( t => Log(t));
	vector dz = new vector(9);

	for(int i = 0; i<9;i++){

		dz[i] = dy[i]/y[i];

 	}

	(vector c, matrix S) t2 = OLSF.lsfit(fs, x, z, dz);

	string c_string = calcs.VTS(t2.c, "c = ");


	/////// A

	/////// B
	
	double HL = Log(2)/(-t2.c[1]);
	double dHL = Log(2)*Sqrt(t2.S[1,1])/t2.c[1]/t2.c[1];

	string Interval = $" t - dt = {Log(2)/(-t2.c[1]) - dHL}		to		t + dt = {Log(2)/(-t2.c[1]) + dHL}";

	/////// B

	string text = File.ReadAllText(@"Homework3.txt");
	WriteLine(text, A_string, Q_string, R_string, Id_string, QR_string, c_string,
			HL, dHL, Interval);
	}
}
