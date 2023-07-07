A: Harmonic12.svg SHM_Phase12.svg Harmonic45.svg SHM_Phase45.svg Damped45.svg B

B: EquatorialMotion1.svg EquatorialMotion2.svg EquatorialMotion3.svg Out.txt

EquatorialMotion1.svg : equatorial.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "EquatorialMotion 1";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [-2:2][-2:2]\
	 "equatorial.txt" index 0 using (1/$$2)*cos($$1):(1/$$2)*sin($$1) with lines title "Simpel Harmonic Motion"\
	'|gnuplot

EquatorialMotion2.svg : equatorial.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "EquatorialMotion 2 ";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [-2:2.75][-2:2.75]\
	 "equatorial.txt" index 1 using (1/$$2)*cos($$1):(1/$$2)*sin($$1) with lines title "Simpel Harmonic Motion"\
	'|gnuplot

EquatorialMotion3.svg : equatorial.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "EquatorialMotion 3 ";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [-2:2.75][-2:2.75]\
	 "equatorial.txt" index 2 using (1/$$2)*cos($$1):(1/$$2)*sin($$1) with lines title "Simpel Harmonic Motion"\
	'|gnuplot

equatorial.txt : equatorial.exe
	mono equatorial.exe > equatorial.txt

equatorial.exe : equatorial.cs math.dll
	mcs equatorial.cs -reference:math.dll -target:exe -out:equatorial.exe



Harmonic12.svg : rkxy.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Harmonic by rkstep12 ";\
	set xlabel "x";\
	set ylabel "f(x)";\
	plot [-1:7][-2:2]\
	 "rkxy.txt" index 0 using 1:2 pointsize 1 title "Simpel Harmonic Motion"\
	'|gnuplot

SHM_Phase12.svg : rkxy.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Harmonic phase-plot by rkstep12 ";\
	set xlabel "f data";\
	set ylabel "fprime data";\
	plot [-2:2][-2:2]\
	 "rkxy.txt" index 0 using 2:3 pointsize 1 title "Simpel Harmonic Motion"\
	'|gnuplot

Harmonic45.svg : rkxy.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Harmonic by rkstep45 ";\
	set xlabel "x data";\
	set ylabel "f(x) data";\
	plot [-1:7][-2:2]\
	 "rkxy.txt" index 1 using 1:2 pointsize 1 title "Simpel Harmonic Motion"\
	'|gnuplot

SHM_Phase45.svg : rkxy.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Harmonic phase-plot by rkstep45";\
	set xlabel "f";\
	set ylabel "fprime";\
	plot [-2:2][-2:2]\
	 "rkxy.txt" index 1 using 2:3 pointsize 1 title "Simpel Harmonic Motion"\
	'|gnuplot

Damped45.svg : Damped.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Damped harmonic motion ";\
	set xlabel "x";\
	set ylabel "f(x)";\
	plot [0:10][-5:5]\
	 "Damped.txt" using 1:2 pointsize 1 title "Damped HM"\
	,"Damped.txt" using 1:3 pointsize 0.5 title "Derivative"\
	'|gnuplot

rkxy.txt : rkxy.exe 
	mono rkxy.exe > rkxy.txt

rkxy.exe : rkxy.cs math.dll
	mcs rkxy.cs -reference:math.dll -target:exe -out:rkxy.exe

Damped.txt : damped.exe 
	mono damped.exe > Damped.txt

damped.exe : damped.cs math.dll
	mcs damped.cs -reference:math.dll -target:exe -out:damped.exe

Out.txt : main.exe Homework5.txt
	mono main.exe > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : calcs.cs ODE.cs matrices.cs vectors.cs
	mcs calcs.cs ODE.cs matrices.cs vectors.cs -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
