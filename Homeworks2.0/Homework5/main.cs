using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(){

	// RKF45

	//matrix A = new matrix(" 0.25 0 0 0 0 ;" +
	//		      " 3.0/32 9.0/32 0 0 0 ;" +
	//		      " 1932.0/2197 -7200.0/2197 7296.0/2197 0 0 ;" +
	//		      " 439.0/216 -8 3680.0/513 -845.0/4104 0 ;" +
	//		      " -8.0/27 2 -3544.0/2565 1859.0/4104 -11.0/40 "
	//			); // Unsure if matrix can be written with /ne

	//matrix A = new matrix($" 0.25 0 0 0 0 ; {3.0/32} {9.0/32} 0 0 0 ; {1932.0/2197} {-7200.0/2197} {7296.0/2197} 0 0 ; {439.0/216} -8 {3680.0/513} {-845.0/4104} 0 ; {-8.0/27} 2 {-3544.0/2565} {1859.0/4104} {-11.0/40} "); // Unsure if matrix can be written with /n

	//vector c = new vector(new double[6] {0, 0.25, 3.0/8, 12.0/13, 1, 0.5});
	//vector b = new vector(new double[6] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
	//vector b0 = new vector(new double[6] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});

	matrix A = new matrix(" 0.5 "); // rkstep12 A_matrix
	vector b = new vector(0,1.0); // rkstep12 b_i coeffecients
	vector c = new vector(0,0.5); // rkstep12 c_i coeffecients

	vector b0 = new vector(1.0,0); // rkstep12 b0_i coefficients.


	// RKF45

	// Butcher's tableau
	
	matrix B = new matrix(A.size1 + 3, A.size2 + 2);

	for(int i = 0; i<c.size-1;i++){
		
		B[i,0] = c[i];
		B[c.size,i+1] = b[i];
		B[c.size+1,i+1] = b0[i];

		for(int j = 0; j<=i; j++){
			
			B[i+1,j+1] = A[i,j];
		}

	}
	B[c.size-1,0] = c[c.size-1];
	B[c.size,B.size2-1] = b[c.size-1];
	B[c.size+1,B.size2-1] = b0[c.size-1];

	// Butcher's tableau

	string B_string = calcs.MTS(B, "B = "); // maybe works, maybe doesn't

	


	string text = File.ReadAllText(@"Homework5.txt");
	WriteLine(text, B_string);	
	}
}
