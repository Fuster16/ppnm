A: Conv_Damp_relation.svg Conv_N_relation.svg B
B: Conv_time_comparison.svg Conv_time_comparison100.svg Before.svg After.svg

Conv_Damp_relation.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Convergence-Damping relation";\
	set xlabel "Damping coefficient w";\
	set ylabel "convergence time";\
	plot [0.6:0.84][1980:2020]\
	 "Exam_plots.txt" index 0 using 2:3 pointsize 1 title "Table Data"\
	'|gnuplot

Conv_N_relation.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Convergence-SwarmSize relation: w = 0.72";\
	set xlabel "Number of particles";\
	set ylabel "convergence time";\
	set logscale x 10;\
	plot [50:2000][1980:2020]\
	 "Exam_plots.txt" index 0 using 4:5 pointsize 1 title "Table Data"\
	'|gnuplot

Conv_time_comparison.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Swarm convergence measure";\
	set xlabel "Number of itterations";\
	set ylabel "Average particle distance to current global best";\
	plot [0:100][0:2]\
	 "Exam_plots.txt" index 1 using 1:2 pointsize 1 title "Standard PSO"\
	 ,"Exam_plots.txt" index 1 using 1:3 pointsize 1 title "Bare Bones PSO"\
	 ,"Exam_plots.txt" index 1 using 1:4 pointsize 1 title "Quasi PSO"\
	'|gnuplot

Conv_time_comparison100.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Swarm distance to global best, averaged over 100 routine instances";\
	set xlabel "Number of itterations";\
	set ylabel "Avg. particle dist. to current best - Averaged again over 100 instances";\
	plot [0:100][0:2.5]\
	 "Exam_plots.txt" index 1 using 1:5 pointsize 1 title "Standard PSO"\
	 ,"Exam_plots.txt" index 1 using 1:6 pointsize 1 title "Bare Bones PSO"\
	 ,"Exam_plots.txt" index 1 using 1:7 pointsize 1 title "Quasi PSO"\
	'|gnuplot

Animation.gif: Out.txt Makefile
	echo '\
	set terminal gif animate delay 2.5;\
	set out "$@";\
	set xrange[-2:4];\
	set yrange[-2:4];\
	do for [i=2:40] { plot for [j=0:19] "Exam_plots.txt" index i using (2*j + 1):(2*j+2) with points pt 7 ps 2 lt 1 }\
	'|gnuplot

Before.svg: Out.txt Makefile
	echo '\
	unset key;\
	set term svg;\
	set out "$@";\
	set title "Particle swarm overview before - Standard PSO";\
	set xlabel "x";\
	set ylabel "y";\
	plot [-2:4][-2:4]\
	 "Exam_plots.txt" index 2 using 1:2 with points pt 7 ps 0.8\
	'|gnuplot

After.svg: Out.txt Makefile
	echo '\
	unset key;\
	set term svg;\
	set out "$@";\
	set title "Particle swarm overview after - Standard PSO";\
	set xlabel "x";\
	set ylabel "y";\
	plot [-2:4][-2:4]\
	 "Exam_plots.txt" index 2 using 3:4 with points pt 7 ps 0.8\
	'|gnuplot

Out.txt : main.exe Exam.txt
	mono main.exe -output:Exam_plots.txt > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : matrices.cs vectors.cs optimization.cs calcs.cs
	mcs $^ -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
