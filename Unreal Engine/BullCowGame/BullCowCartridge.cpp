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
		if (Input == HiddenWord)
		{
			PrintLine(TEXT("Yee! You WON!"));
			EndGame();
		}
		else
		{
			PrintLine(TEXT("You lose a life"));
			PrintLine(TEXT("%i"), --Lives);

			if (Lives > 0)
			{
				if (Input.Len() != HiddenWord.Len())
				{
					PrintLine(TEXT("Incorrect! Try Again. \n(Lives = %i)"), Lives);
				}
				else
				{
					PrintLine(TEXT("You have no lives left. You Lose!"))
					EndGame();
				}
			}
		}
	}
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
	PrintLine(TEXT("Press enter to play again. . ."));
}