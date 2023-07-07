using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class rkxy{

	public static void Main(){

	// RKF45

	//matrix A = new matrix(" 0.25 ;" +
	//		      " 3.0/32 9.0/32 ;" +
	//		      " 1932.0/2197 -7200.0/2197 7296.0/2197 ;" +
	//		      " 439.0/216 -8 3680.0/513 -845.0/4104 ;" +
	//		      " -8.0/27 2 -3544.0/2565 1859.0/4104 -11.0/40"
	//			); // Unsure if matrix can be written with /n

	//matrix A = new matrix(" 0.25 ; 3.0/32 9.0/32 ; 1932.0/2197 -7200.0/2197 7296.0/2197 ; 439.0/216 -8 3680.0/513 -845.0/4104 ; -8.0/27 2 -3544.0/2565 1859.0/4104 -11.0/40 "); // Unsure if matrix can be written with /n

	//vector c = new vector(new double[6] {0, 0.25, 3.0/8, 12.0/13, 1, 0.5});
	//vector b1 = new vector(new double[6] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
	//vector b0 = new vector(new double[6] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});

	matrix A = new matrix(" 0.5 "); // rkstep12 A_matrix
	vector b1 = new vector(0,1); // rkstep12 b_i coeffecients
	vector c = new vector(0,0.5); // rkstep12 c_i coeffecients

	vector b0 = new vector(1,0); // rkstep12 b0_i coefficients.

	// RKF45

	Func<double,vector,double> SHM = (t,X) => -X[0]; // Simpel Harmonic Motion
	Func<double,vector,vector> SHM_vectorized = (x,y) => ODE.Y(x, y, SHM); // Simpel Harmonic Motion

	double a = 0; // initial time
	vector ya = new vector(0,1); // initial vector 
	double b = 2*Math.PI; // end time
	

	(ODE.genlist<double> x12, ODE.genlist<vector> y12) t1 = ODE.driverXY(SHM_vectorized, a, ya, b);
	(ODE.genlist<double> x45, ODE.genlist<vector> y45) t2 = ODE.driverXY(SHM_vectorized, a, ya, b, A, b1, c, b0); 
	
	for(int i = 0; i < t1.x12.size; i++){

		WriteLine($"{t1.x12[i]} {(t1.y12[i])[0]} {(t1.y12[i])[1]}");

	}
	WriteLine("\n");

	for(int i = 0; i < t2.x45.size; i++){

		WriteLine($"{t2.x45[i]} {(t2.y45[i])[0]} {(t2.y45[i])[1]}");

	}
	WriteLine("\n");

	}//Main
}//rkxy
