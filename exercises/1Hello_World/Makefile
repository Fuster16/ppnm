Out.txt : hello.exe 
	mono hello.exe > Out.txt
	cat Out.txt

hello.exe : hello.cs
	mcs hello.cs -out:hello.exe

clean:
	rm -f Out.txt hello.exe
