using System;
using System.IO;
using static System.Math;
using static System.Console;


public class ann{ // The artificial neural network class, "ann" for short

private int N; // Size of a given network --> private as to not be meddled with.
public int size => N; // size method to the network. 

private matrix p; // parameters matrix of size (3,N) because i like this structure and i think it's less prone to err
//public matrix parameters => p; // parameter return

public double this[int r, int c]{ // get/set method for parameters
	get => p[r,c];
	set => p[r,c]=value;
}

private int s; // connected to the trainer method.
public int status => s; // is the network trained? If not --> returns 0. If yes --> returns nr. Of itterations it took to train

private Func<double,double>[] Functions = new Func<double,double>[4]{

	x => -0.5*Exp(-x*x), 			// Analytic anti-derivative
	x => x*Exp(-x*x), 			// activation function
	x => (1 - 2*x*x)*Exp(-x*x), 		// Analytic first derivative.
	x => 2*x*(2*Pow(x,2) - 3)*Exp(-x*x) 	// Analytic second derivative.

};

public Func<double,double> funcs(int n) => Functions[n];

public ann(int n){ // constructor

	N = n;

	p = new matrix(3,n);
	
	for(int j = 0; j<n; j++){ // 
			
		this[0,j] = 0; 
		this[1,j] = 1;
		this[2,j] = 1;
			
	}

	s = 0;

}

public double response(double x, int k = 1){

	double sum = 0;

	for(int i = 0; i < this.size; i++){

		sum += Pow(this[1,i],1-k)*(Functions[k])((x - this[0,i])/this[1,i])*this[2,i];

	}
	return sum; // returns the sum of activation functions.

} // response

public void train(vector x, vector y, double MaxItt = 1000, double acc = 0.01, matrix guess = null){ // table to interpolate. Takes guess in matrix form	--> less prone to err

	vector g = new vector(3*this.size);

	if(guess != null ){ // Setting guess-vector values;

		for(int j = 0; j<3; j++){

			for(int k = 0; k<this.size; k++) g[j + k*3] = guess[j,k];

		}

	}
	else{

		for(int j = 0; j<this.size; j++){
			
			g[j*3] = (j -1.0)/N; 
			g[1+j*3] = 1;
			g[2+j*3] = 1;
			
		}

	}

	Func<vector,double> cost = p => 
	{

		for(int j = 0; j<3; j++){ // takes vector input and sets corresponding parameter matrix values 

			for(int k = 0; k< this.size; k++) this[j,k] = p[j + k*3];

		}

		double c = 0;
		
		for(int i=0;i<x.size;i++) c += Pow(response(x[i])-y[i],2);
		
		return c;

	};
	
	vector Size0 = g.map( t => 0.5);

	vector p_min = minimize2.simplex(cost,g, 20000, 0.1, Size0);

	p_min = minimize2.qnewton(cost,p_min, 10000,false, 0.01);

	for(int j = 0; j<3; j++){

		for(int k = 0; k<this.size; k++) this[j,k] = p_min[j + k*3];

	}

	s = 1; // (int)p_min[3*N] // network is trained with s# of itterations --> response method is "open"

} // train

}//ann