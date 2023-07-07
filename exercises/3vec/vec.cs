using System;
using static System.Console;
using static System.Math;

public class vec{ // a simple 3d-vector class

	public double x,y,z; // values for the vector class --> unique for each "vec" instance. 

	public vec(){x=y=z=0;} // default constructor => Allows for vec() to be called and gives a vec with x=y=z=0
	
	public vec(double x, double y, double z){ this.x = x; this.y = y; this.z = z;} // Also default constructor, but with specified x, y and z values
	
	// Defining operations on the class

	public static vec operator*(vec v, double c){return new vec(c*v.x, c*v.y, c*v.z);} // Define what c# has to do when the intruction (vec u)*(double c) is given
	public static vec operator*(double c, vec v){return v*c;} // Defines the instruction (double c)*(vec u). 


	public static vec operator+(vec u, vec v ){return new vec(v.x+u.x, v.y+u.y, v.z+u.z);} // Just as above. Defines what is meant by the instruction (vec u)+(vec v)
	public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);} // Defines what is meant by -(vec u)
	public static vec operator-(vec u,vec v){return new vec(u.x -v.x, u.y - v.y, u.z - v.z);} // Defines what is meant by (vec u)-(vec v)
	// The last definition could have been constructed from the first two --> (vec u)+(-(vec v)). However, the above definition is direct and thus faster than by going through several methods.
	
	// Defining operations on the class

	// Printing method for debugging

	public void print(string s){Write(s);WriteLine($"{x}, {y}, {z}");} // method which prints the values of the 3d-vec
	public void print(){this.print("");} // default vec.print()

	// Printing method for debugging	

	// Dot product

	public double dot(vec other){ // method for a vector u, where the dotted vector, say v, is taken as an argument.
		
		return this.x*other.x + this.y*other.y + this.z*other.z;

	} // dot product

	public static double dot(vec v, vec w){return v.x*w.x + v.y*w.y + v.z*w.z;}

	// Dot product

	// Comparision method

	static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){

		if(Abs(a-b)<acc) return true; // Checks flat difference between a and b. If below acc the function returns true, else it continues to the next statement.
		if(Abs(a-b)<(Abs(a)+Abs(b))*eps) return true; // If b is ( 100% - eps ) or more of a (if a is the biggest double) return true, else continue to next statement
		
		return false; // None of the above conditions passed

	}

	public bool approx(vec other,double acc=1e-9,double eps=1e-9){ // method similar in syntax to the dot product method

		if(!approx(this.x,other.x, acc, eps)) return false; // Returns false if approx fails for any index of vector
		if(!approx(this.y,other.y, acc, eps)) return false;
		if(!approx(this.z,other.z, acc, eps)) return false;

		return true;
	}

	public static bool approx(vec u, vec v,double acc=1e-9,double eps=1e-9) => u.approx(v,acc,eps);	// syntax sugar. Method similar to the dot product syntax (is used by writing "vec.approx(v,w)" )

	// Comparision method

	public override string ToString(){ return $"{x} {y} {z}"; } // instead of simply returning the class name "vec" when trying to "write" a vector it returns $"{x} {y} {z}", which should be expected.

	

} //vec

