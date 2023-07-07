Out.txt : main.exe Homework1.txt
	mono main.exe > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : calcs.cs QRGS.cs matrices.cs vectors.cs
	mcs calcs.cs QRGS.cs matrices.cs vectors.cs -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
