Out.txt : main.exe Homework8.txt
	mono main.exe -outfile:visuals.txt > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : ODE.cs roots.cs QRGS.cs matrices.cs vectors.cs sfun.cs
	mcs $^ -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
