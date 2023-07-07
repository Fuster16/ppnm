// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using static System.Math;
public partial class matrix{


// Parameters of the "partial" matrix class

public readonly int size1, size2;
public double[][] data;


// Default constructor --> Takes integer sizes n and m and makes "data" an n x m array.
// "Data type"[][] indicates a "jaggedarrays" constructor and has to be initialized according to documentation 
// -> m# of arrays (the rows) and specify each array lenght (same size1 = n for each coloum)

public matrix(int n, int m){
	size1=n; size2=m; data = new double[size2][];
	for(int j=0;j<size2;j++) data[j]=new double[size1];
	}


// "Function" for the matrix class. Takes [row#, coloum#] input and returns the [row#, coloum#]'th double when read
// and executes the set property when assigned a value.

public double this[int r,int c]{
	get{return data[c][r];}
	set{data[c][r]=value;}
	}

// Same as above, however this "function" only takes [coloum#] and thus spits out a coloumb-vector. Is not used here, ever.

public vector this[int c]{
	get{return (vector)data[c];}
	set{data[c]=(double[])value;}
	}

// String --> Matrix. 

public matrix(string s){

        string[] rows = s.Split(';'); // Declares a string array called "rows". It is assumed for this "function", that the string which is to be converted into a matrix consisting og doubles be given as "11 12 13 ; 21 22 23 ; 31 32 33", namely a set of rows. This input is split at ";".
        size1 = rows.Length; // Actually gives nr. of rows --> thus coloumb lenght
	char[] delimiters = {',',' '}; // Array of characters indicating the splitting in the rows 
        var options = StringSplitOptions.RemoveEmptyEntries; //Option required for the splitting operator 
        size2 = rows[0].Split(delimiters,options).Length; // Splitting method used on the first row, followed by the lenght method. This assumes equal lenght for each row.
        data = new double[size2][]; // Declares the rows as having lenght of size2
	for(int j=0;j<size2;j++) data[j]=new double[size1]; // declares (all, individually) coloums of lenght size1
        for(int i=0;i<size1;i++){
                string[] ws = rows[i].Split(delimiters,options); // Puts the i'th row into an array called "ws"
                for(int j=0; j<size2; j++){
                        this[i,j]=double.Parse(ws[j]); // The j'th ws (ws[j]) are now parsed into the set{} property from the this[i,j] function and the i'th matrix row is created in a for-loop over j.
                        }
                }
        }


// operator+ --> plus operator defines summation on two matrices.

public static matrix operator+ (matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2); // A third matrix is created with size of matrix a. It is implicitly assumed that both matrix a and matrix b are of equal size1 and size2.
	for(int j=0;j<a.size2;j++) 
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]+b[i,j]; // For-loop inside for-loop. Takes the j'th row and loops over the i'th element of matrix c and set it's value to that of the sum of a and b. 
	return c; // Returns c
	}


// operator- --> minus operator defines corresponding negative matrix

public static matrix operator-(matrix a){
	matrix c = new matrix(a.size1,a.size2); // Same as for operator+, regarding the for-loop which operates on the elements in matrix a
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=-a[i,j];
	return c;
	}


// operator- --> minus operator defines difference on two matrices (operation overload; The minus operator now have two meanings, depending on the argument.)

public static matrix operator-(matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2); // Could have been made by considering the previously created -operator on just one matrix together with the sum operator, but this should be slightly fast since it only goes though all elements once!
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]-b[i,j];
	return c;
	}


// operator/ --> division operator defines division on matrix with a double (in that order)

public static matrix operator/(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]/x; // same for-loop construction as before
	return c;
}


// operator* --> multiplication operator defines multiplication on matrix with a double in any order 

public static matrix operator*(double x, matrix a){ return a*x; } // reverse of the below defined operator (Does order of definition not matter?)

public static matrix operator*(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]*x;
	return c;
}

// operator* --> matrix-multiplication operator defines multiplication for matrix on matrix

public static matrix operator* (matrix a, matrix b){
        var c = new matrix(a.size1,b.size2); // creates new matrix c of coloum-lenght of a times row-lenght of b 
        for (int k=0;k<a.size2;k++) // loops k over row-lenght of a (same as coloum-lenght b)
        for (int j=0;j<b.size2;j++) // loops j over row lenght of b 
		{
                double bkj=b[k,j]; // The k'th (row) (0'th row to [coloum lenght -1]'th row), j'th (coloum) (0'th coloum to [row lenght -1]'th coloum) elements of the b matrix

                var cj=c.data[j]; // looks to be the j'th coloum vector of c
                var ak=a.data[k]; // looks to be the k'th coloum vector of a (corresponding to the bkj)

		int n=a.size1; // The lenght of a's coloum vector (and c for that sake)

                for (int i=0;i<n;i++){ 
                        //c[i,j]+=a[i,k]*b[k,j];
                      cj[i]+=ak[i]*bkj; // the i'th entry in c's j'th coloum vector is now looped over. This can be seen to correspond nicely with matrix - multiplication if we consider j=0 for all i and k.
                	}
        	}
        return c;
        }


// operator* --> matrix-multiplication operator defines multiplication for matrix on vector

public static vector operator* (matrix a, vector v){
	var u = new vector(a.size1); // The operation declares a new vector (of size n if a is n tall and m wide)
	for(int k=0;k<a.size2;k++)   // loop over k=0 to m-1
	for(int i=0;i<a.size1;i++)   // loop over i=0 to n-1
		u[i]+=a[i,k]*v[k];   // For each u entry adds the k'th element in v times the (i,k)'th coloumb vector in a (builds u by summing coloum vectors of a "weighted" by vector v)
	return u;
	}


// operator% --> matrix-multiplication operator defines multiplication for vector on matrix (vector multiplied from the left)

public static vector operator% (matrix a, vector v){
	var u = new vector(a.size2); // Declare new vector of (of size m if a is n tall and m wide)
	for(int k=0;k<a.size1;k++)   // loop over k=0 to n-1
	for(int i=0;i<a.size2;i++)   // loop over i=0 to m-1
		u[i]+=a[k,i]*v[k];   // Here the k'th element in the vector v is multiplied to the top row (i=0 to m-1) of matrix a and added to the new vector c (loop over k=0 to n-1).
	return u;
	}


// Constructor for making a matrix with vector entries along the diagonal (faster than multiplying vector with identity matrix i suppose)

public matrix(vector e) : this(e.size,e.size) { for(int i=0;i<e.size;i++)this[i,i]=e[i]; } // Colon, :, means "matrix(vector e)" inherits all of "this"'s properties


// hard set and get methods for matrices

public void set(int r, int c, double value){ this[r,c]=value; }
public static void set(matrix A, int i, int j, double value){ A[i,j]=value; }
public double get(int i, int j){ return this[i,j]; }
public static double get(matrix A, int i, int j){ return A[i,j]; }


//

public matrix rows(int a, int b){
  matrix m = new matrix(b-a+1,size2);
  for(int i=0;i<m.size1;i++)
	for(int j=0;j<m.size2;j++)
    		m[i,j]=this[i+a,j];
  return m;
}


//

public matrix cols(int a, int b){
  matrix m = new matrix(size1,b-a+1);
  for(int i=0;i<m.size1;i++)for(int j=0;j<m.size2;j++)
    m[i,j]=this[i,j+a];
  return m;
  }


// Identity matrix

public static matrix id(int n){
	var m = new matrix(n,n);
	for(int i=0;i<n;i++)m[i,i]=1;
	return m;
	}

// Sets a matrix to the identity matrix (without creating a new matrix of perhapse unknown size?)

public void set_identity(){ this.set_unity(); }
public void set_unity(){
	for(int i=0;i<size1;i++){
		this[i,i]=1;
		for(int j=i+1;j<size2;j++){
			this[i,j]=0;this[j,i]=0;
		}
	}
}


// Same as before?

public void setid(){
	for(int i=0;i<size1;i++){
		this[i,i]=1;
		for(int j=i+1;j<size2;j++){ this[i,j]=0;this[j,i]=0; }
	}
	}

// Sets a given matrix to zero

public void set_zero(){
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			this[i,j]=0;
	}


// outer product between two vectors?

public static matrix outer(vector u, vector v){
	matrix c = new matrix(u.size,v.size);
	for(int i=0;i<c.size1;i++) for(int j=0;j<c.size2;j++) c[i,j] =u[i]*v[j]; // Speaks for itself. The new matrix is simply the kroenecker product between the two vectors
	return c;
}


// Inner product times some constant 

public void update(vector u, vector v, double s=1){
	for(int i=0;i<size1;i++)
	for(int j=0;j<size2;j++)
		this[i,j]+=u[i]*v[j]*s;
	}


// Matrix method, which copies all elements into a new matrix

public matrix copy(){
	matrix c = new matrix(size1,size2);
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[i,j]=this[i,j];
	return c;
	}


// ?? Refers to another function

public matrix T{
		get{return this.transpose();}
		set{}
}


// Creates specified submatrix. Takes height range "int ia, int ib" , then width range "int ja, int jb"

public matrix submatrix(int ia, int ib, int ja, int jb){
	matrix m = new matrix(ib-ia+1,jb-ja+1); // Matrix includes the elements ia, ib and ja, jb.
	for(int i=ia;i<=ib;i++)
	for(int j=ja;j<=jb;j++) m[i-ia,j-ja]=this[i,j]; // for-loops over ia to ib and puts into new smaller matrix m (same for ja and jb)
	return m;
}


// Creates transposed matrix. 

public matrix transpose(){
	matrix c = new matrix(size2,size1); //Declares rotated matrix (this.size1 --> c.size2 )
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[j,i]=this[i,j]; // Loops over all elements according to transpose operation
	return c;
	}


// Multiplying a matrix with double without declaring new matrix?

public static void scale(matrix M,double x){
	for(int j=0;j<M.size2;j++)
	for(int i=0;i<M.size1;i++)
		M[i,j]*=x;
	}


// Looks like code which prints the matrix into standard output.

public void print(string s="",string format="{0,10:g3} "){ // defines some sort of System.Console.Write format?
	System.Console.WriteLine(s);
	for(int ir=0;ir<this.size1;ir++){ // Outer loop. Loops "in" the coloum (between rows) (ir) this[ir,ic] 
	for(int ic=0;ic<this.size2;ic++)
		System.Console.Write(format,this[ir,ic]); // Loops a write "in" the row (between coloums) (ic) this[ir,ic] 
		System.Console.WriteLine(); // makes a space between values
		}
	}


// An approx function for matrices.

public static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){ // standard acc AND eps is 1e-6 
	if(Abs(a-b)<acc)return true; // if the difference between a and b is below acc the function returns true, else it continues to the next statement.
	if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true; // If b is ( 100% - eps ) or more of a (if a is the biggest double) return true, else continue to next statement
	return false; // returns false if both if statements fails.
}


// 

public bool approx(matrix B,double acc=1e-6, double eps=1e-6){
	if(this.size1!=B.size1)return false;
	if(this.size2!=B.size2)return false;
	for(int i=0;i<size1;i++)
		for(int j=0;j<size2;j++)
			if(!approx(this[i,j],B[i,j],acc,eps))
				return false;
	return true;
}

}//matrix
