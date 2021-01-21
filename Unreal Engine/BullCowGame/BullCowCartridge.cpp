//Fill out your copyright notice in the Description page of the Project Settings.
#include "BullCowCartridge.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();
	
	SetupGame();

	PrintLine(TEXT("The HiddenWord is: %s. \nIt is %i chars long."), *HiddenWord, Hiddenword.Len()); //Debug-Line
}

void UBullCowCartridge::OnInput(const FString& Input) //On Enter
{	
	if (bGameOver)
	{
		ClearScreen();
		SetupGame();
	}
	else //Check Player Guess
	{
		ProcessGuess(Input);
	}
}

UBullCowCartridge::ProcessGuess(FString Guess)
{
	if (Guess == HiddenWord)
	{
		PrintLine(TEXT("Yee! You WON!"));
		EndGame();
		return;
	}

	if (!IsIsogram)
	{
		PrintLine(TEXT("No repeating letters! Guess Again."));
	}


	if (Guess.Len() != HiddenWord.Len()) //Check Letter Count
	{
		PrintLine(TEXT("The Hidden Word is %i Letters Long"), HiddenWord.Len());
		PrintLine(TEXT("Incorrect! Try Again. \n(Lives = %i)"), Lives);
		return;
	}

	PrintLine(TEXT("You lose a life"));
	--Lives;

	if (Lives <= 0)
	{
		ClearScreen();
		PrintLine(TEXT("You have no lives left. You Lose!"));
		PrintLine(TEXT("The Hidden Word Was: %s"), *HiddenWord);
		EndGame();
		return;
	}

	PrintLine(TEXT("Guess Again! (Lives = %i)"), Lives);
}


void UBullCowCartridge::SetupGame()
{
	PrintLine(TEXT("Hi There! Welcome to the Bull Cow Game"));

	HiddenWord = TEXT("plant");
	Lives = HiddenWord.Len();
	bGameOver = false;

	PrintLine(TEXT("Guess the %i-letter word \n(Lives = %i)"), Hiddenword.Len(), Lives);
	PrintLine(TEXT("Press Enter to continue. . ."));
}

void UBullCowCartridge::EndGame()
{
	bGameOver = true;
	PrintLine(TEXT("\nPress enter to play again. . ."));
}
