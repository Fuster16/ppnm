using System;
using static System.Math;

public static class jacobi{ // The class we are implementing the EVD algorithm under


public static void timesJ(matrix A, int p, int q, double theta){ // function which changes matrix A --> A*J(p,q,theta), where J(p,q,theta) is the Jacobi rotation matrix of the element A_pq rotating with angle = theta 
	
	double c=Cos(theta),s=Sin(theta); // initializing the sine and cosine

	for(int i=0;i<A.size1;i++){ // A is assumed square --> .size1 <=> .size2

		double a_ip=A[i,p], a_iq=A[i,q]; // parallel indice definition --> We ready a for-loop over the p'th and q'th coloum vectors of A (the only coloum vectors of J[i] =\= id[i] are i = p,q)

		A[i,p]=c*a_ip-s*a_iq; 		 // the p'th coloum vector of J have c in the p'th indice and -s in the q'th --> A'[p] = c*A[p]-s*A[q]

		A[i,q]=s*a_ip+c*a_iq; 		 // the q'th coloum vector of J have s in the p'th indice and c in the q'th ---> A'[q] = s*A[p]+c*A[q] (depends on A[p] and not A'[p], thus the reason for a_ip and a_iq. 
                                      		 // The above could have been computed using the Kronecker product (not implemented in code), however directly computing with indicies is faster and clearer)
	}

} //timesJ


public static void Jtimes(matrix A, int p, int q, double theta){ // Same as timesJ, except A --> A' = J^T*A = J^T (A^T)^T = (A^T *J)^T	   //( <=> (A')^T = (A^T)' = A^T *J )
								 // The elements of (A')^T are thus the transposed of A (matrix) multiplied with J from the right. (We can thus simply copy the stucture of the Jtimes function)
	
	double c = Cos(theta), s = Sin(theta);
	
	for(int j=0;j<A.size1;j++){

		double aT_jp = A[p,j], aT_jq = A[q,j]; // A[p,j] = A^T[j,p] = aT_jp

		A[p,j] = c*aT_jp - s*aT_jq; // We run timesJ equivalent on A^T, (A^T)'[j,p] = c*aT_jp - s*aT_jq = A'[p,j]
		A[q,j] = s*aT_jp + c*aT_jq; 
	}
} //Jtimes

public static (vector,matrix) cyclic(matrix M,double acc = 1e-4){

	matrix A=M.copy(); // We intend for the cyclic function to make a new matrix, not change M
	
	matrix V= new matrix(M.size1,M.size2); // Creates new Matrix V of size M
	V.setid(); // Function which sets diagonal elements of V to 1 AND off-diagonal to 0	  
	// If we had simply written "V = matrix.id(M.size1);" we would have gotten a matrix with 1 in the diagonal elements and non-initialized off-diagonal elements.
	
	vector w=new vector(M.size1); // vector of lenght M.size1. M is assumed square

	double Sq_Sum = 0;
	
	for(int i=0;i<M.size1-1;i++){ // Sets the sum of the squared off-diagonal elements of M
		for(int j=i+1;j<M.size2;j++){
			Sq_Sum += Pow(M[i,j],2);
		}
	}

	bool changed;

	double theta;
	double new_apq;

	do{
		changed=false; // Sets while-loop condition to false

		for(int p=0;p<A.size1-1;p++){ // Performing a sweep over each off-diagonal element. The sweep is over each row, implying a for-loop from the p +1 = 1'th row --> (n-1)'th row (n'th row include diagonal element only)
			
			for(int q=p+1;q<A.size2;q++){ // for-loop over the elements in the p'th row. q = p+1 since we only need to sweep over the upper elements of a real symmetric matrix in order to sweep all.

				double apq=A[p,q], app=A[p,p], aqq=A[q,q]; // Necessary to define in order to operate on? --> Ask Dmitri
				
				theta = 0.5*Atan2(2*apq,aqq-app); // For finding theta value which zeroes aqp elements //Atan2 from System.math library --> correctly returns inverse tangent for 0 values.			

				// double c=Cos(theta),s=Sin(theta); 
				// double new_app=c*c*app-2*s*c*apq+s*s*aqq; 
				// double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;

				if(Sq_Sum > acc) // do rotation if the sum of the squared off diagonal elements still is above tolerance
					{
					changed= true;

					timesJ(A,p,q, theta); // A --> A' = A*J // Changes A according to timesJ
					Jtimes(A,p,q, theta); // A --> A' = J.T*A // Changes A according to Jtimes
					new_apq = A[p,q];
				
					timesJ(V,p,q, theta); // V --> V' = V*J Updates the corresponding V matrix
					
					Sq_Sum -= Pow(apq,2) - Pow(new_apq,2); // Updates the sum of the squares. (This update follow from simple mathematical considerations on the off diagonal elements)
					}
			}
		}

	}while(changed); // If changed == true, do again.

	for(int i= 0; i< A.size1 ;i++){
		w[i] = A[i,i]; // sets the i'th value of w as the i'th diagonal element of A
	}

	/* run Jacobi rotations on A and update V */
	/* copy diagonal elements into w */

	return (w,V);

} //cyclic

} //jacobi