//Fill out your copyright notice in the Description page of the Project Settings.
#include "BullCowCartridge.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();
	PrintLine(TEXT("Hi There! Welcome to the Bull Cow Game"));
	PrintLine(TEXT("Guess the 4-letter word")); // Magic Number, Remove!
	PrintLine(TEXT("Press Enter to continue. . ."));
	
	HiddenWord = TEXT("plant");
}

void UBullCowCartridge::OnInput(const FString& Input) //On Enter
{
	ClearScreen();
	
	
	if (Input == HiddenWord)
	{
		PrintLine(TEXT("Yee! You WON!"));
	}
	else
	{
		PrintLine(TEXT("oh no! You LOST!"));
	}
}