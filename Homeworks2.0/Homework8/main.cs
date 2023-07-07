using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(string[] args){

	string outfile = null;

	foreach(var arg in args){ // sets outfile string values to the given data sheets.

		var words=arg.Split(':'); // Splits the command-line string[] at ":"

		if(words[0]=="-output") outfile = words[1]; // If "-output" is given, then the infile name must be words[1]

	}

	/////// A

	Func<vector, vector> f1 = x => sfun.gradRosenbrock(x);
	Func<vector, vector> f2 = x => sfun.gradsfun(x);
	Func<vector, vector> f3 = x => sfun.sine(x);

	//// Newton method

	vector g1 = new vector(1.1,0.9); // guess for f1
	vector g2 = new vector(0.1,0.1);
	vector g3 = new vector(PI/1.5);

	vector r1 = roots.newton(f1, g1); // finding root for f1 by guess of g1
	vector r2 = roots.newton(f2, g2);
	vector r3 = roots.newton(f3, g3);

	////Newton method

	//// Checking results

	double z1 = f1(r1).dot(f1(r1)); // first zero
	double z2 = f2(r2).dot(f2(r2));
	double z3 = f3(r3).dot(f3(r3));

	//// Checking results

	/////// A

	/////// B

	vector F0 = new vector((Pow(2,10) - 1.0)/Pow(2,20), 1 - 2.0/Pow(2,10)); // initial vector

	//WriteLine($"({F0[0]},{F0[1]})");
	
	matrix A = new matrix($" 0.25 0 0 0 0 ; {3.0/32} {9.0/32} 0 0 0 ; {1932.0/2197} {-7200.0/2197} {7296.0/2197} 0 0 ; {439.0/216} -8 {3680.0/513} {-845.0/4104} 0 ; {-8.0/27} 2 {-3544.0/2565} {1859.0/4104} {-11.0/40} "); // Unsure if matrix can be written with 
	vector c = new vector(new double[6] {0, 0.25, 3.0/8, 12.0/13, 1.0, 0.5});
	vector b1 = new vector(new double[6] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
	vector b0 = new vector(new double[6] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});

	//matrix A = new matrix("0.5"); // rkstep12 A_matrix
	//vector b1 = new vector(0,1); // rkstep12 b_i coeffecients
	//vector c = new vector(0,0.5); // rkstep12 c_i coeffecients
	//vector b0 = new vector(1,0); // rkstep12 b0_i coefficients.

	Func<vector,vector> F_E = x => roots.M(x, 1.0/Pow(2,10), F0, 8.0, A, b1, c, b0, 0.01,0.01,0.01);

	vector g0 = new vector(-0.52);

	vector E_0 = roots.newton(F_E, g0, 10);


	//// Plots

	// Opens outstream for plots

	int Bool = 1;

	if( outfile != null && Bool == 1 ){ // if we have an outfile --> stream data into it

		var outstream=new System.IO.StreamWriter(outfile,append:false); // opens outstream
		
		vector plot = new vector(1);

		for(int i = 0; i<= 10; i++){
		
			plot[0] = i*2.0/100 - 0.6;
			outstream.WriteLine($"{i*2.0/100 - 0.6}");

		}

		outstream.Close(); // Important

	}

	// Opens outstream for plots

	//// Plots

	/////// B

	string text = File.ReadAllText(@"Homework8.txt");
	WriteLine(text, z2, z3, r2[0], r2[1], r3[0],z1, r1[0],r1[1],E_0[0]);

	} // Main

} // main
