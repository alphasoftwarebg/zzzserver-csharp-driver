# zzzserver-csharp-driver
C# driver for **ZZZ Base** through ZZZ Server

***ZZZClient.h*** - the header file with driver functions declarations  
***ZZZClient.cpp*** - the file with driver functions implementations  
***main.cpp*** - sample access to **ZZZ Base** through function "zzzclient_zzzprogram"  

**sample usage:**
```csharp
ZZZProgram(
	"localhost",					// ZZZ Server host
	3333,						// ZZZ Server port
	"#[cout;Hello World from ZZZServer!]")		// ZZZ Base sample program
```

Returned result encoded in the utf-8
