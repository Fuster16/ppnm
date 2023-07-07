using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class damped{

	public static void Main(){
	
	// RKF45

	matrix A = new matrix($" 0.25 0 0 0 0 ; {3.0/32} {9.0/32} 0 0 0 ; {1932.0/2197} {-7200.0/2197} {7296.0/2197} 0 0 ; {439.0/216} -8 {3680.0/513} {-845.0/4104} 0 ; {-8.0/27} 2 {-3544.0/2565} {1859.0/4104} {-11.0/40} "); // Unsure if matrix can be written with /n

	//string A_string = calcs.MTS(A, "A = ");
	//WriteLine(A_string);

	vector c = new vector(new double[6] {0, 0.25, 3.0/8, 12.0/13, 1.0, 0.5});
	vector b1 = new vector(new double[6] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
	vector b0 = new vector(new double[6] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});
	
	//string c_string = calcs.VTS(c, "c = ");
	//WriteLine(c_string);

	// RKF45

	Func<double,vector,double> EQ1 = (t,X) => 1 - X[0];
	Func<double,vector,vector> EQ1_vectorized = (x,y) => ODE.Y(x, y, EQ1); // Vectorized ODE

	Func<double,vector,double> EQ3 = (t,X) => 1 - (1 - 0.01*X[0])*X[0];
	Func<double,vector,vector> EQ3_vectorized = (x,y) => ODE.Y(x, y, EQ3); // Vectorized ODE

	
	double a = 0.0; // initial time
	vector E1 = new vector(1.0 - 1.0/128,1.0/128); // initial vector1 
	vector E3 = new vector(1.0,-0.5); // initial vector 2 and 3
	double b = 2*Math.PI; // end time
	
	var x1 = new ODE.genlist<double>(); var y1 = new ODE.genlist<vector>();
	var x2 = new ODE.genlist<double>(); var y2 = new ODE.genlist<vector>();
	var x3 = new ODE.genlist<double>(); var y3 = new ODE.genlist<vector>();
	

	vector y1_b = ODE.driver(EQ1_vectorized, a, E1, b, x1, y1, A, b1, c, b0, 0.000001,0.000001, 0.000001);
	vector y2_b = ODE.driver(EQ1_vectorized, a, E3, b, x2, y2, A, b1, c, b0, 0.0001,0.0001,0.0001);
	vector y3_b = ODE.driver(EQ3_vectorized, a, E3, b, x3, y3, A, b1, c, b0, 0.0001,0.0001,0.0001);
	
	for(int i = 0; i < x1.size; i++){

		WriteLine($"{x1[i]} {(y1[i])[0]} {(y1[i])[1]}");

	}
	WriteLine("\n");

	for(int i = 0; i < x2.size; i++){

		WriteLine($"{x2[i]} {(y2[i])[0]} {(y2[i])[1]}");

	}
	WriteLine("\n");

	for(int i = 0; i < x3.size; i++){

		WriteLine($"{x3[i]} {(y3[i])[0]} {(y3[i])[1]}");

	}
	WriteLine("\n");

}//Main

}//damped
