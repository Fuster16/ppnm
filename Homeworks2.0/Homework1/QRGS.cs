// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.

using System;
using static System.Math;

public static class QRGS{

public static (matrix,matrix) decomp(matrix A){ // (matrix,matrix) indicates expected output form, decomp(matrix A) indicate method for the QRGS class, which takes matrix A as argument.
	
	matrix Q=A.copy(); // Makes a copy of A, as not to change it throughout
	matrix R=new matrix(A.size2,A.size2); // creates new matrix R, which is expected to be an upper triangular m x m matrix (m is the width of A)

	int m = A.size2; // Better to define this here? Or should i substitute m for A.size2 below?
	
	for(int i=0; i<m; i++){ // Initiating a for-loop from 0'th element to the m'th over i

		R[i,i] = Q[i].norm(); // the [i,i]'th element in R is the norm of the i'th coloumn of Q, found using Q[i] (For 0'th we have Q[0] = A[0], cause we haven't changed Q yet)
		Q[i] /= R[i,i] ; // Sets Q[i] --> Q[i]/R[i,i] (Q[0] = A[0]/A[0].norm()). The i'th coloumn vector of Q just got divided by it's norm.

		for(int j=i+1; j<m; j++){ // Initiating a for-loop from (i+1)'th element to the m'th element over j

			R[i,j] = Q[i].dot(Q[j]); // Fills like R[0,j] = Q[0].dot(A[j]), since Q[j] = A[j] for Q[0] (which they are)
			Q[j] -= Q[i]*R[i,j]; // Sets Q[j] --> Q[j] - Q[i]*R[i,j]. For i=0 we have Q[j] --> A[j] - Q[0]∗R[0,j] = A[j] - A[0].dot(A[j])/(A[0].norm())^2 
		
		} 
	}

	return (Q,R); // returns according to the expected, indicated form.

} //decomp
	

// Additional functions, naturally connected to the QRGS class   
   
public static vector solve(matrix Q, matrix R, vector b){ // Indicates expected output ot be a vector, solve(matrix Q, matrix R, vector b) indicates method takes Q and R matrix + some b vector

	vector c = (Q.T)*b; // Necessary? 

	for(int i = (c.size -1);i>=0;i--){ // Back-substitution on R

		double sum=0;

		for(int k = i+1; k<c.size; k++){ 
			sum += R[i,k]*c[k];
		}
		c[i] = (c[i]- sum)/R[i,i];
	} 

	return c; // returns solution vector.
} //solve

public static double det(matrix R){ // Assumes R is right-triangular. A general determinant can be found from the relation det(A) = det(R), where A = QR, namely it's QR decomposition.
	
	int m = R.size1; // R is square, so either 1 or 2 will do
	double Pi = 1; // initiating product operator
	
	for(int i = 0; i<m;i++) Pi *= R[i][i]; // for looping over each diagonal element of R (Assumed right triangular)
	
	return Pi; // Returning the result
}


public static matrix inverse(matrix Q,matrix R){ // indicates the return of a matrix

	matrix B = Q.T; 

	for(int j = 0; j<Q.size1; j++){

		vector c = B[j]; // The solution vectors x_i, i = 1...m of A*x_i = e_i (where A is n x m) can be shown to be the i'th coloum vectors of the inverse of A. 
				 // By QR decomposition, A x_i = e_i <=> QR x_i = e_i <=> R x_i = Q^T e_i = Q^T[i] where Q^T[i] is a method defined in the matrix class and gives the i'th coloum vector of Q^T.
		for(int i = (c.size -1);i>=0;i--){ // Back-substitution on R

			double sum=0;

			for(int k = i+1; k<c.size; k++){ 
				sum += R[i,k]*c[k];
			}

			c[i] = (c[i]- sum)/R[i,i];
		}
	
		B[j] = c;
	}

	return B;

}//inverse

}// QRGS