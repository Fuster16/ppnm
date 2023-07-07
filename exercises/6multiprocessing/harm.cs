using System;
using static System.Console;
using static System.Math;
using System.Threading;

public static class harm{ // the sum function

	public static void harmonic(object obj){

	var local = (data)obj; // Takes object and turns into type data (class defined above)
	local.sum=0; // Sets initial local sum value
	for(int i = local.a; i < local.b; i++) local.sum+=1.0/i; // accesses the data "sum" variable and updates it over given interval (interval is associated with the given object)

} //harmonic

} // harm
