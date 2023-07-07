using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

	public static void Main(string[] args){

	string outfile = null; // "Responses.txt";

	foreach(var arg in args){ // sets outfile string values to the given data sheets.

		var words=arg.Split(':'); // Splits the command-line string[] at ":"

		if(words[0]=="-output") outfile = words[1]; // If "-output" is given, then the infile name must be words[1]

	}


	////// A

	// Functions
	Func<vector,double> h = x => Pow( x[0]*x[0] + x[1] -11, 2) + Pow( x[1]*x[1] + x[0] -7, 2);
	Func<vector,double> r = x => Pow( 1 - x[0] , 2) + 100*Pow(x[1] - x[0]*x[0], 2);

	// Chosen initial search volumes
	vector ha = new vector(-5.0,5.0); vector hb = new vector(5.0, -5.0);
	vector ra = new vector(0.0,2.0); vector rb = new vector(2.0, 0.0);

	// Chosen initial parameters
	int N = 100;
	int MaxItt = 2000;
	double acc = 0.1;
	

	// Generic lists for Out.txt values
	var h_min = new optimization.genlist<(vector,int)>();
	var r_min = new optimization.genlist<(vector,int)>();
	for(int i = 0; i<4; i++){
		h_min.add(optimization.PSO(h, ha, hb, null, null, N, MaxItt, acc));
		r_min.add(optimization.PSO(r, ra, rb, null, null, N, MaxItt, acc));
	}
	// These lists will be made to strings in B and added to the output in the very last line of Main

	// Generic lists for convergence relations
	var ConvergenceA1 = new optimization.genlist<int>(); // might as well use a generic list again
	var ConvergenceA2 = new optimization.genlist<int>();

	Func<double,double> LinFun = t1 => (t1 - 50)*0.2/100 + 0.72;
	Func<int,int> LinFun2 = t2 => t2*9 + 100;
	for(int i = 0; i <= 100; i++){
		ConvergenceA1.add(optimization.PSO(r, ra, rb, null, null, N, MaxItt, acc, LinFun(i)).Item2);
		ConvergenceA2.add(optimization.PSO(r, ra, rb, null, null, LinFun2(i), MaxItt, acc).Item2);
	}

	////// A

	////// B
	
	// The "Bare Bones" Particle Swarm Optimizer is added to the lists from A
	for(int i = 0; i<4; i++){
		h_min.add(optimization.BBPSO(h, ha, hb, null, null, N, MaxItt, acc));
		r_min.add(optimization.BBPSO(r, ra, rb, null, null, N, MaxItt, acc));
	}
	// Values now ready for standard output
	string[] Results = new string[16];
	for(int j = 0; j<2; j++){ // j=0 --> results from A, j=1 --> results from B
		for(int i = 0; i<4; i++){
			Results[i + 8*j ] = calcs.VTS(h_min[i+ 4*j].Item1, $"h{i} = ") + $" with count {h_min[i+ 4*j].Item2}";
			Results[i+4 + 8*j] = calcs.VTS(r_min[i + 4*j].Item1, $"r{i} = ") + $" with count {r_min[i + 4*j].Item2}";
		}
	}

	// New initial parameters for B
	int N_new = 100;
	int MaxItt_new = 1000;
	double acc_new = 0.1;

	int Inst = 100; // Nr. Of routin instances to consider

	// Generic list of lists for best particle positions at i'th time and index for global best
	var Mp1 = new optimization.genlist<optimization.genlist<matrix>>(Inst); var G1 = new optimization.genlist<optimization.genlist<int>>(Inst);
	var Mp2 = new optimization.genlist<optimization.genlist<matrix>>(Inst); var G2 = new optimization.genlist<optimization.genlist<int>>(Inst);
	var Mp3 = new optimization.genlist<optimization.genlist<matrix>>(Inst); var G3 = new optimization.genlist<optimization.genlist<int>>(Inst);

	for(int i = 0; i<Inst; i++){
		Mp1[i] = new optimization.genlist<matrix>(); G1[i] = new optimization.genlist<int>(); 
		Mp2[i] = new optimization.genlist<matrix>(); G2[i] = new optimization.genlist<int>(); 
		Mp3[i] = new optimization.genlist<matrix>(); G3[i] = new optimization.genlist<int>(); 
	}

	//vector count1 = new vector(Inst); //count1[0] = 0; // should be default though
	//vector count2 = new vector(Inst); //count2[0] = 0;
	//vector count3 = new vector(Inst); //count3[0] = 0;

	//// C 
	matrix xprimes = new matrix(2, Inst); // the coloumn vectors will be bases for the upcomming QuasiPSO
	matrix vprimes = new matrix(2, Inst);

	vector bprimes = calcs.bprimes(32); // yields ~1000 unique permutations which i deem good enough

	var rnd = new Random();
	for(int k = 0; k <Inst; k++){ // this way of choosing bases should surffice
		xprimes[0,k] = bprimes[rnd.Next(32)]; xprimes[1,k] = xprimes[0,k];
		vprimes[0,k] = bprimes[rnd.Next(32)]; vprimes[1,k] = vprimes[0,k];

		while(xprimes[0,k] == xprimes[1,k]) xprimes[1,k] = bprimes[rnd.Next(32)];
		while(vprimes[0,k] == vprimes[1,k]) vprimes[1,k] = bprimes[rnd.Next(32)];
	}
	//// C


	for(int i = 0; i < Inst; i++){ // each itteration adds elements to the i'th member of 
		var t1 = optimization.PSO(r, ra, rb, G1[i], Mp1[i], N_new, MaxItt_new, acc_new);
		var t2 = optimization.BBPSO(r, ra, rb, G2[i], Mp2[i], N_new, MaxItt_new, acc_new);
		var t3 = optimization.QuasiPSO(r, ra, rb, xprimes[i], vprimes[i], G3[i], Mp3[i], N_new, MaxItt_new, acc_new);
		//count1[i] = t1.Item2; // + count1[i]; 
		//count2[i] = t2.Item2; // + count2[i];
		//count3[i] = t3.Item2; // + count3[i];
	}

	matrix S1 = new matrix(MaxItt_new,Inst); //#Inst routine itterations of #MaxItt_new PSO itterations
	matrix S2 = new matrix(MaxItt_new,Inst); 
	matrix S3 = new matrix(MaxItt_new,Inst);


	//for(int i = 0; i < Inst; i++){
	//	WriteLine($" {G1[i].size} {count1[i]} {count2[i]} {count3[i]} ");
	//}

	for(int j = 0; j< Inst; j++){ // we have 100 different routine instances (we want those to be coloums of the S matrices )

		for(int i = 0; i< G1[j].size; i++){ // Each routine has a number of matrices and integers up untill convergence

			matrix p1 = (Mp1[j])[i];
			int g1 = (G1[j])[i];
			S1[i,j] = 0;
	
			for(int k = 0; k < N_new; k++){ // opening the i'th matrix and finding the average distance to the i'th global best
				S1[i,j] += (p1[k] - p1[g1]).norm();
			}
		
		}

		for(int i = 0; i< G2[j].size; i++){ 

			matrix p2 = (Mp2[j])[i]; int g2 = (G2[j])[i];
			S2[i,j] = 0;
	
			for(int k = 0; k < N_new; k++){ 
				S2[i,j] += (p2[k] - p2[g2]).norm();
			}
		
		}
		for(int i = 0; i< G3[j].size; i++){

			matrix p3 = (Mp3[j])[i]; int g3 = (G3[j])[i];
			S3[i,j] = 0;
	
			for(int k = 0; k < N_new; k++){ // opening a matrix and finding the average distance to the current global best
				S3[i,j] += (p3[k] - p3[g3]).norm();
			}
		}

	}

	// Taking the average of the coloum vectors in S:

	vector Average1 = new vector(MaxItt_new);
	vector Average2 = new vector(MaxItt_new);
	vector Average3 = new vector(MaxItt_new);

	for(int i = 0; i <Inst; i++){
	
		Average1 += S1[i];
		Average2 += S2[i];
		Average3 += S3[i];
		

	}
	Average1 /= (Inst*N_new); S1 /= N_new; // the 20 comes from the fact that we had 20 particles in the swarm
	Average2 /= (Inst*N_new); S2 /= N_new;
	Average3 /= (Inst*N_new); S3 /= N_new;
	
	
	////// B

	////// C

	// The QuasiPSO has been added to the structure in B

	////// C


	int Bool1 = 1; // do we want to rewrite data?
	if( outfile != null && Bool1 == 1 ){ // if we have an outfile --> stream data into it

		var outstream=new System.IO.StreamWriter(outfile,append:false); // opens outstream

		//// A
		for(int j = 0; j <= 100; j++){
			outstream.WriteLine($"{j} {LinFun(j)} {ConvergenceA1[j]} {LinFun2(j)} {ConvergenceA2[j]}");
		}
		//// A

		outstream.WriteLine(); outstream.WriteLine(); // spacing

		//// B + C
		for(int j = 0; j < MaxItt_new; j++){ 
			
			outstream.WriteLine($"{j+1} {S1[j,0]} {S2[j,0]} {S3[j,0]} {Average1[j]} {Average2[j]} {Average3[j]}");

		}
		//// B + C

		outstream.WriteLine(); outstream.WriteLine(); // spacing

		//// Before and after pictures of the PSO

		matrix Particles1 = (Mp1[0])[0];
		matrix Particles2 = (Mp1[0])[G1[0].size-1];
		for(int j = 0; j < N_new; j++){

			outstream.WriteLine($"{Particles1[0,j]} {Particles1[1,j]} {Particles2[0,j]} {Particles2[1,j]}"); // The x and y position of the j'th best particle pos.

		}

		//// Before and after pictures of the PSO

		outstream.Close(); // Important

	}

	// Animations? 
	int Bool2 = 0;
	if( outfile != null && Bool2 == 1){ // if we have an outfile --> stream data into it

		var outstream=new System.IO.StreamWriter(outfile,append:true); // opens outstream

		//// C (Mp1[j])[i]
	
		outstream.WriteLine(); outstream.WriteLine(); // spacing

		for(int i = 0; i< G3[0].size; i++){ // consider the i'th itteration

			matrix Particles = (Mp3[0])[i];
			for(int j = 0; j < N_new; j++){

				if(j != (G3[0])[i]) outstream.Write($"{Particles[0,j]} {Particles[1,j]} "); // The x and y position of the j'th particle

			}
			outstream.Write($"{Particles[0,(G3[0])[i]]} {Particles[1,(G3[0])[i]]} "); // We keep the i'th global best at the very end
			outstream.WriteLine(); outstream.WriteLine();
		}

		//// C

		outstream.Close(); // Important

	}

	// Opens outstream for plots

	string text = File.ReadAllText(@"Exam.txt");

	WriteLine(text,Results);

} // Main

	

} // main
