all: ls_sol.svg ls_cov_sol.svg Out.txt

ls_sol.svg: ls.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "logarithm of relative activity of ThX";\
	set xlabel "time t (days)";\
	set ylabel "ln(A) (relative units)";\
	plot [][]\
	 "ls.txt" index 0 using 1:2:3 with errorbars pointsize 0.5 title "Data"\
	,"ls.txt" index 1 using 1:2 with lines title "ls-fit"\
	'|gnuplot


ls_cov_sol.svg: ls.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "logarithm of relative activity of ThX";\
	set xlabel "time t (days)";\
	set ylabel "ln(A) (relative units)";\
	plot [][]\
	 "ls.txt" index 0 using 1:2:3 with errorbars pointsize 0.5 title "Data"\
	,"ls.txt" index 1 using 1:2 with lines title "ls-fit"\
	,"ls.txt" index 1 using 1:3 with lines title "ls-fit with c+dc"\
	,"ls.txt" index 1 using 1:4 with lines title "ls-fit with c-dc"\
	'|gnuplot

ls.txt : ls.exe ThX.txt
	mono ls.exe < ThX.txt > ls.txt

ls.exe : ls.cs math.dll
	mcs ls.cs -reference:math.dll -target:exe -out:ls.exe

ThX.txt : Makefile
	>$@
	echo 1 117 5 >>$@
	echo 2 100 5 >>$@
	echo 3 88 5 >>$@
	echo 4 72 4 >>$@
	echo 6 53 4 >>$@
	echo 9 29.5 3 >>$@
	echo 10 25.2 3 >>$@
	echo 13 15.2 2 >>$@
	echo 15 11.1 2 >> $@

Out.txt : main.exe ThX.txt Homework3.txt
	mono main.exe < ThX.txt > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : calcs.cs OLSF.cs QRGS.cs matrices.cs vectors.cs
	mcs calcs.cs OLSF.cs QRGS.cs matrices.cs vectors.cs -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
