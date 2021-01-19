//Fill out your copyright notice in the Description page of the Project Settings.
#include "BullCowCartridge.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();
	
	StartGame();

	PrintLine(TEXT("The HiddenWord is: %s. \nIt is %i chars long."), *HiddenWord, Hiddenword.Len()); //Debug-Line

	PrintLine(TEXT("Hi There! Welcome to the Bull Cow Game"));
	PrintLine(TEXT("Guess the %i-letter word"), Hiddenword.Len());
	PrintLine(TEXT("Press Enter to continue. . ."));
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
		if (Input.Len() != HiddenWord.Len())
		{
			PrintLine(TEXT("The Hidden word is %i char long. Try Again!"), Hiddenword.Len());
		}

		PrintLine(TEXT("Please Try Again"));
		//--Lives;
	}
}

void UBullCowCartridge::StartGame()
{
	HiddenWord = TEXT("plant");
	Lives = 4;
}