#include "Misc/FileHelper.h" //To Locate Hidden Word .txt List
#include "Misc/Paths.h"

#include "BullCowCartridge.h"
//#include "Math/UnreaMathUtility.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();

	const FString HiddenWordListDir = FPaths::ProjectContentDir() / TEXT("HiddenWordList.txt")
	FFileHelper::LoadFileToStringArray(Words, *HiddenWordListDir); //Assigns HiddenWordList.txt to TArray<FString> Words

	SetupGame();
}

void UBullCowCartridge::OnInput(const FString& PlayerInput) //On Enter
{	
	if (bGameOver)
	{
		ClearScreen();
		SetupGame();
	}
	else //Check Player Guess
	{
		ProcessGuess(PlayerInput);
	}
}

UBullCowCartridge::ProcessGuess(const FString& Guess)
{
	if (Guess == HiddenWord) //Guess is Correct Word
	{
		PrintLine(TEXT("Yee! You WON!"));
		EndGame();
		return;
	}

	if (Guess.Len() != HiddenWord.Len()) //Guess Letter Count Doesn't Match Word
	{
		PrintLine(TEXT("The Hidden Word is %i Letters Long"), HiddenWord.Len());
		PrintLine(TEXT("Incorrect! Try Again. \n(Lives = %i)"), Lives);
		return;
	}

	if (!IsIsogram(Guess)) //Guess Has Double Letters
	{
		PrintLine(TEXT("No repeating letters! Guess Again."));
		return;
	}

	PrintLine(TEXT("You lose a life"));
	--Lives;

	if (Lives <= 0) //If No Lives Are Left
	{
		ClearScreen();
		PrintLine(TEXT("You have no lives left. You Lose!"));
		PrintLine(TEXT("The Hidden Word Was: %s"), *HiddenWord);
		EndGame();
		return;
	}

	PrintLine(TEXT("Guess Again! (Lives = %i)"), Lives);
}

bool UBullCowCartridge::IsIsogram(const FString& Word) const
{
	for (int32 Index = 0; Index < Word.Len(); Index++)
	{
		for (int32 Comparison = Index + 1; Comparison < Word.Len(); Comparison++)
		{
			if (Word[Index] == Word[Comparison])
			{
				return false;
			}
		}

		return true;
	}
}

void UBullCowCartridge::SetupGame()
{
	PrintLine(TEXT("Hi There! Welcome to the Bull Cow Game"));

	HiddenWord = GetValidWords(Words)[FMath::RandRange(0, GetValidWords(Words).Num - 1];
	Lives = HiddenWord.Len();
	bGameOver = false;

	/* Debug Line */ PrintLine(TEXT("The HiddenWord is: %s. \nIt is %i chars long."), *HiddenWord, Hiddenword.Len());

	PrintLine(TEXT("Guess the %i-letter word \n(Lives = %i)"), Hiddenword.Len(), Lives);
	PrintLine(TEXT("Press Enter to continue. . ."));

	//PrintLine(TEXT("The Hidden Word Starts With: %c"), HiddenWord[0]);
}

void UBullCowCartridge::EndGame()
{
	bGameOver = true;
	PrintLine(TEXT("\nPress enter to play again. . ."));
}

TArray<FString> UBullCowCartridge::GetValidWords(const TArray<FString>& WordList) const;
{
	TArray<FString> ValidWords;

	for(FString Word : WordList)
	{
		if (Word.Len() <= 8 && Word.Len() >= 4 && IsIsogram(Word))
		{
			ValidWords.Emplace(word);
		}
		return ValidWords;
	}
}