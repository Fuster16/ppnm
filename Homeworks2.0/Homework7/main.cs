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

	// Sine functions 

	double a1 = 0;
	double b1 = 2*PI;
	double c1 = 2; // elevation

	Func<vector,double> asin = x => sfun.asin(x, a1, b1); // area of sine function from a1 to b1
	Func<vector,double> esin = x => sfun.esin(x, c1, a1, b1); // area of elevated sine function from a1 to b1

	// Sine functions

	// Parabola

	vector c = new vector(0,0,1);
	//c[0] = 0; c[1] = 0; c[2] = 1; // sets vector values

	double a2 = -1;
	double b2 = 1;

	Func<vector,double> para = x => sfun.parabola(x, c, a2, b2); // area of given parabola from a2 to b2
	
	// Parabola

	// "volumes"

	vector A0 = new vector(0,0);
	vector B0 = new vector(2*PI,3);

	vector A1 = new vector(0,-1);
	vector B1 = new vector(2*PI,1);

	vector A2 = new vector(-1,-1);
	vector B2 = new vector(1,1);

	// "volumes"

	int N = 8; // number of samples
	int Bool = 1;

	// Opens outstream for plots

	if( outfile != null && Bool == 1 ){ // if we have an outfile --> stream data into it

		var outstream=new System.IO.StreamWriter(outfile,append:false); // opens outstream

		int k = 1;
		for(int j = 1; j <N; j++){ // Itteration from 1st to N'th order 

			if( j>6) k = 0;

			for(int i = (int)Pow(10,j); i < Pow(10,j+1); i += (int)Pow(10,j-k)){ // Each order has 9 samples: "for(int i = 10; i < 100; i += 10)", "for(int i = 100; i < 1000; i += 100)"

				var I1 = montecarlo.plainmc(asin, A1,B1,i);
				var I4 = montecarlo.plainmc(sfun.ucirc, A2,B2,i);
				
				// Quasi-random Monte-Carlo integrations
				var Q1 = montecarlo.quasimc(asin, A1,B1,i); 
				var Q2 = montecarlo.quasimc(sfun.ucirc, A2,B2,i);

				outstream.WriteLine($"{i} {1/Sqrt(i)} {I4.Item1} {I4.Item2} {Abs(PI - I4.Item1)} {I1.Item1} {I1.Item2} {Abs(I1.Item1)} {Math.PI} {Q2.Item1} {Q2.Item2} {Abs(PI - Q2.Item1)} {Q1.Item1} {Q1.Item2} {Abs(Q1.Item1)}");
			}

		}

		outstream.Close(); // Important

	}

	// Opens outstream for plots

	var I11 = montecarlo.plainmc(asin, A1,B1,(int)Pow(10,N));
	var I2 = montecarlo.plainmc(esin, A0,B0,(int)Pow(10,N));
	var I3 = montecarlo.plainmc(para, A2,B2,(int)Pow(10,N));
	var I44 = montecarlo.plainmc(sfun.ucirc, A2,B2,(int)Pow(10,N));

	/// A.3 problem

	vector A3 = new vector(Pow(2,-8),Pow(2,-8),Pow(2,-8),0);
	vector B3 = new vector(PI,PI,PI,44000);

	Func<vector,double> I = x => sfun.integral(x, A3, B3)/Pow(PI,3);
	var I5 = montecarlo.plainmc(I, A3,B3,(int)Pow(10,9));

	// A.3 problem

	string text = File.ReadAllText(@"Homework7.txt");

	WriteLine(text, I44.Item1, I11.Item1, I2.Item1, I3.Item1, I5.Item1);

}// Main

}//main
