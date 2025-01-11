
![chatgpt_baner.jpg](Data/chatgpt_baner.jpg)

## Table of Contents
1. [The Challenge](#the-challenge)
2. [Description](#description)
3. [Infrastructure](#infrastructure)
4. [Commands](#commands)   
   1. [Search](#search)
   2. [Availability](#availability)
5. [Build, Compile and Run](#build-compile-and-run)
6. [Tips](#tips)


## The Challenge
Create a C# program to manage hotel room availability and reservations.

### Description
The application should read from files containing hotel data and booking data, then allow a user to check room availability for a specified hotel, date range, and room type.

## Infrastructure
*Example command to run the program*
``` teminal
myapp
--hotels hotels.json
--bookings bookings.json
```

*Example file contents* 

[hotels.json](Data/hotels.json)   
[bookings.json](Data/bookings.json)   

## Commands
The program should implement the 2 commands described below and exit when a blank line is entered.

### `Search`

*Example input:*
```
Search(H1, 35, SGL)
```

The program should return a comma separated list of date ranges and availability where the room is available.
In this example, `35` is the number of days ahead to query for availability.
If there is no availability the program should return an empty line.

*Example output*
```
(20241101-20241103,2), (20241203-20241210, 1)
```
Means that in the next `35` days rooms of type `SGL` in hotel `H1`, there are:
- `2` roms available in `2024/11/01 - 2024/11/03`
- `1` room available in `2024/12/03 - 2024/12/10`

### `Availability`

*Example input*  
```
Availability(H1, 20240901, SGL)  
Availability(H1, 20240901-20240903, DBL)
```

The program should give the availability count for the specified room type and date range.

> **Note:** hotels sometimes accept over bookings so the value can be negative to indicate that the hotel is over capacity for that room type.

## Build, Compile and Run
Download the code and navigate to the project folder.

To `build` the program, you can use the following command:
``` terminal
dotnet build
```
*Screen shot of the output:*
``` terminal
(base) martataszmowicz@Mac-Studio-Marta MyApp % dotnet build
  Determining projects to restore...
  All projects are up-to-date for restore.
  MyApp -> /Users/martataszmowicz/development/rozmowa/MyApp/MyApp/bin/Debug/net8.0/MyApp.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.56
```

To `compile` and `run` the program, you can use the following command:
``` terminal
dotnet run --hotels Data/hotels.json --bookings Data/bookings.json
```
*Screen shot of the output*
``` terminal
(base) martataszmowicz@Mac-Studio-Marta MyApp % dotnet run --hotels Data/hotels.json --bookings Data/bookings.json
Manage hotel room availability and reservations.

Input parameters
 myapp --hotels <hotels.json> --bookings <bookings.json>
If the parameters are not specified, the files will be loaded from the /Data directory.
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
[Info] Read bookings from: Data/bookings.json
[Info] Read hotels from: Data/hotels.json
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
Choose an command:
 1 Search
 2 Availability
 Any other key to exit.
```

## Tips
Do read our blog post that gives good :muscle: insight into what we value in the code test:
https://medium.com/guestline-labs/hints-for-our-interview-process-and-code-test-ae647325f400


