// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using System.IO;
using static System.Math;
using static System.Console;


public partial class vector{ // The vector class

private double[] data; // Indicates data type and array 

public int size => data.Length; // Whenever size is called, the lambda func gives data.lenght

public double this[int i]{ // double type reffering back to vector 
	get => data[i]; // returns the i'th element when read
	set => data[i]=value; // sets value if statement
}


public vector(int n){data=new double[n];} // Constructor. Declares vecotor of size n
public vector(double[] a){data=a;} // Constructor. Takes array a and sets data accordingly.

public vector(double a)
	{ data = new double[1]{a}; } // Constructor. Basically just preparing for future problems
public vector(double a, double b) // Constructor looking for trouble
	{ data = new double[2]{a,b}; }
public vector(double a, double b, double c) // Constructor looking for trouble
	{ data = new double[3]{a,b,c}; }
public vector(double a, double b, double c, double d) // Constructor looking for trouble
	{ data = new double[4]{a,b,c,d}; }

// vector with string input --> Also a constructor looking for trouble

public vector(string s){
	char[] separators={',',' '}; // Array consisting of separation characters
	var options = StringSplitOptions.RemoveEmptyEntries; // Option required for the Split method
        string[] words = s.Split(separators,options); // Splitting the given string s at "," or spaces " "
        int n = words.Length; // The number of entries to be in the vector
        data = new double[n]; // Declares array with above number of entries
        for(int i=0;i<size;i++){
                        this[i]=double.Parse(words[i]); // parses the string as doubles in the just initiated array
                        }
	}

public static implicit operator vector (double[] a){ return new vector(a); } // Allows for "double[] a" to be parsed as "new vector(a)"
public static implicit operator double[] (vector v){ return v.data; } // Allows for "new vector(a)" to be parsed as "double[] a"


// A print method for vectors. Also Takes from "TextWriter file". 

public void print
(string s="",string format="{0,10:g3} ",TextWriter file=null){
	if(file==null)file = System.Console.Out; // if no TextWriter provided, just returns standard output?
	file.Write(s);
	for(int i=0;i<size;i++) file.Write(format,this[i]); // For-loop writes over current vector elements to "file"
	file.Write("\n"); // new-line
	}


// A print method for vectors. Only takes from "TextWriter file".

public void fprint
(TextWriter file,string s="",string format="{0,10:g3} "){
	file.Write(s);
	for(int i=0;i<size;i++) file.Write(format,this[i]);
	file.Write("\n");
	}

// operator+ --> defines sum on two vectors. 

public static vector operator+(vector v, vector u){
	vector r=new vector(v.size); // Declares new vector of same size of one of the given vectors
	for(int i=0;i<r.size;i++)r[i]=v[i]+u[i]; // Fills vector according to vector summation
	return r; } // returns this new vector


// operator- --> defines negative of a vector. 

public static vector operator-(vector v){
	vector r=new vector(v.size); // initiates new vector 
	for(int i=0;i<r.size;i++)r[i]=-v[i]; // for-loops each value in new vector to be -1* current vector
	return r; } // returns the fucker

// operator- --> defines difference on two vectors. (Instead of running (-v)+(u))

public static vector operator-(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]-u[i];
	return r; }

// operator* --> defines multiplication of vector v with a double a

public static vector operator*(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=a*v[i];
	return r; }

// operator* --> defines multiplication of double a with a vector v

public static vector operator*(double a, vector v){
	return v*a; }


// operator/ --> defines division of vector v with a double a

public static vector operator/(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/a;
	return r; }


// operator/ --> defines division of a double a with a vector v 

public static vector operator/(vector v, vector w){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/w[i];
	return r; }


// inner product of two vectors 

public double dot(vector o){ // not operator, but function
	double sum=0; // preparing/initializing for-loop
	for(int i=0;i<size;i++)sum+=this[i]*o[i]; // loop-adding i'th element of o vector to i'th element of current vector
	return sum;
	}

// dot product, but in "operator form"
public static double operator%(vector a,vector b){
	return a.dot(b); // Very cool
	}

// Looks to be a mapping method. It takes f as a regular R -> R function, but loop-applies it over the array.

public vector map(System.Func<double,double>f){
	vector v=new vector(size);
	for(int i=0;i<size;i++)v[i]=f(this[i]);
	return v;
	}

// Largest absolute value of element in vector. 

public double maxabs(){
	double r=Abs(this[0]);
	for(int i=1;i<size;i++)if(Abs(this[i])>r)r=Abs(this[i]);
	return r;
	}


// Largest element value finder

public double max(){
	double r=this[0];
	for(int i=1;i<size;i++)if(this[i]>r)r=this[i];
	return r;
	}


// Smallest element value finder

public double min(){
	double r=this[0];
	for(int i=1;i<size;i++)if(this[i]<r)r=this[i];
	return r;
	}


// norm function on vector

public double norm(){ //What?? It works tho

	double meanabs=0;
	for(int i=0;i<size;i++)meanabs+=Abs(this[i]);

	if(meanabs==0)meanabs=1;

	meanabs/=size;
	double sum=0;
	for(int i=0;i<size;i++)sum+=(this[i]/meanabs)*(this[i]/meanabs);

	return meanabs*Sqrt(sum);
	}


// Copies new vector 

public vector copy(){
	vector b=new vector(this.size);
	for(int i=0;i<this.size;i++)b[i]=this[i];
	return b;
}


// The same approx as in matrix

public static bool approx(double x, double y, double acc=1e-9, double eps=1e-9){
	if(Abs(x-y)<acc)return true;
	if(Abs(x-y)/Max(Abs(x),Abs(y))<eps)return true;
	return false;
	}


// The same approx above, however for the whole vector. The approc command above for doubles
// are required to be true for the i'th element in the two vectors for all i.

public static bool approx(vector a,vector b,double acc=1e-9,double eps=1e-9){
	if(a.size!=b.size)return false;
	for(int i=0;i<a.size;i++)
		if(!approx(a[i],b[i],acc,eps))return false;
	return true;
}


// method on current vector with vector o. Works just like above.

public bool approx(vector o){
	for(int i=0;i<size;i++)
		if(!approx(this[i],o[i]))return false;
	return true;
	}

}//vector