using System;
using static System.Console;
using static System.Math;

public static class generics{

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

		public void remove(int i){ // removes i'th item from list

			T[] newdata = new T[size-1]; //initialize new list with 1 less element

			if(i == 0){ Array.Copy(data,1,newdata,0,size-1);} // copies from data to newdata

			Array.Copy(data,0,newdata,0,i); // copies all elements before i'th
			Array.Copy(data,i+1,newdata,i+1,size-1-i); // copies all elements after i'th

			data=newdata; // overrides data with newdata

		}

		// optional .add method for genlist

		public int size2=0,capacity=8; // Values unique to the add2 method (cannot be used together with .add --> doesn't keep track of size2)

		// public genlist2(){ data = new T[capacity]; } //default constructor for add2

		public void add2(T item){ /* add item to list */

			if(size2==capacity){ // if at capacity --> double array size

				T[] newdata = new T[capacity*=2];
				System.Array.Copy(data,newdata,size2);
				data=newdata;

			}

			data[size2]=item; // array has indice for next item
			size2++; // size2 is updated
		}

		// optional .add method for genlist

} //genlist<T>

} //generics
