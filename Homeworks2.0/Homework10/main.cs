using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(string[] args){ // -output:Responses.txt

	string outfile = null; // "Responses.txt";

	foreach(var arg in args){ // sets outfile string values to the given data sheets.

		var words=arg.Split(':'); // Splits the command-line string[] at ":"

		if(words[0]=="-output") outfile = words[1]; // If "-output" is given, then the infile name must be words[1]

	}

	////// A

	ann A = new ann(6); // New neural network for the function in A

	Func<double,double> g = t => Cos(5*t-1)*Exp(-t*t);

	int size = 20;

	vector x = new vector(size);
	vector y = new vector(size);

	for(int i = 0; i<size; i++){

		x[i] = i*2.0/(size-1) - 1; // linspaced over interval [-1,1]
		y[i] = g(x[i]);
		
	}

	A.train(x,y);

	matrix Y = new matrix(size, 4); // Connected to question B --> The derivatives + anti-derivative is stored in the matrix Y

	for(int k = 0; k<4; k++){

		for(int j = 0; j < size ; j++){

			Y[j,k] = A.response(x[j],k);
			if( k == 0) Y[j,k] -= A.response(x[0],k);

		}

	}

	ann G1 = new ann(6); 
	ann G2 = new ann(6);

	vector y1 = new vector(size);
	vector y2 = new vector(size);

	// some functions
	Func<double,double> g1 = t1 => Exp(-t1*t1);
	Func<double,double> g2 = t2 => 5;

	for(int i = 0; i<size; i++){ // use the same interval 

		y1[i] = g1(x[i]);
		y2[i] = g2(x[i]);
		
	}
		
	G1.train(x,y1);
	G2.train(x,y2); 

	matrix Y1 = new matrix(size, 4); // Connected to question B --> The derivatives + anti-derivative is stored in the matrix Y
	matrix Y2 = new matrix(size, 4);

	for(int k = 0; k<4; k++){ // recording of the responses

		for(int j = 0; j < size ; j++){

			Y1[j,k] = G1.response(x[j],k);
			Y2[j,k] = G2.response(x[j],k);

		}

	}
	

	////// A

	////// B

	vector G = new vector(size); // Preparing for numerical anti-derivative on the interval [-1,1]
	vector g1st = new vector(size); // Preparing for numerical derivative
	vector g2nd = new vector(size); // Preparing for numerical 2nd derivative

	for(int i = 0; i<size; i++){

		G[i] = adaptive.integrate(g,-1,x[i]); // returns 0 for i = 0 as it should
		g1st[i] = FiniteDiff(g, x[i]);
		g2nd[i] = FiniteDiff(g, x[i], 2);

	}


	////// B

	// Opens outstream for plots

	
	int Bool = 1; 
	if( outfile != null && Bool == 1 ){ // if we have an outfile --> stream data into it

		var outstream=new System.IO.StreamWriter(outfile,append:false); // opens outstream


		for(int j = 1; j < size; j++){ 

			//// We consider k=1, the response of the system, and compare to the given functions
			outstream.WriteLine($"{x[j]} {y[j]} {Y[j,1]} {y1[j]} {Y1[j,1]} {y2[j]} {Y2[j,1]}");
		}

		outstream.WriteLine(); outstream.WriteLine();
		for(int j = 1; j < size; j++){ 
			
			//// We consider k=0,2,3 (Anti-derivative, derivative, second derivative) for the given function in question A
			outstream.WriteLine($"{x[j]} {G[j]} {Y[j,0]} {g1st[j]} {Y[j,2]} {g2nd[j]} {Y[j,3]}");

		}

		outstream.Close(); // Important

	}

	// Opens outstream for plots

	string text = File.ReadAllText(@"Homework10.txt");
	WriteLine(text);

} // Main

	public static double FiniteDiff(Func<double,double> f, double x, int n = 1){

		double h = Pow(2,-26)*Abs(x);

		double sum = 0;

		for(int i = 0; i <= n; i++){

			sum += Pow(-1,i)*Factorial(n)/Factorial(i)/Factorial(n-i)*f(x + (n/2 - i)*h);

		}

		return sum/Pow(h,n); // central n'th order finite difference
	
} // FiniteDiff

	public static double Factorial(int m){

		if(m == 0) return 1.0;

		return Factorial(m-1)*m;

} // Factorial

} // main
