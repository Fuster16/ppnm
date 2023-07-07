
using System;
using static System.Math;

public static class OLSF{ //Ordinary Least Squares fit

	public static (vector, matrix) lsfit
	(Func<double,double>[] fs, vector x, vector y, vector dy){ // Func<double,double>[] fs is an array of fitting functions

		int n = x.size, m = fs.Length; //defines two integer types on one line

		matrix A = new matrix(n,m); // prepare construction of associated ls-problem
		vector b = new vector(n);
		
		for(int i= 0;i<n;i++){ // initializes the ls-problem according to eq. (14) from course note 3

			b[i] = y[i]/dy[i];
	
			for(int j = 0; j<m; j++){

				A[i,j] = fs[j](x[i])/dy[i]; 

			}		
		}
		
		// Preparations have been made and we are now interrested in solving the linear eq. A*c = b for c.
		// For this we write A = QR ==> QRc = b <==> Rc = Q^Tb. Thus the c's 
		
		(matrix Q, matrix R) t = QRGS.decomp(A); // gives the QR decompostion of A
		vector c = QRGS.solve(t.Q,t.R,b); // solves for c
		matrix A_inv = QRGS.inverse(t.Q,t.R); 
		matrix S = A_inv*(A_inv.T); // S = (A^-1 * (A^-1)^T) = (A^-1 * (A^T)^-1) = (A^T *A)^-1. See eq. (10) from course note 3.
		
		return (c,S); // returns fitting coefficients vector c and covariance matrix S.

}//lsfit

	

}//OLSF