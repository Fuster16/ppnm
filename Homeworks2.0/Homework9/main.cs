using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(){

	////// A

	Func<vector,double> Rosen = x => Pow( (1-x[0]) , 2 ) + 100*Pow( (x[1] - x[0]*x[0]) , 2 );
	Func<vector,double> Himmel = x => Pow( x[0]*x[0] + x[1] - 11 ,2 ) + Pow( x[0] + x[1]*x[1] - 7 , 2 );
	
	vector Rg1 = new vector(1.1,0.9);		// Guess for Rosen --> Rosen >= 0 so...
	vector Rg2 = new vector(0.1,-0.1);		// i mistakenly thought there were a minimum at (0,0)!! But the minimizer is cool and proved me wrong
	vector Hg1 = new vector(2.5,2.5);		// guess is some symmetric x,y which yield ~~ 9 (average of 11 and 7) --> x^2 + x = 9 => x ~~ 2.5 or -3.5 
	vector Hg2 = new vector(-3.5,-3.5);

	string qR1 = calcs.VTS(minimize.qnewton(Rosen,Rg1,100, true));
	string qR2 = calcs.VTS(minimize.qnewton(Rosen,Rg2,100, true));
	string qH1 = calcs.VTS(minimize.qnewton(Himmel,Hg1,100,true));
	string qH2 = calcs.VTS(minimize.qnewton(Himmel,Hg2,100,true));

	////// A

	////// B

	//// Fetching Higgs data

	var energy = new genlist<double>();
	var signal = new genlist<double>();
	var error  = new genlist<double>();

	var separators = new char[] {' ','\t'};
	var options = StringSplitOptions.RemoveEmptyEntries;

	do{

	        string line = ReadLine(); 	// reads line of .txt and puts into string 
	        if(line==null)break; 		// stops once there are no more lines

	        string[] words=line.Split(separators,options);

	        energy.add(double.Parse(words[0]));
	        signal.add(double.Parse(words[1]));
	        error.add(double.Parse(words[2]));

	}while(true);

	//// Doing the minimization

	Func<vector,double> D = x => Deviation(x, energy, signal, error);

	vector guess = new vector(126, 2,10);
	string Fit = calcs.VTS(minimize.qnewton(D,guess,100, true, 0.001));
	////// B

	string text = File.ReadAllText(@"Homework9.txt");
	WriteLine(text,qR1,qR2, qH1, qH2,Fit);

	} // Main

	public static double Deviation(vector x, genlist<double> E, genlist<double> s, genlist<double> err){

		double sum = 0;

		for(int i =0; i < E.size; i++){

			double BreitWigner = x[2]/(Pow(E[i]-x[0],2) + x[1]*x[1]/4);

			sum += Pow((BreitWigner - s[i])/err[i],2 );

		}

		return sum;

} // deviation

	public class genlist<T>{               /* "T" is the type parameter */
   		
		private T[] data;                   /* we keep items in the array "data" */
   		public int size => data.Length;     /* I think that "size" sounds better than "Length" */
   		// public T this[int i] => data[i];     /* we get items from our list using [i] notation */
   		public genlist(){ data = new T[0]; }  /* constructor creates empty list */
   		
		public genlist(int n){data = new T[n];} // added additional constructor

		public T this[int i]{ // A combined get and setter for the list

			get => data[i]; // returns the i'th element when read
			set => data[i] = value; // sets value if statement

		}

		public void add(T item){              /* add item of the type "T" to the list */
      			T[] newdata = new T[size+1];   /* we need a larger array (inefective but uses minimal memory) */
      			Array.Copy(data,newdata,size); /* here you do O(size) operations */
      			newdata[size]=item;            /* add the item at the end of the list */
      			data=newdata;                  /* old data should be garbage collected, no worry here */
		}

}//genlist<T> taken from "generics in C-sharp" from course notes - Documentation was included.


} // main
