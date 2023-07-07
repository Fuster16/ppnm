Out.txt : main.exe input.txt
	mono main.exe < input.txt > Out.txt 
	cat Out.txt

main.exe : main.cs packs.dll
	mcs $< -reference:packs.dll -target:exe -out:$@

packs.dll : generics.cs
	mcs $^ -target:library -out:$@

input.txt : Makefile
	>$@
	echo 129.24 24.8             4.847 >>$@
	echo 		88.6   33.745 7.78 >>$@
	echo 30.39  99.811              6.723 >>$@
	echo     -1.33   96.3   2.23 >>$@

clean : 
	rm -f Out.txt main.exe
