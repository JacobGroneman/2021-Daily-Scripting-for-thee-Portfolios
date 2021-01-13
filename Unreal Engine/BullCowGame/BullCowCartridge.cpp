//Fill out your copyright notice in the Description page of the Project Settings.
#include "BullCowCartridge.h"

void UBullCowCartridge::BeginPlay() //Start
{
	Super::BeginPlay();
	PrintLine(TEXT("Hi There! Welcome to the Bull Cow Game"));
	PrintLine(TEXT("Press Enter to continue. . ."));
}

void UBullCowCartridge::OnInput(const FString& Input) //On Enter
{
	ClearScreen();

	private FString HiddenWord = TEXT("plant");
	
	PrintLine(Input);
}