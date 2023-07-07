all: linSpline.svg QuadSpline.svg Out.txt

Out.txt : main.exe Homework4.txt
	mono main.exe > Out.txt
	cat Out.txt

QuadSpline.svg : test.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Quadratic Spline ";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [0:12][-30:30]\
	 "test.txt" index 0 using 1:2 pointsize 1 title "Data"\
	,"test.txt" index 3 using 1:2 pointsize 0.5 title "Spline points"\
	,"test.txt" index 4 using 1:2 pointsize 0.5 title "IntegralSpline points"\
	,"test.txt" index 5 using 1:2 pointsize 0.5 title "DifferentialSpline points"\
	'|gnuplot

linSpline.svg: test.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Linear Spline ";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [0:12][-30:30]\
	 "test.txt" index 0 using 1:2 pointsize 1 title "Data"\
	,"test.txt" index 1 using 1:2 pointsize 0.5 title "Spline points"\
	,"test.txt" index 2 using 1:2 pointsize 0.5 title "IntegralSpline points"\
	'|gnuplot

test.txt : test.exe Testfile.txt
	mono test.exe < Testfile.txt > test.txt

test.exe: test.cs math.dll
	mcs test.cs -reference:math.dll -target:exe -out:test.exe

Testfile.txt : Makefile
	>$@
	echo 1.0 5.0 >>$@
	echo 2.0 5.0 >>$@
	echo 3.0 -5.0 >>$@
	echo 4.0 -5.0 >>$@
	echo 5.0 5.0 >>$@
	echo 6.0 5.0 >>$@
	echo 7.0 5.0 >>$@
	echo 8.0 -5.0 >>$@
	echo 9.0 -5.0 >> $@
	echo 10.0 5.0 >> $@
	echo 11.0 5.0 >> $@

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : spline.cs
	mcs spline.cs -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
